using FoodLab.DAL.Data;
using FoodLab.Domain.Entites;
using FoodLab.Domain.Entites;
using FoodLab.Domain.Interfaces.Categories;
using FoodLab.Infrastructure.Repositories.Common;
using Nest;
using NuGet.Protocol.Core.Types;

namespace FoodLab.Infrastructure.Repositories.Categories;

public class CategoryRepository
    : Repository<Category>, ICategoryRepository
{
    public CategoryRepository(AppDbContext context)
        : base(context)
    {
    }
}