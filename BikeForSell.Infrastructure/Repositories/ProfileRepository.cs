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

        public IQueryable GetListPurchase(int id)
        {
            var puraches = _context.Transactions.Where(x => x.BuyerId == id);
            return puraches;
        }

        public void DeletePurchase(int id)
        {
            var purchase = _context.Transactions.Find(id);

            if(purchase != null)
            {
                if(purchase.SalemanId == 0)
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
                    purchase.BuyerId = 0;
                    _context.Transactions.Update(purchase);
                    _context.SaveChanges();
                }                
            }
        }

        public IQueryable GetListSales(int id)
        {
            var sales = _context.Transactions.Where(x => x.SalemanId == id);
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
                if(sale.BuyerId == 0)
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
                    sale.SalemanId = 0;
                    _context.Transactions.Update(sale);
                    _context.SaveChanges();
                }
            }
        }
    }
}
