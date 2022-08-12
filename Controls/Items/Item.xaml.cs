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

namespace SoftTradePlusStore.Controls
{
    /// <summary>
    /// Interaction logic for Item.xaml
    /// </summary>
    public partial class Item : Grid
    {
        public Item()
        {
            InitializeComponent();

            Loaded += Item_Loaded;
        }

        private void Item_Loaded(object sender, RoutedEventArgs e)
        {
            StatusBlock.Visibility = !string.IsNullOrEmpty(StatusText.Text) ? Visibility.Visible : Visibility.Collapsed;
            ManagerBlock.Visibility = !string.IsNullOrEmpty(ManagerText.Text) ? Visibility.Visible : Visibility.Collapsed;
            IndividualBlock.Visibility = !string.IsNullOrEmpty(IndividualText.Text) ? Visibility.Visible : Visibility.Collapsed;

            if (string.IsNullOrEmpty(CountText.Text))
                CountBlock.Visibility = Visibility.Collapsed;
            else
                IdBlock.Visibility = Visibility.Collapsed;
        }
    }
}
