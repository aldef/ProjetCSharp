using GameLogic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
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
using System.Windows.Threading;

namespace WPFApp.Pages
{
    /// <summary>
    /// Logique d'interaction pour TransitionPage.xaml
    /// </summary>
    public partial class TransitionPage : Page
    {
        private int countDown { get; set; }
        private DispatcherTimer timer { get; set; }
        private Player playerOne { get; set; }
        private Player playerTwo { get; set; }

    public TransitionPage(Player playerOne, Player playerTwo)
        {
            InitializeComponent();
            this.playerOne = playerOne;
            this.playerTwo = playerTwo;
            countDown = 5;
            StartCountdown();

        }

        private void StartCountdown()
        {
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            countDown--;
            if (countDown == 0)
            {
                timer.Stop();
                // go to BattlePage

                var battleWindow = Window.GetWindow(this) as BattleWindow;
                if (battleWindow != null) 
                {
                    var battlePage = new BattlePage(playerOne, playerTwo);
                    battleWindow.MainFrame.Navigate(battlePage);
                }

            }
            else
            {
                transitionLabel.Content = $"{playerOne.name}'s turn in " + countDown.ToString();
            }
        }
    }
}
