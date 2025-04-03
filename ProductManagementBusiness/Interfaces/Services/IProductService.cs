using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProductManagementBusiness.Dtos.Product;

namespace ProductManagementBusiness.Interfaces.Services
{
    public interface IProductService
    {
        Task<ProductDto> GetProductByIdAsync(int id);
        Task<IEnumerable<ProductDto>> GetAllProductsAsync();
        Task<ProductDto> CreateProductAsync(ProductCreateDto dto, string userId);
        Task UpdateProductAsync(ProductUpdateDto dto, string userId);
        Task DeleteProductAsync(int id, string userId);
    }
}
