using AutoMapper;
using BikeForSell.Domain.Interfaces;
using Moq;
using BikeForSell.Application.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BikeForSell.Application.ViewModels.BikeVm;
using FluentAssertions;
using BikeForSell.Domain.Models;

namespace BikeForSell.Tests.Application.BikeServiceTest
{    
    public class BikeServiceAdd
    {
        [Fact]
        public void ReturnBikeObject()
        {
            var repo = new Mock<IBikeRepository>();
            var map = new Mock<IMapper>();
            var bikeService = new BikeService(repo.Object, map.Object);
            var bikeVm = new NewBikeVm();
            bikeVm.DetailInformation = new DetailInformation();

            var result = bikeService.Add(bikeVm, 1);

            result.Should().Be(0);
        }
    }
}
