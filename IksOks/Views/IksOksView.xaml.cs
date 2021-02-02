using IksOks.Models;
using System;
using System.Collections.Generic;
using System.Drawing.Printing;
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

namespace IksOks.Views
{
    /// <summary>
    /// Interaction logic for IksOksView.xaml
    /// </summary>
    public partial class IksOksView : UserControl
    {
        public IksOksIgra igra;
        List<MjestoView> mjestoViews;
        public IksOksView(IksOksIgra igra)
        {
            InitializeComponent();
            mjestoViews = new List<MjestoView>();

            foreach(var v in igra.DostupnaMjesta)
            {
                MjestoView mjestoView = new MjestoView(v);
                Grid.SetColumn(mjestoView, v.X);
                Grid.SetRow(mjestoView, v.Y);
                mjestoView.Margin = new Thickness(10, 10, 10, 10);
                mjestoView.MouseDoubleClick += MjestoView_MouseDoubleClick; 
                gridMjestoView.Children.Add(mjestoView);
                mjestoViews.Add(mjestoView);
            }

            this.igra = igra;
            Refresh();
        }

        private void MjestoView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            igra.Parent.MakeMove(((MjestoView)sender).mjesto );
            Refresh();
        }

        public void Refresh()
        {
            foreach(var m in mjestoViews)
            {
                m.Refresh();
            }
            if (igra.Pobjednik != null)
            {
                switch (igra.Pobjednik.Znak )
                { 
                    case ZNAK.O:
                        BitmapImage image1 = new BitmapImage(new Uri("/IksOks;component/Resources/o.png", UriKind.Relative));
                        overlayImg.Source = image1;
                        gridMjestoView.Opacity = 0.5;
                        break;
                    case ZNAK.X:
                        BitmapImage image = new BitmapImage(new Uri("/IksOks;component/Resources/x.png", UriKind.Relative));
                        overlayImg.Source = image;
                        gridMjestoView.Opacity = 0.5;
                        break;
                }
            }
        }
    }
}
