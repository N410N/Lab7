using Lab7.DAL.EF;
using Lab7.DAL.EF.Entities;
using Lab7.DAL.Repository.Implement;

namespace Lab7.DAL.Repository.Interfaces;

public class OrderRepository : Repository<Order>, IOrderRepository
{
    private readonly Lab7Context context;

    public OrderRepository(Lab7Context context) : base(context)
    {
        this.context = context;
    }

    public ICollection<Order> getOrdersInProgress(int adminId)
    {
        return context.Set<Order>().Where(r => r.AdminId == adminId && r.Status == "In progress").ToList();
    }
}