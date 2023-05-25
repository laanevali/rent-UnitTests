using Microsoft.AspNetCore.Mvc;
using Moq;
using rent.Controllers;
using rent.Models;
using rent.Repository;
using Xunit;
using Assert = Xunit.Assert;

namespace rent_UnitTest.ControllerTests
{
    public class DriverControllerTests
    {
        private readonly Mock<IData> dataMock;
        private readonly DriverController controller;

        public DriverControllerTests()
        {
            dataMock = new Mock<IData>();
            controller = new DriverController(dataMock.Object);
        }

        [Fact]
        public void Index_ReturnsViewResult()
        {
            // Arrange
            var drivers = new List<Driver> { new Driver(), new Driver() };
            dataMock.Setup(d => d.GetAllDrivers()).Returns(drivers);

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
            var driver = new Driver();
            controller.ModelState.AddModelError("Name", "Name is required.");

            // Act
            var result = controller.Add(driver);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.Equal(driver, viewResult.Model);
        }

        [Fact]
        public void Add_POST_WithValidModel_ReturnsViewResultWithoutModel()
        {
            // Arrange
            var driver = new Driver();
            dataMock.Setup(d => d.AddDriver(driver)).Returns(true);

            // Act
            var result = controller.Add(driver);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.Null(viewResult.Model);
        }

        [Fact]
        public void History_ReturnsViewResult()
        {
            // Arrange
            var driverId = 1;
            var driverHistory = new DriverHistory();
            dataMock.Setup(d => d.GetDriverHistory(driverId)).Returns(driverHistory);

            // Act
            var result = controller.History(driverId);

            // Assert
            Assert.IsType<ViewResult>(result);
        }
    }
}


