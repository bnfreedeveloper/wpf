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
       
        private Func<Task> AddWork;

        public AddCommand(Func<Task> addWork)
        {
            AddWork = addWork;
        }

       
        public bool CanExecute(object? parameter)
        {
            return true;
        }
        public async void Execute(object? parameter)
        {
            await AddWork();
        }
    }
}
