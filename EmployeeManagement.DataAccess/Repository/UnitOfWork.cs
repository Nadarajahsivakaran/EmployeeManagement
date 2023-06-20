using EmployeeManagement.DataAccess.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.DataAccess.Repository
{
    public class UnitOfWork : IunitOfWork
    {
        public IEmployeeRepository Employee { get; private set; }

        public UnitOfWork(ApplicationDbContext context)
        {
            Employee = new EmployeeRepository(context);
           
        }
    }
}
