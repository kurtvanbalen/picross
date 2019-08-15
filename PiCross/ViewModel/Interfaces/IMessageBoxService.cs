using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel.Interfaces
{
    /// <summary>
    /// Displays a message box in front of the specified window (if specified). The message box can display a message, title bar caption, 
    /// button, and icon; and accepts a default message box result, and returns a result.
    /// </summary>
    /// <param name="owner">An object that represents the owner window of the message box.</param>
    /// <param name="text">A String that specifies the text to display.</param>
    /// <param name="caption">A String that specifies the title bar caption to display.</param>
    /// <param name="buttons">A MessageBoxButton value that specifies which button or buttons to display.</param>
    /// <param name="icon">A MessageBoxImage value that specifies the icon to display.</param>
    /// <param name="defaultResult">A MessageBoxResult value that specifies the default result of the message box.</param>
    /// <returns>A MessageBoxResult value that specifies which message box button is clicked by the user.</returns>
    /// <remarks>The owner object must be an object returned by the ShowWindow function of an IWindowService instance. If the owner is null, the message box appears in front of the window that is currently active.</remarks>
    public interface IMessageBoxService
    {
        MessageBoxResult Show(
                object owner = null,
                string text = "",
                string caption = "",
                MessageBoxButtons buttons = MessageBoxButtons.OK,
                MessageBoxImage icon = MessageBoxImage.None,
                MessageBoxResult defaultResult = MessageBoxResult.OK
            );
    }

    /// <summary>
    /// Specifies the buttons that are displayed on a message box. Used as an argument of the Show method.
    /// </summary>
    public enum MessageBoxButtons
    {
        /// <summary>
        /// The message box displays an OK button.
        /// </summary>
        OK = 0,
        /// <summary>
        /// The message box displays OK and Cancel buttons.
        /// </summary>
        OKCancel = 1,
        /// <summary>
        /// The message box displays Yes, No, and Cancel buttons.
        /// </summary>
        YesNoCancel = 3,
        /// <summary>
        /// The message box displays Yes and No buttons.
        /// </summary>
        YesNo = 4
    }
    /// <summary>
    /// Specifies which message box button that a user clicks. MessageBoxResult is returned by the Show method.
    /// </summary>
    public enum MessageBoxResult
    {
        /// <summary>
        /// The result value of the message box is OK.
        /// </summary>
        OK = 1,
        /// <summary>
        /// The result value of the message box is Cancel.
        /// </summary>
        Cancel = 2,
        /// <summary>
        /// The result value of the message box is Yes.
        /// </summary>
        Yes = 6,
        /// <summary>
        /// The result value of the message box is No.
        /// </summary>
        No = 7
    }
    /// <summary>
    /// Specifies the icon that is displayed by a message box.
    /// </summary>
    public enum MessageBoxImage
    {
        /// <summary>
        /// No icon is displayed.
        /// </summary>
        None = 0,
        /// <summary>
        /// The message box contains a symbol consisting of white X in a circle with a red background.
        /// </summary>
        Error = 16,
        /// <summary>
        /// The message box contains a symbol consisting of a question mark in a circle.
        /// </summary>
        Question = 32,
        /// <summary>
        /// The message box contains a symbol consisting of an exclamation point in a triangle with a yellow background.
        /// </summary>
        Warning = 48,
        /// <summary>
        /// The message box contains a symbol consisting of a lowercase letter i in a circle.
        /// </summary>
        Information = 64
    }
}