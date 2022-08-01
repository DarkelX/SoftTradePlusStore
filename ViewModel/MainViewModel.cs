using SoftTradePlusStore.Base;
using SoftTradePlusStore.Data;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftTradePlusStore.ViewModel
{
    public class MainViewModel : Observable
    {
        public ObservableCollection<object> Items { get; }
        private IHaveIdName selectedItem;
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
            Items.CollectionChanged += Items_CollectionChanged;
        }

        private void Items_CollectionChanged(object? sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            OnPropertyChanged();
            OnPropertyChanged(nameof(Items));
            if(Items.Count > 0)
            selectedItem = Items[0] as IHaveIdName;
        }

        public void Load(MainWindow.Models model)
        {
            Items.Clear();

            var items = UpdateModelList(model);
            foreach (var item in items)
            {
                Items.Add(item);
            }
            OnPropertyChanged(nameof(Items));
        }

        public async Task SaveAsync()
        {
            //await _customerDataProvider.SaveCustomersAsync(Customers);
        }

        public void AddCustomer()
        {
            //var customer = new Customer { FirstName = "New" };
            //Customers.Add(customer);
            //SelectedCustomer = customer;
        }

        public void DeleteCustomer()
        {
            //var customer = SelectedCustomer;
            //if (customer != null)
            //{
            //    Customers.Remove(customer);
            //    SelectedCustomer = null;
            //}
        }







        private ObservableCollection<object> UpdateModelList(MainWindow.Models models)
        {
            var dataBase = DataManager.GetInstance();

            switch (models)
            {
                case MainWindow.Models.Individual: return new ObservableCollection<object>(dataBase.Individuals.ToList());
                case MainWindow.Models.Entity: return new ObservableCollection<object>(dataBase.Entities.ToList());
                case MainWindow.Models.Manager: return new ObservableCollection<object>(dataBase.Managers.ToList());
                case MainWindow.Models.Product: return new ObservableCollection<object>(dataBase.Products.ToList());
                default: throw new Exception();
            }
        }
    }
}
