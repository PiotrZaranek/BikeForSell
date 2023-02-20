using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BikeForSell.Domain.Models
{
    public class DetailInformation
    {
        public int Id { get; set; }
        public decimal Prize { get; set; }
        public bool Delivery { get; set; }
        public string City { get; set; }
        public DateTime Date { get; set; }

        // 1:1 DetailInformation To Bike
        public int BikeRef { get; set; }
        public Bike Bike { get; set; }

        // 1:1 DetalInformation To ApplicationUser
        public string UserRef { get; set; }
        public ApplicationUser User { get; set; }
    }
}
