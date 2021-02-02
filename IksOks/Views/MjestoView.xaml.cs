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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace IksOks.Views
{
    /// <summary>
    /// Interaction logic for MjestoView.xaml
    /// </summary>
    public partial class MjestoView : UserControl
    {
        public IksOksIgra.Mjesto mjesto;
        public MjestoView(Models.IksOksIgra.Mjesto mjesto)
        {
            InitializeComponent();
            this.mjesto = mjesto;
            Refresh();
        }
        public void Refresh()
        {
            if (mjesto.player != null)
            {
                switch (mjesto.player.Znak)
                { 
                    case ZNAK.O:
                        BitmapImage image1 = new BitmapImage(new Uri("/IksOks;component/Resources/o.png", UriKind.Relative));
                        img.Source = image1;
                        break;
                    case ZNAK.X:
                        BitmapImage image = new BitmapImage(new Uri("/IksOks;component/Resources/x.png", UriKind.Relative));
                        img.Source = image;
                        break;
                }
            }
            IsEnabled = mjesto.player == null;
            
        }

        private void UserControl_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        { 
        }
    }
}
