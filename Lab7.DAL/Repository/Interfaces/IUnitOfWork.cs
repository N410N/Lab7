using Lab7.DAL.EF.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab7.DAL.Repository.Interfaces
{
    
    /// Інтерфейс для управління одиницею роботи (Unit of Work).
    public interface IUnitOfWork : IDisposable
    {
        /// Отримує репозиторій для роботи з адміністраторами.
        IRepository<Admin> GetAdminRepository();

        /// Отримує репозиторій для роботи із замовленнями.
        IRepository<Order> GetOrderRepository();

        /// Зберігає зміни в контексті бази даних.
        void SaveChanges();
    }
}
