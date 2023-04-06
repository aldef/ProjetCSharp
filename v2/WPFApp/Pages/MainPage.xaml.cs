using API;
using GameLogic.Models;
using Newtonsoft.Json;
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
using WPFApp.Pages;

namespace WPFApp
{
    /// <summary>
    /// Logique d'interaction pour MainPage.xaml
    /// </summary>
    public partial class MainPage : Page
    {
        private GameConfig gameConfig { get; set; }
        private Player playerOne { get; set; }
        private Player playerTwo { get; set; }

        public MainPage()
        {
            InitializeComponent();
            Loaded += MainPage_Loaded;

        }

        private void GetPlayers() 
        {
            var mainWindow = Window.GetWindow(this) as MainWindow;
            if (mainWindow != null)
            {
                playerOne = mainWindow.PlayerOne;
                playerTwo = mainWindow.PlayerTwo;
            }
        }

        private void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            var mainWindow = Window.GetWindow(this) as MainWindow;
            if (mainWindow != null)
            {
                gameConfig = mainWindow.GameConfig;
                
                Grid gameboardModel = ControlsHelper.CreateGrid(gameConfig.Lines + 1, gameConfig.Columns + 1);
                BoatGridView.ItemsSource = gameConfig.Boats;
                MainGrid.Children.Add(gameboardModel);
                Grid.SetRow(gameboardModel, 0);
                Grid.SetColumn(gameboardModel, 0);
                Grid.SetRowSpan(gameboardModel, 2);
                Grid.SetColumnSpan(gameboardModel, 2);
            }
            GetPlayers();
            playerOneNameLabel.Content = playerOne != null ? playerOne.name : "";
        }

        private void StartGameButton_Click(object sender, RoutedEventArgs e)
        {
            //// check if both players are ready
            //if ((playerOne.boats == null) || (playerTwo.boats == null)) 
            //{
            //    MessageBox.Show("Both players need to have put down all of their boats in order to start a game");
            //    return;
            //}
            //if ((playerOne.boats.Any(boat => !boat.isPlaced)) && (playerTwo.boats.Any(boat => !boat.isPlaced)))
            //{
            //    MessageBox.Show("Both players need to have put down all of their boats in order to start a game");
            //    return;
            //}

            // Create a new instance of BattleWindow
            BattleWindow battleWindow = new BattleWindow(playerOne, playerTwo);

            var mainWindow = Window.GetWindow(this) as MainWindow;
            if (mainWindow != null)
            {
                mainWindow.Visibility = Visibility.Collapsed; 
            }

            battleWindow.Show();
        }

        private void InitPlayerOneButton_Click(object sender, RoutedEventArgs e)
        {
            var mainWindow = Window.GetWindow(this) as MainWindow;
            if (mainWindow != null)
            {
                var initPlayerPage = new InitPlayerPage(true, gameConfig);
                mainWindow.MainFrame.Navigate(initPlayerPage);
            }
        }

        private void InitPlayerTwoButton_Click(object sender, RoutedEventArgs e)
        {
            var mainWindow = Window.GetWindow(this) as MainWindow;
            if (mainWindow != null)
            {
                var initPlayerPage = new InitPlayerPage(false, gameConfig);
                mainWindow.MainFrame.Navigate(initPlayerPage);
            }
        }
    }
}
