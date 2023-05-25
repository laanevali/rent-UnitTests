using Microsoft.AspNetCore.Mvc;
using Moq;
using rent.Controllers;
using rent.Models;
using rent.Repository;
using Xunit;
using Assert = Xunit.Assert;

namespace rent_UnitTest.Controllers
{
    public class RentControllerTests
    {
        private readonly Mock<IData> dataMock;
        private readonly RentController controller;

        public RentControllerTests()
        {
            dataMock = new Mock<IData>();
            controller = new RentController(dataMock.Object);
        }

        [Fact]
        public void Index_ReturnsViewResult()
        {
            // Arrange
            var rents = new List<Rent> { new Rent(), new Rent() };
            dataMock.Setup(d => d.GetAllRents()).Returns(rents);

            // Act
            var result = controller.Index();

            // Assert
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void Add_GET_ReturnsViewResult()
        {
            // Act
            var result = controller.Add();

            // Assert
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void Add_POST_WithInvalidModel_ReturnsViewResultWithModel()
        {
            // Arrange
            var rent = new Rent();
            controller.ModelState.AddModelError("StartDate", "Start date is required.");

            // Act
            var result = controller.Add(rent);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.Equal(rent, viewResult.Model);
        }

        [Fact]
        public void Add_POST_WithValidModel_ReturnsViewResultWithoutModel()
        {
            // Arrange
            var rent = new Rent();
            dataMock.Setup(d => d.BookingNow(rent)).Returns(true);

            // Act
            var result = controller.Add(rent);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.Null(viewResult.Model);
        }

        [Fact]
        public void GetBrand_ReturnsJsonResult()
        {
            // Arrange
            var brands = new List<string> { "Brand 1", "Brand 2" };
            dataMock.Setup(d => d.GetBrand()).Returns(brands);

            // Act
            var result = controller.GetBrand();

            // Assert
            Assert.IsType<JsonResult>(result);
        }

        [Fact]
        public void GetModel_ReturnsJsonResult()
        {
            // Arrange
            var brand = "Brand";
            var models = new List<string> { "Model 1", "Model 2" };
            dataMock.Setup(d => d.GetModel(brand)).Returns(models);

            // Act
            var result = controller.GetModel(brand);

            // Assert
            Assert.IsType<JsonResult>(result);
        }

        [Fact]
        public void GetDriver_ReturnsJsonResult()
        {
            // Arrange
            var drivers = new List<Driver> { new Driver(), new Driver() };
            dataMock.Setup(d => d.GetAllDrivers()).Returns(drivers);

            // Act
            var result = controller.GetDriver();

            // Assert
            Assert.IsType<JsonResult>(result);
        }
    }
}
