using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace Coursework1
{
    public class FileReader
    {
        private XmlSerializer TicketRateXMLSerializer; // XML serializer for ticket prices
        private XmlSerializer UserXMLSerializer; // XML serializer for User details
        private TicketRate ticketRate; // Ticket rate object
        private List<Customer> CustomerList; // List to store customer details

        public FileReader()
        {
            // initializing the serializer
            CustomerList = new List<Customer>();
            TicketRateXMLSerializer = new XmlSerializer(typeof(TicketRate));
            UserXMLSerializer = new XmlSerializer(typeof(List<User>));
        }

        /// <summary>
        /// Reads the ticket price saved in the XML file
        /// </summary>
        /// <returns><c>TicketRate</c> object with the ticket details</returns>
        public TicketRate ReadTicketPrices()
        {
            // File path of the XML file
            String filePath = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName, "ticket_prices.xml");
            
            // Reading the XML file
            FileStream reader = new FileStream(filePath, FileMode.Open, FileAccess.Read);

            // Deserializing the file content to a TicketRate object
            ticketRate = (TicketRate)TicketRateXMLSerializer.Deserialize(reader);

            // closing the file stream
            reader.Close();

            // Returning the ticket rate
            return ticketRate;
        }

        /// <summary>
        /// Saves the updated ticket rate passed in the 
        /// <c>TicketRate</c> object
        /// </summary>
        /// <param name="newTicketRate">TicketRate object</param>
        public void SaveTicketRatesXML(TicketRate newTicketRate)
        {
            // Saving the new ticket rate to a XML file
            String filePath = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName, "ticket_prices.xml");
            FileStream fileStream = new FileStream(filePath, FileMode.Create, FileAccess.Write);

            TicketRateXMLSerializer.Serialize(fileStream, newTicketRate);
            fileStream.Close();
        }

        /// <summary>
        /// Reads the <c>CSV</c> file to retrieve the customer details.
        /// </summary>
        /// <returns>A list of Customers</returns>
        public List<Customer> ReadCustomerCSV()
        {
            // Opening the CSV file
            String filePath = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName, "customers.csv");
            
            // Reading all lines of the CSV file
            // And storing them into an array
            var csvLines = File.ReadAllLines(filePath);

            // Iterating through the lines of the CSV file
            for (int i = 0; i < csvLines.Length; i++)
            {
                var values = csvLines[i].Split(','); // Splitting the csvLine

                // Creating a new Customer object
                Customer customer = new Customer()
                {
                    CustomerID = int.Parse((String)values[0]),
                    GroupNumber = int.Parse((String)values[1]),
                    NumberOfChildren = int.Parse((String)values[2]),
                    NumberOfAdults = int.Parse((String)values[3]),
                    Hours = int.Parse((String)values[4]),
                    IsUnlimited = bool.Parse((String)values[5]),
                    VisitDate = DateTime.Parse((String)values[6]),
                    IsHoliday = bool.Parse((String)values[7]),
                    Total = float.Parse((String)values[8])
                };
                CustomerList.Add(customer); // Adding the customer to the list
            }
            return CustomerList; // Return the list of all imported csutomer
        }

        /// <summary>
        /// Appending a new customer line to the bottom of the CSV file
        /// </summary>
        /// <param name="newCustomer"><c>Customer</c> Object</param>
        public void AppendCustomerData(Customer newCustomer)
        {
            // Preparing th save format for the visit date
            String visitDate = newCustomer.VisitDate.ToString("yyyy-MM-ddTHH:mm:ss");

            // Creating the string that will ne written to the CSV file
            String customerString = $"\n{newCustomer.CustomerID},{newCustomer.GroupNumber},{newCustomer.NumberOfChildren},{newCustomer.NumberOfAdults},{newCustomer.Hours},{newCustomer.IsUnlimited},{visitDate},{newCustomer.IsHoliday},{newCustomer.Total}";
            
            // Path of the CSV file
            String filePath = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName, "customers.csv");
            
            // Appending the text to the CSV file
            File.AppendAllText(filePath, customerString);
        }

        /// <summary>
        /// Reads the list of users stored in the XML file
        /// </summary>
        /// <returns><c>List</c> of <c>User</c></returns>
        public List<User> readUserList()
        {
            // Initializing the list of users
            List<User> userList = new List<User>();

            // Setting up the path of the XML file
            String filePath = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName, "users.xml");
            
            // Reading the XML file
            FileStream reader = new FileStream(filePath, FileMode.Open, FileAccess.Read);

            // Deserializing the contents of the XML file to a list of users
            userList = (List<User>)UserXMLSerializer.Deserialize(reader);

            // CLosing the file stream
            reader.Close();

            // Returning the user list
            return userList;
        }
    }
}
