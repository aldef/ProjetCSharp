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
        private GameConfig GameConfig { get; set; }
        public Player playerOne { get; set; }
        public Player playerTwo { get; set; }

        public MainPage()
        {
            InitializeComponent();
            InitGame();
        }

        private async void InitGame()
        {
            try
            {
                string configString = await ApiRepo.GetDataAsync();
                GameConfig = JsonConvert.DeserializeObject<GameConfig>(configString);
                if (GameConfig == null)
                {
                    MessageBox.Show("API NULL");
                }
                else
                {
                    Grid gameboardModel = ControlsHelper.CreateGrid(GameConfig.Lines + 1, GameConfig.Columns + 1);

                    BoatGridView.ItemsSource = GameConfig.Boats;

                    MainGrid.Children.Add(gameboardModel);
                    Grid.SetRow(gameboardModel, 0);
                    Grid.SetColumn(gameboardModel, 0);
                    Grid.SetRowSpan(gameboardModel, 2);
                    Grid.SetColumnSpan(gameboardModel, 2);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while getting data from the API: " + ex.Message);
            }
        }

        private void StartGameButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void InitPlayerOneButton_Click(object sender, RoutedEventArgs e)
        {
            var mainWindow = Window.GetWindow(this) as MainWindow;
            if (mainWindow != null)
            {
                var initPlayerPage = new InitPlayerPage(playerOne, GameConfig);
                mainWindow.MainFrame.Navigate(initPlayerPage);
            }
        }

        private void InitPlayerTwoButton_Click(object sender, RoutedEventArgs e)
        {
            var mainWindow = Window.GetWindow(this) as MainWindow;
            if (mainWindow != null)
            {
                var initPlayerPage = new InitPlayerPage(playerTwo, GameConfig);
                mainWindow.MainFrame.Navigate(initPlayerPage);
            }
        }
    }
}
