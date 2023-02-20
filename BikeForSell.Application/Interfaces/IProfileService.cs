using AutoMapper.Configuration.Conventions;
using BikeForSell.Application.ViewModels.ProfileVm;
using BikeForSell.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BikeForSell.Application.Interfaces
{
    public interface IProfileService
    {
        ListPurchaseForListVm GetListPurchases(string userId);
        void DeletePurchase(int id);
        ListSaleForListVm GetListSales(string userId);
        void ChangeState(int saleId, int decision);
        void DeleteSale(int id);
        ApplicationUser GetUser(string id);
        bool UserDetalInformation(string id);
        void AddDetalInformation(DetalInformationVm model);
        void EditDetalInformation(EditDetalInformationVm model);
    }
}
