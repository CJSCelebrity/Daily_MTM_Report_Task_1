using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

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
    }
}
