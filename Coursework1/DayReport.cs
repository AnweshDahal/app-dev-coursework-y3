using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coursework1
{
    public class DayReport
    {
        /// <summary>
        /// The dat to which the report belongs
        /// </summary>
        public String Day { get; set; }

        /// <summary>
        /// Total earning for the day
        /// </summary>
        public float TotalEarning { get; set; }

        /// <summary>
        /// Total visitors for the day
        /// </summary>
        public int TotalVisitors { get; set; }

        public DayReport()
        {
            this.Day = "";
            this.TotalEarning = 0.0f;
            this.TotalVisitors = 0;
        }
    }
}
