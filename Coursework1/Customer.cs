using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coursework1
{
    public class Customer
    {
        /// <summary>
        /// ID of the customer
        /// </summary>
        public int CustomerID { get; set; }

        /// <summary>
        /// Total number of people in the group
        /// </summary>
        public int GroupNumber { get; set; }

        /// <summary>
        /// Total number of children in the group
        /// </summary>
        public int NumberOfChildren { get; set; }

        /// <summary>
        /// Total number of adults in the group
        /// </summary>
        public int NumberOfAdults { get; set; }

        /// <summary>
        /// Total hours spent by the group, for a group with unlimited option
        /// a value of 5 hours is set automatically
        /// </summary>
        public int Hours { get; set; }

        /// <summary>
        /// Indicates if the visitor selected the unlimited time option
        /// </summary>
        public bool IsUnlimited { get; set; }

        /// <summary>
        /// Date of the visit
        /// </summary>
        public DateTime VisitDate { get; set; }

        /// <summary>
        /// Indicates if the visit date was a holiday
        /// </summary>
        public bool IsHoliday { get; set; }

        /// <summary>
        /// Total cost of the visit
        /// </summary>
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
