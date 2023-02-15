using BikeForSell.Application.Services;
using BikeForSell.Domain.Interfaces;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BikeForSell.Tests.Application.BikeServiceTest
{
    public class DeleteBike
    {
        [Fact]
        public void InvokesBikeRepositoryDeleteBike()
        {
            //Arrange
            var repo = new Mock<IBikeRepository>();
            var map = SetUp.AddMapper();
            var ser = new BikeService(repo.Object, map);

            repo.Setup(x => x.DeleteBike(It.IsAny<int>()));

            //Act
            ser.DeleteBike(It.IsAny<int>());

            //Assert
            repo.Verify(x => x.DeleteBike(It.IsAny<int>()), Times.Once);
        }
    }
}
