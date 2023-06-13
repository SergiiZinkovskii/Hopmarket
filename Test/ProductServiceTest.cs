using AutoFixture.Xunit2;
using FluentAssertions;
using Market.DAL.Interfaces;
using Market.Domain.Entity;
using Market.Domain.Enum;
using Market.Domain.Extensions;
using Market.Domain.Response;
using Market.Domain.ViewModels.Product;
using Market.Service.Implementations;
using Market.Services.Services;
using NSubstitute;
using Shouldly;
using Xunit;

namespace Market.Services.Tests
{
	public class ProductServiceTest
	{
		private readonly ProductService _productService;
		private readonly IProductRepository _productRepository;


		public ProductServiceTest()
		{
			_productRepository = Substitute.For<IProductRepository>();
			_productService = new ProductService(_productRepository);
		}

		[Fact]
		public void GetTypesTests()
		{
			// Arrange
			var types = ((TypeProduct[])Enum.GetValues(
					typeof(TypeProduct)))
				.ToDictionary(k => (int)k, t => t.GetDisplayName());

			// Act
			var result = _productService.GetTypes();

			// Assert
			result.Data.ShouldBeEquivalentTo(types);
		}


		[Theory]
		[AutoData]
		public async Task GetProduct_ReturnError(long id)
		{
			// Arrange
			var cts = new CancellationToken();
			_productRepository.Find(id, cts).Returns(new Product()
			{
				Id = id,
				Photos = new List<Photo>(){new()}
			});

			// Act
			var result = await _productService.GetProductAsync(id, cts);

			// Assert
			result.Id.Should().Be(id);
		}
	}
}