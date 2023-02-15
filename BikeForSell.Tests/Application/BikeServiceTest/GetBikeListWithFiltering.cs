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

            repo.Setup(x => x.GetAllActiveBikes()).Returns(list);

            //Act
            var result = ser.GetBikeList("abc", 100, 500, "xyz", 1);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<ListBikeForListVm>();
            result.Bikes.Should().BeOfType<List<BikeForListVm>>();

            result.SearchString.Should().Be("abc");
            result.PrizeFrom.Should().Be(100);
            result.PrizeTo.Should().Be(500);
            result.Type.Should().Be("xyz");
            result.Filter.Should().Be(1);
        }
    }
}
