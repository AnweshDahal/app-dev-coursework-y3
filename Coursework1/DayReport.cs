using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coursework1
{
    public class DayReport
    {
        public String Day { get; set; }
        public float TotalEarning { get; set; }
        public int TotalVisitors { get; set; }

        public DayReport()
        {
            this.Day = "";
            this.TotalEarning = 0.0f;
            this.TotalVisitors = 0;
        }
    }
}
