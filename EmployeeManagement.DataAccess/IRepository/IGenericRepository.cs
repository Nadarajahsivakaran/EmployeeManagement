using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.DataAccess.IRepository
{
    public interface IGenericRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAll(string? includeProperties = "");

        Task Create(T entity);

        Task Update(T entity);

        Task Delete(T entity);

        Task<T> GetData(Expression<Func<T, bool>> predicate, string? includeProperties = "");

        Task Save();

        Task<bool> IsValueExit(Expression<Func<T, bool>> predicate);
    }
}
