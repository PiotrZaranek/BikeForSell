using AutoMapper;
using AutoMapper.Configuration.Conventions;
using AutoMapper.QueryableExtensions;
using BikeForSell.Application.Interfaces;
using BikeForSell.Application.ViewModels.ProfileVm;
using BikeForSell.Domain.Interfaces;
using BikeForSell.Domain.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Globalization;
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

        public ListPurchaseForListVm GetListPurchases(string userId)
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

        public ListSaleForListVm GetListSales(string id)
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

        public ApplicationUser GetUser(string id)
        {
            return _profileRepo.GetUser(id);
        }

        public bool UserDetalInformation(string id)
        {
            return _profileRepo.UserDetalInformation(id);
        }

        public void AddDetalInformation(DetalInformationVm model)
        {
            var user = _profileRepo.GetUser(model.Id);
            user.PhoneNumber = model.PhoneNumber;
            user.FirsName = model.FirsName;
            user.LastName = model.LastName;
            user.AddedDetalInformation = true;

            var role = new IdentityUserRole<string>();
            role.UserId = user.Id;
            role.RoleId = "Allowed";

            _profileRepo.AddDetalInfroamtion(user, role);
        }

        public void EditDetalInformation(EditDetalInformationVm model)
        {
            var user = _profileRepo.GetUser(model.Id);
            user.FirsName = model.FirsName;
            user.LastName = model.LastName;
            user.PhoneNumber = model.PhoneNumber;

            _profileRepo.EditDetalInformation(user);
        }
    }
}
