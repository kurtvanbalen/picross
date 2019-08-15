using Microsoft.Practices.ServiceLocation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using ViewModel.Interfaces;

namespace ViewModel
{
    public class GameErrorViewModel
    {
        public ICommand CloseGameError { get; }

        public GameErrorViewModel()
        {
            CloseGameError = new CloseGameErrorCommand();
        }

        private class CloseGameErrorCommand : ICommand
        {


            public event EventHandler CanExecuteChanged;

            public bool CanExecute(object parameter)
            {
                return true;
            }

            public void Execute(object parameter)
            {
                var winService = ServiceLocator.Current.GetInstance<IWindowService>();
                winService.CloseDialog(true);
            }
        }
    }
}
