using SoftTradePlusStore.Base;
using SoftTradePlusStore.Data;
using SoftTradePlusStore.Models;
using SoftTradePlusStore.Models.Clients;
using SoftTradePlusStore.Models.Products;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SoftTradePlusStore.ViewModel
{
    public class MainViewModel : Observable
    {
        public ObservableCollection<object> Items { get; }
        private IHaveIdName selectedItem;
        private DataManager.Models currentModel;
        public bool IsItemSelected => SelectedItem != null;

        public IHaveIdName SelectedItem 
        {
            get => selectedItem;
            set
            {
                if (selectedItem != value)
                {
                    selectedItem = value;
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(IsItemSelected));
                }
            }
        }
        public MainViewModel()
        {
            Items = new ObservableCollection<object>();
        }

        internal void Load(DataManager.Models model)
        {
            currentModel = model;
            Items.Clear();

            var items = UpdateModelList(model);
            foreach (var item in items)
            {
                Items.Add(item);
            }
            OnPropertyChanged(nameof(Items));
        }

        internal void AddItem(DataManager.Models model)
        {
            IHaveIdName item = CreateItem(model);
            Items.Add(item);
            SelectedItem = item;
        }

        private IHaveIdName CreateItem(DataManager.Models model)
        {
            switch (model)
            {
                case DataManager.Models.Individual: return new Individual(true);
                case DataManager.Models.Entity: return new Entity(true);
                case DataManager.Models.Manager: return new Manager(true);
                case DataManager.Models.Product: return new Product(true);
                case DataManager.Models.BoughtProduct: return new BoughtProduct(true);
                default: throw new Exception();
            }
        }

        public void DeleteItem()
        {
            var item = SelectedItem;
            if (item != null)
            {
                DataManager.GetInstance().DeleteItem(item);
                Items.Remove(item);
                SelectedItem = null;
            }
        }

        private ObservableCollection<IHaveIdName> UpdateModelList(DataManager.Models models)
        {
            var dataBase = DataManager.GetInstance();

            switch (models)
            {
                case DataManager.Models.Individual: return new ObservableCollection<IHaveIdName>(dataBase.Individuals);
                case DataManager.Models.Entity: return new ObservableCollection<IHaveIdName>(dataBase.Entities);
                case DataManager.Models.Manager: return new ObservableCollection<IHaveIdName>(dataBase.Managers);
                case DataManager.Models.Product: return new ObservableCollection<IHaveIdName>(dataBase.Products);
                case DataManager.Models.BoughtProduct: return new ObservableCollection<IHaveIdName>(dataBase.BoughtProducts);
                default: throw new Exception();
            }
        }

        private List<IHaveIdName> GetItems()
        {
            var items = new List<IHaveIdName>();

            foreach(var item in Items)
                items.Add(item as IHaveIdName);

            return items;
        }

        internal void Refresh()
        {
            var selectedItemId = selectedItem.Id;
            Load(currentModel);
            SelectedItem = GetItems().FirstOrDefault(x => x.Id == selectedItemId);
        }

        internal void Reload()
        {
            DataManager.GetInstance().Dispose();
            Refresh();
        }
    }
}
