using Market.DAL.Interfaces;
using Market.Domain.Entity;
using Market.Domain.Enum;
using Market.Domain.ViewModels.Product;
using Market.Service.Implementations;
using Moq;


namespace Tests
{
    public class ProductServiceTest
    {
        [Fact]
        public void GetProductsTest()
        {
            // Arrange
            var productRepositoryMock = new Mock<IBaseRepository<Product>>();
            var productService = new ProductService(productRepositoryMock.Object);
            var expectedProduct = new Product
            {
                Id = 1,
                Name = "Test Product",
                TypeProduct = TypeProduct.Souvenirs,
                Price = 10000,
                Description = "Test description"
               
            };
            productRepositoryMock.Setup(r => r.GetAll())
                .Returns(new List<Product> { expectedProduct }.AsQueryable());

            // Act
            var result = productService.GetProducts();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(StatusCode.OK, result.StatusCode);
            Assert.NotNull(result.Data);
            Assert.Single(result.Data);
            Assert.Equal(expectedProduct, result.Data.First());
        }


        [Fact]
        public async Task GetProductTest()
        {
            // Arrange
            var productRepositoryMock = new Mock<IBaseRepository<Product>>();
            var productService = new ProductService(productRepositoryMock.Object);
        var expectedProduct = new Product
        {
            Id = 1,
            Name = "Test Product",
            TypeProduct = TypeProduct.Souvenirs,
            Price = 10000,
            Description = "Test description"
        };
        var products = new List<Product> { expectedProduct }.AsQueryable();
        var productsTask = Task.FromResult(products);
            productRepositoryMock.Setup(r => r.GetAll())
                   .Returns(new List<Product> { expectedProduct }.AsQueryable());

            // Act
            var result = await productService.GetProduct(1);

        // Assert
        Assert.NotNull(result);
            Assert.Equal(StatusCode.OK, result.StatusCode);
            Assert.NotNull(result.Data);
            Assert.Equal(expectedProduct.Id, result.Data.Id);
            Assert.Equal(expectedProduct.Name, result.Data.Name);
        }



    [Fact]
        public async Task Create_Should_Return_SuccessfulResponse_With_CreatedProduct()
        {
            // Arrange
            var productViewModel = new ProductViewModel
            {
                Name = "Test Product",
                ProdModel = "Test Model",
                Description = "Test Description",
                Power = 100,
                TypeProduct = "1",
                Price = 10000
            };
            var imageDataList = new List<byte[]> { new byte[] { 1, 2, 3 } };

            var productRepositoryMock = new Mock<IBaseRepository<Product>>();
            var productService = new ProductService(productRepositoryMock.Object);

            // Act
            var result = await productService.Create(productViewModel, imageDataList);

            // Assert
            Assert.Equal(StatusCode.OK, result.StatusCode);
            Assert.NotNull(result.Data);
            Assert.Equal(productViewModel.Name, result.Data.Name);
            Assert.Equal(productViewModel.ProdModel, result.Data.Model);
            Assert.Equal(productViewModel.Description, result.Data.Description);
            Assert.Equal(productViewModel.Power, result.Data.Power);
            Assert.Equal((TypeProduct)1, result.Data.TypeProduct);
            Assert.Equal(productViewModel.Price, result.Data.Price);
            Assert.Single(result.Data.Photos);
            Assert.Equal(imageDataList.First(), result.Data.Photos.First().ImageData);
        }

        [Fact]
        public async Task DeleteProduct_Should_Return_SuccessfulResponse_With_True_If_Product_Exists()
        {
            // Arrange
            var productRepositoryMock = new Mock<IBaseRepository<Product>>();
            var productService = new ProductService(productRepositoryMock.Object);
            var expectedProduct = new Product
            {
                Id = 1,
                Name = "Test Product",
                TypeProduct = TypeProduct.Souvenirs,
                Price = 10000,
                Description = "Test description"

            };
            productRepositoryMock.Setup(r => r.GetAll())
                .Returns(new List<Product> { expectedProduct }.AsQueryable());

            // Act
            var result = await productService.DeleteProduct(expectedProduct.Id);

            // Assert
            Assert.Equal(StatusCode.OK, result.StatusCode);
            Assert.True(result.Data);
        }





        [Fact]
        public async Task DeleteProduct_Should_Return_UserNotFound_Response_With_False_If_Product_Does_Not_Exist()
        {
            // Arrange
            var productId = 1;
            var productRepositoryMock = new Mock<IBaseRepository<Product>>();
            productRepositoryMock.Setup(r => r.GetAll()).Returns(new List<Product>().AsQueryable());
            var productService = new ProductService(productRepositoryMock.Object);

            // Act
            var result = await productService.DeleteProduct(productId);

            // Assert
            Assert.Equal(StatusCode.UserNotFound, result.StatusCode);
            Assert.False(result.Data);
        }

        [Fact]
        public async Task Edit_Should_Return_SuccessfulResponse_With_UpdatedProduct_If_Product_Exists()
        {
            // Arrange
            var productId = 1;
            var productViewModel = new ProductViewModel
            {
                Description = "Updated Description",
                ProdModel = "Updated Model",
                Price = 20000,
                Power = 200,
                Name = "Updated Product"
            };

            var existingProduct = new Product { Id = productId };
            var productRepositoryMock = new Mock<IBaseRepository<Product>>();
            productRepositoryMock.Setup(r => r.GetAll()).Returns(new List<Product> { existingProduct }.AsQueryable());
            var productService = new ProductService(productRepositoryMock.Object);

            // Act
            var result = await productService.Edit(productViewModel, productId);

            // Assert
            Assert.Equal(StatusCode.OK, result.StatusCode);
            Assert.Equal(productId, result.Data.Id);
            Assert.Equal(productViewModel.Description, result.Data.Description);
            Assert.Equal(productViewModel.ProdModel, result.Data.Model);
            Assert.Equal(productViewModel.Price, result.Data.Price);
            Assert.Equal(productViewModel.Power, result.Data.Power);
            Assert.Equal(productViewModel.Name, result.Data.Name);
        }

        [Fact]
        public async Task Edit_Should_Return_ProductNotFound_Response_If_Product_Does_Not_Exist()
        {
            // Arrange
            var productId = 1;
            var productViewModel = new ProductViewModel
            {
                Description = "Updated Description",
                ProdModel = "Updated Model",
                Price = 20000,
                Power = 200,
                Name = "Updated Product"
            };

            var productRepositoryMock = new Mock<IBaseRepository<Product>>();
            productRepositoryMock.Setup(r => r.GetAll()).Returns(new List<Product>().AsQueryable());
            var productService = new ProductService(productRepositoryMock.Object);

            // Act
            var result = await productService.Edit(productViewModel, productId);

            // Assert
            Assert.Equal(StatusCode.ProductNotFound, result.StatusCode);
            Assert.Null(result.Data);
        }





    }
}
