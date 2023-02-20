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
    public class Add
    {
        [Fact]
        public void ReturnBikeId()
        {
            // Arrnge
            var repo = new Mock<IBikeRepository>();
            var map = SetUp.AddMapper();            
            var bikeService = new BikeService(repo.Object, map);

            repo.Setup(a => a.Add(It.IsAny<Bike>())).Returns(1);

            var bikeVm = new NewBikeVm();          
            bikeVm.DetailInformation = new DetailInformation();

            //Act
            var result = bikeService.Add(bikeVm, AddUSer());

            //Assert
            result.Should().Be(1);
            bikeVm.IsActive.Should().BeTrue();
            bikeVm.IsBought.Should().BeFalse();
        }

        private static ApplicationUser AddUSer()
        {
            return new ApplicationUser() { Id = "a", Email = "b", PhoneNumber = "c", FirsName = "d", LastName = "e" };
        }
    }
}
