using FoodLab.Domain.Entites;
using FoodLab.Domain.Entites;
using FoodLab.Domain.Interfaces.Common;
using Nest;

namespace FoodLab.Domain.Interfaces.Products;

public interface IProductRepository : IRepository<Product>
{
    Task<IEnumerable<Product>> GetByCategoryIdAsync(int categoryId);
}