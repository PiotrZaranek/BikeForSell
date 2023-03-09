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

            var purchasesVm = CreateListPurchaseForListVm(purchases);

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

            var salesVm = CreateListSaleForListVm(sales);

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
            
            AssigmentProperties(user, userDetalInformation);
            var role = CreateRole(user.Id);

            _profileRepo.AddDetalInfroamtion(user, role);
        }

        public void EditDetalInformation(EditDetalInformationVm userDetalInformation)
        {
            var user = _profileRepo.GetUser(userDetalInformation.Id);
            AssigmentProperties(user, userDetalInformation);

            _profileRepo.EditDetalInformation(user);
        }

        // Private methods

        private ListPurchaseForListVm CreateListPurchaseForListVm(List<TransactionForSalePurchaseListVm> purchases)
        {
            return new ListPurchaseForListVm()
            {
                Purchases = purchases,
                Sieze = purchases.Count
            };
        }
        private ListSaleForListVm CreateListSaleForListVm(List<TransactionForSalePurchaseListVm> sales)
        {
            return new ListSaleForListVm()
            {
                Sales = sales,
                Size = sales.Count
            };
        }
        private IdentityUserRole<string> CreateRole(string userId)
        {
            var newRole = new IdentityUserRole<string>();
            newRole.UserId = userId;
            newRole.RoleId = "Allowed";

            return newRole;
        }

        private void AssigmentProperties(ApplicationUser user, DetalInformationVm information)
        {
            user.FirsName = information.FirsName;
            user.LastName = information.LastName;
            user.PhoneNumber = information.PhoneNumber;
            user.AddedDetalInformation = true;
        }
        private void AssigmentProperties(ApplicationUser user, EditDetalInformationVm information)
        {
            user.FirsName = information.FirsName;
            user.LastName = information.LastName;
            user.PhoneNumber = information.PhoneNumber;
        }
    }
}
