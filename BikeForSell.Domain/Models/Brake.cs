using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BikeForSell.Domain.Models
{
    public class Brake
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public string FrontBrake { get; set; }
        public string RearBrake { get; set; }
        
        // 1:1 Brake To Bike
        public int BikeRef { get; set; }
        public Bike Bike { get; set; }
    }
}
