using System.Collections.Generic;

namespace Lab7.DAL.Repository.Interfaces
{
    /// Загальний інтерфейс репозиторію для роботи з сутностями.

    public interface IRepository<T>
        where T : class
    {
        /// Отримує всі сутності з репозиторію.
        IEnumerable<T> Get();

        /// Отримує сутність за її унікальним ідентифікатором.
        /// <returns>Сутність або null, якщо не знайдено.</returns>
        T GetById(int id);

        /// Створює нову сутність у репозиторії.
        void Create(T entity);

        /// Оновлює наявну сутність у репозиторії.
        void Update(T entity);

        /// Видаляє сутність із репозиторію за її ідентифікатором.
        void Delete(int id);
    }
}
