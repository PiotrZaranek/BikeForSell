using AutoMapper;
using AutoMapper.Configuration.Conventions;
using AutoMapper.QueryableExtensions;
using BikeForSell.Application.Interfaces;
using BikeForSell.Application.ViewModels.ProfileVm;
using BikeForSell.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BikeForSell.Application.Services
{
    public class ProfileService : IProfileService
    {
        private readonly IProfileRepository _profileRepo;
        private readonly IMapper _mapper;

        public ProfileService(IProfileRepository profileRepo, IMapper mapper)
        {
            _profileRepo = profileRepo;
            _mapper = mapper;
        }

        public ListPurchaseForListVm GetListPurchases(int userId)
        {
            var purchases = _profileRepo.GetListPurchase(userId)
                .ProjectTo<TransactionForSalePurchaseListVm>(_mapper.ConfigurationProvider).ToList();

            var purchasesVm = new ListPurchaseForListVm()
            {
                Purchases = purchases,
                Sieze = purchases.Count
            };

            return purchasesVm;
        }
        
        public void DeletePurchase(int id)
        {
            _profileRepo.DeletePurchase(id);
        }

        public ListSaleForListVm GetListSales(int id)
        {
            var sales = _profileRepo.GetListSales(id)
                .ProjectTo<TransactionForSalePurchaseListVm>(_mapper.ConfigurationProvider).ToList();

            var salesVm = new ListSaleForListVm()
            { 
                Sales = sales,
                Size = sales.Count
            };

            return salesVm;
        }

        public void ChangeState(int saleId, int decision)
        {
            _profileRepo.ChangeState(saleId, decision);
        }

        public void DeleteSale(int id)
        {
            _profileRepo.DeleteSale(id);
        }
    }
}
