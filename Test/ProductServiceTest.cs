using AutoFixture;
using AutoFixture.Xunit2;
using FluentAssertions;
using Market.DAL.Interfaces;
using Market.DAL.Repositories;
using Market.Domain.Entity;
using Market.Domain.Enum;
using Market.Domain.Extensions;
using Market.Domain.ViewModels.Product;
using Market.Services.Services;
using Microsoft.EntityFrameworkCore;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using Shouldly;
using Xunit;


namespace Market.Services.Tests
{
	public class ProductServiceTest
	{
		private readonly Fixture _fixture;
		private readonly ProductService _productService;
		private readonly IProductRepository _productRepository;
		private readonly ICommentRepository _commentRepository;


		public ProductServiceTest()
		{
			_fixture = new Fixture();

			_fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList()
				.ForEach(b => _fixture.Behaviors.Remove(b));
			_fixture.Behaviors.Add(new OmitOnRecursionBehavior());

			_productRepository = Substitute.For<IProductRepository>();
			_commentRepository = Substitute.For<ICommentRepository>();
			_productService = new ProductService(_productRepository, _commentRepository);
		}

		[Fact]
		public void GetTypesTests()
		{
			// Arrange
			var types = ((TypeProduct[])Enum.GetValues(typeof(TypeProduct)))
				.ToDictionary(k => (int)k, t => t.GetDisplayName());

			// Act
			var result = _productService.GetTypes();

			// Assert
			result.Data.ShouldBeEquivalentTo(types);
		}

		[Theory]
		[AutoData]
		public async Task FindProductById(long id)
		{
			// Arrange
			var cts = new CancellationToken();
			_productRepository.Find(id, cts).Returns(new Product()
			{
				Id = id,
				Photos = new List<Photo>() { new() }
			});

			// Act
			var result = await _productService.GetProductAsync(id, cts);

			// Assert
			result.Id.Should().Be(id);
		}

		[Theory]
		[AutoData]
		public async Task GetProductAsync_WithValidId_ReturnsProductViewModel(
			long id, CancellationToken cancellationToken)
		{
			// Arrange
			var product = _fixture.Build<Product>()
				.With(p => p.Id, id)
				.With(p => p.Photos, new[] { _fixture.Create<Photo>() })
				.Create();
			_productRepository.Find(id, cancellationToken).Returns(product);

			// Act
			var result = await _productService.GetProductAsync(id, cancellationToken);

			// Assert
			result.Should().NotBeNull();
			result.Id.Should().Be(id);
			result.Photos.Should().NotBeNull();
			result.Photos.Should().HaveCount(1);
		}


		[Theory]
		[AutoData]
		public async Task GetProductAsync_WithInvalidId_ReturnsNull(
			long id, CancellationToken cancellationToken)
		{
			// Arrange
			_productRepository.Find(id, cancellationToken).Returns((Product)null);

			// Act
			var result = await _productService.GetProductAsync(id, cancellationToken);

			// Assert
			result.Should().BeNull();
		}


		[Theory]
		[AutoData]
		public async Task GetProductAsync_WithException_ReturnsBaseResponseWithInternalServerError(
			string searchTerm, Exception exception)
		{
			// Arrange
			_productRepository.GetAll().Throws(exception);

			// Act
			var result = await _productService.GetProductAsync(searchTerm);

			// Assert
			result.StatusCode.Should().Be(StatusCode.InternalServerError);
			result.Description.Should().Contain(exception.Message);
		}

		[Theory]
		[AutoData]
		public async Task Create_ValidProduct_ReturnsBaseResponseWithCreatedProduct(
			ProductViewModel model, List<byte[]> imageDataList, TypeProduct productType)
		{
			// Arrange
			model.TypeProduct = productType.ToString();

			// Act
			var result = await _productService.Create(model, imageDataList);

			// Assert
			result.StatusCode.Should().Be(StatusCode.OK);
			result.Data.Should().NotBeNull();
		}


		[Theory]
		[AutoData]
		public async Task Create_WithException_ReturnsBaseResponseWithInternalServerError(
			ProductViewModel model, 
			List<byte[]> imageDataList, 
			Exception exception, 
			TypeProduct productType)
		{
			// Arrange
			model.TypeProduct = productType.ToString();
			_productRepository.Create(Arg.Any<Product>()).ThrowsAsync(exception);

			// Act
			var result = await _productService.Create(model, imageDataList);

			// Assert
			result.StatusCode.Should().Be(StatusCode.InternalServerError);
			result.Description.Should().Contain(exception.Message);
		}

		[Theory]
		[AutoData]
		public void GetProducts_WithExistingProducts_ReturnsBaseResponseWithProductsList()
		{
			// Arrange
			var products = new List<Product> { new() };

			_productRepository.GetAll().Include(p => p.Photos)
				.Returns(products.AsQueryable());

			// Act
			var result = _productService.GetProducts();

			// Assert
			result.StatusCode.Should().Be(StatusCode.OK);
			result.Data.Should().NotBeNull();
		}

		[Fact]
		public void GetProducts_WithNoProducts_ReturnsBaseResponseWithZeroElements()
		{
			// Arrange
			var products = new List<Product>();

			_productRepository.GetAll().Include(p => p.Photos).Returns(products.AsQueryable());

			// Act
			var result = _productService.GetProducts();

			// Assert
			result.StatusCode.Should().Be(StatusCode.OK);
			result.Description.Should().Contain("0 елементів");
		}

		[Theory]
		[AutoData]
		public void GetProducts_WithException_ReturnsBaseResponseWithInternalServerError(
			Exception exception)
		{
			// Arrange
			_productRepository.GetAll().Include(p => p.Photos).Throws(exception);

			// Act
			var result = _productService.GetProducts();

			// Assert
			result.StatusCode.Should().Be(StatusCode.InternalServerError);
			result.Description.Should().Contain(exception.Message);
		}
	}
}