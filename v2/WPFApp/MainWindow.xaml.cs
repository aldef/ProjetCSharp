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
using WPFApp.Pages;
using static System.Net.Mime.MediaTypeNames;

namespace WPFApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public GameConfig GameConfig { get; set; }
        public Player PlayerOne { get; set; }
        public Player PlayerTwo { get; set; }
        public MainWindow()
        {
            InitializeComponent();
            InitAsync();
            PlayerOne = new Player();
            PlayerTwo = new Player();
        }

        private async void InitAsync()
        {
            await GetConfig();
            MainGrid.Children.Add(MainFrame);
            MainFrame.NavigationUIVisibility = NavigationUIVisibility.Hidden;
            MainFrame.Navigate(new Uri("Pages/OpenPage.xaml", UriKind.Relative));
        }

        private async Task GetConfig()
        {
            try
            {
                string configString = await ApiRepo.GetDataAsync();
                GameConfig = JsonConvert.DeserializeObject<GameConfig>(configString);
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while getting data from the API: " + ex.Message);
                System.Windows.Application.Current.Shutdown();
            }
        }

        public Frame MainFrame { get; } = new Frame();

    }
}

