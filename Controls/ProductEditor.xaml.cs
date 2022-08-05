﻿using System;
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
    }
}
