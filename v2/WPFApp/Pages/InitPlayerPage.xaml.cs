using GameLogic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
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
        private GameConfig gameConfig = null;
        private Gameboard playerGameboard = null;
        private Grid gameboardModelGrid = null;
        private List<Boat> playerBoats = null;
        private bool player;
        public InitPlayerPage(bool player, GameConfig gameConfig)
        {
            InitializeComponent();
            this.player = player;
            this.gameConfig = gameConfig;
            Init();

            // a supprimer
            playerBoats.RemoveRange(0, 4);

        }

        private void finishButton_Click(object sender, RoutedEventArgs e)
        {
            if (playerBoats.Any(boat => !boat.isPlaced)) 
            {
                MessageBox.Show("You must place all boats to keep going");
                return; 
            }

            var mainWindow = Window.GetWindow(this) as MainWindow;
            if (mainWindow != null)
            {
                if (player)
                {
                    mainWindow.PlayerOne.playerBoard = playerGameboard;
                    mainWindow.PlayerOne.noteBoard = new Gameboard(gameConfig.Lines, gameConfig.Columns);
                    mainWindow.PlayerOne.boats = playerBoats;
                    mainWindow.PlayerOne.name = playerNameTextBox.Text == "" ? "Player 1" : playerNameTextBox.Text;

                    mainWindow.MainFrame.Navigate(new Uri("Pages/MainPage.xaml", UriKind.Relative));
                }
                else 
                {
                    mainWindow.PlayerTwo.playerBoard = playerGameboard;
                    mainWindow.PlayerTwo.noteBoard = new Gameboard(gameConfig.Lines, gameConfig.Columns);
                    mainWindow.PlayerTwo.boats = playerBoats;
                    mainWindow.PlayerTwo.name = playerNameTextBox.Text == "" ? "Player 2" : playerNameTextBox.Text;

                    mainWindow.MainFrame.Navigate(new Uri("Pages/MainPage.xaml", UriKind.Relative));
                }
            }
        }
        private void Init()
        {
            gameboardModelGrid = ControlsHelper.CreateGrid(gameConfig.Lines + 1, gameConfig.Columns + 1);
            playerBoats = Helper.DuplicateBoatList(gameConfig.Boats.Where(boat => !boat.isPlaced).ToList());

            // add right / left click event to allow boats to be placed
            foreach (var child in gameboardModelGrid.Children)
            {
                if (child is Button button)
                {
                    button.Click += GridButton_Click;
                    button.MouseRightButtonDown += GridButton_MouseRightButtonDown;
                }
            }

            playerGameboard = new Gameboard(gameConfig.Lines, gameConfig.Columns);
            // bind datasource
            BoatListView.ItemsSource = playerBoats.Where(boat => !boat.isPlaced).ToList();
            MainGrid.Children.Add(gameboardModelGrid);
            Grid.SetRow(gameboardModelGrid, 0);
            Grid.SetColumn(gameboardModelGrid, 0);
            Grid.SetRowSpan(gameboardModelGrid, 2);
            Grid.SetColumnSpan(gameboardModelGrid, 2);
        }

        private void placeBoatButton_Click(object sender, RoutedEventArgs e)
        {
            if ((BoatListView.SelectedItem == null) || (startPositionLabel.Content == "") ||
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
            Coordinate endPos = new Coordinate(endX - 1, endY - 1);

            Boat selectedBoat = (Boat)BoatListView.SelectedItem;

            if (playerGameboard.PlaceBoat(selectedBoat, startPos, endPos))
            {
                // remove the boat from unplaced ones + rebind datasource + refresh grid
                selectedBoat.isPlaced = true;
                BoatListView.ItemsSource = playerBoats.Where(boat => !boat.isPlaced).ToList();
                ControlsHelper.RefreshGrid(gameboardModelGrid, playerGameboard);
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
    }
}
