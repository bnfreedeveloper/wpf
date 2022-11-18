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


        public bool Add(EmployeeDto emp)
        {
            if (emp.Age < 18) return false;
            try
            {

            }
            catch (Exception ex)
            {

            }

        }
        public bool Update(EmployeeDto emp)
        {
            if (emp.Age < 18) return false;
            try
            {

            }
            catch (Exception ex)
            {
                return false;
            }

        }
        public bool Delete(EmployeeDto emp)
        {

            try
            {

            }
            catch (Exception ex)
            {
                return false;
            }
            finally { connection.Close(); }

        }
        public EmployeeDto? Search(int id)
        {
            EmployeeDto emp = null;
            try
            {

            }
            catch (Exception ex)
            {
                return null;
            }

        }
    }
}

