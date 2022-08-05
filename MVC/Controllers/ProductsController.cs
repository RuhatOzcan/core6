﻿using AutoMapper;
using core6.DTOs;
using core6.Models;
using core6.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MVC.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IProductService _services;
        private readonly ICategoryService _categoryService;
        private readonly IMapper _mapper;

        public ProductsController(IProductService services, ICategoryService categoryService = null, IMapper mapper = null)
        {
            _services = services;
            _categoryService = categoryService;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {

            return View(await _services.GetProductsWithCategory());
        }

        public async Task<IActionResult> Save()
        {
            var categories = await _categoryService.GetAllAsync();
            var categoiresDto = _mapper.Map<List<CategoryDto>>(categories.ToList());
            ViewBag.categoires = new SelectList(categoiresDto, "Id", "Name");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Save(ProductDto productDto)
        {

            if (ModelState.IsValid)
            {
                await _services.AddAsync(_mapper.Map<Product>(productDto));
                return RedirectToAction(nameof(Index));
            }
            var categories = await _categoryService.GetAllAsync();
            var categoiresDto = _mapper.Map<List<CategoryDto>>(categories.ToList());
            ViewBag.categoires = new SelectList(categoiresDto, "Id", "Name");
            return View();

        }

        [ServiceFilter(typeof(NotFoundFilter<Product>))]
        public async Task<IActionResult> Update(int id)
        {
            var product = await _services.GetByIdAsync(id);
            var categories = await _categoryService.GetAllAsync();
            var categoiresDto = _mapper.Map<List<CategoryDto>>(categories.ToList());
            ViewBag.categoires = new SelectList(categoiresDto, "Id", "Name", product.CategoryId);
            return View(_mapper.Map<ProductDto>(product));
        }

        [HttpPost]
        public async Task<IActionResult> Update(ProductDto productDto)
        {
            if (ModelState.IsValid)
            {
                await _services.UpdateAsync(_mapper.Map<Product>(productDto));
                return RedirectToAction(nameof(Index));
            }
            var categories = await _categoryService.GetAllAsync();
            var categoiresDto = _mapper.Map<List<CategoryDto>>(categories.ToList());
            ViewBag.categoires = new SelectList(categoiresDto, "Id", "Name", productDto.CategoryId);

            return View(productDto); 

        }

        public async Task<IActionResult> Remove(int id)
        {
            var product = await _services.GetByIdAsync(id);
            await _services.RemoveAsync(product);
            return RedirectToAction(nameof(Index));
        }
    }
}
