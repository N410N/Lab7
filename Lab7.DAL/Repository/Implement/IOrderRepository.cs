using Lab7.DAL.EF.Entities;
using Lab7.DAL.Repository.Interfaces;

namespace Lab7.DAL.Repository.Implement;

public interface IOrderRepository : IRepository<Order>
{
    ICollection<Order> getOrdersInProgress(int techId);
}