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

        private bool checkedGroupNumber; // checks if the number of adults and children add up

        private List<Customer> CustomerList; // stores the customer visit details

        private FileReader fileReader; // reads the XML file to import bulk data

        private bool CustomerDetailsImported;

        private List<DayReport> weeklyReport; // Array that will store stats for 7 days

        private bool weeklyReportGenerated;

        public Form1()
        {
            importedTicketRates = false;
            checkedGroupNumber = false;
            CustomerList = new List<Customer>();
            fileReader = new FileReader();
            CustomerDetailsImported = false;
            weeklyReport = new List<DayReport>();
            weeklyReportGenerated = false;
            InitializeComponent();
        }

        // Update the fields in ticket rates tab
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
            ticketRate = fileReader.ReadTicketPrices();

            // calling the function to update fields
            updateFields();


            // setting the imported value to true
            importedTicketRates = true;

            // Label that displays a message after the ticket rates have been imported
            messageLBL.Text = "Successfully Imported";

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

            fileReader.SaveTicketRatesXML(ticketRate);

            // Display save message
            messageLBL.Text = "Successfully Saved";

        }

        private void ImportBTN_Click(object sender, EventArgs e)
        {
            // Bulk importing the customer details
            CustomerList = fileReader.ReadCustomerCSV();

            // The progress bar fills up indicating the 
            // import has been completed
            importVisitorPB.Value = 100;
            // display the number of entires that has been imported.
            dataImportedStatusLBL.Text = String.Format("{0} entries imported", CustomerList.Count);

            // Update the list box element to display the total price of each entry
            foreach(Customer customer in CustomerList)
            {
                TotalListBox.Items.Add(customer.Total);
            }

            // Set the customer details imorted check to true
            CustomerDetailsImported = true;
        }

        private void checkNumberBTN_Click(object sender, EventArgs e)
        {
            // Checks if the values in number of adults text box is empty
            // the number of children text box is empty
            // the group number check box does not have a valid value selected
            // hours check box does not have a valid value selected
            // and display an error message accordingly.

            if (numberOfAdultsTB.Text == "" || numberOfChildrenTB.Text == "" || groupNumberCB.Text == "-- Select One --" || hoursCB.Text == "-- Select One --")
            {
                MessageBox.Show("Please enter values in the field", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Check if the number of adults and children add up to the group size selected
            int totalVisitors = int.Parse(numberOfChildrenTB.Text) + int.Parse(numberOfAdultsTB.Text);
            
            if (totalVisitors != int.Parse(groupNumberCB.Text))
            {
                MessageBox.Show("Number Did Not Match", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Display a success message
            MessageBox.Show("Verified", "OK", MessageBoxButtons.OK, MessageBoxIcon.Information);

            // set the verified status to true
            checkedGroupNumber = true;
        }

        private void saveCustomerBTN_Click(object sender, EventArgs e)
        {
            // Check if the group number add up
            // the ticket rates have been imported
            // and the customer details have been imported in bulk
            if (checkedGroupNumber && importedTicketRates && CustomerDetailsImported)
            {
                Customer customer = new Customer();
                customer.CustomerID = int.Parse(visitorIDTB.Text);
                customer.GroupNumber = int.Parse(groupNumberCB.Text);
                customer.NumberOfAdults = int.Parse(numberOfAdultsTB.Text);
                customer.NumberOfChildren = int.Parse(numberOfChildrenTB.Text);

                // if the customer have selected the unlimited option
                // 
                if (hoursCB.Text == "UNLIMITED")
                {
                    customer.IsUnlimited = true;
                    customer.Hours = 0;
                } else
                {
                    customer.Hours = int.Parse(hoursCB.Text);
                    customer.IsUnlimited = false;
                }

                customer.VisitDate = dateTimePicker.Value;

                customer.IsHoliday = isHolidayCBox.Checked;

                customer.CalculateTotal(ticketRate);

                CustomerList.Add(customer);

                fileReader.AppendCustomerData(customer);

                TotalListBox.Items.Add(customer.Total);
            } else
            {
                MessageBox.Show("Please Verify Before Proceeding or Import Ticket Rates and Customer Details", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            checkedGroupNumber = false;
        }

        private DayReport generateReportForDay(DateTime day)
        {
            DayReport dayReport = new DayReport();
            dayReport.Day = day.ToString("dddd");

            foreach (Customer customer in CustomerList)
            {
                Console.WriteLine($"{customer.VisitDate.ToString()} {day.ToString()}");
                if (customer.VisitDate.Date == day.Date)
                {
                    dayReport.TotalEarning += customer.Total;
                    dayReport.TotalVisitors += customer.GroupNumber;
                }
            }

            return dayReport;
        }

        private void generateDailyReportBTN_Click(object sender, EventArgs e)
        {
            if (importedTicketRates && CustomerDetailsImported)
            {
                DailyReport dailyReport = new DailyReport(); // Daily Report Object

                // Iterating through each customer in the list
                foreach(Customer customer in CustomerList)
                {
                    // Checking if the Visit Date is Today
                    if (customer.VisitDate.Date == DateTime.Now.Date)
                    {
                        // Assigning the data to the dailyReports object
                        dailyReport.TotalEarning += customer.Total;
                        dailyReport.TotalVisitors += customer.GroupNumber;
                        dailyReport.TotalAdult += customer.NumberOfAdults;
                        dailyReport.TotalChildren += customer.NumberOfChildren;
                        dailyReport.TotalHours += customer.Hours;

                        // Checking if the customer has taken unlimited time
                        if (customer.IsUnlimited)
                        {
                            dailyReport.TotalUnlimitedVisits += 1;
                        }

                        // Checking group of five
                        if (customer.GroupNumber == 5)
                        {
                            dailyReport.TotalGroupOfFive += 1;
                        } else
                        {
                            dailyReport.TotalGoupOfTen += 1;
                        }
                    }
                }

                // Displaying the report 
                todayDateLBL.Text = dailyReport.Today.ToString("yyyy/MM/dd dddd");
                totalVisitorLBL.Text = Convert.ToString(dailyReport.TotalVisitors);
                totalEarningLBL.Text = $"Rs. {Convert.ToString(dailyReport.TotalEarning)}";
                totalUnlimitedVisits.Text = Convert.ToString(dailyReport.TotalUnlimitedVisits);
                totalAdultLBL.Text = Convert.ToString(dailyReport.TotalAdult);
                totalChildrenLBL.Text = Convert.ToString(dailyReport.TotalChildren);
                totalGroupOfFiveLBL.Text = Convert.ToString(dailyReport.TotalGroupOfFive);
                totalGroupOfTenLBL.Text = Convert.ToString(dailyReport.TotalGoupOfTen);
                totalHoursLBL.Text = $"{Convert.ToString(dailyReport.TotalHours)} Hrs";

            }
        }

        private void UpdateWeeklyStatTable()
        {
            weeklyReportTable.Rows.Clear();
            weeklyReportTable.Refresh();

            foreach (DayReport report in weeklyReport)
            {
                weeklyReportTable.Rows.Add(new string[] { report.Day, Convert.ToString(report.TotalEarning), Convert.ToString(report.TotalVisitors) });
            }
        }

        private void generateWeeklyReportBTN_Click(object sender, EventArgs e)
        {
            DateTime today = DateTime.Now;
            if (importedTicketRates && CustomerDetailsImported)
            {
                for (int i = 6; i > -1; i--)
                {
                    DayReport dayReport = generateReportForDay(today.AddDays(-i));
                    weeklyReport.Add(dayReport);
                }

                UpdateWeeklyStatTable();
            }

            weeklyReportGenerated = true;
        }

        private void BubbleSort(string sortBy)
        {
            if (sortBy == "EARNING")
            {
                for (int i = 0; i < weeklyReport.Count - 1; i++)
                {
                    Console.WriteLine($"i={i}");
                    for (int j = 0; j < weeklyReport.Count - i - 1; j++)
                    {
                        Console.WriteLine($"    j={j}");
                        if (weeklyReport[j].TotalEarning < weeklyReport[j + 1].TotalEarning)
                        {
                            
                            DayReport temp = weeklyReport[j];
                            weeklyReport[j] = weeklyReport[j + 1];
                            weeklyReport[j + 1] = temp;
                        }
                    }
                }
                
            } else
            {
                for (int i = 0; i < weeklyReport.Count - 1; i++)
                {
                    for (int j = 0; j < weeklyReport.Count - i - 1; j++)
                    {
                        if (weeklyReport[j].TotalVisitors < weeklyReport[j + 1].TotalVisitors)
                        {
                            DayReport temp = weeklyReport[j];
                            weeklyReport[j] = weeklyReport[j + 1];
                            weeklyReport[j+1] = temp;
                        }
                    }
                }
            }

            MessageBox.Show($"Sorted by {sortBy}");
            UpdateWeeklyStatTable();
            
        }

        private void sortByEarningBTN_Click(object sender, EventArgs e)
        {
            if (weeklyReportGenerated)
            {
                BubbleSort("EARNING");
            } else
            {
                MessageBox.Show("Generate weekly report first");
            }
        }

        private void sortByVisitorsBTN_Click(object sender, EventArgs e)
        {
            if (weeklyReportGenerated)
            {
                BubbleSort("VISITORS");
            } else
            {
                MessageBox.Show("Generate weekly report first");
            }
        }
    }
}
