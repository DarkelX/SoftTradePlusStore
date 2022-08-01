using SoftTradePlusStore.Data;
using SoftTradePlusStore.Models;
using SoftTradePlusStore.Controls;
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
using System.Collections.ObjectModel;
using SoftTradePlusStore.ViewModel;

namespace SoftTradePlusStore
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public MainViewModel ViewModel { get; }

        public MainWindow()
        {
            InitializeComponent();

            ViewModel = new MainViewModel();

            Loaded += MainWindow_Loaded;
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            ModelComboBox.ItemsSource = GetModels();
            ModelComboBox.SelectedIndex = 0;
        }

        private void ComboBox_Model_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var combobox = (ComboBox) sender;
            var selectedItem = combobox.SelectedItem.ToString();

            ViewModel.Load(Enum.Parse<Models>(selectedItem));

            //Items.Clear();
            //var items = UpdateModelList(Enum.Parse<Models>(selectedItem));
            //foreach(var item in items)
            //{
            //    Items.Add(item); 
            //    OnPropertyChanged(nameof(Items));
            //}
            //ModelList.ItemsSource = UpdateModelList(Enum.Parse<Models>(selectedItem));

            Editor.Children.Clear();
            Editor.Children.Add(UpdateEditor(Enum.Parse<Models>(selectedItem)));

            //SelectedItem = null;
        }

        private void ModelList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //var modelList = (ListView)sender;
            //SelectedItem = modelList.SelectedItem;  

            //var editControl = (UserControl) Editor.Children[0];

            //editControl.DataContext = SelectedItem;
        }

        private void RemoveButton_Click(object sender, RoutedEventArgs e)
        {
            //var item = SelectedItem;
            //if (item != null)
            //{
            //    DeleteItemFromDataBase(item);
            //    //ModelList.Items.Remove(item);
            //    SelectedItem = null;
            //}
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
