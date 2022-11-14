using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using WpfMvvm.Models;
using System.Diagnostics.CodeAnalysis;

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

        private List<Employee> employeeList;
        public EmployeeViewModel()
        {
            EmployeeService = new EmployeeService(); 
            LoadData();
        }
        #region display
        public List<Employee> EmployeeList
        {
            get { return employeeList; }    
            set { 
              if(!employeeList.SequenceEqual(value, new CompareEmployees()))
                {
                    employeeList = value;
                    OnPropertyChanged(nameof(EmployeeList));
                }
            }
        }
        private void LoadData()
        {
            employeeList = EmployeeService.GetAll();
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
