
using System.Collections.Generic;
namespace ForecastFrontEnd.Models
{
    public class MarketShareModel
    {
        public string MarketShareJson { get; set; }
        public List<Manufacturer> manu { get; set; }
    }
}