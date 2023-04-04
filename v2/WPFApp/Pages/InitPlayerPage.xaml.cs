using GameLogic.Models;
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

namespace WPFApp.Pages
{
    /// <summary>
    /// Logique d'interaction pour InitPlayerPage.xaml
    /// </summary>
    public partial class InitPlayerPage : Page
    {
        private Player player = null;
        private GameConfig gameConfig = null;
        private Gameboard playerGameboard = null;
        public InitPlayerPage(Player player, GameConfig gameConfig)
        {
            InitializeComponent();
            this.player = player;
            this.gameConfig = gameConfig;
            test();
        }

        private void returnButton_Click(object sender, RoutedEventArgs e)
        {
            var mainWindow = Window.GetWindow(this) as MainWindow;
            if (mainWindow != null)
            {
                mainWindow.MainFrame.Navigate(new Uri("Pages/MainPage.xaml", UriKind.Relative));
            }
        }

        private void test() 
        {

            Grid gameboardModel = ControlsHelper.CreateGrid(gameConfig.Lines + 1, gameConfig.Columns + 1);

            foreach (var child in gameboardModel.Children)
            {
                if (child is Button button)
                {
                    button.Click += GridButton_Click;
                    button.MouseRightButtonDown += GridButton_MouseRightButtonDown;

                }
            }

            playerGameboard = new Gameboard(gameConfig.Lines, gameConfig.Columns);
            // bind datasource
            BoatGridView.ItemsSource = gameConfig.Boats.Where(boat => !boat.isPlaced).ToList();
            MainGrid.Children.Add(gameboardModel);
            Grid.SetRow(gameboardModel, 0);
            Grid.SetColumn(gameboardModel, 0);
            Grid.SetRowSpan(gameboardModel, 2);
            Grid.SetColumnSpan(gameboardModel, 2);
        }

        private void placeBoatButton_Click(object sender, RoutedEventArgs e)
        {
            if ((BoatGridView.SelectedItem == null) || (startPositionLabel.Content == "") ||
                (endPositionLabel.Content == "")) { return; }

            string startStr = (string)startPositionLabel.Content;
            string startRowStr = startStr.Substring(0, 1);
            string startColStr = startStr.Substring(1);
            int.TryParse(startColStr, out int startCol);
            char.TryParse(startRowStr.ToUpper(), out char startLine);    
            int startY = startCol;
            int startX = (int)startLine - (int)'A' + 1;
            Coordinate startPos = new Coordinate(startX - 1, startY - 1);

            string endStr = (string)endPositionLabel.Content;
            string endRowStr = endStr.Substring(0, 1);
            string endColStr = endStr.Substring(1);
            int.TryParse(endColStr, out int endCol);
            char.TryParse(endRowStr.ToUpper(), out char endLine);
            int endY = endCol;
            int endX = (int)endLine - (int)'A' + 1;
            Coordinate endPos = new Coordinate(endX -1, endY -1);

            Boat selectedBoat = (Boat)BoatGridView.SelectedItem;

            if (playerGameboard.PlaceBoat(selectedBoat, startPos, endPos))
            {
                // remove the boat from unplaced ones + rebind datasource
                selectedBoat.isPlaced= true;
                BoatGridView.ItemsSource = gameConfig.Boats.Where(boat => !boat.isPlaced).ToList();
            }
            else 
            {
                MessageBox.Show("This boat doesnt fit here !");
            }


        }

        private void GridButton_Click(object sender, RoutedEventArgs e)
        {
            Button clickedButton = (Button)sender;
            int row = Grid.GetRow(clickedButton);
            int col = Grid.GetColumn(clickedButton);
            startPositionLabel.Content = $"{(char)('A' + row - 1)}{col}";
        }

        private void GridButton_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            Button clickedButton = (Button)sender;
            int row = Grid.GetRow(clickedButton);
            int col = Grid.GetColumn(clickedButton);
            endPositionLabel.Content = $"{(char)('A' + row - 1)}{col}";
        }


        //private void CreatePlayer() 
        //{
        //    player = new Player();
        //}

    }
}
