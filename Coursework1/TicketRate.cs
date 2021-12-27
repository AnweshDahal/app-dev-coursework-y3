using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coursework1
{
    public class TicketRate
    {
        public float ChildBaseRate { get; set; }
        public float AdultBaseRate { get; set; }
        public float TwoHourMultiplier { get; set; }
        public float ThreeHourMultiplier { get; set; }
        public float UnlimitedMultiplier { get; set; }
        public float GroupFiveMultiplier { get; set; }
        public float GroupTenMultiplier { get; set; }
        public float WeekDayMultiplier { get; set; }
        public float WeekendMultiplier { get; set; }
    }
}
