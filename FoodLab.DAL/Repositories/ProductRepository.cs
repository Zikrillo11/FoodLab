using FoodLab.DAL.Data;
using FoodLab.Domain.Entites;
using FoodLab.Domain.Entites;
using FoodLab.Domain.Interfaces.Products;
using FoodLab.Infrastructure.Repositories.Common;
using Microsoft.EntityFrameworkCore;
using Nest;

namespace FoodLab.Infrastructure.Repositories.Products;

public class ProductRepository
    : Repository<Product>, IProductRepository
{
    public ProductRepository(AppDbContext context)
        : base(context)
    {
    }

    public async Task<IEnumerable<Product>> GetByCategoryIdAsync(int categoryId)
    {
        return await _dbSet
            .Where(p => p.CategoryId == categoryId && !p.IsDeleted)
            .Include(p => p.Category)
            .ToListAsync();
    }
}