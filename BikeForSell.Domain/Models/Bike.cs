using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BikeForSell.Domain.Models
{
    public class Bike
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string Size { get; set; }
        public string Description { get; set; }
        public bool IsBought { get; set; }
        public bool IsActive { get; set; }

        // 1:1 Frame To Bike        
        public virtual Frame Frame { get; set; }

        // 1:1 Drive To Bike        
        public Drive Drive { get; set; }

        // 1:1 Brake To Bike
        public Brake Brake { get; set; }

        // 1:1 Wheel To Bike
        public Wheel Wheel { get; set; }

        // 1:1 DetailInfromation To Bike    
        public DetailInformation DetailInformation { get; set; }

        // 1:1 Transaction To Bike
        public Transaction Transaction { get; set; }
    }
}
