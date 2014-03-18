using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using TsChartsPlots;
using TsExponentialSmoothing;
using TsExponentialSmoothingWithTests;
using TSSSMsupportedForecasters;
using System.Runtime.InteropServices;

//(C) Microsoft, 2011 - 2012
//This sample is provided "AS IS" for demo and educational purposes
//no re-distribution rights conferred or implied
//Original Author , alexeib@microsoft.com


namespace Sample_Periodic
{
    class Program
    {
        static Dictionary<string, List<double>> ParseCSVRows(string path)
        {
            Dictionary<string, List<double>> res = new Dictionary<string, List<double>>(40);
            TextReader tr = new StreamReader(path);
            string line = null;
            while ((line = tr.ReadLine()) != null)
            {
                string[] rg = line.Split(',');
                List<double> data = new List<double>(100);
                for (int i = 1; i < rg.Length; i++)
                {
                    double val;
                    if (!double.TryParse(rg[i], out val))
                    {
                        break;
                    }
                    data.Add(val);
                }
                res.Add(rg[0], data);
            }
            return res;
        }

        static void MakeForecastOnlyDictionaries(int trainCount, IList<IScalarDistribution> forecast, out List<Timestamp> timeIndex,
                                out Dictionary<Timestamp, double> means, out Dictionary<Timestamp, double> sigmas)
        {
            means = null;
            sigmas = null;
            timeIndex = null;
            int fcastCount = forecast.Count;
            if (fcastCount <= 0)
            {
                return;
            }
            means = new Dictionary<Timestamp, double>(fcastCount);
            sigmas = new Dictionary<Timestamp, double>(fcastCount);
            timeIndex = new List<Timestamp>(trainCount + fcastCount);
            for (int it = 0; it < trainCount; it++)
            {
                Timestamp tsit = new Timestamp(it);
                timeIndex.Add(tsit);
                sigmas.Add(tsit, 0.0);
            }


            for (int it = 0; it < forecast.Count; it++)
            {
                Timestamp ts = new Timestamp(trainCount + it);
                timeIndex.Add(ts);
                means.Add(ts, forecast[it].Mean);
                sigmas.Add(ts, forecast[it].StDev);
            }
        } //MakeForecastOnlyDictionaries

        static Dictionary<Timestamp, double> MakeDataDictionary(List<Timestamp> timeIndex, List<double> data)
        {
            int cnt = data.Count;
            if (cnt > timeIndex.Count)
            {
                cnt = timeIndex.Count;
            }
            Dictionary<Timestamp, double> res = new Dictionary<Timestamp, double>(cnt);
            for (int i = 0; i < cnt; i++)
            {
                res.Add(timeIndex[i], data[i]);
            }
            return res;
        }


        static void AddExcelChart(ExcelApplicationWrapper excelApp, EventImpactTrace eventTrace,
string name,
Dictionary<Timestamp, double> dictData,
Dictionary<Timestamp, double> dictForecast,
Dictionary<Timestamp, double> dictSigmas
)
        {

            List<Timestamp> timeline = new List<Timestamp>(dictData.Keys);
            timeline.Sort();

            Dictionary<string, Dictionary<int, string>> dictLabels =
                        new Dictionary<string, Dictionary<int, string>>(1);
            Dictionary<int, string> eventLabels = new Dictionary<int, string>();

            //Label standalone (observation) spikes
            if ((eventTrace != null) && (eventTrace.ObservationShocks != null))
            {
                foreach (int ilk in eventTrace.ObservationShocks.Keys)
                {
                    eventLabels.Add(ilk, "M");
                }
            }

            //Label inflection points in broad, deviant patterns
            foreach (int ilk in eventTrace.StateShocks.Keys)
            {
                if ((ilk >= 0) && (!eventLabels.ContainsKey(ilk)))
                {
                    eventLabels.Add(ilk, "S");
                }

            }
            dictLabels.Add(name, eventLabels);
            Dictionary<string, Dictionary<int, string>> dictNews =
                        new Dictionary<string, Dictionary<int, string>>(1);

            Dictionary<string, Dictionary<Timestamp, double>> tmp =
                new Dictionary<string, Dictionary<Timestamp, double>>(2);
            tmp.Add(name, dictData);



            Dictionary<string, Dictionary<Timestamp, double>> tmpSigmas = null;
            if (dictForecast != null)
            {
                string strForecastName = name + " (forecast)";
                tmp.Add(strForecastName, dictForecast);
                foreach (Timestamp fts in dictForecast.Keys)
                {
                    if (!timeline.Contains(fts))
                    {
                        timeline.Add(fts);
                    }
                }
                timeline.Sort();
                tmpSigmas = new Dictionary<string, Dictionary<Timestamp, double>>(1);
                tmpSigmas.Add(strForecastName, dictSigmas);
            }


            try
            {
                excelApp.CreateChart(name + ", trends", timeline,
                    tmp,
                    tmpSigmas,
                    0, int.MaxValue, dictLabels, dictNews);

            }
            catch (COMException comExIgnore)
            {
            }

        } //AddExcelChart

        static void Main(string[] args)
        {
            string filename = Environment.GetEnvironmentVariable("TSF_NET_DIR") + "Sample_Periodic\\NorwichTemp2.csv";
            Dictionary<string, List<double>> inputData = ParseCSVRows(filename);
            ExcelApplicationWrapper excelApp = new ExcelApplicationWrapper();
            excelApp.OpenExcel();

            foreach (string q1 in inputData.Keys)
            {

                string strTab = q1 + ", trends";
                if (strTab.Length > 30)
                {
                    strTab = strTab.Substring(0, 30);
                }

                int c_maxIter = 2;
                List<DateTime> timeline = null;
                List<double> data = inputData[q1];
                //Done extracting and aggregating data for query q1

                int periodicityHint = 12;

                //Step1. Detect deviant patterns and outlying spikes

                //A slower much more precise model builder commented out here
                //AAdAdrWithShocks modelBuilder = new AAdAdrWithShocks(periodicityHint, 0.1, (List<int>) null, null);
                //modelBuilder.InterferenceLag = 12;

                AAdArapidEventDetector modelBuilder = new AAdArapidEventDetector(periodicityHint);
                IList<EventImpactTrace> detectionHistory = null;
                bool fOK = modelBuilder.EventDetectionLoop(data, 0, c_maxIter, false,
                    out detectionHistory);


                int fcastRange = 12;
                IList<IScalarDistribution> fullForecast = null;
                HoltWintersMultithreadedDimensionalityReduction forecaster = 
                    new HoltWintersMultithreadedDimensionalityReduction(periodicityHint);

                IList<double> paramtrs = null;
                IList<double> residuals = null;
                try
                {
                    //Example of "full service" forecast, with expected values and predict distributions
                    forecaster.SingleThreadForecast(data, 0, fcastRange, "NONPARAMETRIC", 0.0, double.MaxValue,
                        out paramtrs, out fullForecast, out residuals);
                }
                catch (Exception outerException)
                {
                }


                //Preparing for ... and using Excel middleware to generate Excel chart programmatically
                Dictionary<Timestamp, double> dictSigmas = null;
                Dictionary<Timestamp, double> dictMeans = null;
                List<Timestamp> timeIndex = null;
                MakeForecastOnlyDictionaries
                (data.Count, fullForecast, out timeIndex, out dictMeans, out dictSigmas);

                //Cosmetics tweak
                dictMeans.Add(new Timestamp(data.Count - 1), data.Last());

                AddExcelChart(excelApp, detectionHistory.Last(), q1,
                            MakeDataDictionary(timeIndex, data), dictMeans, dictSigmas);

            }
            //excelApp.CloseExcel(); //Uncomment to get the workbook closed automatically

        }

    }
}
