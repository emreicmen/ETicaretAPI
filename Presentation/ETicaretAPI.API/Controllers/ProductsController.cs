﻿using ETicaretAPI.Application.Repositories;
using ETicaretAPI.Application.RequestParameters;
using ETicaretAPI.Application.ViewModels.Products;
using ETicaretAPI.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace ETicaretAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        readonly private IProductWriteRepository _productWriteRpository;
        readonly private IProductReadRepository _productReadRpository;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ProductsController(IProductWriteRepository productWriteRpository, IProductReadRepository productReadRpository,IWebHostEnvironment webHostEnvironment)
        {
            _productWriteRpository = productWriteRpository;
            _productReadRpository = productReadRpository;
            _webHostEnvironment = webHostEnvironment;
        }

        
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery]Pagination pagination)
        {
            //await Task.Delay(2000);
            var totalCount = _productReadRpository.GetAll(false).Count();
            var products = _productReadRpository.GetAll(false).Skip(pagination.Page * pagination.Size).Take(pagination.Size).Select(p => new
            {
                p.Id,
                p.Name,
                p.Price,
                p.Stock,
                p.CreatedDate,
                p.UpdatedDate

            }).ToList();

            return Ok(new
            {
                totalCount,
                products
            });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            return Ok(await _productReadRpository.GetByIdAsync(id, false));
        }

        [HttpPost]
        public async Task<IActionResult> Post(VM_Create_Product model)
        {
            if (ModelState.IsValid)
            {

            }
            await _productWriteRpository.AddAsync(new()
            {
                Name = model.Name,
                Price = model.Price,
                Stock = model.Stock
            });
            await _productWriteRpository.SaveAsync();
            return StatusCode((int)HttpStatusCode.Created);
        }

        [HttpPut]
        public async Task<IActionResult> Put(VM_Update_Product model)
        {
            Product product = await _productReadRpository.GetByIdAsync(model.Id);
            product.Stock = model.Stock;
            product.Name = model.Name;
            product.Price = model.Price;

            await _productWriteRpository.SaveAsync();

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            await _productWriteRpository.RemoveAsync(id);
            await _productWriteRpository.SaveAsync();
            return Ok(); 
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Upload()
        {
            string uploadPath = Path.Combine(_webHostEnvironment.WebRootPath,"resource/product-images");

            if (!Directory.Exists(uploadPath))
            {
                Directory.CreateDirectory(uploadPath);
            }
            Random r = new();
            foreach (IFormFile file in Request.Form.Files)
            {
                string fullPath = Path.Combine(uploadPath,$"{r.Next()}{Path.GetExtension(file.FileName)}");
                using FileStream fileStream = new(fullPath,FileMode.Create,FileAccess.Write,FileShare.None,1024*1024,useAsync:false);

                await file.CopyToAsync(fileStream);
                await fileStream.FlushAsync();
            }

            return Ok();
            
        }
    }
}
