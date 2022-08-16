using System;
using System.Collections.Generic;
using System.ComponentModel;
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

            ProductList.Items.SortDescriptions.Add(new SortDescription("Products.EndActivationDate", ListSortDirection.Ascending));
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

            SaveCancelButtons.CheckForEnable(sender);
            ProductList.Visibility = Visibility.Visible;
        }

        private void ProductList_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue is not Client client)
                return;

            ProductList.Visibility = client.Products.Count > 0 ? Visibility.Visible : Visibility.Collapsed;
        }

        private void SaveAndCancelButtons_SaveButtonClicked(object sender, RoutedEventArgs e)
        {
            if (sender is not SaveAndCancelButtons saveAndCancelButtons)
                return;

            var isRequiredField = false;

            if (string.IsNullOrEmpty(NameField.Text))
            {
                RequiredName.Show();
                isRequiredField = true;
            }

            if (ManagerComboBox.SelectedItem == null)
            {
                RequiredManager.Show();
                isRequiredField = true;
            }

            if(IndividualPanel.IsVisible && IndividualComboBox.SelectedItem == null)
            {
                RequiredIndividual.Show();
                isRequiredField = true;
            }
            
            if(!isRequiredField)
            {
                RequiredName.Hide();
                RequiredManager.Hide();
                RequiredIndividual.Hide();
                saveAndCancelButtons.SaveShanges();
                SaveCancelButtons.IsEnabled = false;
            }
        }

        private void StatusComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SaveCancelButtons.CheckForEnable(sender);
        }

        private void NameField_TextChanged(object sender, TextChangedEventArgs e)
        {
            SaveCancelButtons.CheckForEnable(sender);
        }

        private void ManagerComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SaveCancelButtons.CheckForEnable(sender);
        }

        private void IndividualComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SaveCancelButtons.CheckForEnable(sender);
        }

        private void UserControl_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (!IsLoaded)
                return;
            RequiredName.Hide();
            RequiredManager.Hide();
            RequiredIndividual.Hide();
            SaveCancelButtons.HideSuccessfullyText();
        }

        private void RemoveButton_Click(object sender, RoutedEventArgs e)
        {
            if (sender is not Button button)
                return;

            if (button.DataContext is not Client client)
                return;

            client.Products.Remove((BoughtProduct)ProductList.SelectedItem);
            SaveCancelButtons.CheckForEnable(sender);
        }
    }
}
