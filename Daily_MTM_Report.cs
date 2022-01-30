using System;
using System.Windows.Forms;
using HtmlAgilityPack;
using System.IO;
using System.Net;
using System.Threading;

namespace Daily_MTM_Report_Task_1
{
    public partial class Daily_MTM_Report : Form
    {
        public static CancellationTokenSource cancellationToken = new CancellationTokenSource();
        public Daily_MTM_Report()
        {
            InitializeComponent();
            Common.ResourcesLink = "https://clientportal.jse.co.za/downloadable-files?RequestNode=/YieldX/Derivatives/Docs_DMTM";
            Common.JSE_URL = "https://clientportal.jse.co.za";
        }

        private void btnDownload_Click(object sender, EventArgs e)
        {
            ScrapeWeb();
        }

        private static void ScrapeWeb() 
        {
            Common.CreateORCheckLocalFolders();

            #region Retrieve/Download Excel Files for 2022
            try
            {
                HtmlWeb htmlWeb = new HtmlWeb();

                var htmlDoc = htmlWeb.Load(Common.ResourcesLink);

                foreach (var node in htmlDoc.DocumentNode.SelectNodes("//a[@class='inline']"))
                {
                    if (node.InnerText != "Parent")
                    {
                        //Get Excel Links
                        if (node.InnerText.Length > 4)
                        {
                            //Only store the Excel files
                            if (node.InnerText.EndsWith(".xls")) 
                            {
                                //Appending Link
                                HtmlAttribute attr = node.Attributes["href"];
                                
                                string path = $@"C:\Daily_MTM_Report_Sheets_2022\{node.InnerText}";
                                if (!File.Exists(path))
                                {
                                   WebClient webClient = new WebClient();
                                   webClient.Headers.Add("Content-Type", "application/vnd.ms-excel");
                                   string downloadURL = Common.JSE_URL + attr.Value;
                                   webClient.DownloadFile(downloadURL, path);
                                }
                                //ThreadPool.QueueUserWorkItem(new WaitCallback(DownloadFiles(), new object[] { node.InnerText }));
                            }
                        }
                    }
                }
            }
            catch (Exception ex) 
            {
                MessageBox.Show(ex.Message,"Something has happened when retrieving the files",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
            #endregion

            #region Retrieve/Download Excel Files for 2021
            try
            {
                HtmlWeb additionalWeb = new HtmlWeb();

                var additionalWebDoc = additionalWeb.Load(Common.ResourcesLink + "/" + "2021");

                foreach (var node in additionalWebDoc.DocumentNode.SelectNodes("//a[@class='inline']"))
                {
                    if (node.InnerText != "Parent")
                    {
                        //Get Excel Links
                        if (node.InnerText.Length > 4)
                        {
                            //Only store the Excel files
                            if (node.InnerText.EndsWith(".xls"))
                            {
                                //Appending Link
                                HtmlAttribute attr = node.Attributes["href"];

                                string path = $@"C:\Daily_MTM_Report_Sheets_2021\{node.InnerText}";
                                if (!File.Exists(path))
                                {
                                    WebClient webClient = new WebClient();
                                    webClient.Headers.Add("Content-Type", "application/vnd.ms-excel");
                                    string downloadURL = Common.JSE_URL + attr.Value;
                                    webClient.DownloadFile(downloadURL, path);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex) 
            {
                MessageBox.Show(ex.Message, "Something has happened when retrieving the files", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            #endregion
        }
    }
}
