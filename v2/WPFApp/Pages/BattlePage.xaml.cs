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
using WPFApp.Pages;

namespace WPFApp.Pages
{
    public partial class BattlePage : Page
    {
        private Player player { get; set; }
        private Grid playerGameGrid { get; set; }
        private Grid playerAttackGrid { get; set; }
        private Player enemy { get; set; }
        private Turn turn { get; set; }
        private bool hasPlayed { get; set; }

        public BattlePage(Player player, Player enemy)
        {
            InitializeComponent();
            this.player = player;
            this.enemy = enemy; 
            turn = new Turn(player, enemy);
            hasPlayed = false;
            InitPlayer();
        }

        private void InitPlayer()
        {
            playerGameGrid = ControlsHelper.CreateGrid(player.playerBoard);
            playerAttackGrid = ControlsHelper.CreateGrid(player.noteBoard);

            BattlePageGrid.Children.Add(playerGameGrid);
            Grid.SetRow(playerGameGrid, 0);
            Grid.SetColumn(playerGameGrid, 0);
            Grid.SetRowSpan(playerGameGrid, 2);
            Grid.SetColumnSpan(playerGameGrid, 2);
            playerGameGrid.HorizontalAlignment = HorizontalAlignment.Left;

            BattlePageGrid.Children.Add(playerAttackGrid);
            Grid.SetRow(playerAttackGrid, 0);
            Grid.SetColumn(playerAttackGrid, 2);
            Grid.SetRowSpan(playerAttackGrid, 2);
            Grid.SetColumnSpan(playerAttackGrid, 2);
            playerAttackGrid.HorizontalAlignment = HorizontalAlignment.Right;
            playerAttackGrid.Margin = new Thickness(0, 0, 50, 0);
            AddClicks(playerAttackGrid);
        }

        private void AddClicks(Grid grid)
        {
            foreach (var child in grid.Children)
            {
                if (child is Button button)
                {
                    button.Click += GridButton_Click;
                    // button.MouseRightButtonDown += GridButton_MouseRightButtonDown;
                    // right click a faire si ya le temps pour mettre des notes
                }
            }
        }

        private void GridButton_Click(object sender, RoutedEventArgs e)
        {
            if (!hasPlayed) 
            {
                hasPlayed = true;
                Button clickedButton = (Button)sender;
                int row = Grid.GetRow(clickedButton);
                int col = Grid.GetColumn(clickedButton);

                // check if hit or miss using the enemy.
                Coordinate strike = new Coordinate(row - 1, col - 1);
                if (turn.DoStrike(strike))
                {
                    
                    if (turn.DmgBoat(strike))
                    {
                        MessageBox.Show("Boat was sunk !!");
                    }                   
                }
                ControlsHelper.RefreshGrid(playerAttackGrid, player.noteBoard);

                CheckForWin();
            }
            else
            {
                MessageBox.Show("You've already played this turn ! \n " +
                    "the game will now proceed to the next player's turn");

                // close this and go to transition page
                GoNextTurn();
            }
        }

        private void FinishTurnButton_Click(object sender, RoutedEventArgs e)
        {
            if (hasPlayed)
            {
                GoNextTurn();
            }
            else 
            {
                MessageBox.Show("Please play your turn first");
            }
        }

        private void CheckForWin() 
        {
            if (enemy.boats.All(boat => !boat.isAlive()))
            {
                MessageBox.Show(player.name + " Has Won !!!");
                NavigationService.Navigate(new Uri("MainWindow.xaml", UriKind.Relative));
                
            }
        }

        private void GoNextTurn() 
        {
            if (enemy.boats.Any(boat => boat.isAlive()))
            {

                var battleWindow = Window.GetWindow(this) as BattleWindow;
                if (battleWindow != null)
                {
                    var transitionPage = new TransitionPage(enemy, player);
                    battleWindow.MainFrame.Navigate(transitionPage);
                }

            }
        }
    }
}


