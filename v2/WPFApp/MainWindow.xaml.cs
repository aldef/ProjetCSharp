using API;
using GameLogic.Models;
using Newtonsoft.Json;
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
using static System.Net.Mime.MediaTypeNames;

namespace WPFApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private GameConfig GameConfig { get; set; }
        public MainWindow()
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

                    SecondScreenGrid.Children.Add(gameboardModel);
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

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }

        private void Start_Click(object sender, RoutedEventArgs e)
        {
            MainScreenGrid.Visibility = Visibility.Visible;
            SecondScreenGrid.Visibility = Visibility.Collapsed;
        }

        private void playButton_Click(object sender, RoutedEventArgs e)
        {
            MainScreenGrid.Visibility = Visibility.Collapsed;
            SecondScreenGrid.Visibility = Visibility.Visible;
        }

        private void Start_Click_1(object sender, RoutedEventArgs e)
        {

        }
    }
}
