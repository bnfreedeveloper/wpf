using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace WpfMvvm.Commands
{
    public class AddCommand : ICommand
    {
        public event EventHandler? CanExecuteChanged;
        private Action AddWork;
        public AddCommand(Action addWork)
        {
            AddWork = addWork;
        }
        public bool CanExecute(object? parameter)
        {
            return true;
        }
        public void Execute(object? parameter)
        {
            AddWork(); 
        }
    }
}
