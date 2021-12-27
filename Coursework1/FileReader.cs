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
        XmlSerializer CustomerXMLSerializer;
        TicketRate ticketRate;
        List<Customer> CustomerList;

        public FileReader()
        {
            // initializing the serializer
            CustomerList = new List<Customer>();
            TicketRateXMLSerializer = new XmlSerializer(typeof(TicketRate));
            CustomerXMLSerializer = new XmlSerializer(typeof(List<Customer>));
        }

        public TicketRate ReadTicketPrices()
        {

            String filePath = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName, "ticket_prices.xml");
            FileStream reader = new FileStream(filePath, FileMode.Open, FileAccess.Read);

            ticketRate = (TicketRate)TicketRateXMLSerializer.Deserialize(reader);

            return ticketRate;
        }

        public void ReadCustomers()
        {
            String filePath = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName, "customers.xml");
            FileStream writer = new FileStream(filePath, FileMode.Create, FileAccess.Write);

            Customer customer = new Customer();

            customer.CustomerID = 1;
            customer.GroupNumber = 5;
            customer.NumberOfChildren = 2;
            customer.NumberOfAdults = 3;
            customer.Hours = 3;
            customer.IsUnlimited = false;
            customer.VisitDate = DateTime.Today;
            customer.IsHoliday = true;

            float total = 0.0f;

            if (customer.IsUnlimited)
            {
                total = customer.NumberOfChildren * ticketRate.ChildBaseRate * ticketRate.UnlimitedMultiplier
                    + customer.NumberOfAdults * ticketRate.AdultBaseRate * ticketRate.UnlimitedMultiplier;
            }
            else
            {
                if (customer.Hours == 2)
                {
                    total = customer.NumberOfChildren * ticketRate.ChildBaseRate * ticketRate.TwoHourMultiplier
                    + customer.NumberOfAdults * ticketRate.AdultBaseRate * ticketRate.TwoHourMultiplier;
                }
                else
                {
                    total = customer.NumberOfChildren * ticketRate.ChildBaseRate * ticketRate.ThreeHourMultiplier
                    + customer.NumberOfAdults * ticketRate.AdultBaseRate * ticketRate.ThreeHourMultiplier;
                }
            }

            if (customer.GroupNumber == 5)
            {
                total = total * ticketRate.GroupFiveMultiplier;
            } else
            {
                total = total * ticketRate.GroupTenMultiplier;
            }

            if (customer.IsHoliday)
            {
                total = total * ticketRate.WeekendMultiplier;
            } else
            {
                total = total * ticketRate.WeekDayMultiplier;
            }

            customer.Total = total;

            CustomerList.Add(customer);
            CustomerList.Add(customer);

            CustomerXMLSerializer.Serialize(writer, CustomerList);
            writer.Close();
        }
    }
}
