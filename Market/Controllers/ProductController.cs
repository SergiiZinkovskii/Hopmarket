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
        public async Task<IActionResult> Save(ProductViewModel viewModel)
        {
            ModelState.Remove("Id");
            ModelState.Remove("DateCreate");
            //if (ModelState.IsValid)
            //{
            if (viewModel.Id == 0)
            {


               byte[] imageData = new byte[0];
               if (viewModel.Avatar != null)
                {
                    using (var binaryReader = new BinaryReader(viewModel.Avatar.OpenReadStream()))
                    {
                        imageData = binaryReader.ReadBytes((int)viewModel.Avatar.Length);
                    }
                }
                byte[] imageData2 = new byte[0];
                if (viewModel.Avatar2 != null)
                {
                    using (var binaryReader2 = new BinaryReader(viewModel.Avatar2.OpenReadStream()))
                    {
                        imageData2 = binaryReader2.ReadBytes((int)viewModel.Avatar2.Length);
                    }
                }
                byte[] imageData3 = new byte[0]; ;
                if (viewModel.Avatar3 != null)
                {
                    using (var binaryReader3 = new BinaryReader(viewModel.Avatar3.OpenReadStream()))
                    {
                        imageData3 = binaryReader3.ReadBytes((int)viewModel.Avatar3.Length);
                    }
                }
                byte[] imageData4 = new byte[0]; ;
                if (viewModel.Avatar4 != null)
                {
                    using (var binaryReader4 = new BinaryReader(viewModel.Avatar4.OpenReadStream()))
                    {
                        imageData4 = binaryReader4.ReadBytes((int)viewModel.Avatar4.Length);
                    }
                }
                byte[] imageData5 = new byte[0]; 
                if (viewModel.Avatar5 != null)
                {
                    using (var binaryReader5 = new BinaryReader(viewModel.Avatar5.OpenReadStream()))
                    {
                        imageData5 = binaryReader5.ReadBytes((int)viewModel.Avatar5.Length);
                    }
                }
                await _productService.Create(viewModel, imageData, imageData2, imageData3, imageData4, imageData5);
            }
            else
            {
                await _productService.Edit(viewModel, viewModel.Id);
            }
            //}
            return RedirectToAction("GetProducts");
        }


        [HttpGet]
        public async Task<IActionResult> GetProduct(int id, bool isJson)
        {
            var response = await _productService.GetProduct(id);
            
            if (isJson)
            {
                return Json(response.Data);
            }
            return View("GetProduct", response.Data);
        }

        [HttpPost]
        public async Task<IActionResult> GetProduct(string term)
        {
            var response = await _productService.GetProduct(term);
            return Json(response.Data);
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

            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Edit(long id)
        
        {
            if (id == 0)
                return View();

            var response = await _productService.GetProduct(id);
            
            if (response.StatusCode == Domain.Enum.StatusCode.OK)
            {
                return View(response.Data);
            }
            ModelState.AddModelError("", response.Description);
            return View();
        }
    }
}