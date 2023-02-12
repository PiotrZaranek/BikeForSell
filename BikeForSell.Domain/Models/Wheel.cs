using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BikeForSell.Domain.Models
{
    public class Wheel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Hub { get; set; }
        public string Tyres { get; set; }
        public float Size { get; set; }

        // 1:1 Wheel To Bike
        public int BikeRef { get; set; }
        public Bike Bike { get; set; }
    }
}
