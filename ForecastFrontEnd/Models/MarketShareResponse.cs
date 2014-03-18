using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ForecastFrontEnd.Models
{
    public class Header
    {
        public string API_Version { get; set; }
        public string API_Name { get; set; }
        public string Date { get; set; }
        public string MessageID { get; set; }
        public string Content_Type { get; set; }
        public string Currency { get; set; }
    }

    public class Manufacturer
    {
        public string ManufacturerName { get; set; }
        public string TotalSalesDollar { get; set; }
        public string TotalUnitsSold { get; set; }
        public string DollarSharePercentage { get; set; }
    }

    public class Category
    {
        public string CategoryName { get; set; }
        public List<Manufacturer> Manufacturer { get; set; }
    }

    public class Market
    {
        public string MarketName { get; set; }
        public List<Category> Category { get; set; }
    }

    public class PeriodData
    {
        public string Period { get; set; }
        public List<Market> Market { get; set; }
    }

    public class Summary
    {
        public string PageNo { get; set; }
        public string PageSize { get; set; }
        public string TotalPages { get; set; }
    }

    public class Root
    {
        public Header Header { get; set; }
        public List<PeriodData> PeriodData { get; set; }
        public Summary Summary { get; set; }
    }

    public class MarketShareRootObject
    {
        public Root root { get; set; }
    }
}