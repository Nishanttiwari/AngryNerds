using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using APIHelper;
using ForecastFrontEnd.Models;
using Newtonsoft.Json;
using System.Web.Script.Serialization;

namespace ForecastFrontEnd.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            DataView main = new DataView();
            JSONProvider jp = new JSONProvider();

            main.MarketShareModel = new MarketShareModel();

            // string result = jp.GetJSON("RMS/v1/ManufactureDolShr?period=1%20W%2FE%2012%2F14%2F13");
            var result = "{  \"root\":{    \"Header\":{      \"API_Version\":\"1.0\",      \"API_Name\":\"RMS Measurement- $Share by Manufacture API\",      \"Date\":\"2014-03-18T12:09:51.23-04:00\",      \"MessageID\":\"123014\",      \"Content_Type\":\"JSON\",      \"Currency\":\"$\"    },    \"PeriodData\":[      {        \"Period\":\"1 W\\/E 12\\/14\\/13\",        \"Market\":[          {            \"MarketName\":\"Oahu Food\",            \"Category\":[              {                \"CategoryName\":\"CEREAL-READY TO EAT\",                \"Manufacturer\":[                  {                    \"ManufacturerName\":\"KELLOGG COMPANY\",                    \"TotalSalesDollar\":\"59671.696\",                    \"TotalUnitsSold\":\"12677.42\",                    \"DollarSharePercentage\":\"24.94%\"                  },                  {                    \"ManufacturerName\":\"GENERAL MILLS\",                    \"TotalSalesDollar\":\"59508.75\",                    \"TotalUnitsSold\":\"13671.18\",                    \"DollarSharePercentage\":\"24.87%\"                  },                  {                    \"ManufacturerName\":\"MOM BRANDS CO\",                    \"TotalSalesDollar\":\"35422.074\",                    \"TotalUnitsSold\":\"8817.18\",                    \"DollarSharePercentage\":\"14.81%\"                  },                  {                    \"ManufacturerName\":\"PEPSICO INC\",                    \"TotalSalesDollar\":\"26079.944\",                    \"TotalUnitsSold\":\"7205.62\",                    \"DollarSharePercentage\":\"10.9%\"                  },                  {                    \"ManufacturerName\":\"POST HOLDINGS INC\",                    \"TotalSalesDollar\":\"25281.187\",                    \"TotalUnitsSold\":\"5117.48\",                    \"DollarSharePercentage\":\"10.57%\"                  },                  {                    \"ManufacturerName\":\"PRIVATE LABEL\",                    \"TotalSalesDollar\":\"23338.356\",                    \"TotalUnitsSold\":\"5798.96\",                    \"DollarSharePercentage\":\"9.75%\"                  },                  {                    \"ManufacturerName\":\"NATURE'S PATH FOODS INC.\",                    \"TotalSalesDollar\":\"3118.085\",                    \"TotalUnitsSold\":\"490.66\",                    \"DollarSharePercentage\":\"1.3%\"                  },                  {                    \"ManufacturerName\":\"BRIGHT FOOD GROUP CO LTD\",                    \"TotalSalesDollar\":\"1869.087\",                    \"TotalUnitsSold\":\"309.76\",                    \"DollarSharePercentage\":\"0.78%\"                  },                  {                    \"ManufacturerName\":\"ANAHOLA GRANOLA\",                    \"TotalSalesDollar\":\"1665.462\",                    \"TotalUnitsSold\":\"222.4\",                    \"DollarSharePercentage\":\"0.7%\"                  },                  {                    \"ManufacturerName\":\"KIND LLC\",                    \"TotalSalesDollar\":\"902.616\",                    \"TotalUnitsSold\":\"114.4\",                    \"DollarSharePercentage\":\"0.38%\"                  }                ]              }            ]          }        ]      }    ],    \"Summary\":{      \"PageNo\":\"1\",      \"PageSize\":\"10\",      \"TotalPages\":\"2572\"    }  }}";
            var marketShareResonse = JsonConvert.DeserializeObject<MarketShareRootObject>(result);
 
            main.MarketShareModel.MarketShareJson = JsonConvert.SerializeObject(marketShareResonse.root.PeriodData[0].Market[0].Category[0].Manufacturer);
            main.MarketShareModel.manu = marketShareResonse.root.PeriodData[0].Market[0].Category[0].Manufacturer;

            return View(main);
        }

        public ActionResult MarketShareData()
        {
            DataView main = new DataView();
            JSONProvider jp = new JSONProvider();

            main.MarketShareModel = new MarketShareModel();

            // string result = jp.GetJSON("RMS/v1/ManufactureDolShr?period=1%20W%2FE%2012%2F14%2F13");
            var result = "{  \"root\":{    \"Header\":{      \"API_Version\":\"1.0\",      \"API_Name\":\"RMS Measurement- $Share by Manufacture API\",      \"Date\":\"2014-03-18T12:09:51.23-04:00\",      \"MessageID\":\"123014\",      \"Content_Type\":\"JSON\",      \"Currency\":\"$\"    },    \"PeriodData\":[      {        \"Period\":\"1 W\\/E 12\\/14\\/13\",        \"Market\":[          {            \"MarketName\":\"Oahu Food\",            \"Category\":[              {                \"CategoryName\":\"CEREAL-READY TO EAT\",                \"Manufacturer\":[                  {                    \"ManufacturerName\":\"KELLOGG COMPANY\",                    \"TotalSalesDollar\":\"59671.696\",                    \"TotalUnitsSold\":\"12677.42\",                    \"DollarSharePercentage\":\"24.94%\"                  },                  {                    \"ManufacturerName\":\"GENERAL MILLS\",                    \"TotalSalesDollar\":\"59508.75\",                    \"TotalUnitsSold\":\"13671.18\",                    \"DollarSharePercentage\":\"24.87%\"                  },                  {                    \"ManufacturerName\":\"MOM BRANDS CO\",                    \"TotalSalesDollar\":\"35422.074\",                    \"TotalUnitsSold\":\"8817.18\",                    \"DollarSharePercentage\":\"14.81%\"                  },                  {                    \"ManufacturerName\":\"PEPSICO INC\",                    \"TotalSalesDollar\":\"26079.944\",                    \"TotalUnitsSold\":\"7205.62\",                    \"DollarSharePercentage\":\"10.9%\"                  },                  {                    \"ManufacturerName\":\"POST HOLDINGS INC\",                    \"TotalSalesDollar\":\"25281.187\",                    \"TotalUnitsSold\":\"5117.48\",                    \"DollarSharePercentage\":\"10.57%\"                  },                  {                    \"ManufacturerName\":\"PRIVATE LABEL\",                    \"TotalSalesDollar\":\"23338.356\",                    \"TotalUnitsSold\":\"5798.96\",                    \"DollarSharePercentage\":\"9.75%\"                  },                  {                    \"ManufacturerName\":\"NATURE'S PATH FOODS INC.\",                    \"TotalSalesDollar\":\"3118.085\",                    \"TotalUnitsSold\":\"490.66\",                    \"DollarSharePercentage\":\"1.3%\"                  },                  {                    \"ManufacturerName\":\"BRIGHT FOOD GROUP CO LTD\",                    \"TotalSalesDollar\":\"1869.087\",                    \"TotalUnitsSold\":\"309.76\",                    \"DollarSharePercentage\":\"0.78%\"                  },                  {                    \"ManufacturerName\":\"ANAHOLA GRANOLA\",                    \"TotalSalesDollar\":\"1665.462\",                    \"TotalUnitsSold\":\"222.4\",                    \"DollarSharePercentage\":\"0.7%\"                  },                  {                    \"ManufacturerName\":\"KIND LLC\",                    \"TotalSalesDollar\":\"902.616\",                    \"TotalUnitsSold\":\"114.4\",                    \"DollarSharePercentage\":\"0.38%\"                  }                ]              }            ]          }        ]      }    ],    \"Summary\":{      \"PageNo\":\"1\",      \"PageSize\":\"10\",      \"TotalPages\":\"2572\"    }  }}";
            var marketShareResonse = JsonConvert.DeserializeObject<MarketShareRootObject>(result);

            return Json(marketShareResonse.root.PeriodData[0].Market[0].Category[0].Manufacturer, "application/json", JsonRequestBehavior.AllowGet);
            // main.MarketShareModel.manu = marketShareResonse.root.PeriodData[0].Market[0].Category[0].Manufacturer;
        }
    }
}
