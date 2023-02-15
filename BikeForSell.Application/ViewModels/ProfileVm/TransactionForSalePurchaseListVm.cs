using AutoMapper;
using BikeForSell.Application.Mapping;
using BikeForSell.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BikeForSell.Application.ViewModels.ProfileVm
{
    public class TransactionForSalePurchaseListVm : IMapFrom<Transaction>
    {
        public int Id { get; set; }
        public string BuyerId { get; set; }
        public string SalemanId { get; set; }
        public DateTime Date { get; set; }
        public int State { get; set; }
        public decimal Prize { get; set; }
        public string Name { get; set; }
        public int BikeRef { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Transaction, TransactionForSalePurchaseListVm>()
                .ForMember(des => des.Name, opt => opt.MapFrom(src => src.Bike.Name))
                .ForMember(des => des.Prize, opt => opt.MapFrom(src => src.Bike.DetailInformation.Prize));
        }
    }
}
