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

        private bool CustomerDetailsImported;  // Checks if the customer details have been imported

        private List<DayReport> weeklyReport; // Array that will store stats for 7 days

        private bool weeklyReportGenerated;  // Checks if the weekly report have been generated

        /// <summary>
        ///  Constructor
        /// </summary>
        public Form1()
        {
            // Initializing all of the values
            importedTicketRates = false;
            checkedGroupNumber = false;
            CustomerList = new List<Customer>();
            fileReader = new FileReader();
            CustomerDetailsImported = false;
            weeklyReport = new List<DayReport>();
            weeklyReportGenerated = false;
            InitializeComponent();
        }

        /// <summary>
        /// Update the fields in ticket rates tab
        /// </summary>
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
            ticketRate = fileReader.ReadTicketPrices(); // Read ticket rates form file

            // calling the function to update fields
            updateFields();


            // setting the imported value to true
            importedTicketRates = true;

            // Label that displays a message after the ticket rates have been imported
            messageLBL.Text = "Successfully Imported";

        }

        private void saveTicketRatesBTN_Click(object sender, EventArgs e)
        {
            // check if the ticket rates have been imported
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
                if (hoursCB.Text == "UNLIMITED")
                {
                    customer.IsUnlimited = true;
                    customer.Hours = 5; // if the customer chooses unlimited a maximum value of 5 hours is saved
                } else
                {
                    customer.Hours = int.Parse(hoursCB.Text);
                    customer.IsUnlimited = false;
                }

                // sets the value of remaining property
                customer.VisitDate = dateTimePicker.Value;

                customer.IsHoliday = isHolidayCBox.Checked;

                customer.CalculateTotal(ticketRate);

                CustomerList.Add(customer); // add the customer to teh customer list

                fileReader.AppendCustomerData(customer); // append the customer to the csv file

                TotalListBox.Items.Add(customer.Total); // add an entry to a list showing the totals
            } else
            {
                // if the number of visitors have not been checked 
                // a message box pops up telling the user to check the number
                // before saving the data
                MessageBox.Show("Please Verify Before Proceeding or Import Ticket Rates and Customer Details", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            checkedGroupNumber = false; // reset the value of checked group number
        }

        /// <summary>
        /// Generates a report for a specific day passed in <c>day</c>
        /// This is used in generating a weekly report
        /// </summary>
        /// <param name="day">The day for which the report needs to be generated</param>
        /// <returns>a <c>DayReport</c> object with the report for the day</returns>
        private DayReport generateReportForDay(DateTime day)
        {
            DayReport dayReport = new DayReport(); // declare a new day report object
            dayReport.Day = day.ToString("dddd"); // get the week day of the passed date

            // fetch all of the entries with the specified date
            foreach (Customer customer in CustomerList)
            {
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
            // checks if the ticket rates have been imported
            // and the customer details have been imported in bulk
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

        /// <summary>
        /// Updates the Weekly Report Table
        /// </summary>
        private void UpdateWeeklyStatTable()
        {
            // The following lines ensure that the table does not take
            // duplicate values when the data in the table have been sorted
            weeklyReportTable.Rows.Clear(); // Clears the collection for the table
            weeklyReportTable.Refresh(); // Refresh the table

            // Add weekly report in the weekly report table
            foreach (DayReport report in weeklyReport)
            {
                weeklyReportTable.Rows.Add(new string[] { report.Day, Convert.ToString(report.TotalEarning), Convert.ToString(report.TotalVisitors) });
            }
        }

        private void generateWeeklyReportBTN_Click(object sender, EventArgs e)
        {
            DateTime today = DateTime.Now; // get today's date

            // check if the ticket rate nad customer details have been imported
            if (importedTicketRates && CustomerDetailsImported)
            {
                // The report for the last seven days will ben generated
                for (int i = 6; i > -1; i--)
                {
                    DayReport dayReport = generateReportForDay(today.AddDays(-i));
                    weeklyReport.Add(dayReport);
                }

                UpdateWeeklyStatTable(); // update the table after updating the list
            }

            weeklyReportGenerated = true; // set the weekly report generated value to true
        }

        /// <summary>
        /// Use bubble sort algorithm to sort the table by <c>EARNING</c> or <c>Visitors</c>
        /// </summary>
        /// <param name="sortBy">Feature to sort by</param>
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
            // check if the weekly report have been imported
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
            // check if the weekly report have been imported
            if (weeklyReportGenerated)
            {
                BubbleSort("VISITORS");
            } else
            {
                MessageBox.Show("Generate weekly report first");
            }
        }

        /// <summary>
        /// Enabeling the disabled fields
        /// </summary>
        private void enableAdminFields()
        {
            childBaseTicketTB.Enabled = true;
            adultBaseTicketTB.Enabled = true;
            twoHoursMultiplierTB.Enabled = true;
            threeHoursMultiplierTB.Enabled = true;
            unlimitedMultiplierTB.Enabled = true;
            groupOfFiveMultiplierTB.Enabled = true;
            groupOfTenMultiplierTB.Enabled = true;
            weekdayMultiplierTB.Enabled = true;
            holidayMultiplierTB.Enabled = true;
            adminImportBtn.Enabled = true;
            saveTicketRatesBTN.Enabled = true;
        }

        /// <summary>
        /// enabeling the fields for entering visitor in and out time.
        /// </summary>
        private void enableEmployeeFields()
        {
            checkNumberBTN.Enabled = true;
            saveCustomerBTN.Enabled = true;
            ImportBTN.Enabled = true;
            importTicketRatesBTN.Enabled = true;
        }

        private void loginBTN_Click(object sender, EventArgs e)
        {
            List<User> userList = fileReader.readUserList(); // Get the list of user from the file reader

            // iterate through the list
            foreach(User user in userList)
            {
                // checking if the username and password match
                if (user.Username == usernameTB.Text && user.Password == passwordTB.Text)
                {
                    // checking if the user is the admin
                    if (user.IsAdmin)
                    {
                        ticketRateModifyBarrier.Visible = false; // unlocking the ticket rate update form 
                        enableAdminFields(); // enabeling the fields in the ticket rate update form
                    }

                    enableEmployeeFields(); // enabeling all employee forms after login
                    // displaying a message stating that the login was successful
                    MessageBox.Show("Logged In", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return; // esit checking
                }
            }

            // display a message if the username and password was not found
            MessageBox.Show("Username or Password did not match", "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
    }
}
