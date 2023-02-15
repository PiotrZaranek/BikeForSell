using BikeForSell.Application.Services;
using BikeForSell.Domain.Interfaces;
using BikeForSell.Domain.Models;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BikeForSell.Tests.Application.BikeServiceTest
{
    public class BuyBike
    {
        [Fact]
        public void InvokesBikeRepositoryBuyBike()
        {
            //Arrange
            var repo = new Mock<IBikeRepository>();
            var map = SetUp.AddMapper();
            var ser = new BikeService(repo.Object, map);

            repo.Setup(x => x.BuyBike(It.IsAny<Transaction>(), It.IsAny<int>()));

            //Act
            ser.BuyBike(It.IsAny<int>(), It.IsAny<string>());

            //Assert
            repo.Verify(x => x.BuyBike(It.IsAny<Transaction>(), It.IsAny<int>()), Times.Once);
        }
    }
}
