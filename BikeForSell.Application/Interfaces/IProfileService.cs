using AutoMapper.Configuration.Conventions;
using BikeForSell.Application.ViewModels.ProfileVm;
using BikeForSell.Domain.Enums;
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
        void DeletePurchase(int purchaseId);
        ListSaleForListVm GetListSales(string userId);
        void ChangeState(int saleId, Decision salesmanDecision);
        void DeleteSale(int saleId);
        ApplicationUser GetUser(string userId);
        bool UserDetalInformation(string userId);
        void AddDetalInformation(DetalInformationVm userDetalInformation);
        void EditDetalInformation(EditDetalInformationVm userDetalInformation);
    }
}
