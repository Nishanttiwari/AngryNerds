using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ForecastFrontEnd.Models
{
    public class DataView
    {
        public MarketShareModel MarketShareModel { get; set; }
        public AdSpentModel AdSpentModel { get; set; }
        public ChannelSpentTrendModel ChannelSpentTrendModel { get; set; }
        public IdealSpendingModel IdealSpendingModel { get; set; }
    }
}