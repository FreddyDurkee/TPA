using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace UIBackend.ViewModel
{
    public class RelayCommand : ICommand
    {

        Action<object> _executemethod;


        public RelayCommand(Action<object> executemethod)
        {
            _executemethod = executemethod;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            _executemethod(parameter);
        }
    }
}
