using System;
using System.Collections.Generic;
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
using ViewModel;

namespace View
{
    /// <summary>
    /// Interaction logic for MainPage.xaml
    /// </summary>
    public partial class MainPage : Page
    {
        public MainPage(MainWindowViewModel main)
        {
            InitializeComponent();
            Main = main;
        }
        public MainWindowViewModel Main { get; }
        private void StartGameButton_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new GamePage(Main));
        }

        private void RulesButton_Click(object sender, RoutedEventArgs e)
        {
            Window rulesWindow = new RulesWindow();
            rulesWindow.Show();
        }
        private void SelectGame_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new SelectPuzzle(Main));
        }
    }
}
