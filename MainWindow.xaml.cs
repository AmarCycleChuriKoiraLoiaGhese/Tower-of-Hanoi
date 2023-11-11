using System.Windows;

namespace Tower_of_Hanoi
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        #region Global Variables

        static MainWindow CurrentMainWindow;
        public static MainWindow AccessibleMainWindow
        {
            get
            {
                if (CurrentMainWindow == null)
                {
                    CurrentMainWindow = new MainWindow();
                }
                return CurrentMainWindow;
            }
        }

        #endregion

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            #region Definition of this MainWindow

            CurrentMainWindow = this;

            #endregion
        }
    }
}
