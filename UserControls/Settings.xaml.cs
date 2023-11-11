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

namespace Tower_of_Hanoi.UserControls
{
    /// <summary>
    /// Interaction logic for Settings.xaml
    /// </summary>
    
    public partial class Settings : UserControl
    {

        #region Global Variables

        static Settings CurrentSettingsWindow;
        public static Settings AccessibleSettingWindow
        {
            get
            {
                if (CurrentSettingsWindow == null)
                {
                    CurrentSettingsWindow = new Settings();
                }
                return CurrentSettingsWindow;
            }
        }

        int Counter = 0;

        #endregion

        public Settings()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            #region Definition of this SettingWindow

            CurrentSettingsWindow = this;

            #endregion
        }

        private void btnContinue_Click(object sender, RoutedEventArgs e)
        {

            #region Switching on to the MatchStation

            MatchStation matchStation = MatchStation.AccessibleMatchStationWindow;
            MainWindow.AccessibleMainWindow.MainGrid.Children.Remove(this);
            MainWindow.AccessibleMainWindow.MainGrid.Children.Add(matchStation);

            #endregion

        }

        private void btnDifficultyChanger_Click(object sender, RoutedEventArgs e)
        {
            #region StringChanger

            Counter++;
            switch (Counter)
            {
                case 0:
                    lblDifficulty.Text = "Easy";
                    break;
                case 1:
                    lblDifficulty.Text = "Medium";
                    break;
                case 2:
                    lblDifficulty.Text = "Hard";
                    break;
                case 3:
                    Counter = -1;
                    btnDifficultyChanger_Click(sender, e);
                    break;
            }

            #endregion
        }
    }
}
