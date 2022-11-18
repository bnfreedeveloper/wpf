using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfMvvm.Models
{
    public class EmployeeService
    {
        private static List<EmployeeDto> _employees = null!;
        public EmployeeService()
        {
            _employees = new List<EmployeeDto>()
            {
                new EmployeeDto()
                {
                    Id = 1,
                    Name =  "jean valjeant",
                    Age =  52
                }
            };
        }
        public List<EmployeeDto> GetAll()
        {
            return _employees;
        }
        public bool Add(EmployeeDto emp)
        {
            if(emp.Age <18) return false;
            _employees.Add(emp);    
            return true;
        }
        public bool Update(EmployeeDto emp)
        {
            var employee = _employees.FirstOrDefault(x => x.Id == emp.Id);
            if (employee == null) return false;
            employee.Name = emp.Name;   
            employee.Age = emp.Age;
            return true;
        }
        public bool Delete(EmployeeDto emp)
        {
            var index = _employees.FindIndex(x => x.Id == emp.Id);
            if(index == -1) return false;
            _employees.RemoveAt(index);
            return true;

            
        }
        public EmployeeDto? Search(int id)
        {
            return _employees.FirstOrDefault(x =>x.Id == id);   
        }
    }
}
