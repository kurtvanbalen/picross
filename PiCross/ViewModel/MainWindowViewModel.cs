using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PiCross;

namespace ViewModel
{
    public class MainWindowViewModel
    {

        public MainWindowViewModel()
        {
            Game = new GameWindowViewModel();
            SelectedPuzzle = new SelectPuzzleViewModel();
        }
        public GameWindowViewModel Game { get; }
        public SelectPuzzleViewModel SelectedPuzzle { get; }
    }
}
