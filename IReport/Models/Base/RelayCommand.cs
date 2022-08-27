using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace IReport.Models.Base
{
    public class RelayCommand : ICommand
    {

        Action<object> _executeAction;
        Func<object, bool> _canExecute;

        public ICommand DeleteSqlCommand { get; set; }
        public RelayCommand(Action<object> _executeAction,  Func<object,bool> _canExecute)
        {
            this._executeAction = _executeAction;
            this._canExecute = _canExecute; 
        }

        public event EventHandler CanExecuteChanged;
        public bool CanExecute(object parameter)
        {
            if (_canExecute == null)
            {
                return true;
            }
            return _canExecute(parameter);
        }

        public void Execute(object parameter)
        {
            _executeAction(parameter);
        }

    }
}
