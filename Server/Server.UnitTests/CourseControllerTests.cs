using System;
using Xunit;
using Moq;
using Server.Api.Services;
using Server.Api.Entities;
using Server.Api.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace Server.UnitTests
{
    public class UnitTest1
    {
        [Fact]
        public async void Login_WithNonExistentUser_ReturnsNotFound()
        {
            var serviceStub = new Mock<ICourseService>();
            var mapperStub = 
            serviceStub.Setup(service => service.GetById(It.IsAny<int>()))
            .ReturnsAsync((Course)null);

            var controller = new CourseController(serviceStub.Object, );

            var result = await controller.GetCourse(1);
            Assert.IsType<NotFoundResult>(result.Result);
        }
    }
}
