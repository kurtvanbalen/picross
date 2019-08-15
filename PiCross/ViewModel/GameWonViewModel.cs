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
    public class GameWonViewModel
    {
        public ICommand CloseGameWon { get; }

        public GameWonViewModel(MainWindowViewModel main)
        {
            CloseGameWon = new CloseGameWonCommand(main);
        }

        private class CloseGameWonCommand : ICommand
        {
            private MainWindowViewModel main;

            public CloseGameWonCommand(MainWindowViewModel main)
            {
                this.main = main;
            }

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
