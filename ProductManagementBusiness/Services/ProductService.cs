using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using ProductManagementBusiness.Dtos.Product;
using ProductManagementBusiness.Interfaces.Services;
using ProductManagementData.Entities;
using ProductManagementData.Repositories.Interfaces;


namespace ProductManagementBusiness.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public ProductService(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ProductDto>> GetAllProductsAsync()
        {
            var products = await _productRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<ProductDto>>(products);
        }

        public async Task<ProductDto> GetProductByIdAsync(int id)
        {
            var product = await _productRepository.GetByIdAsync(id);
            return _mapper.Map<ProductDto>(product);
        }

        public async Task<ProductDto> CreateProductAsync(ProductCreateDto dto, string userId)
        {
            var product = _mapper.Map<Product>(dto);
            product.CreatedBy = userId;
            await _productRepository.AddAsync(product);
            return _mapper.Map<ProductDto>(product);
        }

        public async Task UpdateProductAsync(ProductUpdateDto dto, string userId)
        {
            var product = await _productRepository.GetByIdAsync(dto.Id);
            if (product == null || product.CreatedBy != userId)
                throw new UnauthorizedAccessException("Unauthorized");

            _mapper.Map(dto, product);
            await _productRepository.UpdateAsync(product);
        }

        public async Task DeleteProductAsync(int id, string userId)
        {
            var product = await _productRepository.GetByIdAsync(id);
            if (product == null || product.CreatedBy != userId)
                throw new UnauthorizedAccessException("Unauthorized");

            await _productRepository.DeleteAsync(id);
        }
    }
}