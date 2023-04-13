using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BikeForSell.Domain.Exceptions
{
    public class BikeNotFoundException : Exception
    {
        public BikeNotFoundException() : base("Bike not found in database!") { }
    }
}
