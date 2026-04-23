using FoodLab.Domain.Entites;
using FoodLab.Domain.Entites;
using FoodLab.Domain.Interfaces.Common;
using Nest;

namespace FoodLab.Domain.Interfaces.Orders;

public interface IOrderRepository : IRepository<Order>
{
    Task<IEnumerable<Order>> GetFullOrdersAsync();
}