namespace ForecastService
{
    using System;
    using System.Collections.Generic;
    using TsExponentialSmoothing;
    using TsExponentialSmoothingWithTests;

    public class Forecaster : IForecaster
    {
        public bool Forecast(int periodicityHint, List<double> data, int holdout, int range, string mode, double minValue, double maxValue, out IList<double> parameters, 
            out IList<IScalarDistribution> forecast, out IList<double> residuals)
        {
            bool retval = false;
            parameters = null;
            forecast = null;
            residuals = null;
            int c_maxIter = 2;
            int fcastRange = 12;

            AAdArapidEventDetector modelBuilder = new AAdArapidEventDetector(periodicityHint);
            IList<EventImpactTrace> detectionHistory = null;

            bool fOK = modelBuilder.EventDetectionLoop(data, 0, c_maxIter, false, out detectionHistory);

            HoltWintersMultithreadedDimensionalityReduction forecaster = new HoltWintersMultithreadedDimensionalityReduction(periodicityHint);

            retval = forecaster.SingleThreadForecast(data, 0, fcastRange, "NONPARAMETRIC", 0.0, double.MaxValue, out parameters, out forecast, out residuals);

            return retval;
        }
    }
}
