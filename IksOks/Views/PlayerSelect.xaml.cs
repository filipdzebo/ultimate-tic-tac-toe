using IksOks.Models;
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
using System.Windows.Shapes;

namespace IksOks.Views
{
    /// <summary>
    /// Interaction logic for PlayerSelect.xaml
    /// </summary>
    public partial class PlayerSelect : Window
    {
        public PlayerSelect()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Player playerHuman = new Player(ZNAK.O, "COVJEK");
            Player playerAI = new Player(ZNAK.X, "AI");
            MainWindow mainWindow = new MainWindow(playerHuman, playerAI);
            this.Close();
            mainWindow.Show();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Player playerHuman = new Player(ZNAK.X, "COVJEK");
            Player playerAI = new Player(ZNAK.O, "AI");
            MainWindow mainWindow = new MainWindow(playerHuman, playerAI);
            this.Close();
            mainWindow.Show();
        }
    }
}
