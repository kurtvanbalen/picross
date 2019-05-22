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
using System.Windows.Shapes;

namespace View
{
    /// <summary>
    /// Interaction logic for GameWon.xaml
    /// </summary>
    public partial class GameWon : Window
    {
        public GameWon(TimeSpan time)
        {
            InitializeComponent();

            SpentTime.Text = "It took you " + time + " to complete the puzzle!";
        }

        private void CloseGame_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
