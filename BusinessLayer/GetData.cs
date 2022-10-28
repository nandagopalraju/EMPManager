using BusinessLayer.Interfaces;
using DataLayer;
using ModelLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class GetData : IEmployeeServices
    {
        private readonly DataContext _employeeContext;
        public GetData(DataContext employeeContext)
        {
            _employeeContext = employeeContext;
        }
        public Employee Get(string username)
        {
            return _employeeContext.Employees.FirstOrDefault(i => i.username == username);
        }

        public List<Employee> Get()
        {
            return _employeeContext.Employees.ToList();
        }
        public void delete(string username)
        {
            Employee emp = _employeeContext.Employees.FirstOrDefault(i => i.username == username);
            if (emp != null)
            {
                _employeeContext.Remove(emp);
                _employeeContext.SaveChanges();
            }
        }
        public void create(Employee employee)
        {
            _employeeContext.Employees.Add(employee);
            _employeeContext.SaveChanges();
        }
        public void Edit(Employee employee)
        {
            Employee emp = _employeeContext.Employees.FirstOrDefault(i => i.username == employee.username);
            if (emp != null)
            {
                _employeeContext.Remove(emp);
                _employeeContext.Employees.Add(employee);
                _employeeContext.SaveChanges();
            }

        }

    }
}
