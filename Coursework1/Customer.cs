using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coursework1
{
    public class Customer
    {
        public int CustomerID { get; set; }
        public int GroupNumber { get; set; }
        public int NumberOfChildren { get; set; }
        public int NumberOfAdults { get; set; }
        public int Hours { get; set; }
        public bool IsUnlimited { get; set; }
        public DateTime VisitDate { get; set; }
        public bool IsHoliday { get; set; }
        public float Total { get; set; }
        
    }
}
