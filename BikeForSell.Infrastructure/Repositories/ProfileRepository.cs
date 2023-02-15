using BikeForSell.Domain.Interfaces;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
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
                if(purchase.SalemanId == null)
                {       
                    if(purchase.State != 2)
                    {
                        var bike = _context.Bikes.Find(purchase.BikeRef);
                        _context.Bikes.Remove(bike);
                    }                    

                    _context.Transactions.Remove(purchase);
                    _context.SaveChanges();
                }
                else
                {
                    purchase.BuyerId = null;
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

        public void ChangeState(int saleId, int decision)
        {
            var transaction = _context.Transactions.Find(saleId);
            if(transaction != null)
            {
                transaction.State = decision;
                _context.Transactions.Update(transaction);

                var bike = _context.Bikes.Find(transaction.BikeRef);

                if (decision == 1)
                {                    
                    bike.IsActive = false;                        
                }
                else
                {
                    bike.IsBought = false;
                }

                _context.SaveChanges();
            }            
        }

        public void DeleteSale(int id)
        {
            var sale = _context.Transactions.Find(id);

            if (sale != null)
            {
                if(sale.BuyerId == null)
                {
                    if(sale.State != 2)
                    {
                        var bike = _context.Bikes.Find(sale.BikeRef);
                        _context.Bikes.Remove(bike);
                    }
                    
                    _context.Transactions.Remove(sale);
                    _context.SaveChanges();
                }
                else
                {
                    sale.SalemanId = null;
                    _context.Transactions.Update(sale);
                    _context.SaveChanges();
                }
            }
        }
    }
}
