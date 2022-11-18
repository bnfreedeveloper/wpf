using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfMvvm.Models
{
    public class EmployeeServiceWithEntity
    {
        public EmployeeServiceWithEntity()
        {

        }
        public async Task<List<EmployeeDto>> GetAll()
        {
            List<EmployeeDto> employees = new List<EmployeeDto>();
            try
            {
                using (var empContext = new DbEmployeeContext())
                {
                    employees = await empContext.Employees
                        .Select(e => new EmployeeDto
                        {
                            Id = e.Id,
                            Name = e.Name,  
                            Age = e.Age,    
                        })
                        .ToListAsync();

                }
                return employees;   

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public async Task<bool> Add(EmployeeDto emp)
        {
            if (emp.Age < 18) return false;
            try
            {
                using(var empContext = new DbEmployeeContext())
                {
                    var employe = new Employee()
                    {
                        Id = emp.Id,
                        Name = emp.Name,
                        Age = (int)emp.Age,
                    };
                    empContext.Employees.Add(employe);
                   var result = await empContext.SaveChangesAsync();
                    return result> 1;
                }
            }
            catch (Exception ex)
            {
                return false;
            }

        }
        public async Task<bool> Update(EmployeeDto emp)
        {
            if (emp.Age < 18) return false;
            try
            {
                using (var empDb = new DbEmployeeContext())
                {
                    var employe = new Employee
                    {
                        Id = emp.Id,
                        Name = emp.Name,
                        Age = (int)emp.Age
                    };
                    empDb.Entry(employe).State = EntityState.Modified;
                    await empDb.SaveChangesAsync();
                    return true;
                }

            }
            catch (Exception ex)
            {
                return false;
            }

        }
        public async Task<bool> Delete(EmployeeDto emp)
        {

            try
            {
                using (var empDb = new DbEmployeeContext())
                {
                    var employee = await empDb.Employees.FirstOrDefaultAsync(x => x.Id == emp.Id);
                    if(employee != null)
                    {
                        empDb.Remove(employee);
                        await empDb.SaveChangesAsync();
                        return true;
                    }
                    return false;

                }

            }
            catch (Exception ex)
            {
                return false;
            }
            

        }
        public async Task<EmployeeDto?> Search(int id)
        {
            EmployeeDto emp = null;
            try
            {
                using(var empDb = new DbEmployeeContext())
                {
                    var employ = await empDb.Employees.FirstOrDefaultAsync(x => x.Id == id);
                    if (employ == null) return null;
                    emp = new EmployeeDto
                    {
                        Id = employ.Id,
                        Age = (int)employ.Age,
                        Name = employ.Name
                    };
                    return emp;
                }
            }
            catch (Exception ex)
            {
                return null;
            }

        }
    }
}

