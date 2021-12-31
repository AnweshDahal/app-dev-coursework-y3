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

        /// <summary>
        /// Calculates the total ticket price for the Customer
        /// the ticket price is the product of <strong>base ticket rate</strong> and 
        /// the <strong>total number of Adult and Children</strong>
        /// <em>Discounts</em> are applied according to the group number, and hourly package
        /// finally extra charge is applied if the customer visits during holidays
        /// </summary>
        /// <param name="ticketRate">The latest ticket rate </param>
        public void CalculateTotal(TicketRate ticketRate)
        {
            float total = 0.0f;

            if (this.IsUnlimited)
            {
                total = this.NumberOfChildren * ticketRate.ChildBaseRate * ticketRate.UnlimitedMultiplier
                    + this.NumberOfAdults * ticketRate.AdultBaseRate * ticketRate.UnlimitedMultiplier;
            }
            else
            {
                if (this.Hours == 2)
                {
                    total = this.NumberOfChildren * ticketRate.ChildBaseRate * ticketRate.TwoHourMultiplier
                    + this.NumberOfAdults * ticketRate.AdultBaseRate * ticketRate.TwoHourMultiplier;
                }
                else if (this.Hours == 3)
                {
                    total = this.NumberOfChildren * ticketRate.ChildBaseRate * ticketRate.ThreeHourMultiplier
                    + this.NumberOfAdults * ticketRate.AdultBaseRate * ticketRate.ThreeHourMultiplier;
                } else
                {
                    total = this.NumberOfChildren * ticketRate.ChildBaseRate + this.NumberOfAdults * ticketRate.AdultBaseRate;
                }
            }

            if (this.GroupNumber == 5)
            {
                total = total * ticketRate.GroupFiveMultiplier;
            }
            else
            {
                total = total * ticketRate.GroupTenMultiplier;
            }

            if (this.IsHoliday)
            {
                total = total * ticketRate.WeekendMultiplier;
            }
            else
            {
                total = total * ticketRate.WeekDayMultiplier;
            }

            this.Total = total;
        }
    }
}
