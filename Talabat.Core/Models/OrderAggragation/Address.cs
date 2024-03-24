using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.Core.Models.OrderAggragation
{
    public class Address
    {
        public Address()
        {
            
        }
        public Address(string fristName, string lastName, string street, string country , string city)
        {
            FristName = fristName;
            LastName = lastName;
            Street = street;
            Country = country;
            City = city;
        }

        public string FristName { get; set; }

        public string LastName { get; set; }

        public string Street { get; set; } 

        public string City { get; set; }

        public string Country { get; set; }


    }
}
