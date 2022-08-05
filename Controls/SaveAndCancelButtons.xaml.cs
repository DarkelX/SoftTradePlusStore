using SoftTradePlusStore.Data;
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
    /// Interaction logic for SaveAndCancelButtons.xaml
    /// </summary>
    public partial class SaveAndCancelButtons : UserControl
    {
        public SaveAndCancelButtons()
        {
            InitializeComponent();
        }
        private void ButtonSave_Click(object sender, RoutedEventArgs e)
        {
            var dataBase = DataManager.GetInstance();

            var viewModel = (Window.GetWindow(App.Current.MainWindow) as MainWindow).ViewModel; //TODO:Refactor
            var selectedItem = viewModel.SelectedItem;
            if (selectedItem.Id > 0)//TODO:Refactor
            {
                dataBase.SaveChanges();
            }
            else
            {
                dataBase.CreateItem(selectedItem);
                viewModel.Items.Remove(viewModel.SelectedItem);//TODO:Refactor
                viewModel.Items.Add(selectedItem);
            }
        }

        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            var viewModel = (Window.GetWindow(App.Current.MainWindow) as MainWindow).ViewModel;
            if (viewModel.SelectedItem.Id == -1)
                viewModel.Items.Remove(viewModel.SelectedItem);
        }
    }
}
