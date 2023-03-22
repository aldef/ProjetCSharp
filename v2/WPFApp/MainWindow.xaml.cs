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
                    Grid Gameboard = CreateGrid(GameConfig.Lines + 1, GameConfig.Columns + 1);
                    BoatGridView.ItemsSource = GameConfig.Boats;

                    SecondScreenGrid.Children.Add(Gameboard);
                    Grid.SetRow(Gameboard, 1);
                    Grid.SetColumn(Gameboard, 1);
                    Grid.SetRowSpan(Gameboard, int.MaxValue);
                    Grid.SetColumnSpan(Gameboard, int.MaxValue);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while getting data from the API: " + ex.Message);
            }
        }

        private Grid CreateGrid(int numRows, int numCols)
        {
            // grid definition
            var grid = new Grid();
            for (int i = 0; i < numRows; i++)
            {
                grid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(50, GridUnitType.Pixel) });
            }
            for (int j = 0; j < numCols; j++)
            {
                grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(50, GridUnitType.Pixel) });
            }

            // column headers
            for (int j = 0; j < numCols - 1; j++)
            {
                TextBlock colHeader = new TextBlock();
                colHeader.Text = (j + 1).ToString(); // Column numbering starts at 1
                colHeader.TextAlignment = TextAlignment.Center;
                colHeader.FontWeight = FontWeights.Bold;
                colHeader.Margin = new Thickness(0, 0, 0, 10);
                Grid.SetRow(colHeader, 0);
                Grid.SetColumn(colHeader, j + 1);
                grid.Children.Add(colHeader);
            }

            // row headers
            for (int i = 0; i < numRows - 1; i++)
            {
                TextBlock rowHeader = new TextBlock();
                rowHeader.Text = ((char)('A' + i)).ToString(); // Row lettering starts at A
                rowHeader.TextAlignment = TextAlignment.Center;
                rowHeader.FontWeight = FontWeights.Bold;
                rowHeader.Margin = new Thickness(0, 0, 10, 0);
                rowHeader.VerticalAlignment= VerticalAlignment.Center;
                Grid.SetRow(rowHeader, i + 1);
                Grid.SetColumn(rowHeader, 0);
                grid.Children.Add(rowHeader);
            }

            // buttons
            for (int i = 1; i < numRows; i++)
            {
                for (int j = 1; j < numCols; j++)
                {
                    Button button = new Button();
                    button.Content = "";
                    button.Background = new SolidColorBrush(Colors.LightBlue);
                    Grid.SetRow(button, i);
                    Grid.SetColumn(button, j);
                    grid.Children.Add(button);
                }
            }
            return grid;
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
    }
}
