using BikeForSell.Domain.Enums;
using BikeForSell.Domain.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BikeForSell.Domain.Interfaces
{
    public interface IProfileRepository
    {
        IQueryable GetListPurchase(string userId);
        void DeletePurchase(int purchaseId);
        IQueryable GetListSales(string userId);
        void ChangeTransactionState(int saleId, Decision salesmanDecision);
        void DeleteSale(int saleId);
        bool UserDetalInformation(string userId);
        ApplicationUser GetUser(string userId);
        void AddDetalInfroamtion(ApplicationUser user, IdentityUserRole<string> newRole);
        void EditDetalInformation(ApplicationUser user);
    }
}
