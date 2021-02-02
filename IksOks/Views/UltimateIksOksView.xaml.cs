using IksOks.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel; 
using System.Linq;
using System.Text;
using System.Threading;
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
using static IksOks.Models.IksOksIgra;

namespace IksOks.Views
{
    /// <summary>
    /// Interaction logic for UltimateIksOksView.xaml
    /// </summary>
    public partial class UltimateIksOksView : UserControl
    {
        private static SolidColorBrush brushActive = new SolidColorBrush( Colors.LightBlue);
        private static SolidColorBrush brushInactive = new SolidColorBrush( Colors.White);

        public static UltimateIksOks Igra { get; internal set; }
        public List<IksOksView> views = new List<IksOksView>();
        private static Minimax minimax = new Minimax();
        private static UltimateIksOksView thisView = null;
        private BackgroundWorker backgroundWorker;
        public UltimateIksOksView(UltimateIksOks igra)
        {
            InitializeComponent();
             Igra = igra;

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                { 
                    IksOksView view = new IksOksView(igra.igre[i,j]);
                    Grid.SetColumn(view, i);
                    Grid.SetRow(view, j);
                    view.MouseDoubleClick += View_MouseDoubleClick;
                    view.Margin = new Thickness(20, 20, 20, 20);
                    gridIgara.Children.Add(view);
                    views.Add(view);
                }
            }
            Refresh();
            thisView = this;
        }

        private void View_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        { 
            Refresh();
        }

        private void Refresh()
        {
            foreach(var view in views)
            {
                view.Refresh();
                if (Igra.NextIgraToBePlayed != null)
                {
                    view.gridMjestoView.Opacity = Igra.NextIgraToBePlayed == view.igra ? 1 : 0.5;
                    view.gridMjestoView.Background = Igra.NextIgraToBePlayed == view.igra ? brushActive : brushInactive;
                    view.gridMjestoView.IsEnabled = Igra.NextIgraToBePlayed == view.igra ? true : false;

                }
                else
                {
                    view.gridMjestoView.Opacity = 1;
                    view.gridMjestoView.Background = brushActive;
                    view.gridMjestoView.IsEnabled =  true  ;
                }
            }
            lblNextPlayer.Content = "Next player: " + Igra.PlayerPlaying.Name;
            if (Igra.Pobjednik != null)
            {
                this.IsEnabled = false;
                lblNextPlayer.Content = "Winner is " + Igra.Pobjednik.ToString();
                progressBar.IsIndeterminate = false;
                MessageBox.Show("Pobjedio je " + Igra.Pobjednik.ToString());
                if (this.Parent is MainWindow)
                {
                    PlayerSelect ps = new PlayerSelect();
                    ((MainWindow)this.Parent).Close();
                    ps.Show();
                }
            }
            else if (Igra.DostupnaMjesta.Count == 0)
            {
                this.IsEnabled = false;
                progressBar.IsIndeterminate = false;
                MessageBox.Show("Igra je neriješena!");
                if (this.Parent is MainWindow)
                {
                    PlayerSelect ps = new PlayerSelect();
                    ((MainWindow)this.Parent).Close();
                    ps.Show();
                }
            }
            else
            {
                this.IsEnabled = (Igra.PlayerPlaying != Igra.PlayerAI);
                progressBar.IsIndeterminate = (Igra.PlayerPlaying == Igra.PlayerAI);
                progressBar.Visibility = (Igra.PlayerPlaying == Igra.PlayerAI) ? Visibility.Visible : Visibility.Hidden;
                if (Igra.PlayerPlaying == Igra.PlayerAI)
                {
                    narediDaAIIgra();
                }
            }
            lblPosjeceno.Content = String.Format("Posjeceno heuristika {0} cvorova u {1} sekundi", minimax.Posjeceno, minimax.vrijemeRacunanja.TotalSeconds);
        }

        
        private void narediDaAIIgra()
        {
            backgroundWorker = new BackgroundWorker();
            backgroundWorker.DoWork += BackgroundWorker_DoWork;
            backgroundWorker.RunWorkerCompleted += BackgroundWorker_RunWorkerCompleted;
            progressBar.IsIndeterminate = true;
            lblPosjeceno.Content = String.Format("AI PRERACUNAVA");
            progressBar.Visibility =  Visibility.Visible   ;
            backgroundWorker.RunWorkerAsync();
        }

        private void BackgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            Refresh();
        }

        private void BackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            minimax.doMinimax(Igra, 0, Igra.PlayerAI, -1000000000, 1000000000);
            Igra.MakeMove(minimax.bestMjesto);
        }
         
    }
}
