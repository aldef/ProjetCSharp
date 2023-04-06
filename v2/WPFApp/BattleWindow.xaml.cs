using GameLogic.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
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
    /// Logique d'interaction pour BattleWindow.xaml
    /// </summary>
    public partial class BattleWindow : Window
    {
        private Player playerOne { get; set; }
        private Player playerTwo { get; set; }
        public Frame MainFrame { get; } = new Frame();

        public BattleWindow(Player PlayerOne, Player PlayertTwo)
        {
            InitializeComponent();
            this.playerOne = PlayerOne;
            this.playerTwo = PlayertTwo;
            MainGrid.Children.Add(MainFrame);
            MainFrame.NavigationUIVisibility = NavigationUIVisibility.Hidden;

            var transitionPage = new TransitionPage(playerOne, playerTwo);
            MainFrame.Navigate(transitionPage);

            //MainFrame.SetValue(Grid.RowSpanProperty, 3);
            //MainFrame.SetValue(Grid.ColumnSpanProperty, 4);
        }

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);

            MainWindow mainWindow = Application.Current.MainWindow as MainWindow;
            if (mainWindow != null)
            {
                mainWindow.Visibility = Visibility.Visible;   
                mainWindow.Show();               
            }
        }

        private void FinishTurnButton_Click(object sender, RoutedEventArgs e)
        {
            var battlePage = new BattlePage(playerOne, playerTwo);
            MainFrame.Navigate(battlePage);
        }
    }
}
