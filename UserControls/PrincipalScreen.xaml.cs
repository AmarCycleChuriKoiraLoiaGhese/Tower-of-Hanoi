using System.Windows;
using System.Windows.Controls;

namespace Tower_of_Hanoi.UserControls
{
    /// <summary>
    /// Interaction logic for PrincipalScreen.xaml
    /// </summary>
    
    public partial class PrincipalScreen : UserControl
    {
        public PrincipalScreen()
        {
            InitializeComponent();
        }

        private void btnPlay_Click(object sender, RoutedEventArgs e)
        {
            #region Switching on to the next window

            Settings Setting = Settings.AccessibleSettingWindow;
            MainWindow.AccessibleMainWindow.MainGrid.Children.Remove(this);
            MainWindow.AccessibleMainWindow.MainGrid.Children.Add(Setting);

            #endregion
        }

    }
}
