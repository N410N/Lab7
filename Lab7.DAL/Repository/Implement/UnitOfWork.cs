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
        private IRepository<Admin>? _techRepository;
        private IRepository<Order>? _OrderRepository;

        public UnitOfWork(Lab7Context context)
        {
            this.context = context;
        }
        public IRepository<Admin> GetTechRepository()
        {
            if (_techRepository == null)
                _techRepository = new Repository<Admin>(context);
            return _techRepository;
        }
        public IRepository<Order> GetOrderRepository()
        {
            if (_OrderRepository == null)
                _OrderRepository = new Repository<Order>(context);
            return _OrderRepository;
        }

        public void SaveChanges()
        {
            context.SaveChanges();
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
