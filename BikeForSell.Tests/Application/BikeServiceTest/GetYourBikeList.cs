using BikeForSell.Application.Services;
using BikeForSell.Domain.Interfaces;
using Moq;
using BikeForSell.Domain.Models;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using BikeForSell.Application.ViewModels.BikeVm;

namespace BikeForSell.Tests.Application.BikeServiceTest
{
    public class GetYourBikeList
    {
        [Fact]
        public void ReturnListBikeForYourBikesVm()
        {
            // Arrange
            var repo = new Mock<IBikeRepository>();
            var map = SetUp.AddMapper();

            List<Bike> bikes = new List<Bike>();
            IQueryable<Bike> bikeAsQueryalbe = bikes.AsQueryable();

            repo.Setup(x => x.GetYourBikesList("1")).Returns(bikeAsQueryalbe);

            var ser = new BikeService(repo.Object, map);

            //Act
            var result = ser.GetYourBikesList("1");

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<ListBiekForYourBikes>();
            result.Bikes.Should().BeOfType<List<BikeForYourBikesVm>>();
        }       
    }
}
