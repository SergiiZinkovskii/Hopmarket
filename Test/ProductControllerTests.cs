using Market.Controllers;
using Market.Domain.Entity;
using Market.Domain.Enum;
using Market.Domain.Extensions;
using Market.Domain.Response;
using Market.Domain.ViewModels.Product;
using Market.Service.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Tests
{
    public class ProductControllerTests
    {
        private readonly ProductController _productController;
        private readonly Mock<IProductService> _productServiceMock;

        public ProductControllerTests()
        {
            _productServiceMock = new Mock<IProductService>();
            _productController = new ProductController(_productServiceMock.Object);
        }

        [Fact]
        public void GetProducts_ReturnsViewWithProducts()
        {
            // Arrange
            var products = new List<Product>
            {
                new Product { Id = 1, Name = "Product 1", TypeProduct = TypeProduct.AppliancesForHome },
                new Product { Id = 2, Name = "Product 2", TypeProduct = TypeProduct.AppliancesForHome }
            };
            var response = new BaseResponse<List<Product>>()
            {
                StatusCode = StatusCode.OK,
                Data = products
            };
            _productServiceMock.Setup(s => s.GetProducts()).Returns(response);

            // Act
            var result = _productController.GetProducts();

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<List<Product>>(viewResult.ViewData.Model);
            Assert.Equal(products.Count, model.Count);
        }

        [Fact]
        public void GetProducts_ReturnsErrorView_WhenResponseStatusCodeIsNotOK()
        {
            // Arrange
            var response = new BaseResponse<List<Product>>()
            {
                StatusCode = StatusCode.ProductNotFound,
                Description = "Error retrieving products"
            };
            _productServiceMock.Setup(s => s.GetProducts()).Returns(response);

            // Act
            var result = _productController.GetProducts();

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.Equal("Error", viewResult.ViewName);
            var model = Assert.IsType<string>(viewResult.Model);
            Assert.Equal(response.Description, model);
        }

        [Fact]
        public void GetCategory_ReturnsViewWithFilteredProducts()
        {
            // Arrange
            var category = TypeProduct.AppliancesForHome.ToString();
            var products = new List<Product>
            {
                new Product { Id = 1, Name = "Product 1", TypeProduct = TypeProduct.AppliancesForHome },
                new Product { Id = 2, Name = "Product 2", TypeProduct = TypeProduct.AppliancesForHome },
                new Product { Id = 3, Name = "Product 3", TypeProduct = TypeProduct.AppliancesForHome }
            };
            var response = new BaseResponse<List<Product>>()
            {
                StatusCode = StatusCode.OK,
                Data = products
            };
            _productServiceMock.Setup(s => s.GetProducts()).Returns(response);

            // Act
            var result = _productController.GetCategory(category);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<List<Product>>(viewResult.ViewData.Model);
            Assert.Equal(2, model.Count);
            Assert.True(model.All(p => p.TypeProduct.ToString() == category));
        }

        [Fact]
        public void GetCategory_ReturnsErrorView_WhenResponseStatusCodeIsNotOK()
        {
            // Arrange
            var category = TypeProduct.AppliancesForHome.ToString();
            var response = new BaseResponse<List<Product>>()
            {
                StatusCode = StatusCode.ProductNotFound,
                Description = "Error retrieving products"
            };
            _productServiceMock.Setup(s => s.GetProducts()).Returns(response);

            // Act
            var result = _productController.GetCategory(category);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.Equal("Error", viewResult.ViewName);
            var model = Assert.IsType<string>(viewResult.Model);
            Assert.Equal(response.Description, model);
        }

        [Fact]
        public async Task Delete_ReturnsRedirectToAction_WhenDeleteIsSuccessful()
        {
            // Arrange
            var productId = 1;
            var response = new BaseResponse<bool>()
            {
                StatusCode = StatusCode.OK,
                Data = true
            };
            _productServiceMock.Setup(s => s.DeleteProduct(productId));

            // Act
            var result = await _productController.Delete(productId);

            // Assert
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("GetProducts", redirectToActionResult.ActionName);
        }

       

        [Fact]
        public void Compare_ReturnsPartialView()
        {
            // Act
            var result = _productController.Compare();

            // Assert
            var partialViewResult = Assert.IsType<PartialViewResult>(result);
            Assert.Null(partialViewResult.ViewName);
        }

        [Fact]
        public async Task Save_Get_ReturnsPartialView_WithModel()
        {
            // Arrange
            var productId = 1;
            var product = new ProductViewModel { Id = productId };
            var response = new BaseResponse<ProductViewModel>()
            {
                StatusCode = StatusCode.OK,
                Data = product
            };
            _productServiceMock.Setup(s => s.GetProduct(productId));

            // Act
            var result = await _productController.Save(productId);

            // Assert
            var partialViewResult = Assert.IsType<PartialViewResult>(result);
            Assert.Null(partialViewResult.ViewName);
            var model = Assert.IsType<ProductViewModel>(partialViewResult.Model);
            Assert.Equal(productId, model.Id);
        }

        [Fact]
        public async Task Save_Get_ReturnsPartialView_WithoutModel()
        {
            // Arrange
            var productId = 0;

            // Act
            var result = await _productController.Save(productId);

            // Assert
            var partialViewResult = Assert.IsType<PartialViewResult>(result);
            Assert.Null(partialViewResult.ViewName);
            Assert.Null(partialViewResult.Model);
        }

       
       

        [Fact]
        public async Task GetProduct_Get_ReturnsView_WithProduct()
        {
            // Arrange
            var productId = 1;
            var product = new ProductViewModel { Id = productId };
            var response = new BaseResponse<ProductViewModel>()
            {
                StatusCode = StatusCode.OK,
                Data = product
            };
            _productServiceMock.Setup(s => s.GetProduct(productId));

            // Act
            var result = await _productController.GetProduct(productId);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.Null(viewResult.ViewName);
            var model = Assert.IsType<ProductViewModel>(viewResult.Model);
            Assert.Equal(productId, model.Id);
        }

        [Fact]
        public async Task GetProduct_Get_ReturnsErrorView_WhenResponseStatusCodeIsNotOK()
        {
            // Arrange
            var productId = 1;
            var response = new BaseResponse<ProductViewModel>()
            {
                StatusCode = StatusCode.ProductNotFound,
                Description = "Error retrieving product"
            };
            _productServiceMock.Setup(s => s.GetProduct(productId));

            // Act
            var result = await _productController.GetProduct(productId);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.Equal("Error", viewResult.ViewName);
            var model = Assert.IsType<string>(viewResult.Model);
            Assert.Equal(response.Description, model);
        }

    }
}
