using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BikeForSell.Application.ViewModels.ProfileVm
{
    public class ListPurchaseForListVm
    {
        public List<TransactionForSalePurchaseListVm> Purchases { get; set; }
        public int Sieze { get; set; }
    }
}
