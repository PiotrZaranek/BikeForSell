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
        IQueryable GetListPurchase(string id);
        void DeletePurchase(int id);
        IQueryable GetListSales(string id);
        void ChangeState(int salesId, Decision decision);
        void DeleteSale(int id);
        bool UserDetalInformation(string id);
        ApplicationUser GetUser(string id);
        void AddDetalInfroamtion(ApplicationUser user, IdentityUserRole<string> newRole);
        void EditDetalInformation(ApplicationUser user);
    }
}
