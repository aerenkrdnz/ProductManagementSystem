using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProductManagementBusiness.Dtos.Product;

namespace ProductManagementBusiness.Interfaces.Managers
{
    public interface IProductManager
    {
        Task<ProductDto> CreateProductAsync(ProductCreateDto dto, string userId);        
        Task<ProductDto> GetProductByIdAsync(int id);
        Task<IEnumerable<ProductDto>> GetAllProductsAsync();       
        Task UpdateProductAsync(ProductUpdateDto dto, string userId);               
        Task DeleteProductAsync(int id, string userId);
    }
}
