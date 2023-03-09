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
using Microsoft.EntityFrameworkCore.Metadata.Conventions;

namespace BikeForSell.Infrastructure.Repositories
{
    public class ProfileRepository : IProfileRepository
    {
        private readonly Context _context;

        public ProfileRepository(Context context)
        {
            _context = context;
        }

        public IQueryable GetListPurchase(string userId)
        {
            var puraches = _context.Transactions
                .Where(x => x.BuyerId == userId)
                .OrderByDescending(d => d.Date);

            return puraches;
        }

        public void DeletePurchase(int purchaseId)
        {
            var purchase = GetTransaction(purchaseId);

            if (IsTransactionWasRemovedBySalesman(purchase))
            {
                ChceckTransactionStateAndRemoveTransactionAndBike(purchase);
            }
            else
            {
                SetTransactionToRemovedByBuyer(purchase);
            }
        }

        public IQueryable GetListSales(string userId)
        {
            var sales = _context.Transactions
                .Where(x => x.SalemanId == userId)
                .OrderByDescending(d => d.Date);

            return sales;
        }

        public void ChangeTransactionState(int saleId, Decision salesmanDecision)
        {
            var transaction = GetTransaction(saleId);

            var bike = GetBike(transaction.BikeRef);

            if (IsSalesmanDecisionPositive(salesmanDecision))
            {
                SetBikeIsActiveAndTransactionState(bike, transaction);
            }
            else
            {
                SetBikeIsBoughtAndTransactionState(bike, transaction);
            }

            _context.Transactions.Update(transaction);
            _context.SaveChanges();
        }

        public void DeleteSale(int saleId)
        {
            var sale = GetTransaction(saleId);

            if (IsTransactionWasRemovedByBuyer(sale))
            {
                ChceckTransactionStateAndRemoveTransactionAndBike(sale);
            }
            else
            {
                SetTransactionToRemovedBySalesman(sale);
            }
        }

        public bool UserDetalInformation(string userId)
        {
            var user = GetUser(userId);
            bool isAdded = user.AddedDetalInformation;
            return isAdded;
        }

        public ApplicationUser GetUser(string userId)
        {
            return _context.Users.Find(userId);
        }

        public void AddDetalInfroamtion(ApplicationUser user, IdentityUserRole<string> newRole)
        {
            _context.Users.Update(user);
            SetNewUserRole(user, newRole);
            _context.SaveChanges();
        }

        public void EditDetalInformation(ApplicationUser user)
        {
            _context.Users.Update(user);
            _context.SaveChanges();
        }

        // Private methods

        private Bike GetBike(int bikeId)
        {
            return _context.Bikes.Find(bikeId);
        }
        private Transaction GetTransaction(int idTransaction)
        {
            return _context.Transactions.Find(idTransaction);
        }
        private IdentityUserRole<string> GetRole(string userId) 
        { 
            return _context.UserRoles.FirstOrDefault(x => x.UserId == userId);
        }

        private bool IsTransactionWasRemovedBySalesman(Transaction transaction)
        {
            if (transaction.SalemanId == "Deleted")
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        private bool IsTransactionWasRemovedByBuyer(Transaction transaction)
        {
            if (transaction.BuyerId == "Deleted")
            {
                return true;
            }
            else
            {
                return false;
            }

        }
        private bool IsSalesmanDecisionPositive(Decision salesmanDecision)
        {
            if(salesmanDecision == Decision.Positive)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void ChceckTransactionStateAndRemoveTransactionAndBike(Transaction transaction)
        {
            if (transaction.State == State.Sell)
            {
                var bike = GetBike(transaction.BikeRef);
                _context.Bikes.Remove(bike);
            }

            _context.Transactions.Remove(transaction);
            _context.SaveChanges();
        }

        private void SetTransactionToRemovedByBuyer(Transaction transaction)
        {
            transaction.BuyerId = "Deleted";
            _context.Transactions.Update(transaction);
            _context.SaveChanges();
        }
        private void SetTransactionToRemovedBySalesman(Transaction transaction)
        {
            transaction.SalemanId = "Deleted";
            _context.Transactions.Update(transaction);
            _context.SaveChanges();
        }
        private void SetBikeIsActiveAndTransactionState(Bike bike, Transaction transaction)
        {
            bike.IsActive = false;
            transaction.State = State.Sell;
        }
        private void SetBikeIsBoughtAndTransactionState(Bike bike, Transaction transaction)
        {
            bike.IsBought = false;
            transaction.State = State.Refusal;
        }
        private void SetNewUserRole(ApplicationUser user, IdentityUserRole<string> newRole)
        {
            var role = GetRole(user.Id);
            _context.UserRoles.Remove(role);
            _context.UserRoles.Add(newRole);
        }
    }
}
