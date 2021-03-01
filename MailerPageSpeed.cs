using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace PageSpeed
{
    class MailerPageSpeed
    {
        public static string getHtml(PageSpeedEntityList input)
        {
            try
            {
                string messageBody = "<div style=\"width:100%;text-align:center\"><h2 style=\"color:red\">Google PageSpeed Data Analysis as on " + DateTime.Now.ToShortDateString() + " </h2><br><br>";
                string htmlTableStart = "<table style=\"border-collapse:collapse; text-align:center;margin:0 auto;width:90%;\" >";
                string htmlTableEnd = "</table>";
                string htmlHeaderRowStart = "<tr style=\"background-color:#6FA1D2; color:#ffffff;\">";
                string htmlHeaderRowEnd = "</tr>";
                string htmlTrStart = "<tr style=\"color:#555555;\">";
                string htmlTrEnd = "</tr>";
                string htmlTdStart = "<td style=\" border-color:#5c87b2; border-style:solid; border-width:thin; padding: 5px;\">";
                string htmlTdEnd = "</td>";
                messageBody += htmlTableStart;
                messageBody += htmlHeaderRowStart;
                messageBody += htmlTdStart + "URL" + htmlTdEnd;
                messageBody += htmlTdStart + "DateTime" + htmlTdEnd;
                messageBody += htmlTdStart + "Device" + htmlTdEnd;
                messageBody += htmlTdStart + "Score" + htmlTdEnd;
                messageBody += htmlTdStart + "No.of CSS" + htmlTdEnd;
                messageBody += htmlTdStart + "CSS Size (MB)" + htmlTdEnd;
                messageBody += htmlTdStart + "No.of JS" + htmlTdEnd;
                messageBody += htmlTdStart + "JS Size (MB)" + htmlTdEnd;
                messageBody += htmlTdStart + "Images Size (MB)" + htmlTdEnd;
                messageBody += htmlTdStart + "HTML Response Time (MB)" + htmlTdEnd;
                messageBody += htmlTdStart + "Page Size (MB)" + htmlTdEnd;
                messageBody += htmlHeaderRowEnd;
                //Loop all the rows from grid vew and added to html td
                for (int i = 0; i <= input.PageSpeedEntityData.Count - 1; i++)
                {
                    messageBody = messageBody + htmlTrStart;
                    messageBody = messageBody + htmlTdStart + input.PageSpeedEntityData[i].Webiste + htmlTdEnd;
                    messageBody = messageBody + htmlTdStart + input.PageSpeedEntityData[i].FetchTime + htmlTdEnd;
                    messageBody = messageBody + htmlTdStart + input.PageSpeedEntityData[i].Type + htmlTdEnd;
                    messageBody = messageBody + htmlTdStart + input.PageSpeedEntityData[i].PageScore + htmlTdEnd;
                    messageBody = messageBody + htmlTdStart + input.PageSpeedEntityData[i].numberCssResources + htmlTdEnd;
                    messageBody = messageBody + htmlTdStart + Math.Round(input.PageSpeedEntityData[i].cssResponseBytes, 2) + htmlTdEnd;
                    messageBody = messageBody + htmlTdStart + input.PageSpeedEntityData[i].numberJsResources + htmlTdEnd;
                    messageBody = messageBody + htmlTdStart + Math.Round(input.PageSpeedEntityData[i].javascriptResponseBytes, 2) + htmlTdEnd;
                    messageBody = messageBody + htmlTdStart + Math.Round(input.PageSpeedEntityData[i].imageResponseBytes, 2) + htmlTdEnd;
                    messageBody = messageBody + htmlTdStart + Math.Round(input.PageSpeedEntityData[i].HTMLResponseTime, 2) + htmlTdEnd;
                    messageBody = messageBody + htmlTdStart + Math.Round(input.PageSpeedEntityData[i].totalRequestBytes, 2) + htmlTdEnd;
                    messageBody = messageBody + htmlTrEnd;
                }
                messageBody = messageBody + htmlTableEnd;

                messageBody += "<h2 style=\"color:red;\">HDFCERGO with Competitor</h2><div style=\"display:flex;\"><div style=\"width:50%;text-align:center\"><h4 style=\"color:red;\">Desktop Page Speed Score</h4>" + htmlTableStart;
                messageBody += htmlHeaderRowStart;
                messageBody += htmlTdStart + "Rank" + htmlTdEnd;
                messageBody += htmlTdStart + "Company" + htmlTdEnd;
                messageBody += htmlTdStart + "Score" + htmlTdEnd;
                messageBody += htmlTdStart + "PageSize" + htmlTdEnd;
                messageBody += htmlHeaderRowEnd;
                //Loop all the rows from grid vew and added to html td

                var PageSpeedEntityData = input.PageSpeedEntityData.OrderByDescending(x => x.PageScore).Where(x => x.Type == "desktop").ToList();
                int rank = 1;
                for (int i = 0; i <= PageSpeedEntityData.Count - 1; i++)
                {
                    messageBody = messageBody + htmlTrStart;
                    messageBody = messageBody + htmlTdStart + rank + htmlTdEnd;
                    messageBody = messageBody + htmlTdStart + PageSpeedEntityData[i].Webiste + htmlTdEnd;
                    messageBody = messageBody + htmlTdStart + PageSpeedEntityData[i].PageScore + htmlTdEnd;
                    messageBody = messageBody + htmlTdStart + Math.Round(PageSpeedEntityData[i].totalRequestBytes, 2) + htmlTdEnd;
                    messageBody = messageBody + htmlTrEnd;
                    rank = rank + 1;
                }
                messageBody = messageBody + htmlTableEnd + "</div>";

                messageBody += "<div style=\"width:50%;text-align:center\"><h4 style=\"color:red;\">Mobile Page Speed Score</h4>" + htmlTableStart;
                messageBody += htmlHeaderRowStart;
                messageBody += htmlTdStart + "Rank" + htmlTdEnd;
                messageBody += htmlTdStart + "Company" + htmlTdEnd;
                messageBody += htmlTdStart + "Score" + htmlTdEnd;
                messageBody += htmlTdStart + "PageSize" + htmlTdEnd;
                messageBody += htmlHeaderRowEnd;
                //Loop all the rows from grid vew and added to html td
                var PageSpeedEntityData1 = input.PageSpeedEntityData.OrderByDescending(x => x.PageScore).Where(x => x.Type == "mobile").ToList();
                rank = 1;
                for (int i = 0; i <= PageSpeedEntityData1.Count - 1; i++)
                {
                    messageBody = messageBody + htmlTrStart;
                    messageBody = messageBody + htmlTdStart + rank + htmlTdEnd;
                    messageBody = messageBody + htmlTdStart + PageSpeedEntityData1[i].Webiste + htmlTdEnd;
                    messageBody = messageBody + htmlTdStart + PageSpeedEntityData1[i].PageScore + htmlTdEnd;
                    messageBody = messageBody + htmlTdStart + Math.Round(PageSpeedEntityData1[i].totalRequestBytes, 2) + htmlTdEnd;
                    messageBody = messageBody + htmlTrEnd;
                    rank = rank + 1;
                }

                messageBody = messageBody + htmlTableEnd + "</div></div>";

                messageBody += "<h2 style=\"color:red;\">HDFCERGO with Aggregators</h2><div style=\"display:flex;\"><div style=\"width:50%;text-align:center\"><h4 style=\"color:red;\">Desktop Page Speed Score</h4>" + htmlTableStart;
                messageBody += htmlHeaderRowStart;
                messageBody += htmlTdStart + "Rank" + htmlTdEnd;
                messageBody += htmlTdStart + "Company" + htmlTdEnd;
                messageBody += htmlTdStart + "Score" + htmlTdEnd;
                messageBody += htmlTdStart + "PageSize" + htmlTdEnd;
                messageBody += htmlHeaderRowEnd;
                //Loop all the rows from grid vew and added to html td
                var PageSpeedEntityData2 = input.PageSpeedEntityData.OrderByDescending(x => x.PageScore).Where(x => x.Type == "desktop").ToList();
                int rankHDFC = PageSpeedEntityData2.FindIndex(x => x.Webiste == "https://www.hdfcergo.com") + 1;
                var PageSpeedEntityDataHDFC = input.PageSpeedEntityData.Where(x => x.Type == "desktop").Where(x => x.Webiste == "https://www.hdfcergo.com").ToList();
                for (int i = 0; i < 1; i++)
                {
                    messageBody = messageBody + htmlTrStart;
                    messageBody = messageBody + htmlTdStart + "1" + htmlTdEnd;
                    messageBody = messageBody + htmlTdStart + PageSpeedEntityData2[i].Webiste + htmlTdEnd;
                    messageBody = messageBody + htmlTdStart + PageSpeedEntityData2[i].PageScore + htmlTdEnd;
                    messageBody = messageBody + htmlTdStart + Math.Round(PageSpeedEntityData2[i].totalRequestBytes, 2) + htmlTdEnd;
                    messageBody = messageBody + htmlTrEnd;
                    messageBody = messageBody + htmlTrStart;
                    messageBody = messageBody + htmlTdStart + rankHDFC + htmlTdEnd;
                    messageBody = messageBody + htmlTdStart + PageSpeedEntityDataHDFC[i].Webiste + htmlTdEnd;
                    messageBody = messageBody + htmlTdStart + PageSpeedEntityDataHDFC[i].PageScore + htmlTdEnd;
                    messageBody = messageBody + htmlTdStart + Math.Round(PageSpeedEntityDataHDFC[i].totalRequestBytes, 2) + htmlTdEnd;
                    messageBody = messageBody + htmlTrEnd;
                }
                messageBody = messageBody + htmlTableEnd + "</div>";

                messageBody += "<div style=\"width:50%;text-align:center\"><h4 style=\"color:red;\">Mobile Page Speed Score</h4>" + htmlTableStart;
                messageBody += htmlHeaderRowStart;
                messageBody += htmlTdStart + "Rank" + htmlTdEnd;
                messageBody += htmlTdStart + "Company" + htmlTdEnd;
                messageBody += htmlTdStart + "Score" + htmlTdEnd;
                messageBody += htmlTdStart + "PageSize" + htmlTdEnd;
                messageBody += htmlHeaderRowEnd;
                //Loop all the rows from grid vew and added to html td
                var PageSpeedEntityData3 = input.PageSpeedEntityData.OrderByDescending(x => x.PageScore).Where(x => x.Type == "mobile").ToList();
                int rankHDFC2 = PageSpeedEntityData3.FindIndex(x => x.Webiste == "https://www.hdfcergo.com") + 1;
                var PageSpeedEntityData2HDFC = input.PageSpeedEntityData.Where(x => x.Type == "mobile").Where(x => x.Webiste == "https://www.hdfcergo.com").ToList();
                for (int i = 0; i < 1; i++)
                {
                    messageBody = messageBody + htmlTrStart;
                    messageBody = messageBody + htmlTdStart + "1" + htmlTdEnd;
                    messageBody = messageBody + htmlTdStart + PageSpeedEntityData3[i].Webiste + htmlTdEnd;
                    messageBody = messageBody + htmlTdStart + PageSpeedEntityData3[i].PageScore + htmlTdEnd;
                    messageBody = messageBody + htmlTdStart + Math.Round(PageSpeedEntityData3[i].totalRequestBytes, 2) + htmlTdEnd;
                    messageBody = messageBody + htmlTrEnd;
                    messageBody = messageBody + htmlTrStart;
                    messageBody = messageBody + htmlTdStart + rankHDFC2 + htmlTdEnd;
                    messageBody = messageBody + htmlTdStart + PageSpeedEntityData2HDFC[i].Webiste + htmlTdEnd;
                    messageBody = messageBody + htmlTdStart + PageSpeedEntityData2HDFC[i].PageScore + htmlTdEnd;
                    messageBody = messageBody + htmlTdStart + Math.Round(PageSpeedEntityData2HDFC[i].totalRequestBytes, 2) + htmlTdEnd;
                    messageBody = messageBody + htmlTrEnd;
                }
                messageBody = messageBody + htmlTableEnd + "</div></div></div>";
                return messageBody; // return HTML Table as string from this function  
            }
            catch (Exception ex)
            {
                WriteToFile("getHtml() => " + ex.ToString());
                return null;
            }
        }
        public static void Email(string htmlString)
        {
            try
            {
                using (MailMessage message = new MailMessage())
                {

                    SmtpClient smtp = new SmtpClient();
                    message.From = new MailAddress("Kishor.Borchate@hdfcergo.co.in");
                    //message.To.Add(new MailAddress("Riyaz.Chaugule@hdfcergo.com"));
                    //message.CC.Add(new MailAddress("vinayak.patil@hdfcergo.com"));
                    //message.CC.Add(new MailAddress("subhrajit.burman@hdfcergo.com"));
                    message.To.Add(new MailAddress("Kishor.Borchate@hdfcergo.co.in"));
                    //message.CC.Add(new MailAddress("saloni.jhawar@hdfcergo.co.in"));
                    //message.CC.Add(new MailAddress("Kishor.Borchate@hdfcergo.co.in"));
                    message.Subject = "PageSpeed Report : " + DateTime.Now.ToShortDateString() + " | " + DateTime.Now.ToShortTimeString();
                    message.IsBodyHtml = true; //to make message body as html  
                    message.Body = htmlString;
                    smtp.Port = 465;
                    smtp.Host = "smtp.rediffmailpro.com";
                    smtp.Credentials = new NetworkCredential("Kishor.Borchate@hdfcergo.co.in", "Oct@2020");
                    smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                    smtp.Send(message);
                    smtp.Dispose();
                    smtp = null;
                    Console.WriteLine("Mail Sent");
                    WriteToFile("Mail Sent");
                    Environment.Exit(0);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Failure Sending Mail");
                WriteToFile("Failure Sending Mail => " + ex.ToString());
                Console.WriteLine("Trying Again");
                WriteToFile("Trying Again");
                Email(htmlString);
            }
        }

        //public static void Email(string htmlString)
        //{
        //    try
        //    {
        //        using (MailMessage message = new MailMessage())
        //        {

        //            SmtpClient smtp = new SmtpClient();
        //            message.From = new MailAddress("hdfcergo.service@hdfcergo.com");
        //            message.To.Add(new MailAddress("Riyaz.Chaugule@hdfcergo.com"));
        //            message.CC.Add(new MailAddress("vinayak.patil@hdfcergo.com"));
        //            message.CC.Add(new MailAddress("subhrajit.burman@hdfcergo.com"));
        //            message.CC.Add(new MailAddress("saikiran.jindam@hdfcergo.co.in"));
        //            message.CC.Add(new MailAddress("saloni.jhawar@hdfcergo.co.in"));
        //            message.CC.Add(new MailAddress("Kishor.Borchate@hdfcergo.co.in"));
        //            message.Subject = "PageSpeed Report : " + DateTime.Now.ToShortDateString() + " | " + DateTime.Now.ToShortTimeString();
        //            message.IsBodyHtml = true; //to make message body as html  
        //            message.Body = htmlString;
        //            smtp.Send(message);
        //            smtp.Dispose();
        //            smtp = null;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine("Failure Sending Mail");
        //        WriteToFile("Failure Sending Mail => " + ex.ToString());
        //        Console.WriteLine("Trying Again");
        //        WriteToFile("Trying Again");
        //        Email(htmlString);
        //    }
        //}

        public static void WriteToFile(string Message)
        {
            string path = "E:\\Logs";
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            string filepath = "E:\\Logs\\PageSpeedMailerLog_" + DateTime.Now.Date.ToShortDateString().Replace('/', '_') + ".txt";
            if (!File.Exists(filepath))
            {
                // Create a file to write to.   
                using (StreamWriter sw = File.CreateText(filepath))
                {
                    sw.WriteLine(Message);
                }
            }
            else
            {
                using (StreamWriter sw = File.AppendText(filepath))
                {
                    sw.WriteLine(Message + Environment.NewLine + Environment.NewLine);
                }
            }
        }
    }
}
