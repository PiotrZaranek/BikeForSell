using BikeForSell.Application.Services;
using BikeForSell.Application.ViewModels.BikeVm;
using BikeForSell.Domain.Interfaces;
using BikeForSell.Domain.Models;
using FluentAssertions;
using Moq;

namespace BikeForSell.Tests.Application.BikeServiceTest
{
    public class BikeServiceGetBikeList
    {
        [Fact]
        public void ReturnListBikeForListVmObject()
        {
            //Arrange
            var repo = new Mock<IBikeRepository>();
            var map = SetUp.AddMapper();

            List<Bike> bikes = new List<Bike>();
            IQueryable<Bike> bikeAsQueryalbe = bikes.AsQueryable();
            repo.Setup(x => x.GetAllActiveBikes()).Returns(bikeAsQueryalbe);            

            var ser = new BikeService(repo.Object, map);

            //Act
            var result = ser.GetBikeList();

            //Assert
            result.Bikes.Should().NotBeNull();
            result.Bikes.Should().BeOfType<List<BikeForListVm>>();
        }
    }
}
