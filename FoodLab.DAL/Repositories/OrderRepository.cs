using FoodLab.DAL.Data;
using FoodLab.Domain.Entites;
using FoodLab.Domain.Entites;
using FoodLab.Domain.Interfaces.Orders;
using FoodLab.Infrastructure.Repositories.Common;
using Microsoft.EntityFrameworkCore;
using Nest;
using Octokit;

namespace FoodLab.Infrastructure.Repositories.Orders;

public class OrderRepository
    : Repository<Order>, IOrderRepository
{
    public OrderRepository(AppDbContext context)
        : base(context)
    {
    }

    public async Task<IEnumerable<Order>> GetFullOrdersAsync()
    {
        return await _dbSet
            .Include(o => o.Items)
                .ThenInclude(i => i.Product)
            .OrderByDescending(o => o.CreatedAt)
            .ToListAsync();
    }
}