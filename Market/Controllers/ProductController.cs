using Market.Domain.Response;
using Market.Domain.ViewModels.Product;
using Market.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Market.Controllers
{
	public class ProductController : Controller
	{
		private readonly IProductService _productService;

		public ProductController(IProductService productService)
		{
			_productService = productService;
		}

		[HttpGet]
		public IActionResult GetProducts()
		{
			var response = _productService.GetProducts();
			return response.StatusCode == Domain.Enum.StatusCode.OK 
				? View(response.Data) 
				: View("Error", $"{response.Description}");
		}


		[HttpGet]
		public IActionResult GetCategory(string category)
		{
			var response = _productService.GetProducts();
			var list = response.Data;
			if (response.StatusCode != Domain.Enum.StatusCode.OK) return View("Error", $"{response.Description}");
			var newList = list.Where(item => item.TypeProduct.ToString() == category).ToList();
			return View(newList);
		}

		[Authorize(Roles = "Admin")]
		public async Task<IActionResult> Delete(int id)
		{
			var response = await _productService.DeleteProduct(id);
			if (response.StatusCode == Domain.Enum.StatusCode.OK)
			{
				return RedirectToAction("GetProducts");
			}

			return View("Error", $"{response.Description}");
		}

		public IActionResult Compare() => PartialView();


		[HttpGet]
		public async Task<IActionResult> Save(int id, CancellationToken cancellationToken)
		{
			if (id == 0)
				return PartialView();

			var data = await _productService.GetProductAsync(id, cancellationToken);

			var response = new BaseResponse<ProductViewModel>();

			if (data == null)
			{
				response.Description = "Товар не знайдено";
				response.StatusCode = Domain.Enum.StatusCode.UserNotFound;
			}
			else
			{
				response = new BaseResponse<ProductViewModel>()
				{
					StatusCode = Domain.Enum.StatusCode.OK,
					Data = data
				};
			}
			
			if (response.StatusCode == Domain.Enum.StatusCode.OK)
			{
				return PartialView(response.Data);
			}

			ModelState.AddModelError("", response.Description);
			return PartialView();
		}

		[HttpPost]
		public async Task<IActionResult> Save(ProductViewModel viewModel, IFormFile[] avatars)
		{
			ModelState.Remove("Id");
			ModelState.Remove("DateCreate");

			var imageDataList = new List<byte[]>();

			if (avatars is { Length: > 0 })
			{
				foreach (var avatar in avatars)
				{
					using var binaryReader = new BinaryReader(avatar.OpenReadStream());
					var imageData = binaryReader.ReadBytes((int)avatar.Length);
					imageDataList.Add(imageData);
				}
			}

			await _productService.Create(viewModel, imageDataList);

			return RedirectToAction("GetProducts");
		}


		[HttpGet]
		public async Task<IActionResult> GetProduct(int id, CancellationToken cancellationToken)
		{
			var data = await _productService.GetProductAsync(id, cancellationToken);
			return data != null 
				? View("GetProduct", data) 
				: View("Error", $"{"Товар не знайдено"}");
		}

		[HttpPost]
		public async Task<IActionResult> GetProduct(string term)
		{
			var response = await _productService.GetProductAsync(term);
			if (response.StatusCode == Domain.Enum.StatusCode.OK)
			{
				return Json(response.Data);
			}

			return View("Error", $"{response.Description}");
		}

		[HttpPost]
		public JsonResult GetTypes()
		{
			var types = _productService.GetTypes();
			return Json(types.Data);
		}


		[HttpPost]
		public async Task<IActionResult> Edit(ProductViewModel viewModel)
		{
			await _productService.Edit(viewModel, viewModel.Id);

			return RedirectToAction("GetProducts");
		}
	}
}