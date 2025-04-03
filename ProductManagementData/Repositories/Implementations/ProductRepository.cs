using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProductManagementData.Context;
using ProductManagementData.Entities;
using ProductManagementData.Repositories.Interfaces;

namespace ProductManagementData.Repositories.Implementations
{
    public class ProductRepository : IProductRepository
    {
        private readonly AppDbContext _context;

        public ProductRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Product> GetByIdAsync(int id)
        {
            return await _context.Products
                .IgnoreQueryFilters() 
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            return await _context.Products
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task AddAsync(Product product)
        {
            product.IsDeleted = false;
            
            product.UpdatedDate = DateTime.UtcNow;

            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Product product)
        {
            product.UpdatedDate = DateTime.UtcNow;
            _context.Products.Update(product);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var product = await GetByIdAsync(id);
            if (product != null)
            {
                product.IsDeleted = true;
                product.UpdatedDate = DateTime.UtcNow;
                await UpdateAsync(product); 
            }
        }

        public async Task<IEnumerable<Product>> GetByUserAsync(string userId)
        {
            return await _context.Products
                .Where(p => p.CreatedBy == userId && !p.IsDeleted)
                .AsNoTracking()
                .ToListAsync();
        }

       
        public async Task<IEnumerable<Product>> GetAllWithDeletedAsync()
        {
            return await _context.Products
                .IgnoreQueryFilters()
                .AsNoTracking()
                .ToListAsync();
        }       
    }
}