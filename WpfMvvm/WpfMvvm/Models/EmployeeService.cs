using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfMvvm.Models
{
    public class EmployeeService
    {
        private static List<Employee> _employees = null!;
        public EmployeeService()
        {
            _employees = new List<Employee>()
            {
                new Employee()
                {
                    Id = 1,
                    Name =  "jean valjeant",
                    Age =  52
                }
            };
        }
        public List<Employee> GetAll()
        {
            return _employees;
        }
        public bool Add(Employee emp)
        {
            if(emp.Age <18) return false;
            _employees.Add(emp);    
            return true;
        }
        public bool Update(Employee emp)
        {
            var employee = _employees.FirstOrDefault(x => x.Id == emp.Id);
            if (employee == null) return false;
            employee.Name = emp.Name;   
            employee.Age = emp.Age;
            return true;
        }
        public bool Delete(Employee emp)
        {
            var index = _employees.FindIndex(x => x.Id == emp.Id);
            if(index == -1) return false;
            _employees.RemoveAt(index);
            return true;

            
        }
        public Employee? Search(int id)
        {
            return _employees.FirstOrDefault(x =>x.Id == id);   
        }
    }
}
