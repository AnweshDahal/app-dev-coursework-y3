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
        XmlSerializer TicketRateXMLSerializer;
        XmlSerializer UserXMLSerializer;
        TicketRate ticketRate;
        List<Customer> CustomerList;

        public FileReader()
        {
            // initializing the serializer
            CustomerList = new List<Customer>();
            TicketRateXMLSerializer = new XmlSerializer(typeof(TicketRate));
            UserXMLSerializer = new XmlSerializer(typeof(List<User>));
        }

        public TicketRate ReadTicketPrices()
        {
            String filePath = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName, "ticket_prices.xml");
            FileStream reader = new FileStream(filePath, FileMode.Open, FileAccess.Read);

            ticketRate = (TicketRate)TicketRateXMLSerializer.Deserialize(reader);

            reader.Close();

            return ticketRate;
        }

        public void SaveTicketRatesXML(TicketRate newTicketRate)
        {
            // Saving the new ticket rate to a XML file
            String filePath = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName, "ticket_prices.xml");
            FileStream fileStream = new FileStream(filePath, FileMode.Create, FileAccess.Write);

            TicketRateXMLSerializer.Serialize(fileStream, newTicketRate);
            fileStream.Close();
        }

        public List<Customer> ReadCustomerCSV()
        {
            // Opening the CSV file
            String filePath = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName, "customers.csv");
            var csvLines = File.ReadAllLines(filePath);

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

        public void AppendCustomerData(Customer newCustomer)
        {
            String visitDate = newCustomer.VisitDate.ToString("yyyy-MM-ddTHH:mm:ss");
            String customerString = $"\n{newCustomer.CustomerID},{newCustomer.GroupNumber},{newCustomer.NumberOfChildren},{newCustomer.NumberOfAdults},{newCustomer.Hours},{newCustomer.IsUnlimited},{visitDate},{newCustomer.IsHoliday},{newCustomer.Total}";
            String filePath = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName, "customers.csv");
            File.AppendAllText(filePath, customerString);
        }

        public List<User> readUserList()
        {
            List<User> userList = new List<User>();
            String filePath = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName, "users.xml");
            FileStream reader = new FileStream(filePath, FileMode.Open, FileAccess.Read);

            userList = (List<User>)TicketRateXMLSerializer.Deserialize(reader);

            reader.Close();

            return userList;
        }
    }
}
