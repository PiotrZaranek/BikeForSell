using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BikeForSell.Domain.Models
{
    public class Frame
    {        
        public int Id { get; set; }
        public string Type { get; set; }
        public string Fork { get; set; } 
        public string ForkType { get; set; }
        public string Stem { get; set; }
        public string Handlebar { get; set; }
        public string Seatpost { get; set; }
        public string Saddle { get; set; }

        // 1:1 Frame To Bike
        public int BikeRef { get; set; }
        public virtual Bike Bike { get; set; }
    }
}
