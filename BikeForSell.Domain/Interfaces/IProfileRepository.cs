using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BikeForSell.Domain.Interfaces
{
    public interface IProfileRepository
    {
        IQueryable GetListPurchase(string id);
        void DeletePurchase(int id);
        IQueryable GetListSales(string id);
        void ChangeState(int salesId, int decision);
        void DeleteSale(int id);
    }
}
