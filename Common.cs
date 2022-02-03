using System;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Text.RegularExpressions;
using System.Globalization;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Daily_MTM_Report_Task_1
{
    
    class Common
    {
        public static string JSE_URL;

        public static string ResourcesLink;

        public static void CreateORCheckLocalFolders() 
        {
            string Excel2022FolderPath = @"C:\Daily_MTM_Report_Sheets_2022";
            if (!Directory.Exists(Excel2022FolderPath)) 
            {
                Directory.CreateDirectory(Excel2022FolderPath);
            }
            string Excel2021FolderPath = @"C:\Daily_MTM_Report_Sheets_2021";
            if (!Directory.Exists(Excel2021FolderPath)) 
            {
                Directory.CreateDirectory(Excel2021FolderPath);
            }
            
        }

        public static void ReadExcelData(string excelFilePath) 
        {
            #region Read the data from the excel file and send it through to the DB
            string excelFileDate = "";

            string connString = $"Provider=Microsoft.Jet.OLEDB.4.0;Data Source={excelFilePath}; Extended Properties='Excel 8.0;HDR=YES;IMEX=1;'";

                OleDbConnection oledbConn = new OleDbConnection(connString);

                oledbConn.Open();

                OleDbCommand cmd = new OleDbCommand("SELECT * FROM [Sheet1$]", oledbConn);

                OleDbDataAdapter oleda = new OleDbDataAdapter();

                oleda.SelectCommand = cmd;

                DataSet ds = new DataSet();

                oleda.Fill(ds, "DailyMTM");

                foreach (var m in ds.Tables[0].DefaultView)
                {
                var rowName = ((DataRowView)m).Row.ItemArray[0].ToString();

                if (rowName.Contains("DAILY SUMMARY")) {
                    var removeText = "DAILY SUMMARY FOR:";
                    string filedate = rowName.Remove(0,removeText.Length);
                    excelFileDate = filedate.Trim();
                }

                if (!rowName.Contains("DAILY MTM REPORT") && !rowName.Contains("DAILY SUMMARY") && !rowName.Contains("Contract Details") && !rowName.Contains("Derivatives")) 
                {
                    SendToDB(((DataRowView)m).Row.ItemArray, excelFileDate);
                }
                };

                oledbConn.Close();
            #endregion
        }

        private static void SendToDB(object[] values, string excelfileDate) 
        {
            #region Process the data through to the DB
            DateTime fileDate = DateTime.Parse(excelfileDate);
            string contract = values[0].ToString();
            DateTime expiryDate = DateTime.Parse(values[2].ToString());
            string classification = values[3].ToString();
            var strike = float.Parse(values[4].ToString(),NumberStyles.Float,CultureInfo.CurrentCulture);
            string callPut = values[5].ToString();
            float mTMYield = float.Parse(values[6].ToString(),NumberStyles.Float, CultureInfo.CurrentCulture);
            float markPrice = float.Parse(values[7].ToString(), NumberStyles.Float, CultureInfo.CurrentCulture);
            float spotRate = float.Parse(values[8].ToString(), NumberStyles.Float, CultureInfo.CurrentCulture);
            float previousMTM = float.Parse(values[9].ToString(), NumberStyles.Float, CultureInfo.CurrentCulture);
            float previousPrice = float.Parse(values[10].ToString(), NumberStyles.Float, CultureInfo.CurrentCulture);
            float premiumOnOption = float.Parse(values[11].ToString() == "" ? "0" : values[11].ToString(), NumberStyles.Float, CultureInfo.CurrentCulture);
            float volatility = float.Parse(values[12].ToString(), NumberStyles.Float, CultureInfo.CurrentCulture);
            float delta = float.Parse(values[13].ToString(), NumberStyles.Float, CultureInfo.CurrentCulture);
            float deltaValue = float.Parse(values[14].ToString(), NumberStyles.Float, CultureInfo.CurrentCulture);
            float contractsTraded = float.Parse(values[15].ToString(), NumberStyles.Float, CultureInfo.CurrentCulture);
            float openInterest = float.Parse(values[16].ToString(), NumberStyles.Float, CultureInfo.CurrentCulture);

            
            string connectionString = "Data Source=.;Initial Catalog=master;Integrated Security=True";

            SqlConnection connection = new SqlConnection(@connectionString);

            string query = $"INSERT INTO DailyMTM (FileDate,Contract,ExpiryDate,Classification,Strike,CallPut,MTMYield,MarkPrice,SpotRate,PreviousMTM,PreviousPrice,PremiumOnOption,Volatility,Delta,DeltaValue,ContractsTraded,OpenInterest) VALUES({fileDate},{contract},{expiryDate},{classification},{strike},{callPut},{mTMYield},{markPrice},{spotRate},{previousMTM},{previousPrice},{premiumOnOption},{volatility},{delta},{deltaValue},{contractsTraded},{openInterest})";

            SqlCommand command = new SqlCommand(query, connection);
            try
            {
                connection.Open();
                command.ExecuteNonQuery();
            }
            catch (SqlException e)
            {
                MessageBox.Show(e.Message, "Something has when processing the file to the database", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            finally 
            {
                connection.Close();
            }
            #endregion
        }
    }
}
