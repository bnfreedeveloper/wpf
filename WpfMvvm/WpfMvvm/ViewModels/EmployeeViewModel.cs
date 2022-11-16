using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using WpfMvvm.Models;
using System.Diagnostics.CodeAnalysis;
using WpfMvvm.Commands;
using System.Collections.ObjectModel;
using System.Windows.Controls;

namespace WpfMvvm.ViewModels
{
    public class EmployeeViewModel : INotifyPropertyChanged
    {
        #region InotifyPropertyChanged
        public event PropertyChangedEventHandler? PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion
        private EmployeeService EmployeeService;
        private Employee employee;
        private AddCommand addCommand;
        private string message;

        public string Message
        {
            get { return message; }
            set { message = value; OnPropertyChanged("Message"); }  
        }   

        private ObservableCollection<Employee> employeeList;
        public EmployeeViewModel()
        {
            EmployeeService = new EmployeeService(); 
            employeeList = new ObservableCollection<Employee>();    
            LoadData();
            Employee = new Employee();
            addCommand = new AddCommand(AddEmployee);
            searchCommand = new AddCommand(Search);
            updateCommand = new AddCommand(Update);
            deleteCommand = new AddCommand(Delete); 
        }
        #region display
        public ObservableCollection<Employee> EmployeeList
        {
            get { return employeeList; }    
            set {
                if (employeeList.Count ==0 || !employeeList.SequenceEqual(value, new CompareEmployees()))
                {
                    employeeList = value;
                    OnPropertyChanged(nameof(EmployeeList));
                }
            }
        }
        public AddCommand AddCommand => addCommand; 

        public void AddEmployee()
        {
            try
            {
                var added = EmployeeService.Add(employee);
                LoadData();
                Employee = new Employee();
                if (added) Message = "employee successfully added";
                else Message = "operation failed";
            }
            catch(Exception ex)
            {

            }
        }
        public Employee Employee
        {
            get { return employee; }
            set { 
                if((value.Id != employee?.Id) || (value.Name !=employee?.Name) || (value.Age != employee?.Age))
                {
                    employee = value;
                    OnPropertyChanged(nameof(Employee));
                }
                }  
        }
        private void LoadData()
        {
            EmployeeList = new ObservableCollection<Employee>(EmployeeService.GetAll());
        }
        #endregion

        #region search
        private AddCommand searchCommand;
        public AddCommand SearchCommand => searchCommand;

        public void Search()
        {
            try
            {
                var emp = EmployeeService.Search(Employee.Id);
                if(emp != null)
                {
                    Employee.Name = emp.Name;
                    Employee.Age = emp.Age;
                    Message = "";
                }
            }
            catch (Exception ex)
            {
                Message = "employee not found ";
            }
        }
        #endregion

        #region update
        private AddCommand updateCommand;
        public AddCommand UpdateCommand => updateCommand; 
        
        public void Update()
        {
            try
            {
                var isUpdated = EmployeeService.Update(Employee);
                if (isUpdated)
                {
                    Message = "employe was updated";
                    LoadData();
                }
                else Message = "update operation failed";
            }
            catch(Exception ex)
            {
                Message = "employee not updated";
            }
        }
        #endregion

        #region delete
        private AddCommand deleteCommand;
        public  AddCommand DeleteCommand => deleteCommand;  

        public void Delete()
        {
            try
            {
                var isDeleted = EmployeeService.Delete(Employee);
                if (isDeleted)
                {
                    Message = "employee was deleted";
                    LoadData();
                    Employee = new Employee();
                }
                else Message = "employee wasn't deleted";

            }
            catch(Exception ex)
            {
                Message = "delete operation failed";
            }
        }
        #endregion
    }

    public class CompareEmployees : IEqualityComparer<Employee>
    {
        public bool Equals(Employee? x, Employee? y)
        {
            if (x == null || y == null) return false;
            else return (x.Name == y.Name) && (x.Age == y.Age) && (x.Id == y.Id);
        }

        public int GetHashCode([DisallowNull] Employee obj)
        {
            return obj == null ? 0 : obj.GetHashCode(); 
        }
    }
}
