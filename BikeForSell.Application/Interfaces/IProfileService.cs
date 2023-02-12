using AutoMapper.Configuration.Conventions;
using BikeForSell.Application.ViewModels.ProfileVm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BikeForSell.Application.Interfaces
{
    public interface IProfileService
    {
        ListPurchaseForListVm GetListPurchases(int userId);
        void DeletePurchase(int id);
        ListSaleForListVm GetListSales(int userId);
        void ChangeState(int saleId, int decision);
        void DeleteSale(int id);
    }
}
