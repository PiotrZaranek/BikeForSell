using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BikeForSell.Application.ViewModels.ProfileVm
{
    public class ListSaleForListVm
    {
        public List<TransactionForSalePurchaseListVm> Sales { get; set; }
        public int Size { get; set; }
    }
}
