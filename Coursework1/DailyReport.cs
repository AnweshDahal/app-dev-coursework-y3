using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coursework1
{
    public class DailyReport
    {
        /// <summary>
        /// Today's Date
        /// </summary>
        public DateTime Today { get; set; }

        /// <summary>
        /// Total number of visitors in today's date
        /// </summary>
        public int TotalVisitors { get; set; }

        /// <summary>
        /// Total hours spent by the visitors today
        /// </summary>
        public int TotalHours { get; set; }

        /// <summary>
        /// Total number of unlimited visit in the date
        /// </summary>
        public int TotalUnlimitedVisits { get; set; }

        /// <summary>
        /// Total earning in the date
        /// </summary>
        public float TotalEarning { get; set; }

        /// <summary>
        /// Total number of children visitor for the date
        /// </summary>
        public int TotalChildren { get; set; }

        /// <summary>
        /// Total number of adult for teh date
        /// </summary>
        public int TotalAdult { get; set; }

        /// <summary>
        /// Total number of people that came in group of five
        /// </summary>
        public int TotalGroupOfFive { get; set; }

        /// <summary>
        /// Total number of people that came in group of ten
        /// </summary>
        public int TotalGoupOfTen { get; set; }

        public DailyReport()
        {
            this.Today = DateTime.Now.Date; // Setting the date to today's date

            // Initiating the properties with an empty value
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
