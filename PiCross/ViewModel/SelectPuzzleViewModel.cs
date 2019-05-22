using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Cells;
using PiCross;
namespace ViewModel
{
    public class SelectPuzzleViewModel
    {
        private const string Path = "C:/repos/VGO/picross-1819-kurtvanbalen/python/picross.zip";
        
        public SelectPuzzleViewModel()
        {
            Library = new PiCrossFacade().LoadGameData(Path);
            Puzzles = new List<PuzzleEntryViewModel>();
            Chosen = Cell.Create<PuzzleEntryViewModel>(new PuzzleEntryViewModel("empty"));
            foreach (IPuzzleLibraryEntry i in Library.PuzzleLibrary.Entries)
            {
                Puzzles.Add(new PuzzleEntryViewModel(i, this));
            }
        }
        public void Select(PuzzleEntryViewModel entry)
        {
            Chosen.Value.Selected.Value = false;
            entry.Selected.Value = true;
            Chosen.Value = entry;
        }
        public List<PuzzleEntryViewModel> Puzzles { get; }
        public IGameData Library { get; }
        public Cell<PuzzleEntryViewModel> Chosen { get; }
    }

    public class PuzzleEntryViewModel
    {
        public SelectPuzzleViewModel Parent { get; }
        public IPuzzleLibraryEntry Puzzle { get; }

        public PuzzleEntryViewModel(IPuzzleLibraryEntry puzzle, SelectPuzzleViewModel parent)
        {
            Parent = parent;
            Puzzle = puzzle;
            Select = new PuzzleLeft(this);
            Selected =  Cell.Create<bool>(false);
        }

        public PuzzleEntryViewModel(string v)
        {
            Selected = Cell.Create<bool>(false);
        }

        public ICommand Select { get; }
        public Cell<bool> Selected { get; }
    }

    public class PuzzleLeft : ICommand
    {
        public PuzzleEntryViewModel Parent { get; }
        public PuzzleLeft(PuzzleEntryViewModel parent)
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
            this.Parent.Parent.Select(this.Parent);
        }
    }
}
