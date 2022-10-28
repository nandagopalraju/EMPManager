using ModelLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Interfaces
{
    public interface IEmployeeServices
    {
        List<Employee> Get();
        Employee Get(String username);
        void delete(String username);
        void create(Employee employee);
        void Edit(Employee employee);
    }
}
