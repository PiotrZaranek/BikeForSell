using AutoMapper;
using AutoMapper.QueryableExtensions;
using BikeForSell.Application.Interfaces;
using BikeForSell.Application.ViewModels.BikeVm;
using BikeForSell.Domain.Interfaces;
using BikeForSell.Domain.Models;
using System.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography;
using AutoMapper.Configuration.Conventions;
using System.Runtime.InteropServices;

namespace BikeForSell.Application.Services
{
    public class BikeService : IBikeService
    {
        private readonly IBikeRepository _bikeRepo;
        private readonly IMapper _mapper;

        public BikeService(IBikeRepository bikeRepo, IMapper mapper)
        {
            _bikeRepo = bikeRepo;
            _mapper = mapper;
        }

        public int AddBike(NewBikeVm bikeVm, ApplicationUser user)
        {            
            AssigmentProperties(bikeVm, user);

            var bike = _mapper.Map<Bike>(bikeVm);
            int idBike = _bikeRepo.Add(bike);

            return idBike;
        }

        public ListBikeForListVm GetBikeList()
        {
            var bikes = _bikeRepo.GetAllActiveBikes()
                .ProjectTo<BikeForListVm>(_mapper.ConfigurationProvider).ToList();

            var bikesList = CreateListBikeForList(bikes, new BikeFilterParameters());

            return bikesList;
        }

        public ListBikeForListVm GetBikeList(BikeFilterParameters bikeFilter)
        {
            var bikes = _bikeRepo.GetAllActiveBikes()
                .ProjectTo<BikeForListVm>(_mapper.ConfigurationProvider).ToList();

            bikes = Filtering(bikes, bikeFilter);
            var bikesList = CreateListBikeForList(bikes, bikeFilter);

            return bikesList;
        }     

        public BikeForDetailsVm GetBikeDetails(int bikeId)
        {
            var bike = _bikeRepo.GetBikeForDetailsOrEdit(bikeId);
            var bikeVm = _mapper.Map<BikeForDetailsVm>(bike);

            return bikeVm;
        }

        public ListBiekForYourBikes GetYourBikesList(string userId)
        {
            var bikes = _bikeRepo.GetYourBikesList(userId)
                .ProjectTo<BikeForYourBikesVm>(_mapper.ConfigurationProvider).ToList();

            var bikeList = CreateListBiekForYourBikes(bikes);

            return bikeList;
        }

        public void ChangeStatus(int bikeId)
        {
            _bikeRepo.ChangeStatus(bikeId);
        }

        public BikeForEditVm GetBikeForEdit(int bikeId)
        {
            var bike = _bikeRepo.GetBikeForDetailsOrEdit(bikeId);
            var bikeVm = _mapper.Map<BikeForEditVm>(bike);

            return bikeVm;
        }

        public void EditBike(BikeForEditVm bikeVm)
        {
            var bike = _mapper.Map<Bike>(bikeVm);

            _bikeRepo.EditBike(bike);
        }

        public void DeleteBike(int bikeId)
        {
            _bikeRepo.DeleteBike(bikeId);
        }

        public void BuyBike(int bikeId, string buyerId)
        {
            var Transaction = CreateTransaction(buyerId);

            _bikeRepo.BuyBike(Transaction, bikeId);       
        }


        //Private methods

        private Transaction CreateTransaction(string buyerId)
        {
            return new Transaction()
            {
                BuyerId = buyerId,
                Date = DateTime.Now,
                State = 0
            };
        }
        private ListBiekForYourBikes CreateListBiekForYourBikes(List<BikeForYourBikesVm> bike)
        {
            return new ListBiekForYourBikes()
            {
                Bikes = bike,
                Size = bike.Count
            };
        }
        private ListBikeForListVm CreateListBikeForList(List<BikeForListVm> bikes, BikeFilterParameters bikeFilter)
        {
            return new ListBikeForListVm()
            {
                Bikes = bikes,
                Size = bikes.Count,
                BikeFilterParameters = bikeFilter
            };
        }

        private List<BikeForListVm> Filtering(List<BikeForListVm> bikes, BikeFilterParameters bikeFilters)
        {
            if (bikeFilters.SearchString == null)
            {
                bikeFilters.SearchString = string.Empty;
            }

            if (bikeFilters.PrizeTo == 0)
            {
                bikeFilters.PrizeTo = 999999;
            }

            if (bikeFilters.Type == null)
            {
                bikeFilters.Type = string.Empty;
            }

            bikes = bikes.Where(x => x.Name.StartsWith(bikeFilters.SearchString))
                .Where(x => x.Prize >= bikeFilters.PrizeFrom)
                .Where(x => x.Prize <= bikeFilters.PrizeTo)
                .Where(x => x.Type.StartsWith(bikeFilters.Type))
                .ToList();

            switch (bikeFilters.Filter)
            {
                case 1:
                    {
                        bikes = bikes.OrderBy(x => x.Prize).ToList();
                        break;
                    }
                case 2:
                    {
                        bikes = bikes.OrderByDescending(x => x.Prize).ToList();
                        break;
                    }
                case 3:
                    {
                        bikes = bikes.OrderBy(x => x.Date).ToList();
                        break;
                    }
                default:
                    {
                        bikes = bikes.OrderBy(x => x.Date).ToList();
                        break;
                    }
            }

            return bikes;
        }

        private void AssigmentProperties(NewBikeVm bikeVm, ApplicationUser user)
        {
            bikeVm.IsActive = true;
            bikeVm.IsBought = false;

            bikeVm.DetailInformation.Date = DateTime.Now;
            bikeVm.DetailInformation.UserRef = user.Id;
            bikeVm.DetailInformation.User = user;
        }
    }
}
