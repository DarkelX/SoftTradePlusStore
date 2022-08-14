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
using SoftTradePlusStore.Data;
using SoftTradePlusStore.Models.Clients;
using SoftTradePlusStore.Models.Products;

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

            ManagerComboBox.ItemsSource = dataManager.GetManagers();

            IndividualComboBox.ItemsSource = dataManager.GetIndividuals();

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
            var button = sender as DefaultButton;
            var client = button?.DataContext as Client;
            var selectedProduct = (Product)ProductsComboBox.SelectedItem;
            var products = client?.Products.Where(x => x.Name == selectedProduct.Name).ToList();

            if (products == null)
                return;

            var latestActivationDate = products.Count == 0 ? DateTime.Now : products.Max(y => y.EndActivationDate);
            var boughtProduct = new BoughtProduct((Product)((Product)ProductsComboBox.SelectedItem).Clone(), latestActivationDate);

            var viewModel = (Window.GetWindow(App.Current.MainWindow) as MainWindow)?.ViewModel;
            ((Client)viewModel?.SelectedItem).Products.Add(boughtProduct);
        }
    }
}
