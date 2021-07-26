using System;
using Xunit;
using Moq;
using Server.Api.Repositories;
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
            var repositoryStub = new Mock<ICoursesRepository>();
            repositoryStub.Setup(repo => repo.Get(It.IsAny<int>()))
            .ReturnsAsync((Course)null);

            var controller = new CourseController(repositoryStub.Object);

            var result = await controller.GetCourse(1);
            Assert.IsType<NotFoundResult>(result.Result);
        }
    }
}
