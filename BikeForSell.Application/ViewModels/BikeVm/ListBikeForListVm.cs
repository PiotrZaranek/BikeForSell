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

        // filter
        public string SearchString { get; set; }
        public int PrizeFrom { get; set; }
        public int PrizeTo { get; set; }
        public string Type { get; set; }
        public int Filter { get; set; }
    }
}
