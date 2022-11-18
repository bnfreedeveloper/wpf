using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace WpfMvvm.Models
{
    public class EmployeeServiceAdo
    {
        SqlConnection connection;
        SqlCommand command;
        public EmployeeServiceAdo()
        {
            //connection = new SqlConnection(
            //   ConfigurationManager.ConnectionStrings["employeeConnection"].ConnectionString);
            connection = new SqlConnection("Server=LAPTOP-1VTJ2SQK\\SQLEXPRESS;Initial Catalog=DbEmployee;integrated security=true;");
            command = new SqlCommand();
            command.Connection = connection;
            command.CommandType = CommandType.StoredProcedure;
        }
        public List<EmployeeDto> GetAll()
        {
            List<EmployeeDto> employees = new List<EmployeeDto>();
            try
            {
                command.Parameters.Clear();
                command.CommandText = "proc_SelectAllEmployee";
                connection.Open();
                var reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    EmployeeDto emp = null;
                    while (reader.Read())
                    {
                        emp = new EmployeeDto();
                        emp.Id = reader.GetInt32(0);
                        emp.Name = reader.GetString(1); 
                        emp.Age = reader.GetInt32(2);   
                        employees.Add(emp);
                    }

                }
                reader.Close();
                return employees;   

            }catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                connection.Close();
            }
        }

            public bool Add(EmployeeDto emp)
            {
            if (emp.Age < 18) return false;
            try
            {
                command.Parameters.Clear();
                command.CommandText = "proc_InsertEmployee";
                command.Parameters.AddWithValue("@Name", emp.Name);
                command.Parameters.Add("@Age",SqlDbType.Int);
                command.Parameters["@Age"].Value = emp.Age;
                connection.Open();
               int rows = command.ExecuteNonQuery();
               return rows > 0;
            }
            catch (Exception ex)
            {
                return false;
            }
            finally { connection.Close(); }
           
            }
            public bool Update(EmployeeDto emp)
            {
            if (emp.Age < 18) return false;
            try
            {
                command.Parameters.Clear();
                command.CommandText = "proc_UpdateEmploee";
                command.Parameters.AddWithValue("@Id",emp.Id);  
                command.Parameters.AddWithValue("@Name", emp.Name);
                command.Parameters.Add("@Age", SqlDbType.Int);
                command.Parameters["@Age"].Value = emp.Age;
                connection.Open();
                int rows = command.ExecuteNonQuery();
                return rows > 0;
            }
            catch (Exception ex)
            {
                return false;
            }
            finally { connection.Close(); }
        }
            public bool Delete(EmployeeDto emp)
            {

            try
            {
                command.Parameters.Clear();
                command.CommandText = "proc_deleteEmploee";
                command.Parameters.AddWithValue("@Id", emp.Id);
                connection.Open();
                int rows = command.ExecuteNonQuery();
                return rows > 0;
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
                command.Parameters.Clear();
                command.CommandText = "proc_SelectEmployeeById";
                command.Parameters.AddWithValue("@Id", id);
                connection.Open();
                var reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    emp = new EmployeeDto();
                    reader.Read();
                    emp.Name = reader.GetString(1);
                    emp.Age = reader.GetInt32(2);
                }
                reader.Close();
                return emp;
            }
            catch (Exception ex)
            {
                return null;
            }
            finally { connection.Close(); }
        }
        
    }
}
