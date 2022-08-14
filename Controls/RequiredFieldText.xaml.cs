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
    /// Interaction logic for RequiredFieldText.xaml
    /// </summary>
    public partial class RequiredFieldText : TextBlock
    {
        public RequiredFieldText()
        {
            InitializeComponent();
        }

        public void Show()
        {
            Text.Visibility = Visibility.Visible;
        }

        public void Hide()
        {
            Text.Visibility = Visibility.Collapsed;
        }
    }
}
