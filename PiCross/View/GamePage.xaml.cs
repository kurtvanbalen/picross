using PiCross;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using ViewModel;

namespace View
{
    /// <summary>
    /// Interaction logic for GameWindow.xaml
    /// </summary>
    public partial class GamePage : Page
    {
        DispatcherTimer timer = new DispatcherTimer();
        TimeSpan time = new TimeSpan(0);
        public GamePage(MainWindowViewModel main)
        {
            InitializeComponent();
            Main = main;

            //timer
            timer.Interval = new TimeSpan(0, 0, 1);
            timer.Start();
            timer.Tick += new EventHandler(timer_Tick);

            //gamedata
            GameWindowVM = new GameWindowViewModel();
            picrossControl.Grid = GameWindowVM.SquareGrid;
            picrossControl.RowConstraints = GameWindowVM.PlayablePuzzle.RowConstraints;
            picrossControl.ColumnConstraints = GameWindowVM.PlayablePuzzle.ColumnConstraints;
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            time = time.Add(new TimeSpan(0, 0, 1));
            Timer.Content = time;
        }

        public GameWindowViewModel GameWindowVM { get; }
        public MainWindowViewModel Main { get; }

        private void SolvedButton_Click(object sender, RoutedEventArgs e)
        {
            if (GameWindowVM.PlayablePuzzle.IsSolved.Value == true)
            {
                timer.Stop();
                Window GameWonWindow = new GameWon(time);
                GameWonWindow.Show();
                this.NavigationService.Navigate(new MainPage(Main));
            }
            else
            {
                FailGame.Visibility = Visibility.Visible;
            }
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            timer.Stop();
            this.NavigationService.Navigate(new MainPage(Main));
        }
    }
    public class SquareConverter : IValueConverter
    {
        public object Filled { get; set; }
        public object Empty { get; set; }
        public object Unknown { get; set; }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var square = (Square)value;
            if (square == Square.EMPTY)
            {
                return Empty;
            }

            else if (square == Square.FILLED)

            {
                return Filled;
            }

            else
            {
                return Unknown;
            }
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
    public class ConstraintColorConverter : IValueConverter
    {
        public object Satisfied { get; set; }
        public object Wrong { get; set; }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {       
            var satisfied = (bool)value;
            if (satisfied)
            {
                return Satisfied;
            }
            return Wrong;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
