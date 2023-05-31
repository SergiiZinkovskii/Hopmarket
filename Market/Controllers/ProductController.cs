using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Azure.Core;
using Market.Domain.Extensions;
using Market.Domain.ViewModels.Product;
using Market.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
            if (response.StatusCode == Domain.Enum.StatusCode.OK)
            {
                return View(response.Data);
            }
            return View("Error", $"{response.Description}");
        }


        [HttpGet]
        public IActionResult GetCategory(string category)
        {
            var response = _productService.GetProducts();
            var list = response.Data;
            var newList = new List<Market.Domain.Entity.Product>();
            if (response.StatusCode == Domain.Enum.StatusCode.OK)
            {
                foreach (var item in list)
                {

                    if (item.TypeProduct.ToString() == category)
                    {
                        newList.Add(item);
                    }
                }
                return View(newList);
            }
            return View("Error", $"{response.Description}");
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
        public async Task<IActionResult> Save(int id)
        {
            if (id == 0)
                return PartialView();

            var response = await _productService.GetProduct(id);
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

            if (viewModel.Id == 0)
            {
                List<byte[]> imageDataList = new List<byte[]>();

                if (avatars != null && avatars.Length > 0)
                {
                    foreach (var avatar in avatars)
                    {
                        using (var binaryReader = new BinaryReader(avatar.OpenReadStream()))
                        {
                            byte[] imageData = binaryReader.ReadBytes((int)avatar.Length);
                            imageDataList.Add(imageData);
                        }
                    }
                }

                await _productService.Create(viewModel, imageDataList);
            }
            else
            {
                await _productService.Edit(viewModel, viewModel.Id);
            }

            return RedirectToAction("GetProducts");
        }



        [HttpGet]
        public async Task<IActionResult> GetProduct(int id)
        {
            var response = await _productService.GetProduct(id);
            if (response.StatusCode == Domain.Enum.StatusCode.OK)
            {
 
                return View("GetProduct", response.Data);
            }
            return View("Error", $"{response.Description}");
        }

        [HttpPost]
        public async Task<IActionResult> GetProduct(string term)
        {
            var response = await _productService.GetProduct(term);
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