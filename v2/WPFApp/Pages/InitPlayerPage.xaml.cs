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

namespace WPFApp.Pages
{
    /// <summary>
    /// Logique d'interaction pour InitPlayerPage.xaml
    /// </summary>
    public partial class InitPlayerPage : Page
    {
        Player player = null;
        GameConfig gameConfig = null;
        public InitPlayerPage(Player player, GameConfig gameConfig)
        {
            InitializeComponent();
            this.player = player;
            this.gameConfig = gameConfig;
        }

        
    }
}
