using BikeForSell.Application.Services;
using BikeForSell.Application.ViewModels.BikeVm;
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
    public class EditBike
    {
        [Fact]
        public void InvokesBikeRepositoryEditBike()
        {
            //Arrange
            var repo = new Mock<IBikeRepository>();
            var map = SetUp.AddMapper();
            var ser = new BikeService(repo.Object, map);

            repo.Setup(x => x.EditBike(It.IsAny<Bike>()));

            //Act
            ser.EditBike(It.IsAny<BikeForEditVm>());

            //Assert
            repo.Verify(x => x.EditBike(It.IsAny<Bike>()), Times.Once);
        }
    }
}
