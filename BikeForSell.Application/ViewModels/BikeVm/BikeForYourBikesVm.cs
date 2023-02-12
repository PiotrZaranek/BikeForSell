using AutoMapper;
using BikeForSell.Application.Mapping;
using BikeForSell.Domain.Models;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BikeForSell.Application.ViewModels.BikeVm
{
    public class BikeForYourBikesVm : IMapFrom<Bike>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public bool IsBought { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Bike, BikeForYourBikesVm>();                
        }
    }
}
