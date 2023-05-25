using Microsoft.AspNetCore.Mvc;
using Moq;
using rent.Controllers;
using rent.Models;
using rent.Repository;
using Xunit;
using Assert = Xunit.Assert;

namespace rent_UnitTest.Controllers
{
    public class CarControllerTests
    {
        private readonly Mock<IData> dataMock;
        private readonly CarController controller;

        public CarControllerTests()
        {
            dataMock = new Mock<IData>();
            controller = new CarController(dataMock.Object);
        }

        [Fact]
        public void Index_ReturnsViewResult()
        {
            // Arrange
            var cars = new List<Car> { new Car(), new Car() };
            dataMock.Setup(d => d.GetAllCars()).Returns(cars);

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
            var car = new Car();
            controller.ModelState.AddModelError("Brand", "Brand is required.");

            // Act
            var result = controller.Add(car);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.Equal(car, viewResult.Model);
        }

        [Fact]
        public void Add_POST_WithValidModel_ReturnsViewResultWithoutModel()
        {
            // Arrange
            var car = new Car();
            dataMock.Setup(d => d.AddNewCar(car)).Returns(true);

            // Act
            var result = controller.Add(car);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.Null(viewResult.Model);
        }
    }
}

