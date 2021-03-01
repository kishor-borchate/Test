using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PageSpeed
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            MailerPageSpeed.WriteToFile("DateTime => " + DateTime.Now.ToString());
            string result = PageSpeed();
            Environment.Exit(0);
        }
        public static string PageSpeed()
        {
            string[] websites = { "https://www.hdfcergo.com", "https://www.reliancegeneral.co.in", "https://www.godigit.com", "https://www.icicilombard.com", "https://www.acko.com", "https://www.policybazaar.com" };
            //string[] websites = { "https://www.hdfcergo.com", "https://www.reliancegeneral.co.in", "https://www.policybazaar.com" };
            //string[] websites = { "https://www.hdfcergo.com/health-insurance/family-health-insurance" };
            string[] type = { "mobile", "desktop" };

            PageSpeedEntityList response = new PageSpeedEntityList();
            List<PageSpeedEntity> PSEList = new List<PageSpeedEntity>();
            for (int i = 0; i < websites.Length; i++)
            {
                int j;
                for (j = 0; j < type.Length; j++)
                {
                    using (WebClient wc = new WebClient())
                    {
                        try
                        {
                            PageSpeedEntity PSE = new PageSpeedEntity();
                            string url = "https://www.googleapis.com/pagespeedonline/v5/runPagespeed?url=" + websites[i].ToString() + "&strategy=" + type[j].ToString() + "&category=performance&apiKey";
                            Console.WriteLine();
                            Console.WriteLine();
                            Console.WriteLine("Analysing Page Speed => " + type[j].ToString() + " => " + websites[i].ToString());
                            MailerPageSpeed.WriteToFile("Analysing Page Speed => " + type[j].ToString() + " => " + websites[i].ToString());
                            var json = wc.DownloadString(url);
                            dynamic d = JsonConvert.DeserializeObject(json);
                            PSE.FetchTime = DateTime.Now.ToShortTimeString();
                            PSE.Webiste = websites[i].ToString();
                            PSE.Type = type[j].ToString();
                            PSE.PageScore = d.lighthouseResult.categories.performance.score.Value * 100;
                            PSE.numberResources = Convert.ToString(d.lighthouseResult.audits["resource-summary"].details.items.Count);
                            PSE.totalRequestBytes = Convert.ToDouble(d.lighthouseResult.audits["resource-summary"].details.items[0].transferSize.Value / 1024) / 1024;
                            PSE.HTMLResponseTime = Convert.ToDouble(d.lighthouseResult.audits["server-response-time"].numericValue.Value);
                            PSE.HTMLResponseTime = TimeSpan.FromMilliseconds(PSE.HTMLResponseTime).TotalSeconds;
                            PSE.imageResponseBytes = Convert.ToDouble(d.lighthouseResult.audits["resource-summary"].details.items[1].transferSize.Value / 1024) / 1024;
                            PSE.javascriptResponseBytes = Convert.ToDouble(d.lighthouseResult.audits["resource-summary"].details.items[2].transferSize.Value / 1024) / 1024;
                            PSE.cssResponseBytes = Convert.ToDouble(d.lighthouseResult.audits["resource-summary"].details.items[3].transferSize.Value / 1024) / 1024;
                            PSE.numberJsResources = Convert.ToString(d.lighthouseResult.audits["resource-summary"].details.items[2].requestCount.Value);
                            PSE.numberCssResources = Convert.ToString(d.lighthouseResult.audits["resource-summary"].details.items[3].requestCount.Value);
                            PSEList.Add(PSE);
                            Console.WriteLine("Done => " + type[j].ToString() + " => " + websites[i].ToString());
                            MailerPageSpeed.WriteToFile("Done => " + type[j].ToString() + " => " + websites[i].ToString());
                        }
                        catch (Exception ex)
                        {
                            PSEList.Clear();
                            Console.WriteLine("Failed");
                            MailerPageSpeed.WriteToFile("Analysis => Failed => " + ex.ToString());
                            Console.WriteLine();
                            Console.WriteLine();
                            Console.WriteLine("Trying Again . . .");
                            MailerPageSpeed.WriteToFile("Trying Again . . .");
                            PageSpeed();
                        }
                        response.PageSpeedEntityData = PSEList;
                    }
                }
                j = 0;
                Thread.Sleep(15000);
            }
            string html = MailerPageSpeed.getHtml(response);
            MailerPageSpeed.WriteToFile(html);
            MailerPageSpeed.Email(html);
            MailerPageSpeed.WriteToFile("succes");
            return "success";
        }
    }
}
