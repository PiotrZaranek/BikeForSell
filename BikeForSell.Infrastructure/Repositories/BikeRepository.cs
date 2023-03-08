using BikeForSell.Domain.Interfaces;
using BikeForSell.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace BikeForSell.Infrastructure.Repositories
{
    public class BikeRepository : IBikeRepository
    {
        private readonly Context _context;

        public BikeRepository(Context context)
        {
            _context = context;
        }

        public IQueryable<Bike> GetAllActiveBikes()
        {
            var bikes = _context.Bikes
                .Where(x => x.IsActive == true)
                .Where(x => x.IsBought == false);            

            return bikes;                                    
        }

        public int Add(Bike newBike)
        {
            _context.Bikes.Add(newBike);
            _context.SaveChanges();

            return newBike.Id;
        }   
        
        public Bike GetBikeDetails(int bikeId)
        {
            var bike = _context.Bikes
                .Include(d => d.DetailInformation)
                .Include(f => f.Frame)
                .Include(d => d.Drive)
                .Include(b => b.Brake)
                .Include(w => w.Wheel)            
                .First(k => k.Id == bikeId);

            bike.DetailInformation.User = _context.Users.First(k => k.Id == bike.DetailInformation.UserRef);

            return bike;
        }

        public IQueryable GetYourBikesList(string userId)
        {
            var bikes = _context.Bikes
                .Where(x => x.DetailInformation.UserRef == userId)
                .Where(x => x.IsBought == false);

            return bikes;
        }        

        public void ChangeStatus(int bikeId)
        {
            var bike = _context.Bikes.FirstOrDefault(x => x.Id == bikeId);

            if(bike != null)
            {
                if(bike.IsActive)
                {
                    bike.IsActive = false;
                }
                else
                {
                    bike.IsActive = true;
                }

                _context.Bikes.Update(bike);
                _context.SaveChanges();
            }            
        }

        public Bike GetBikeForEdit(int bikeId)
        {
            var bike = _context.Bikes
                .Include(d => d.DetailInformation)
                .Include(f => f.Frame)
                .Include(d => d.Drive)
                .Include(b => b.Brake)
                .Include(w => w.Wheel)
                .First(k => k.Id == bikeId);

            bike.DetailInformation.User = _context.Users.First(k => k.Id == bike.DetailInformation.UserRef);

            return bike;
        }

        public void EditBike(Bike bike)
        {
            _context.Update(bike);
            _context.SaveChanges();
        }

        public void DeleteBike(int bikeId)
        {
            var bike = _context.Bikes.Find(bikeId);

            if(bike != null)
            {
                _context.Bikes.Remove(bike);
                _context.SaveChanges();
            }

        }

        public void BuyBike(Transaction transaction, int bikeId)
        {
            var bike = _context.Bikes.Include(d => d.DetailInformation).FirstOrDefault(x => x.Id == bikeId);

            if(bike != null)
            {                
                bike.IsBought = true;
                transaction.SalemanId = bike.DetailInformation.UserRef;
                transaction.BikeRef = bike.Id;
                _context.Transactions.Add(transaction);
                _context.Bikes.Update(bike);
                _context.SaveChanges();
            }            
        }
    }
}
