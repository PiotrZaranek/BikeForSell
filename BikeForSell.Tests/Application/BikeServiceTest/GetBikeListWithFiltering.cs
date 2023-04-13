using BikeForSell.Application.Services;
using BikeForSell.Application.ViewModels.BikeVm;
using BikeForSell.Domain.Interfaces;
using BikeForSell.Domain.Models;
using FluentAssertions;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BikeForSell.Tests.Application.BikeServiceTest
{
    public class GetBikeListWithFiltering
    {
        [Fact]
        public void ReturnListBikeForListVm()
        {
            //Arrange
            var repo = new Mock<IBikeRepository>();
            var map = SetUp.AddMapper();
            var ser = new BikeService(repo.Object, map);

            List<Bike> bikes = new List<Bike>();
            IQueryable<Bike> list = bikes.AsQueryable();

            var filter = new BikeFilterParameters()
            {
                SearchString = "abc",
                PrizeFrom = 100,
                PrizeTo = 500,
                Type = "xyz",
                Filter = 1
            };

            repo.Setup(x => x.GetAllActiveBikes()).Returns(list);

            //Act
            var result = ser.GetBikeList(filter);

            //Assert
            result.BikeFilterParameters.SearchString.Should().Be("abc");
            result.BikeFilterParameters.PrizeFrom.Should().Be(100);
            result.BikeFilterParameters.PrizeTo.Should().Be(500);
            result.BikeFilterParameters.Type.Should().Be("xyz");
            result.BikeFilterParameters.Filter.Should().Be(1);            
        }
    }
}
