using AutoMapper;
using AutoMapper.Configuration.Conventions;
using AutoMapper.QueryableExtensions;
using BikeForSell.Application.Interfaces;
using BikeForSell.Application.ViewModels.ProfileVm;
using BikeForSell.Domain.Enums;
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
        
        public void DeletePurchase(int purchaseId)
        {
            _profileRepo.DeletePurchase(purchaseId);
        }

        public ListSaleForListVm GetListSales(string userId)
        {
            var sales = _profileRepo.GetListSales(userId)
                .ProjectTo<TransactionForSalePurchaseListVm>(_mapper.ConfigurationProvider).ToList();

            var salesVm = new ListSaleForListVm()
            { 
                Sales = sales,
                Size = sales.Count
            };

            return salesVm;
        }

        public void ChangeState(int saleId, Decision salesmanDecision)
        {
            _profileRepo.ChangeTransactionState(saleId, salesmanDecision);
        }

        public void DeleteSale(int saleId)
        {
            _profileRepo.DeleteSale(saleId);
        }

        public ApplicationUser GetUser(string userId)
        {
            return _profileRepo.GetUser(userId);
        }

        public bool UserDetalInformation(string userId)
        {
            return _profileRepo.UserDetalInformation(userId);
        }

        public void AddDetalInformation(DetalInformationVm userDetalInformation)
        {
            var user = _profileRepo.GetUser(userDetalInformation.Id); 
            
            AssigmentPropperties(user, userDetalInformation);
            user.AddedDetalInformation = true;

            var role = CreateRole(user.Id);

            _profileRepo.AddDetalInfroamtion(user, role);
        }

        private IdentityUserRole<string> CreateRole(string userId)
        {
            var newRole = new IdentityUserRole<string>();
            newRole.UserId = userId;
            newRole.RoleId = "Allowed";

            return newRole;
        }

        public void EditDetalInformation(EditDetalInformationVm userDetalInformation)
        {
            var user = _profileRepo.GetUser(userDetalInformation.Id);
            AssigmentPropperties(user, userDetalInformation);

            _profileRepo.EditDetalInformation(user);
        }

        private void AssigmentPropperties(ApplicationUser user, DetalInformationVm information)
        {
            user.FirsName = information.FirsName;
            user.LastName = information.LastName;
            user.PhoneNumber = information.PhoneNumber;
        }

        private void AssigmentPropperties(ApplicationUser user, EditDetalInformationVm information)
        {
            user.FirsName = information.FirsName;
            user.LastName = information.LastName;
            user.PhoneNumber = information.PhoneNumber;
        }
    }
}
