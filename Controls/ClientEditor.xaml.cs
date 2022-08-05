using System;
using SoftTradePlusStore.Models;
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
using SoftTradePlusStore.Data;

namespace SoftTradePlusStore.Controls
{
    public partial class ClientEditor : UserControl
    {
        public ClientEditor()
        {
            InitializeComponent();

            Loaded += ClientEditor_Loaded;
        }

        private void ClientEditor_Loaded(object sender, RoutedEventArgs e)
        {
            var dataManager = DataManager.GetInstance();

            StatusComboBox.ItemsSource = GetStatuses();
            StatusComboBox.SelectedIndex = 0;

            ManagerComboBox.ItemsSource = dataManager.GetManagers();
            if (ManagerComboBox.Items.Count != 0)
                ManagerComboBox.SelectedIndex = 0;

            IndividualComboBox.ItemsSource = dataManager.GetIndividuals();
            if (IndividualComboBox.Items.Count != 0)
                IndividualComboBox.SelectedIndex = 0;

            ProductsComboBox.ItemsSource = dataManager.GetProducts();
            if (ProductsComboBox.Items.Count != 0)
            {
                ProductsComboBox.SelectedIndex = 0;
                AddButton.IsEnabled = true;
            }
            else AddButton.IsEnabled = false;

        }

        private static List<Client.ClientStatus> GetStatuses()
        {
            return Enum.GetValues<Client.ClientStatus>().ToList();
        }

        private void ProductList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            var dataBase = DataManager.GetInstance();

            var boughtProduct = new BoughtProduct( (Product) ((Product)ProductsComboBox.SelectedItem).Clone());
            boughtProduct.ActivationDate = boughtProduct.PurchaseDate;
            var viewModel = (Window.GetWindow(App.Current.MainWindow) as MainWindow).ViewModel;

            ((Client)viewModel.SelectedItem).Products.Add(boughtProduct);
            dataBase.SaveChanges();//TODO:Save after click
        }
    }
}
