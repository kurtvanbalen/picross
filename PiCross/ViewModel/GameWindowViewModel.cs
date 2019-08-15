using Cells;
using DataStructures;
using Microsoft.Practices.ServiceLocation;
using PiCross;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Input;
using ViewModel.Interfaces;

namespace ViewModel
{
    public class GameWindowViewModel
    {
        public IPlayablePuzzle PlayablePuzzle { get; set; }
        public IGrid<SquareViewModel> SquareGrid { get; set; }
        public IGameData Library { get; }
        public String Timer { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;
        public ICommand CloseGameWindow { get; }
        public ICommand IsSolved { get; }
        public MainWindowViewModel Main;

        private double _millisec;
        private DateTimeOffset _start;
        private ITimerService _timer;
        private Puzzle ActivePuzzle { get; set; }

        public GameWindowViewModel(MainWindowViewModel main )
        {
            Main = main;
           var puzzle = Puzzle.FromRowStrings(
                "xxxxx",
                "x...x",
                "x...x",
                "x...x",
                "xxxxx"
           );
            var facade = new PiCrossFacade();

            //timer
            var timeService = ServiceLocator.Current.GetInstance<ITimeService>();
            _start = timeService.Now;

            _timer = ServiceLocator.Current.GetInstance<ITimerService>();
            _timer.Tick += Timer_Tick;
            _timer.Start(new TimeSpan(0, 0, 0, 0, 100));

            PlayablePuzzle = facade.CreatePlayablePuzzle(puzzle);
            SquareGrid = Grid.Create<SquareViewModel>(PlayablePuzzle.Grid.Size, p => new SquareViewModel(PlayablePuzzle.Grid[p]));
            Timer = "00:00:00";
            CloseGameWindow = new CloseGameWindowCommand(main);
            IsSolved = new IsSolvedCommand(this);
        }

        private void Timer_Tick(ITimerService obj)
        {
            var timeService = ServiceLocator.Current.GetInstance<ITimeService>();
            var timePassed = timeService.Now - _start;
            Milliseconds = Math.Round(timePassed.TotalMilliseconds / 100) * 100;
        }

        public double Milliseconds
        {
            get
            {
                return _millisec;
            }
            private set
            {
                if (_millisec != value)
                {
                    _millisec = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Milliseconds)));
                }
            }
        }

        public void StartGame(IPuzzleLibraryEntry puzzle)
        {
            ActivePuzzle = puzzle.Puzzle;
            this.PlayablePuzzle = new PiCrossFacade().CreatePlayablePuzzle(puzzle.Puzzle);
            this.SquareGrid = Grid.Create<SquareViewModel>(PlayablePuzzle.Grid.Size, p => new SquareViewModel(PlayablePuzzle.Grid[p]));
        }

        private class IsSolvedCommand : ICommand
        {
            public event EventHandler CanExecuteChanged;
            private GameWindowViewModel vm;

            public IsSolvedCommand(GameWindowViewModel vm)
            {
                this.vm = vm;
            }
            public bool CanExecute(object parameter)
            {
                return true;
            }

            public void Execute(object parameter)
            {
                var winService = ServiceLocator.Current.GetInstance<IWindowService>();
                if (vm.PlayablePuzzle.IsSolved.Value)
                { 
                    winService.CloseDialog(true);//vm.Main.getActiveWindow()
                    winService.ShowDialog(new GameWonViewModel(vm.Main));
                }
                else
                {
                    winService.ShowDialog(new GameErrorViewModel());
                }
            }
        }

        private class CloseGameWindowCommand : ICommand
        {
            private MainWindowViewModel vm;

            public CloseGameWindowCommand(MainWindowViewModel vm)
            {
                this.vm = vm;
            }

            public event EventHandler CanExecuteChanged;

            public bool CanExecute(object parameter)
            {
                return true;
            }

            public void Execute(object parameter)
            {
                var winService = ServiceLocator.Current.GetInstance<IWindowService>();
                winService.CloseDialog(false);//vm.getActiveWindow()
                //vm.setActiveWindow(null);
            }
        }
    }

    public class SquareViewModel
    {
        public SquareViewModel(IPlayablePuzzleSquare square)
        {
            Square = square;
            RightClick = new Right(this);
            LeftClick = new Left(this);
        }
        public IPlayablePuzzleSquare Square {get;}
        public ICommand RightClick { get; }
        public ICommand LeftClick { get; }
    }
    public class Right : ICommand
    {
        public SquareViewModel Parent { get; }
        public Right(SquareViewModel parent)
        {
            Parent = parent;
        }
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            if (Parent.Square.Contents.Value == Square.EMPTY)
            {
                Parent.Square.Contents.Value = Square.UNKNOWN;
            }
            else
            {
                Parent.Square.Contents.Value = Square.EMPTY;
            }
        }
    }
    public class Left : ICommand
    {
        public SquareViewModel Parent { get; }
        public Left(SquareViewModel parent)
        {
            Parent = parent;
        }
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            if (Parent.Square.Contents.Value == Square.FILLED)
            {
                Parent.Square.Contents.Value = Square.UNKNOWN;
            }
            else
            {
                Parent.Square.Contents.Value = Square.FILLED;
            }      
        }
    }
}
