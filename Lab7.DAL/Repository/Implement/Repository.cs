using Lab7.DAL.EF;
using Lab7.DAL.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using static Lab7.DAL.EF.Lab7Context;

namespace Lab7.DAL.Repository.Implement;

public class Repository<T> : IRepository<T> where T : class
{
    private readonly Lab7Context context;
    public Repository(Lab7Context context)
    {
        this.context = context;
    }

    public IEnumerable<T> Get()
    {
        IQueryable<T> list = context.Set<T>();
        return list;
    }
    public void Create(T entity, string createBody = null)
    {
        context.Set<T>().Add(entity);
    }
    public T GetById(int id)
    {
        return context.Set<T>().Find(id);
    }
    public void Update(T entity, string updateBody = null)
    {
        context.Set<T>().Attach(entity);
        context.Entry(entity).State = EntityState.Modified;
    }
    public void Delete(int id)
    {
        T entity = context.Set<T>().Find(id);
        context.Remove(entity);
    }
}