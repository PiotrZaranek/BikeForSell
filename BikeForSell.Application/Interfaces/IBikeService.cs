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
        int Add(NewBikeVm bikeVm, ApplicationUser user);
        ListBikeForListVm GetBikeList();
        ListBikeForListVm GetBikeList(string searchString, int prizeFrom, int prizeTo, string type, int filter);
        BikeForDetailsVm GetBikeDetails(int id);
        ListBiekForYourBikes GetYourBikesList(string id);
        void ChangeStatus(int id);
        BikeForEditVm GetBikeForEdit(int id);
        void EditBike(BikeForEditVm bikeVm);
        void DeleteBike(int id);
        void BuyBike(int id, string buyerId);
    }
}
