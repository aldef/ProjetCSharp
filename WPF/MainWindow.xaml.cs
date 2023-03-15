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

namespace WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            GameConfig gameConfig = null;

            try
            {
                string configString = ApiRepo.GetDataAsync().GetAwaiter().GetResult();
                gameConfig = JsonConvert.DeserializeObject<GameConfig>(configString);

                if (gameConfig == null)
                {
                    //Console.Error.WriteLine($"gameConfig is Null");
                    Environment.Exit(1); // No config no game 
                }
            }
            catch (Exception ex)
            {
               // Console.Error.WriteLine($"An error occurred: {ex.Message}");
                Environment.Exit(1); // No config no game 
            }

            // For tests, delete later
            gameConfig.Boats.RemoveRange(0, 3);

            // 2- Create Players
            //string playerOneName = UserInputs.AskPlayerName("player one");
            //string playerTwoName = UserInputs.AskPlayerName("player two");

            Player playerOne = new Player(Helper.DuplicateBoatList(gameConfig.Boats),
                new Gameboard(gameConfig.Lines, gameConfig.Columns),
                new Gameboard(gameConfig.Lines, gameConfig.Columns), "player1");

            Player playerTwo = new Player(Helper.DuplicateBoatList(gameConfig.Boats),
                new Gameboard(gameConfig.Lines, gameConfig.Columns),
                new Gameboard(gameConfig.Lines, gameConfig.Columns), "player2");



        }
    }
}
