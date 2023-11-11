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
    /// Interaction logic for MatchStation.xaml
    /// </summary>

    public partial class MatchStation : UserControl
    {

        #region Global Variables

        static MatchStation CurrentMatchStationWindow;
        public static MatchStation AccessibleMatchStationWindow
        {
            get
            {
                if (CurrentMatchStationWindow == null)
                {
                    CurrentMatchStationWindow = new MatchStation();
                }
                return CurrentMatchStationWindow;
            }
        }

        int[] FirstStack, SecondStack, ThirdStack;
        int FirstStackPointer, SecondStackPointer, ThirdStackPointer, StackSize = 3;

        #endregion

        public MatchStation()
        {
            InitializeComponent();
        }

        #region Making Rectangles

        /// <summary>
        /// Loops through the Canvas' StackPanels to disable the 'PreviewMouseLeftButtonDown' trigger event.
        /// Creates a new Rectangle.
        /// </summary>
        /// 
        /// <param name="RectanglesWidth">
        /// The desired width of rectangle is input as a parameter.
        /// </param>
        /// 
        /// <returns>
        /// Returns a rectangle.
        /// </returns>

        private Rectangle RectangleMaker(int RectanglesWidth)
        {
            StackPanel JustAStackPanel;

            foreach (UIElement Object in TowersContainer.Children)
            {
                if (Object is StackPanel)
                {
                    JustAStackPanel = (StackPanel)Object;
                    foreach (Rectangle Commonrectangle in JustAStackPanel.Children)
                    {
                        Commonrectangle.PreviewMouseLeftButtonDown -= Rectangle_MouseLeftButtonDown;
                    }
                }
            }

            Rectangle rectangle = new Rectangle
            {
                Fill = Brushes.White,
                Height = 31.25,
                Width = RectanglesWidth,
                RadiusX = 10,
                RadiusY = 10,
                StrokeThickness = 5,
                Stroke = new SolidColorBrush(Color.FromRgb(135, 200, 132)),
                AllowDrop = true
            };

            rectangle.PreviewMouseLeftButtonDown += Rectangle_MouseLeftButtonDown;

            return rectangle;
        }

        #endregion

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            #region Variables

            int Accumulator = 300;

            #endregion

            #region Definition of this current window

            CurrentMatchStationWindow = this;

            #endregion

            #region Initial Difficulty

            /// <summary>
            /// Based on the on the difficult level set on the previous 'slide', a specific number of rectangles are being created through the 'RectangleMaker()' subroutine.
            /// 
            /// </summary>

            FirstStackPanel.Children.Add(RectangleMaker(300));
            FirstStackPanel.Children.Add(RectangleMaker(275));
            FirstStackPanel.Children.Add(RectangleMaker(250));

            switch (Settings.AccessibleSettingWindow.lblDifficulty.Text)
            {
                case "Normal":

                    StackSize = 5;

                    FirstStackPanel.Children.Add(RectangleMaker(225));
                    FirstStackPanel.Children.Add(RectangleMaker(200));
                    FirstStackPanel.Children.Add(RectangleMaker(175));

                    break;

                case "Hard":

                    StackSize = 8;

                    FirstStackPanel.Children.Add(RectangleMaker(225));
                    FirstStackPanel.Children.Add(RectangleMaker(200));
                    FirstStackPanel.Children.Add(RectangleMaker(175));
                    FirstStackPanel.Children.Add(RectangleMaker(150));
                    FirstStackPanel.Children.Add(RectangleMaker(125));
                    FirstStackPanel.Children.Add(RectangleMaker(100));

                    break;

            }

            #region Stacks' settlement

            /// <summary>
            /// Declaring the stacks.
            /// Assigning the stacks.
            /// </summary>

            FirstStack = new int[StackSize];
            SecondStack = new int[StackSize];
            ThirdStack = new int[StackSize];

            FirstStackPointer = 0;
            SecondStackPointer = 0;
            ThirdStackPointer = 0;

            #endregion

            /// <summary>
            /// Fills the FirstStack with the respective values.
            /// </summary>

            while (FirstStackPointer != StackSize)
            {
                FirstStack[FirstStackPointer] = Accumulator;
                Accumulator -= 25;

                FirstStackPointer++;
            }

            #endregion

        }

        #region Drag and Drop

        private void StackPanel_Drop(object sender, DragEventArgs e)
        {
            #region Variables

            Rectangle rectangle;
            StackPanel stackPanel;
            StackPanel stackPanel1;
            StackPanel panel;

            #endregion

            #region Assignment

            /// <summary>
            /// rectangle is the Rectangle being dragged and dropped.
            /// stackPanel is the Stackpanel from which the Rectangle comes from.
            /// stackPanel1 is the StackPanel in which the Rectangle is being dropped into.
            /// </summary>

            rectangle = (Rectangle)e.Data.GetData(typeof(Rectangle));
            stackPanel = (StackPanel)rectangle.Parent;
            stackPanel1 = (StackPanel)sender;

            #endregion

            #region Switch between StackPanels

            /// <summary>
            /// Removes the Rectangle from the source StackPanel.
            /// Adds the Rectangle onto the StackPanel in which it dropped at.
            /// Checks which StackPanel the Rectangle is dropped at and Adds the value of the Rectangle's width onto the correspective stack.
            /// Loops thorough the StackPanel of the Canvcas to disable the 'PreviewMouseLeftButtonDown' trigger event and applying this event to the Rectangle to the top.
            /// Calls the subroutine 'Checker()'.
            /// </summary>

            stackPanel.Children.Remove(rectangle);
            stackPanel1.Children.Add(rectangle);

            if (stackPanel1 == FirstStackPanel)
            {
                FirstStack[FirstStackPointer] = (int)rectangle.Width;
                FirstStackPointer++;
            }
            else if (stackPanel1 == SecondStackPanel)
            {
                SecondStack[SecondStackPointer] = (int)rectangle.Width;
                SecondStackPointer++;
            }
            else if (stackPanel1 == ThirdStackPanel)
            {
                SecondStack[SecondStackPointer] = (int)rectangle.Width;
                ThirdStackPointer++;
            }

            foreach (UIElement Object in TowersContainer.Children)
            {
                int Count;
                if (Object is StackPanel)
                {
                    panel = (StackPanel)Object;
                    foreach (Rectangle LoopingRect in panel.Children)
                    {
                        LoopingRect.PreviewMouseLeftButtonDown -= Rectangle_MouseLeftButtonDown;
                    }

                    Count = panel.Children.Count;
                    switch (Count)
                    {

                        case 0:
                            break;

                        default:

                            rectangle = (Rectangle)panel.Children[Count - 1];
                            rectangle.PreviewMouseLeftButtonDown += Rectangle_MouseLeftButtonDown;

                            break;
                    }
                }
            }

            Checker();

            #endregion
        }

        private void Rectangle_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            #region Variables

            Rectangle r;
            DataObject dataObject;
            StackPanel ParentStackPanel;

            #endregion

            #region Assignment

            r = (Rectangle)sender;
            dataObject = new DataObject(r);
            ParentStackPanel = (StackPanel)r.Parent;

            #endregion

            #region Actual Shift 

            /// <summary>
            /// When mouse click on a Rectangle, value on the respective Stack is popped.
            /// DragDrop operation occurs.
            /// </summary>

            if (ParentStackPanel == FirstStackPanel)
            {
                FirstStackPointer--;
                FirstStack[FirstStackPointer] = 0;
            }
            else if (ParentStackPanel == SecondStackPanel)
            {
                SecondStackPointer--;
                SecondStack[SecondStackPointer] = 0;
            }
            else if (ParentStackPanel == ThirdStackPanel)
            {
                ThirdStackPointer--;
                ThirdStack[ThirdStackPointer] = 0;
            }

            DragDrop.DoDragDrop(r.Parent, dataObject, DragDropEffects.Move);

            #endregion
        }

        #endregion

        #region WinOrLose

        /// <summary>
        /// Check whether the player has won or not. 
        /// </summary>

        public bool WinOrLose; 

        private void Checker()
        {
            #region Variable

            Podium WinWindow;

            #endregion

            #region Looper and Checker
            
            if (FirstStackPointer >= 2)
            {
                if (FirstStack[FirstStackPointer - 1] > FirstStack[FirstStackPointer - 2])
                {
                    WinWindow = Podium.AccessiblePodiumWindow;
                    MainWindow.AccessibleMainWindow.MainGrid.Children.Add(WinWindow);
                    WinOrLose = false;
                }
                else if (FirstStackPointer == StackSize)
                {
                    WinWindow = Podium.AccessiblePodiumWindow;
                    MainWindow.AccessibleMainWindow.MainGrid.Children.Add(WinWindow);
                    WinOrLose = true;
                }
            }

            if (SecondStackPointer >= 2)
            {
                if (SecondStack[SecondStackPointer - 1] > SecondStack[SecondStackPointer - 2])
                {
                    WinWindow = Podium.AccessiblePodiumWindow;
                    MainWindow.AccessibleMainWindow.MainGrid.Children.Add(WinWindow);
                    WinOrLose = false;
                }
                else if (SecondStackPointer == StackSize)
                {
                    WinWindow = Podium.AccessiblePodiumWindow;
                    MainWindow.AccessibleMainWindow.MainGrid.Children.Add(WinWindow);
                    WinOrLose = true;
                }
            }

            if (ThirdStackPointer >= 2)
            {
                if (ThirdStack[ThirdStackPointer - 1] > ThirdStack[ThirdStackPointer - 2])
                {
                    WinWindow = Podium.AccessiblePodiumWindow;
                    MainWindow.AccessibleMainWindow.MainGrid.Children.Add(WinWindow);
                    WinOrLose = false;
                }
                else if (ThirdStackPointer == StackSize)
                {
                    WinWindow = Podium.AccessiblePodiumWindow;
                    MainWindow.AccessibleMainWindow.MainGrid.Children.Add(WinWindow);
                    WinOrLose = true;
                }
            }

            #endregion
        }

        #endregion

    }
}
