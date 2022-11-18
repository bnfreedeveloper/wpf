using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace WpfMvvm.Models
{
    public class EmployeeDto : INotifyPropertyChanged
    {
        private string? _name;
        private int? _age;
        private int _id;

        public event PropertyChangedEventHandler? PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        public string Name
        {
            get { return _name; }
            set
            {
                if (_name != value)
                {
                    _name = value; OnPropertyChanged("Name");
                }

            }
        }
        public int? Age
        {
            get { return _age; }
            set
            {
                if (_age != value)
                {
                    _age = value;
                    OnPropertyChanged("Age");
                }
            }
        }
        public int Id
        {
            get { return _id; } 
            set { 
                if(_id != value)
                {
                    _id = value; OnPropertyChanged("Id");
                }
               
            }
        }
    }
}
