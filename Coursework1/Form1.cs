using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Coursework1
{
    public partial class Form1 : Form
    {
        // Declaring the variables
        private bool importedTicketRates; // Checks if the ticket rates have been imported

        private TicketRate ticketRate; // Model for the ticket rate



        public Form1()
        {
            importedTicketRates = false;
            InitializeComponent();
        }

        private void updateFields()
        {
            // Updating labels to display the ticket rates
            // Child Labels
            childBaseLBL.Text = Convert.ToString(ticketRate.ChildBaseRate);
            childTwoHourLBL.Text = Convert.ToString(ticketRate.ChildBaseRate * ticketRate.TwoHourMultiplier);
            childThreeHourLBL.Text = Convert.ToString(ticketRate.ChildBaseRate * ticketRate.ThreeHourMultiplier);
            childUnlimitedLBL.Text = Convert.ToString(ticketRate.ChildBaseRate * ticketRate.UnlimitedMultiplier);
            childGroupFiveLBL.Text = Convert.ToString(ticketRate.ChildBaseRate * ticketRate.GroupFiveMultiplier);
            childGroupTenLBL.Text = Convert.ToString(ticketRate.ChildBaseRate * ticketRate.GroupTenMultiplier);
            childWeekdaysLBL.Text = Convert.ToString(ticketRate.ChildBaseRate * ticketRate.WeekDayMultiplier);
            childWeekendsLBL.Text = Convert.ToString(ticketRate.ChildBaseRate * ticketRate.WeekendMultiplier);

            // Adult Labels
            adultBaseLBL.Text = Convert.ToString(ticketRate.AdultBaseRate);
            adultTwoHourLBL.Text = Convert.ToString(ticketRate.AdultBaseRate * ticketRate.TwoHourMultiplier);
            adultThreeHourLBL.Text = Convert.ToString(ticketRate.AdultBaseRate * ticketRate.ThreeHourMultiplier);
            adultUnlimitedLBL.Text = Convert.ToString(ticketRate.AdultBaseRate * ticketRate.UnlimitedMultiplier);
            adultGroupFiveLBL.Text = Convert.ToString(ticketRate.AdultBaseRate * ticketRate.GroupFiveMultiplier);
            adultGroupTenLBL.Text = Convert.ToString(ticketRate.AdultBaseRate * ticketRate.GroupTenMultiplier);
            adultWeekdaysLBL.Text = Convert.ToString(ticketRate.AdultBaseRate * ticketRate.WeekDayMultiplier);
            adultWeekendsLBL.Text = Convert.ToString(ticketRate.AdultBaseRate * ticketRate.WeekendMultiplier);

            // Add values to the text box
            childBaseTicketTB.Text = Convert.ToString(ticketRate.ChildBaseRate);
            adultBaseTicketTB.Text = Convert.ToString(ticketRate.AdultBaseRate);
            twoHoursMultiplierTB.Text = Convert.ToString(ticketRate.TwoHourMultiplier);
            threeHoursMultiplierTB.Text = Convert.ToString(ticketRate.ThreeHourMultiplier);
            unlimitedMultiplierTB.Text = Convert.ToString(ticketRate.UnlimitedMultiplier);
            groupOfFiveMultiplierTB.Text = Convert.ToString(ticketRate.GroupFiveMultiplier);
            groupOfTenMultiplierTB.Text = Convert.ToString(ticketRate.GroupTenMultiplier);
            weekdayMultiplierTB.Text = Convert.ToString(ticketRate.WeekDayMultiplier);
            holidayMultiplierTB.Text = Convert.ToString(ticketRate.WeekendMultiplier);

        }

        private void importTicketRatesBTN_Click(object sender, EventArgs e)
        {
            FileReader fileReader = new FileReader();
            ticketRate = fileReader.ReadTicketPrices();

            // calling the function to update fields
            updateFields();


            // setting the imported value to true
            importedTicketRates = true;

            // Label that displays a message after the ticket rates have been imported
            messageLBL.Text = "Successfully Imported";

            fileReader.ReadCustomers();
        }

        private void saveTicketRatesBTN_Click(object sender, EventArgs e)
        {
            if (!importedTicketRates)
            {
                MessageBox.Show("Import Data Before Saving", "Could Not Import", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // getting the ticket rates from the text boxes
            ticketRate.ChildBaseRate = float.Parse(childBaseTicketTB.Text);
            ticketRate.AdultBaseRate = float.Parse(adultBaseTicketTB.Text);
            ticketRate.TwoHourMultiplier = float.Parse(twoHoursMultiplierTB.Text);
            ticketRate.ThreeHourMultiplier = float.Parse(threeHoursMultiplierTB.Text);
            ticketRate.UnlimitedMultiplier = float.Parse(unlimitedMultiplierTB.Text);
            ticketRate.GroupFiveMultiplier = float.Parse(groupOfFiveMultiplierTB.Text);
            ticketRate.GroupTenMultiplier = float.Parse(groupOfTenMultiplierTB.Text);
            ticketRate.WeekDayMultiplier = float.Parse(weekdayMultiplierTB.Text);
            ticketRate.WeekendMultiplier = float.Parse(holidayMultiplierTB.Text);

            // Updating the fields after new update
            updateFields();

            // Display save message
            messageLBL.Text = "Successfully Saved";

        }

        private void ImportBTN_Click(object sender, EventArgs e)
        {
            importVisitorPB.Value = 100;
            dataImportedStatusLBL.Text = "Data Imported";
        }
    }
}
