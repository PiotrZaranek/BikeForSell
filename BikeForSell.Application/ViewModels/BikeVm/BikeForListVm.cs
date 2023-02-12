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
    public class BikeForListVm : IMapFrom<Bike>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Prize { get; set; }
        public string Type { get; set; }
        public string Size { set; get; }
        public DateTime Date { get; set; }
        public bool IsActive { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Bike, BikeForListVm>()
                .ForMember(des => des.Prize, opt => opt.MapFrom(src => src.DetailInformation.Prize))
                .ForMember(des => des.Date, opt => opt.MapFrom(src => src.DetailInformation.Date));
        }
    }
}
