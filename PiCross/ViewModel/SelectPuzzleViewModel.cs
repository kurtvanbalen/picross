using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Cells;
using Microsoft.Practices.ServiceLocation;
using PiCross;
using ViewModel.Interfaces;

namespace ViewModel
{
    public class SelectPuzzleViewModel
    {
        private const string path = "picross.zip";

        public SelectPuzzleViewModel(MainWindowViewModel main)
        {
            this.Main = main;
            Library = new PiCrossFacade().LoadGameData(path);
            Puzzles = new List<PuzzleEntryViewModel>();
            Chosen = Cell.Create<PuzzleEntryViewModel>(new PuzzleEntryViewModel("empty"));

            ClosePuzzleSelect = new ClosePuzzleSelectCommand(Main);
            SelectPuzzleSelect = new SelectPuzzleSelectCommand(this);
            

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

        public MainWindowViewModel Main { get; }
        public List<PuzzleEntryViewModel> Puzzles { get; }
        public IGameData Library { get; }
        public ICommand ClosePuzzleSelect { get; }
        public ICommand SelectPuzzleSelect { get; }
        public Cell<PuzzleEntryViewModel> Chosen { get; }

        private class ClosePuzzleSelectCommand : ICommand
        {
            private MainWindowViewModel _vm;

            public ClosePuzzleSelectCommand(MainWindowViewModel vm)
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
                winService.CloseDialog(false);
            }
        }

        private class SelectPuzzleSelectCommand : ICommand
        {
            private SelectPuzzleViewModel _vm;

            public SelectPuzzleSelectCommand(SelectPuzzleViewModel vm)
            {
                this._vm = vm;
            }

            public event EventHandler CanExecuteChanged;


            public bool CanExecute(object parameter)
            {
                return true;
            }

            public void Execute(object parameter)
            {
                if (_vm.Chosen.Value.Selected.Value != false)
                {
                    var winService = ServiceLocator.Current.GetInstance<IWindowService>();
                    winService.CloseDialog(true);
                    _vm.Main.StartGame(true);                
                }
            }
        }
    }

    public class PuzzleEntryViewModel
    {
        public SelectPuzzleViewModel Parent { get; }
        public IPuzzleLibraryEntry Puzzle { get; }
        public String Author { get; }
        public DataStructures.IGrid<bool> Grid { get; }
        public PuzzleEntryViewModel(IPuzzleLibraryEntry puzzle, SelectPuzzleViewModel parent)
        {
            Parent = parent;
            Puzzle = puzzle;
            Grid = Puzzle.Puzzle.Grid;
            Author = "Author: " + puzzle.Author;
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
