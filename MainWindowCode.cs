using SoftTradePlusStore.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using SoftTradePlusStore.Controls;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using SoftTradePlusStore.Models;
using System.Collections.ObjectModel;
using SoftTradePlusStore.Models.Clients;
using SoftTradePlusStore.Models.Products;
using System.Windows.Controls;

namespace SoftTradePlusStore
{
    public partial class MainWindow
    {
        private enum Models
        {
            Individual,
            Entity,
            Manager,
            Product
        }

        public enum SortClientBy
        {
            Name,
            Manager,
            Status
        }

        public List<SortClientBy> GetSortClientBy => Enum.GetValues<SortClientBy>().ToList();

        private List<Models> GetModels()
        {
            return Enum.GetValues<Models>().ToList();
        }

        private UIElement GetEditor(Models models)
        {
            switch (models)
            {
                case Models.Individual: 
                case Models.Entity:
                    var client = new ClientEditor();
                    client.IndividualPanel.Visibility = models == Models.Entity ? Visibility.Visible : Visibility.Collapsed;
                    return client;
                case Models.Manager: 
                    return new ManagerEditor();
                case Models.Product: 
                    return new ProductEditor();
                default: throw new Exception();
            }
        }

        public void UpdateEditor()
        {
            var selectedModel = ModelComboBox.SelectedItem.ToString();
            var modelEnum = Enum.Parse<Models>(selectedModel);

            Editor.Children.Clear();
            Editor.Children.Add(GetEditor(modelEnum));
            UpdateEditorVisibility();

            UpdateSortByBlockVisible(modelEnum);

            SortByComboBox.SelectedIndex = 0;

            if (ViewModel.IsItemSelected)
            {
                var editControl = (UserControl)Editor.Children[0];
                editControl.DataContext = ViewModel.SelectedItem;
            }
        }

        private void UpdateEditorVisibility()
        {
            Editor.Visibility = ViewModel.IsItemSelected ? Visibility.Visible : Visibility.Collapsed;
        }

        private void UpdateSortByBlockVisible(Models model)
        {
            SortByBlock.Visibility = model == Models.Entity || model == Models.Individual ? Visibility.Visible : Visibility.Collapsed;
        }

        private void SortItems(SortClientBy sortClientByEnum)
        {
            if (ItemsList.Items.SortDescriptions.Count == 0)
                ItemsList.Items.SortDescriptions.Add(new SortDescription("Name", ListSortDirection.Ascending));

            SortDescription newSortDescription;
            switch (sortClientByEnum)
            {
                case SortClientBy.Name:
                    newSortDescription = new SortDescription("Name", ListSortDirection.Ascending);
                    break;
                case SortClientBy.Status:
                    newSortDescription = new SortDescription("Status", ListSortDirection.Ascending);
                    break;
                case SortClientBy.Manager:
                    newSortDescription = new SortDescription("Manager.Name", ListSortDirection.Ascending);
                    break;
                default:
                    newSortDescription = new SortDescription();
                    break;
            }

            ItemsList.Items.SortDescriptions[ItemsList.Items.SortDescriptions.Count - 1] = newSortDescription;
        }
    }
}
