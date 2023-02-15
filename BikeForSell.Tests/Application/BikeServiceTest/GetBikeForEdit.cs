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
    public class GetBikeForEdit
    {
        [Fact]
        public void ReturnBikeForEditVm()
        {
            //Arrange
            var repo = new Mock<IBikeRepository>();
            var map = SetUp.AddMapper();
            var ser = new BikeService(repo.Object, map);

            repo.Setup(x => x.GetBikeForEdit(It.IsAny<int>())).Returns(new Bike());

            //Act
            var result = ser.GetBikeForEdit(It.IsAny<int>());

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<BikeForEditVm>();
        }
    }
}
