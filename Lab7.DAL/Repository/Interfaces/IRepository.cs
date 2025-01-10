using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab7.DAL.Repository.Interfaces
{
    public interface IRepository<T>
        where T : class
    {
        public IEnumerable<T> Get();
        T GetById(int id);
        void Create(T entity, string createBody = null);
        void Update(T entity, string modifieBody = null);
        void Delete(int id);
    }
}
