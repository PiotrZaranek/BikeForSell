using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BikeForSell.Domain.Models
{
    public class Drive
    {
        public int Id { get; set; }
        public string RearDerailleur { get; set; }
        public string Cassette { get; set; }
        public string Chain { get; set; }
        public string FrontDerailleur { get; set; }
        public string CrankSet { get; set; }
        public string Shifter { get; set; }

        // 1:1 Drive To Bike
        public int BikeRef { get; set; }
        public Bike Bike { get; set; }
    }
}
