using AutoMapper;
using BikeForSell.Application.Mapping;
using BikeForSell.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BikeForSell.Application.ViewModels.BikeVm
{
    public class BikeForDetailsVm : IMapFrom<Bike>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string Size { get; set; }
        public string Description { get; set; }    
        public virtual Frame Frame { get; set; }   
        public Drive Drive { get; set; }
        public Brake Brake { get; set; }
        public Wheel Wheel { get; set; } 
        public BikeDetailInformation DetailInformation { get; set; }

        public string CurrentUserId { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Bike, BikeForDetailsVm>();
        }
    }
}
