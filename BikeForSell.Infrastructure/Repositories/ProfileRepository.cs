using BikeForSell.Domain.Interfaces;
using BikeForSell.Domain.Models;
using BikeForSell.Domain.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace BikeForSell.Infrastructure.Repositories
{
    public class ProfileRepository : IProfileRepository
    {
        private readonly Context _context;

        public ProfileRepository(Context context)
        {
            _context = context;
        }

        public IQueryable GetListPurchase(string id)
        {
            var puraches = _context.Transactions.Where(x => x.BuyerId == id)
                .OrderByDescending(d => d.Date);

            return puraches;
        }

        public void DeletePurchase(int id)
        {
            var purchase = _context.Transactions.Find(id);

            if(purchase != null)
            {
                if(purchase.SalemanId == "Deleted")
                {       
                    if(purchase.State != State.Refusal)
                    {
                        var bike = _context.Bikes.Find(purchase.BikeRef);
                        _context.Bikes.Remove(bike);
                    }                    

                    _context.Transactions.Remove(purchase);
                    _context.SaveChanges();
                }
                else
                {
                    purchase.BuyerId = "Deleted";
                    _context.Transactions.Update(purchase);
                    _context.SaveChanges();
                }                
            }
        }

        public IQueryable GetListSales(string id)
        {
            var sales = _context.Transactions.Where(x => x.SalemanId == id)
                .OrderByDescending(d => d.Date);

            return sales;
        }

        public void ChangeState(int saleId, Decision decision)
        {
            var transaction = _context.Transactions.Find(saleId);
            if(transaction != null)
            {
                var bike = _context.Bikes.Find(transaction.BikeRef);

                if (decision == Decision.Positive)
                {
                    bike.IsActive = false;
                    transaction.State = State.Sell;
                }
                else
                {
                    bike.IsBought = false;
                    transaction.State = State.Refusal;
                }

                _context.Transactions.Update(transaction);
                _context.SaveChanges();
            }            
        }

        public void DeleteSale(int id)
        {
            var sale = _context.Transactions.Find(id);

            if (sale != null)
            {
                if(sale.BuyerId == "Deleted")
                {
                    if(sale.State != State.Refusal)
                    {
                        var bike = _context.Bikes.Find(sale.BikeRef);
                        _context.Bikes.Remove(bike);
                    }
                    
                    _context.Transactions.Remove(sale);
                    _context.SaveChanges();
                }
                else
                {
                    sale.SalemanId = "Deleted";
                    _context.Transactions.Update(sale);
                    _context.SaveChanges();
                }
            }
        }

        public bool UserDetalInformation(string id)
        {
            var user = _context.Users.Find(id);
            bool lol = user.AddedDetalInformation;
            return lol;
        }

        public ApplicationUser GetUser(string id)
        {
            return _context.Users.Find(id);
        }

        public void AddDetalInfroamtion(ApplicationUser user, IdentityUserRole<string> newRole)
        {
            _context.Users.Update(user);

            var role = _context.UserRoles.FirstOrDefault(x => x.UserId == user.Id);
            _context.UserRoles.Remove(role);
            _context.UserRoles.Add(newRole);

            _context.SaveChanges();
        }

        public void EditDetalInformation(ApplicationUser user)
        {
            _context.Users.Update(user);
            _context.SaveChanges();
        }
    }
}
