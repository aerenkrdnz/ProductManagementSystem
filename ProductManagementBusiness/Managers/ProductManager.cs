
using ProductManagementBusiness.Dtos.Product;
using ProductManagementBusiness.Interfaces.Managers;
using ProductManagementBusiness.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProductManagementBusiness.Managers
{
    public class ProductManager : IProductManager
    {
        private readonly IProductService _productService;

        public ProductManager(IProductService productService)
        {
            _productService = productService;
        }

        public async Task<ProductDto> CreateProductAsync(ProductCreateDto dto, string userId)
        {
            // Business validations
            if (dto.Price <= 0)
                throw new ArgumentException("Price must be greater than zero");

            if (dto.Stock < 0)
                throw new ArgumentException("Stock cannot be negative");

            return await _productService.CreateProductAsync(dto, userId);
        }

        public async Task<ProductDto> GetProductByIdAsync(int id)
        {
            return await _productService.GetProductByIdAsync(id);
        }

        public async Task<IEnumerable<ProductDto>> GetAllProductsAsync()
        {
            return await _productService.GetAllProductsAsync();
        }

        public async Task UpdateProductAsync(ProductUpdateDto dto, string userId)
        {
            if (dto.Price <= 0)
                throw new ArgumentException("Price must be greater than zero");

            await _productService.UpdateProductAsync(dto, userId);
        }

        public async Task DeleteProductAsync(int id, string userId)
        {
            await _productService.DeleteProductAsync(id, userId);
        }
    }
}