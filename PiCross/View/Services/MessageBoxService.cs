using ViewModel.Interfaces;

namespace View.Services
{
    public class MessageBoxService : IMessageBoxService
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
        public MessageBoxResult Show(object owner = null, string text = "", string caption = "", MessageBoxButtons buttons = MessageBoxButtons.OK, MessageBoxImage icon = MessageBoxImage.None, MessageBoxResult defaultResult = MessageBoxResult.OK)
        {
            var w = owner as System.Windows.Window;
            if (w == null)
            {
                return (MessageBoxResult)System.Windows.MessageBox.Show(text, caption, (System.Windows.MessageBoxButton)buttons, (System.Windows.MessageBoxImage)icon, (System.Windows.MessageBoxResult)defaultResult);
            }
            else
            {
                return (MessageBoxResult)System.Windows.MessageBox.Show(w, text, caption, (System.Windows.MessageBoxButton)buttons, (System.Windows.MessageBoxImage)icon, (System.Windows.MessageBoxResult)defaultResult);
            }
        }
    }
}