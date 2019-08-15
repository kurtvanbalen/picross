using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel.Interfaces
{
    public interface IWindowService
    {
        /// <summary>
        /// Shows a non-modal window associated with a given viewmodel object. The association between the view and viewmodel
        /// is defined in the Resources tag of the App.xaml file as a DataTemplate.
        /// </summary>
        /// <param name="viewModel">The viewmodel object to use when looking up the corresponding view.</param>
        /// <returns>An object that represents the window. This object can be used as a parameter to the
        /// CloseWindow function.</returns>
        object ShowWindow(object viewModel);
        /// <summary>
        /// Closes a given window.
        /// </summary>
        /// <param name="window">A window object, as returned by the ShowWindow function.</param>
        void CloseWindow(object window);
        /// <summary>
        /// Shows a modal window associated with a given viewmodel object. The association between the view and viewmodel
        /// is defined in the Resources tag of the App.xaml file as a DataTemplate.
        /// </summary>
        /// <param name="viewModel">The viewmodel object to use when looking up the corresponding view.</param>
        /// <returns>The value of the DialogResult property.</returns>
        bool ShowDialog(object viewModel);
        /// <summary>
        /// Closes the active dialog window.
        /// </summary>
        /// <param name="result">The value the DialogResult property of the window should be set to.</param>
        void CloseDialog(bool result);
    }
}