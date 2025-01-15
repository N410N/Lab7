using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lab7.DAL.EF;
using Lab7.DAL.EF.Entities;
using Lab7.DAL.Repository.Interfaces;

namespace Lab7.DAL.Repository.Implement
{
    public class UnitOfWork : IUnitOfWork
    {
        public Lab7Context context;
        private IRepository<Admin>? _adminRepository;
        private IRepository<Order>? _orderRepository;

        public UnitOfWork(Lab7Context context)
        {
            this.context = context;
        }

        public IRepository<Admin> GetAdminRepository()
        {
            if (_adminRepository == null)
                _adminRepository = new Repository<Admin>(context);
            return _adminRepository;
        }

        public IRepository<Order> GetOrderRepository()
        {
            if (_orderRepository == null)
                _orderRepository = new Repository<Order>(context);
            return _orderRepository;
        }

        public void SaveChanges()
        {
            context.SaveChanges();
        }

        // Додаткові методи для бізнес-логіки

        /// <summary>
        /// Додає нового адміністратора.
        /// </summary>
        /// <param name="admin">Об'єкт Admin для додавання.</param>
        public void CreateAdmin(Admin admin)
        {
            GetAdminRepository().Create(admin);
            SaveChanges();
        }

        /// <summary>
        /// Знаходить адміністратора за його ID.
        /// </summary>
        /// <param name="id">ID адміністратора.</param>
        /// <returns>Адміністратор або null, якщо не знайдено.</returns>
        public Admin? GetAdminById(int id)
        {
            return GetAdminRepository().GetById(id);
        }

        /// <summary>
        /// Додає нове замовлення.
        /// </summary>
        /// <param name="order">Об'єкт Order для додавання.</param>
        public void CreateOrder(Order order)
        {
            GetOrderRepository().Create(order);
            SaveChanges();
        }

        /// <summary>
        /// Отримує всі замовлення.
        /// </summary>
        /// <returns>Колекція замовлень.</returns>
        public IEnumerable<Order> GetOrders()
        {
            return GetOrderRepository().Get();
        }

        /// <summary>
        /// Змінює статус замовлення.
        /// </summary>
        /// <param name="orderId">ID замовлення.</param>
        /// <param name="newStatus">Новий статус замовлення.</param>
        public void UpdateOrderStatus(int orderId, string newStatus)
        {
            var order = GetOrderRepository().GetById(orderId);
            if (order != null)
            {
                order.Status = newStatus;
                GetOrderRepository().Update(order);
                SaveChanges();
            }
        }
        public void DeleteOrder(int orderId)
        {
            var order = GetOrderRepository().GetById(orderId);
            if (order != null)
            {
                GetOrderRepository().Delete(orderId); // Передаємо ідентифікатор замовлення
                SaveChanges();
            }
        }


        private bool disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
