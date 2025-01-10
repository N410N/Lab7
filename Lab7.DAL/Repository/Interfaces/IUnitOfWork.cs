using Lab7.DAL.EF.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab7.DAL.Repository.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<Admin> GetTechRepository();
        IRepository<Order> GetOrderRepository();
        void SaveChanges();
    }
}
