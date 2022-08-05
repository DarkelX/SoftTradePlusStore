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
    public partial class MainWindow : Window
    {

        public MainViewModel ViewModel { get; }

        public MainWindow()
        {
            ViewModel = new MainViewModel();

            InitializeComponent();

            Loaded += MainWindow_Loaded;
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            ModelComboBox.ItemsSource = GetModels();
            ModelComboBox.SelectedIndex = 0;
        }

        private void ComboBox_Model_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var combobox = (ComboBox)sender;
            var selectedItem = combobox.SelectedItem.ToString();

            ViewModel.Load(Enum.Parse<DataManager.Models>(selectedItem));

            Editor.Children.Clear();
            Editor.Children.Add(UpdateEditor(Enum.Parse<Models>(selectedItem)));

            UpdateSortByBlockVisible(Enum.Parse<Models>(selectedItem));
        }

        private void UpdateSortByBlockVisible(Models model)
        {
            SortByBlock.Visibility = model== Models.Entity || model == Models.Individual ? Visibility.Visible : Visibility.Collapsed;
        }

        private void ModelList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var modelList = (ListView)sender;
            var editControl = (UserControl)Editor.Children[0];

            editControl.DataContext = modelList.SelectedItem;
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            var modelEnum = Enum.Parse<DataManager.Models>(ModelComboBox.SelectedItem.ToString());//TODO: Refactor
            ViewModel.AddItem(modelEnum);
        }

        private void RemoveButton_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.DeleteItem();
        }

        private void SortByComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
