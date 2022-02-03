using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Daily_MTM_Report_Task_1
{
    public class DailyMTM
    {
        public DateTime FileDate { get; set; } 
        public string Contract { get; set; } 
        public DateTime ExpiryDate { get; set; } 
        public string Classification { get; set; } 
        public float Strike { get; set; } 
        public string CallPut { get; set; } 
        public float MTMYield { get; set; } 
        public float MarkPrice { get; set; } 
        public float SpotRate { get; set; } 
        public float PreviousMTM { get; set; } 
        public float PreviousPrice { get; set; } 
        public float PremiumOnOption { get; set; } 
        public float Volatility { get; set; } 
        public float Delta { get; set; } 
        public float DeltaValue { get; set; } 
        public float ContractsTraded { get; set; } 
        public float OpenInterest { get; set; } 
    }
}
