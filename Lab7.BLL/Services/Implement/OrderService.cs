using Lab7.BLL.DTOs;
using Lab7.BLL.Services.Interfaces;
using Lab7.CCL;
using Lab7.CCL.Security.Identity;
using Lab7.DAL.EF.Entities;
using Lab7.DAL.Repository.Implement;
using Lab7.DAL.Repository.Interfaces;

namespace Lab7.BLL.Services.Implement;

public class OrderService : IOrderService
{
    private readonly IOrderRepository _OrderRepository;
    private readonly IUnitOfWork? _unitOfWork;

    public OrderService(IUnitOfWork? unitOfWork)
    {
        if (unitOfWork == null)
        {
            throw new ArgumentNullException("Юнiт оф ворк пустий");
        }
        _unitOfWork = unitOfWork;
        _OrderRepository = (IOrderRepository?)unitOfWork.GetOrderRepository();
    }

    public void FinishOrder(OrderDTO order)
    {
        var user = SecurityContext.GetUser();
        var userType = user.GetType();

        if (userType != typeof(AdmIdentity))
            throw new MethodAccessException("Доступ заблокований!");
        var result = _OrderRepository.GetById(order.Id);
        if (result == null)
            throw new ArgumentNullException("Не знайдено заявку");
        if (result.Status != "In progress")
            throw new ArgumentException("Заявка не в роботі!");

        result = new Order(order.Id, order.AdminId, order.CreatedDate, order.OrderDescription, order.Status, order.CustomerFeedback);

        _OrderRepository.Update(result);
    }

    public void FinishReport(OrderDTO report)
    {
        throw new NotImplementedException();
    }
}