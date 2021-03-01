using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PageSpeed
{
    class PageSpeedEntity
    {
        public string Webiste { get; set; }
        public string Type { get; set; }
        public double PageScore { get; set; }
        public string numberResources { get; set; }
        public double totalRequestBytes { get; set; }
        public double HTMLResponseTime { get; set; }
        public double imageResponseBytes { get; set; }
        public double javascriptResponseBytes { get; set; }
        public double cssResponseBytes { get; set; }
        public string numberJsResources { get; set; }
        public string numberCssResources { get; set; }
        public string FetchTime { get; set; }
    }

    class PageSpeedEntityList
    {
        public List<PageSpeedEntity> PageSpeedEntityData { get; set; }
    }
}
