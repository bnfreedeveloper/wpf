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
        //ado part
        //private EmployeeServiceAdo EmployeeService;
        private EmployeeServiceWithEntity EmployeeService;
        private EmployeeDto employee;
        private AddCommand addCommand;
        private string message;

        public string Message
        {
            get { return message; }
            set { message = value; OnPropertyChanged("Message"); }  
        }   

        private ObservableCollection<EmployeeDto> employeeList;
        public EmployeeViewModel()
        {
            //ado part
            //EmployeeService = new EmployeeServiceAdo(); 
            EmployeeService = new EmployeeServiceWithEntity(); 
            employeeList = new ObservableCollection<EmployeeDto>();    
            LoadData();
            Employee = new EmployeeDto();
            addCommand = new AddCommand(AddEmployee);
            searchCommand = new AddCommand(Search);
            updateCommand = new AddCommand(Update);
            deleteCommand = new AddCommand(Delete); 
        }
        #region display
        public ObservableCollection<EmployeeDto> EmployeeList
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

        public async Task AddEmployee()
        {
            try
            {
                var added = await EmployeeService.Add(employee);
                LoadData();
                Employee = new EmployeeDto();
                if (added) Message = "employee successfully added";
                else Message = "operation failed";
            }
            catch(Exception ex)
            {

            }
        }
        public EmployeeDto Employee
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
        private async Task LoadData()
        {
            EmployeeList = new ObservableCollection<EmployeeDto>(await EmployeeService.GetAll());
        }
        #endregion

        #region search
        private AddCommand searchCommand;
        public AddCommand SearchCommand => searchCommand;

        public async Task Search()
        {
            try
            {
                var emp = await EmployeeService.Search(Employee.Id);
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
        
        public async Task Update()
        {
            try
            {
                var isUpdated = await EmployeeService.Update(Employee);
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

        public async Task Delete()
        {
            try
            {
                var isDeleted = await EmployeeService.Delete(Employee);
                if (isDeleted)
                {
                    Message = "employee was deleted";
                    LoadData();
                    Employee = new EmployeeDto();
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

    public class CompareEmployees : IEqualityComparer<EmployeeDto>
    {
        public bool Equals(EmployeeDto? x, EmployeeDto? y)
        {
            if (x == null || y == null) return false;
            else return (x.Name == y.Name) && (x.Age == y.Age) && (x.Id == y.Id);
        }

        public int GetHashCode([DisallowNull] EmployeeDto obj)
        {
            return obj == null ? 0 : obj.GetHashCode(); 
        }
    }
}
