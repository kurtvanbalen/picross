using DataStructures;
using PiCross;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ViewModel
{
    public class GameWindowViewModel
    {
        public GameWindowViewModel( )
        {
           var puzzle = Puzzle.FromRowStrings(
                "xxxxx",
                "x...x",
                "x...x",
                "x...x",
                "xxxxx"
           );
            var facade = new PiCrossFacade();
            PlayablePuzzle = facade.CreatePlayablePuzzle(puzzle);
            SquareGrid = Grid.Create<SquareViewModel>(PlayablePuzzle.Grid.Size, p => new SquareViewModel(PlayablePuzzle.Grid[p]));
        }
        public IPlayablePuzzle PlayablePuzzle { get; set; }
        public IGrid<SquareViewModel> SquareGrid { get; set; }
        public IGameData Library { get; }

        public void StartGame(IPuzzleLibraryEntry puzzle)
        {
            this.PlayablePuzzle = new PiCrossFacade().CreatePlayablePuzzle(puzzle.Puzzle);
            this.SquareGrid = Grid.Create<SquareViewModel>(PlayablePuzzle.Grid.Size, p => new SquareViewModel(PlayablePuzzle.Grid[p]));
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
