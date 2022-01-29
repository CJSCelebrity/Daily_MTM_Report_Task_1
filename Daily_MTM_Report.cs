using Newtonsoft.Json;
using System;
using System.Windows.Forms;
using HtmlAgilityPack;

namespace Daily_MTM_Report_Task_1
{
    public partial class Daily_MTM_Report : Form
    {
        public Daily_MTM_Report()
        {
            InitializeComponent();
            Common.JSE_URL = "https://clientportal.jse.co.za/downloadable-files?RequestNode=/YieldX/Derivatives/Docs_DMTM";
        }

        private void btnDownload_Click(object sender, EventArgs e)
        {

            CrawlWeb();
            
        }

        private static void CrawlWeb() 
        {
            #region Download Excel Links for 2022
            HtmlWeb htmlWeb = new HtmlWeb();

            var htmlDoc = htmlWeb.Load(Common.JSE_URL);

            foreach (var node in htmlDoc.DocumentNode.SelectNodes("//a[@class='inline']"))
            {
                //var test = JsonConvert.SerializeObject(node.InnerText + "\n");
                if (node.InnerText != "Parent")
                {
                    //Get Excel Links
                    if (node.InnerText.Length > 4)
                    {
                        //Appending Link
                        Common.ExcelLinks2022 = new System.Collections.Generic.List<string>();
                        Common.ExcelLinks2022.Add(node.InnerText);
                    }
                }
            }
            #endregion

            #region Download Excel Links for 2021
            HtmlWeb additionalWeb = new HtmlWeb();

            var additionalWebDoc = additionalWeb.Load(Common.JSE_URL + "/" + "2021");

            foreach (var node in additionalWebDoc.DocumentNode.SelectNodes("//a[@class='inline']"))
            {
                if (node.InnerText != "Parent")
                {
                    //Get Excel Links
                    if (node.InnerText.Length > 4)
                    {
                        //Appending Link
                        Common.ExcelLinks2021 = new System.Collections.Generic.List<string>();
                        Common.ExcelLinks2021.Add(node.InnerText);
                    }
                }
            }
            #endregion
        }

        //private static 
    }
}
