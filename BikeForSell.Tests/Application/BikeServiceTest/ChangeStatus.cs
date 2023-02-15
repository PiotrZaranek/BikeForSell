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
    public class ChangeStatus
    {
        [Fact]
        public void InvokesBikeRepositoryChangeStatus()
        {
            //Arrange
            var repo = new Mock<IBikeRepository>();
            var map = SetUp.AddMapper();
            var ser = new BikeService(repo.Object, map);

            repo.Setup(x => x.ChangeStatus(It.IsAny<int>()));

            //Act
            ser.ChangeStatus(It.IsAny<int>());

            //Assert
            repo.Verify(x => x.ChangeStatus(It.IsAny<int>()), Times.Once);
        }
    }
}
