using EmployeeManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.DataAccess.IRepository
{
    public interface IEmployeeRepository : IGenericRepository<Employee>
    {
    }
}
