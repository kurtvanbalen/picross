using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Microsoft.Practices.ServiceLocation;
using PiCross;
using ViewModel.Interfaces;

namespace ViewModel
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {

        public MainWindowViewModel()
        {
            Game = new GameWindowViewModel(this);
            SelectedPuzzle = new SelectPuzzleViewModel(this);
            this.OpenRules = new OpenRulesCommand();
            this.OpenPuzzleSelect = new OpenPuzzleSelectCommand(SelectedPuzzle);
            this.OpenStartGame = new OpenStartGameCommand(this);
            Exit = new ExitCommand(this);
            Closing = new ClosingCommand(this);
        }

        public event Action ApplicationExit;
        public GameWindowViewModel Game { get; }
        public SelectPuzzleViewModel SelectedPuzzle { get; }
        public ICommand OpenRules { get; set; }
        public ICommand OpenPuzzleSelect { get; set; }
        public ICommand OpenStartGame { get; set; }
        public ICommand Exit { get; set; }
        public ICommand Closing { get; set; }
        //private object ActiveWindow { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
        public void StartGame(Boolean selectedPuzzleBool)
        {
            if(selectedPuzzleBool) Game.StartGame(getPuzzle());
            var winService = ServiceLocator.Current.GetInstance<IWindowService>();
            var window = winService.ShowDialog(Game);
            //setActiveWindow(window);
        }
        /*public object getActiveWindow()
        {
            return ActiveWindow;
        }
        public void setActiveWindow(object window)
        {
            ActiveWindow = window;
        }*/
        public IPuzzleLibraryEntry getPuzzle()
        {
            return SelectedPuzzle.Chosen.Value.Puzzle;
        }

        private class OpenRulesCommand : ICommand
        {
            public event EventHandler CanExecuteChanged;

            public bool CanExecute(object parameter)
            {
                return true;
            }

            public void Execute(object parameter)
            {
                var winService = ServiceLocator.Current.GetInstance<IWindowService>();
                winService.ShowDialog(new RulesViewModel());

            }

        }

        private class ExitCommand : ICommand
        {
            public event EventHandler CanExecuteChanged;
            private MainWindowViewModel _vm;

            public ExitCommand(MainWindowViewModel vm)
            {
                _vm = vm;
            }
            public bool CanExecute(object parameter)
            {
                return true;
            }

            public void Execute(object parameter)
            {
                var mbService = ServiceLocator.Current.GetInstance<IMessageBoxService>();
                var result = mbService.Show(null, "Are you sure you want to quit?", "Quit...", MessageBoxButtons.YesNo, MessageBoxImage.Question, MessageBoxResult.No);
                if (result == MessageBoxResult.Yes)
                {
                    _vm.ApplicationExit?.Invoke();
                }
            }
        }
        private class ClosingCommand : ICommand
        {
            public event EventHandler CanExecuteChanged;
            private MainWindowViewModel _vm;

            public ClosingCommand(MainWindowViewModel vm)
            {
                _vm = vm;
            }
            public bool CanExecute(object parameter)
            {
                var mbService = ServiceLocator.Current.GetInstance<IMessageBoxService>();
                var result = mbService.Show(null, "Are you sure you want to quit?", "Quit...", MessageBoxButtons.YesNo, MessageBoxImage.Question, MessageBoxResult.No);
                return result == MessageBoxResult.Yes;
            }

            public void Execute(object parameter)
            {
                    _vm.ApplicationExit?.Invoke();
            }
        }
        private class OpenPuzzleSelectCommand : ICommand
        {
            public OpenPuzzleSelectCommand(SelectPuzzleViewModel vm)
            {
                _vm = vm;
            }
            public event EventHandler CanExecuteChanged;


            public bool CanExecute(object parameter)
            {
                return true;
            }

            public void Execute(object parameter)
            {
                var winService = ServiceLocator.Current.GetInstance<IWindowService>();
                winService.ShowDialog(_vm);
            }
            private SelectPuzzleViewModel _vm;
        }

        private class OpenStartGameCommand : ICommand
        {
            public OpenStartGameCommand(MainWindowViewModel vm)
            {
                _vm = vm;
            }
            public event EventHandler CanExecuteChanged;


            public bool CanExecute(object parameter)
            {
                return true;
            }

            public void Execute(object parameter)
            {
                _vm.StartGame(false);
            }
            private MainWindowViewModel _vm;
        }
    }
}
