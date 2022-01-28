using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Daily_MTM_Report_Task_1
{
    public partial class Daily_MTM_Report : Form
    {
        public Daily_MTM_Report()
        {
            InitializeComponent();
            Common.JSE_URL = "https://clientportal.jse.co.za/downloadable-files?RequestNode=/YieldX/Derivatives/Docs_DMTM ";
        }

        private void btnDownload_Click(object sender, EventArgs e)
        {
            DownloadFiles();
        }

        private void DownloadFiles() 
        {
            try
            {
                #region Search for 2021 files/folders
                //File
                //WebClient client = new WebClient();
                //Stream stream = client.OpenRead();
                //StreamReader reader = new StreamReader(stream);
                #endregion
                //string Content = reader.ReadToEnd();
                //NetwrokCredentials
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
