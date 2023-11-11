using System.Windows;
using System.Windows.Controls;

namespace Tower_of_Hanoi.UserControls
{
    /// <summary>
    /// Interaction logic for Podium.xaml
    /// </summary>
    public partial class Podium : UserControl
    {

        #region Global Variables

        static Podium CurrentPodiumWindow;
        public static Podium AccessiblePodiumWindow
        {
            get
            {
                if (CurrentPodiumWindow == null)
                {
                    CurrentPodiumWindow = new Podium();
                }
                return CurrentPodiumWindow;
            }
        }

        #endregion

        public Podium()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            CurrentPodiumWindow = this;

            switch (MatchStation.AccessibleMatchStationWindow.WinOrLose)
            {
                case true:
                    txtWinOrLose.Text = "You Won";
                    break;
                case false:
                    txtWinOrLose.Text = "You Lost";
                    break;
            }
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.AccessibleMainWindow.Close();
        }

    }
}
