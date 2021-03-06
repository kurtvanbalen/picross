﻿using System;
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
using System.Windows.Shapes;

namespace View
{
    /// <summary>
    /// Interaction logic for SelectPuzzleWindow.xaml
    /// </summary>
    public partial class SelectPuzzleWindow : Window
    {
        public SelectPuzzleWindow()
        {
            InitializeComponent();
        }
    }
    public class PuzzleSizeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var size = (DataStructures.Size)value;
            if (size != null) return size.Width.ToString() + " x " + size.Height.ToString();
            return "ERROR!";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
    public class PuzzleSelectedConverter : IValueConverter
    {
        public object Selected { get; set; }
        public object Not { get; set; }
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var Selected = (bool)value;
            if (Selected) { return Selected; }
            return Not;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
