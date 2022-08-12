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
            TypeComboBox.SelectedIndex = 0;

            TermComboBox.ItemsSource = GetTerms();
            TermComboBox.SelectedIndex = 0;
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
            var comboBox = (ComboBox)sender; //TODO:Refactor
            if(comboBox.SelectedValue == null) return;
            var productType = Enum.Parse<Product.ProductType>(comboBox.SelectedValue.ToString());
            TermBlock.Visibility = productType == Product.ProductType.Subscription ? Visibility.Visible : Visibility.Collapsed;
        }

        private void ShowHideClientsButton_Click(object sender, RoutedEventArgs e)
        {
            var button = (DefaultButton)sender;
            var thisProduct = (Product)button.DataContext;
            var nameOfProduct = thisProduct.Name;
            var clients = GetClientsWhoBoughtItem(nameOfProduct);
            ClientsWhoBoughtItem.Items.Clear();//TODO:Refactor

            foreach (var client in clients)
                ClientsWhoBoughtItem.Items.Add(client);
        }

        private List<ClientsWithCount> GetClientsWhoBoughtItem(string name)//TODO:Refactor
        {
            var dataBase = DataManager.GetInstance();

            var clients = new List<ClientsWithCount>();

            foreach(var client in dataBase.Individuals)
            {
                foreach(var product in client.Products)
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
    }
}
