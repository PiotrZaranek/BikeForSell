using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BikeForSell.Application.ViewModels.BikeVm
{
    public class ListBikeForListVm
    {
        public List<BikeForListVm> Bikes { get; set; }
        public int Size { get; set; }
        public BikeFilterParameters BikeFilterParameters { get; set; }
    }
}
