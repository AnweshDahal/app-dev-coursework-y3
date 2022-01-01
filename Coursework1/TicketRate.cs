using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coursework1
{
    public class TicketRate
    {
        /// <summary>
        /// Base ticket rate for child
        /// </summary>
        public float ChildBaseRate { get; set; }

        /// <summary>
        /// Base ticket rate for adult
        /// </summary>
        public float AdultBaseRate { get; set; }

        /// <summary>
        /// Cost multiplier for a visit time of two hour
        /// </summary>
        public float TwoHourMultiplier { get; set; }

        /// <summary>
        /// Cost multiplier for a visit time of three hour
        /// </summary>
        public float ThreeHourMultiplier { get; set; }

        /// <summary>
        /// Cost multiplier for an unlimited visit time
        /// </summary>
        public float UnlimitedMultiplier { get; set; }

        /// <summary>
        /// Discount for a group of five persons
        /// </summary>
        public float GroupFiveMultiplier { get; set; }

        /// <summary>
        /// Discount for a group of ten persons
        /// </summary>
        public float GroupTenMultiplier { get; set; }

        /// <summary>
        /// Discount for a visit in week day
        /// </summary>
        public float WeekDayMultiplier { get; set; }

        /// <summary>
        /// Cost multiplier for a visit in weekends
        /// </summary>
        public float WeekendMultiplier { get; set; }
    }
}
