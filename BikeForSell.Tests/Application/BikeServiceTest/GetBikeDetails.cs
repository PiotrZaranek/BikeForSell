﻿using AutoMapper;
using BikeForSell.Application.Mapping;
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
    public class GetBikeDetails
    {
        [Fact]
        public void ReturnBikeForDetailVmObject()
        {
            //Arrange
            var repo = new Mock<IBikeRepository>();
            var map = SetUp.AddMapper();

            repo.Setup(x => x.GetBikeForDetailsOrEdit(It.IsAny<int>())).Returns(new Bike());

            var ser = new BikeService(repo.Object, map);

            //Act
            var result = ser.GetBikeDetails(It.IsAny<int>());

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<BikeForDetailsVm>();
        }
    }
}
