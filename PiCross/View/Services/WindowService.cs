using System;
using System.Collections.Generic;
using ViewModel.Interfaces;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using View;

namespace View.Services
{
    public class WindowService : IWindowService
    {
        public WindowService()
        {
            _currentDialogs = new LinkedList<Window>();
        }
        /// <summary>
        /// Shows a non-modal window associated with a given viewmodel object. The association between the view and viewmodel
        /// is defined in the Resources tag of the App.xaml file as a DataTemplate.
        /// </summary>
        /// <param name="viewModel">The viewmodel object to use when looking up the corresponding view.</param>
        /// <returns>An object that represents the window. This object can be used as a parameter to the
        /// CloseWindow function.</returns>
        public object ShowWindow(object viewModel)
        {
            var window = FindWindow(viewModel);
            window.DataContext = viewModel;
            window.Show();
            return window;
        }
        /// <summary>
        /// Shows a modal window associated with a given viewmodel object. The association between the view and viewmodel
        /// is defined in the Resources tag of the App.xaml file as a DataTemplate.
        /// </summary>
        /// <param name="viewModel">The viewmodel object to use when looking up the corresponding view.</param>
        /// <returns>The value of the DialogResult property.</returns>
        public bool ShowDialog(object viewModel)
        {
            var d = FindWindow(viewModel);
            d.DataContext = viewModel;
            _currentDialogs.AddLast(d);
            var ret = d.ShowDialog();
            _currentDialogs.RemoveLast();
            return ret.Value;
        }
        /// <summary>
        /// Closes the active dialog window.
        /// </summary>
        /// <param name="result">The value the DialogResult property of the window should be set to.</param>
        public void CloseDialog(bool result)
        {
            if (_currentDialogs.Count == 0)
                throw new NotSupportedException("No dialog open at this time.");
            _currentDialogs.Last.Value.DialogResult = result;
        }
        /// <summary>
        /// Closes a given window.
        /// </summary>
        /// <param name="window">A window object, as returned by the ShowWindow function.</param>
        public void CloseWindow(object window)
        {
            var w = window as Window;
            w.Close();
        }

        private Window FindWindow(object vm)
        {
            var t = vm.GetType();
            foreach (var value in App.Current.Resources.Values)
            {
                DataTemplate dtemp = value as DataTemplate;
                if (dtemp != null && t.Equals(dtemp.DataType))
                {
                    var win = dtemp.LoadContent() as Window;
                    if (win != null)
                        return win;
                }
            }
            throw new ArgumentException($"Unable to find the window corresponding to the { vm.GetType().Name } viewmodel class.");
        }

        private LinkedList<Window> _currentDialogs;
    }
}
