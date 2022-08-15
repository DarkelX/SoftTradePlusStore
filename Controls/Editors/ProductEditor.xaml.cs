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
    /// <summary>
    /// Interaction logic for ProductEditor.xaml
    /// </summary>
    public partial class ProductEditor : UserControl
    {
        public ProductEditor()
        {
            InitializeComponent();
            Loaded += ProductEditor_Loaded;
        }

        private void ProductEditor_Loaded(object sender, RoutedEventArgs e)
        {
            TypeComboBox.ItemsSource = GetTypes();

            TermComboBox.ItemsSource = GetTerms();
        }

        private static List<Product.ProductType> GetTypes()
        {
            return Enum.GetValues<Product.ProductType>().ToList();
        }
        private static List<Product.SubscriptionTerm> GetTerms()
        {
            return Enum.GetValues<Product.SubscriptionTerm>().ToList();
        }

        private void TypeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (sender is not ComboBox comboBox)
                return;

            if (comboBox.SelectedValue == null) return;

            var productType = Enum.Parse<Product.ProductType>(comboBox.SelectedValue.ToString());
            TermBlock.Visibility = productType == Product.ProductType.Subscription ? Visibility.Visible : Visibility.Collapsed;

            SaveCancelButtons.CheckForEnable(sender);
        }

        private void ShowHideClientsButton_Click(object sender, RoutedEventArgs e)
        {
            if(ClientsWhoBoughtItem.Items.Count > 0)
            {
                if(ClientsWhoBoughtItem.Visibility == Visibility.Collapsed)
                {
                    ClientsWhoBoughtItem.Visibility = Visibility.Visible;
                    ShowHideClientsButton.Content = "Hide";
                }
                else
                {
                    ClientsWhoBoughtItem.Visibility = Visibility.Collapsed;
                    ShowHideClientsButton.Content = "Show";
                }
                return;
            }


            if (sender is not Button button)
                return;

            if (button.DataContext is not Product thisProduct)
                return;

            var nameOfProduct = thisProduct.Name;
            var clients = GetClientsWhoBoughtItem(nameOfProduct);

            foreach (var client in clients)
                ClientsWhoBoughtItem.Items.Add(client);

            ClientsWhoBoughtItem.Visibility = Visibility.Visible;
            ShowHideClientsButton.Content = "Hide";
            ClientsWhoBoughtItem.Items.SortDescriptions.Add(new SortDescription("Name", ListSortDirection.Ascending));
        }

        private List<ClientsWithCount> GetClientsWhoBoughtItem(string name)//TODO:Refactor
        {
            var dataBase = DataManager.GetInstance();

            var clients = new List<ClientsWithCount>();

            foreach (var client in dataBase.Individuals)
            {
                foreach (var product in client.Products)
                {
                    if (product.Name == name)
                    {
                        var a = clients.FirstOrDefault(x => x.Name == client.Name);
                        if (a != null)
                            a.IncreaseCount();
                        else
                            clients.Add(new ClientsWithCount(client));
                    }
                }
            }

            foreach (var client in dataBase.Entities)
            {
                foreach (var product in client.Products)
                {
                    if (product.Name == name)
                    {
                        var a = clients.FirstOrDefault(x => x.Name == name);
                        if (a != null)
                            a.IncreaseCount();
                        else
                            clients.Add(new ClientsWithCount(client));
                    }
                }
            }

            return clients;
        }

        private void ClientsWhoBoughtItem_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue is not Product product) //TODO
                return;

            //ClientsWhoBoughtItem.Visibility = client.Products.Count > 0 ? Visibility.Visible : Visibility.Collapsed;
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
            

            if (string.IsNullOrEmpty(PriceField.Text))
            {
                RequiredPrice.Show();
                isRequiredField = true;
            }
                

            if(!isRequiredField)
            {
                RequiredName.Hide();
                RequiredPrice.Hide();
                saveAndCancelButtons.SaveShanges();
            }
        }

        private void PriceField_TextChanged(object sender, TextChangedEventArgs e)
        {
            SaveCancelButtons.CheckForEnable(sender);
        }

        private void NameField_TextChanged(object sender, TextChangedEventArgs e)
        {
            SaveCancelButtons.CheckForEnable(sender);
        }

        private void TermComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SaveCancelButtons.CheckForEnable(sender);
        }
    }
}
