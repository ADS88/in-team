using Xunit;
using Moq;
using Server.Api.Services;
using Server.Api.Entities;
using Server.Api.Controllers;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;

namespace Server.UnitTests
{
    public class CourseControllerTests
    {
        private IMapper mapper;
        public CourseControllerTests(){
            var config = new MapperConfiguration(opts =>
            {
           
            });

            mapper = config.CreateMapper(); // Use this mapper to instantiate your class
        }


        [Fact]
        public async void GetCourse_WithNonExistentCourse_ReturnsNotFound()
        {
            var serviceStub = new Mock<ICourseService>();
            var mapperStub = 
            serviceStub.Setup(service => service.GetById(It.IsAny<int>()))
            .ReturnsAsync((Course)null);

            var controller = new CourseController(serviceStub.Object, mapper);

            var result = await controller.GetCourse(1);
            Assert.IsType<NotFoundResult>(result.Result);
        }
    }
}
