﻿using BikeForSell.Domain.Interfaces;
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
            var bikes = _context.Bikes.Where(x => x.IsActive == true).Where(x => x.IsBought == false);
            return bikes;
        }

        public int Add(Bike bike)
        {
            _context.Bikes.Add(bike);
            _context.SaveChanges();

            return bike.Id;
        }   
        
        public Bike GetBikeDetails(int id)
        {
            var bike = _context.Bikes
                .Include(d => d.DetailInformation)
                .Include(f => f.Frame)
                .Include(d => d.Drive)
                .Include(b => b.Brake)
                .Include(w => w.Wheel)
                .FirstOrDefault(k => k.Id == id);     
            
            return bike;
        }

        public IQueryable GetYourBikesList(int id)
        {
            var bikes = _context.Bikes.Where(x => x.DetailInformation.UserRef == id);

            return bikes;
        }        

        public void ChangeStatus(int id)
        {
            var bike = _context.Bikes.FirstOrDefault(x => x.Id == id);

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

        public Bike GetBikeForEdit(int id)
        {
            var bike = _context.Bikes
                .Include(d => d.DetailInformation)
                .Include(f => f.Frame)
                .Include(d => d.Drive)
                .Include(b => b.Brake)
                .Include(w => w.Wheel)
                .FirstOrDefault(k => k.Id == id);

            return bike;
        }

        public void EditBike(Bike bike)
        {
            _context.Bikes.Update(bike);
            _context.SaveChanges();
        }

        public void DeleteBike(int id)
        {
            var bike = _context.Bikes.Find(id);

            if(bike != null)
            {
                _context.Bikes.Remove(bike);
                _context.SaveChanges();
            }

        }

        public void BuyBike(Transaction transaction)
        {
            var bike = _context.Bikes.Find(transaction.BikeRef);

            if(bike != null)
            {
                bike.DetailInformation = _context.DetailInformations.FirstOrDefault(x => x.BikeRef == transaction.BikeRef);
                bike.IsBought = true;
                transaction.SalemanId = bike.DetailInformation.UserRef;
                _context.Transactions.Add(transaction);
                _context.Bikes.Update(bike);
                _context.SaveChanges();
            }            
        }
    }
}
