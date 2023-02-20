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

        public int Add(NewBikeVm bikeVm, ApplicationUser user)
        {
            bikeVm.IsActive = true;
            bikeVm.IsBought = false;

            bikeVm.DetailInformation.Date = DateTime.Now;
            bikeVm.DetailInformation.UserRef = user.Id;
            bikeVm.DetailInformation.User = user;

            var bike = _mapper.Map<Bike>(bikeVm);
            int idBike = _bikeRepo.Add(bike);

            return idBike;
        }

        public ListBikeForListVm GetBikeList()
        {
            var bikes = _bikeRepo.GetAllActiveBikes()
                .ProjectTo<BikeForListVm>(_mapper.ConfigurationProvider).ToList();

            var bikesList = new ListBikeForListVm()
            {
                Bikes = bikes,
                Size = bikes.Count
            };

            return bikesList;
        }

        public ListBikeForListVm GetBikeList(string searchString, int prizeFrom, int prizeTo, string type, int filter)
        {
            var bikes = _bikeRepo.GetAllActiveBikes()
                .ProjectTo<BikeForListVm>(_mapper.ConfigurationProvider).ToList();

            bikes = Filtering(bikes, searchString, prizeFrom, prizeTo, type, filter);

            var bikesList = new ListBikeForListVm()
            {
                Bikes = bikes,
                Size = bikes.Count,

                SearchString = searchString,
                PrizeFrom = prizeFrom,
                PrizeTo = prizeTo,
                Type = type,
                Filter = filter
            };

            return bikesList;
        }

        private static List<BikeForListVm> Filtering(List<BikeForListVm> bikes, string searchString, int prizeFrom, int prizeTo, string type, int filter)
        {
            if (searchString == null)
            {
                searchString = string.Empty;
            }

            if (prizeTo == 0)
            {
                prizeTo = 999999;
            }

            if (type == null)
            {
                type = string.Empty;
            }

            bikes = bikes.Where(x => x.Name.StartsWith(searchString))
                .Where(x => x.Prize >= prizeFrom)
                .Where(x => x.Prize <= prizeTo)
                .Where(x => x.Type.StartsWith(type))
                .ToList();            

            switch (filter)
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

        public BikeForDetailsVm GetBikeDetails(int id)
        {
            var bike = _bikeRepo.GetBikeDetails(id);
            var bikeVm = _mapper.Map<BikeForDetailsVm>(bike);

            return bikeVm;
        }

        public ListBiekForYourBikes GetYourBikesList(string id)
        {
            var bikes = _bikeRepo.GetYourBikesList(id)
                .ProjectTo<BikeForYourBikesVm>(_mapper.ConfigurationProvider).ToList();            

            var bikeList = new ListBiekForYourBikes()
            {
                Bikes = bikes,
                Size = bikes.Count
            };

            return bikeList;
        }

        public void ChangeStatus(int id)
        {
            _bikeRepo.ChangeStatus(id);
        }

        public BikeForEditVm GetBikeForEdit(int id)
        {
            var bike = _bikeRepo.GetBikeForEdit(id);
            var bikeVm = _mapper.Map<BikeForEditVm>(bike);

            return bikeVm;
        }

        public void EditBike(BikeForEditVm bikeVm)
        {
            var bike = _mapper.Map<Bike>(bikeVm);

            _bikeRepo.EditBike(bike);
        }

        public void DeleteBike(int id )
        {
            _bikeRepo.DeleteBike(id);
        }

        public void BuyBike(int id, string buyerId)
        {
            var Transaction = new Transaction()
            {
                BuyerId = buyerId,
                Date = DateTime.Now,
                State = 0,                
            };

            _bikeRepo.BuyBike(Transaction, id);       
        }
    }
}
