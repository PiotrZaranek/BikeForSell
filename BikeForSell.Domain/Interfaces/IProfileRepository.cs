using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BikeForSell.Domain.Interfaces
{
    public interface IProfileRepository
    {
        IQueryable GetListPurchase(int id);
        void DeletePurchase(int id);
        IQueryable GetListSales(int id);
        void ChangeState(int salesId, int decision);
        void DeleteSale(int id);
    }
}
