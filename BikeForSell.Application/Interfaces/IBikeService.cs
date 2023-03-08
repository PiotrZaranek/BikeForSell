using AutoMapper.Configuration.Conventions;
using BikeForSell.Application.ViewModels.BikeVm;
using BikeForSell.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BikeForSell.Application.Interfaces
{
    public interface IBikeService
    {
        int AddBike(NewBikeVm bikeVm, ApplicationUser user);
        ListBikeForListVm GetBikeList();
        ListBikeForListVm GetBikeList(BikeFilterParameters bikeFilter);
        BikeForDetailsVm GetBikeDetails(int id);
        ListBiekForYourBikes GetYourBikesList(string id);
        void ChangeStatus(int id);
        BikeForEditVm GetBikeForEdit(int id);
        void EditBike(BikeForEditVm bikeVm);
        void DeleteBike(int id);
        void BuyBike(int id, string buyerId);
    }
}
