using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coursework1
{
    public class DailyReport
    {
        public DateTime Today { get; set; }
        public int TotalVisitors { get; set; }
        public int TotalHours { get; set; }
        public int TotalUnlimitedVisits { get; set; }
        public float TotalEarning { get; set; }
        public int TotalChildren { get; set; }
        public int TotalAdult { get; set; }
        public int TotalGroupOfFive { get; set; }
        public int TotalGoupOfTen { get; set; }

        public DailyReport()
        {
            this.Today = DateTime.Now.Date;
            this.TotalEarning = 0.0f;
            this.TotalHours = 0;
            this.TotalUnlimitedVisits = 0;
            this.TotalChildren = 0;
            this.TotalAdult = 0;
            this.TotalGroupOfFive = 0;
            this.TotalGoupOfTen = 0;
        }
    }
}
