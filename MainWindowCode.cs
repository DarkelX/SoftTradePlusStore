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

        private ObservableCollection<object> UpdateModelList(Models models)
        {
            var dataBase = DataManager.GetInstance();

            switch (models)
            {
                case Models.Individual: return new ObservableCollection<object>(dataBase.Individuals.ToList());
                case Models.Entity: return new ObservableCollection<object>(dataBase.Entities.ToList());
                case Models.Manager: return new ObservableCollection<object>(dataBase.Managers.ToList());
                case Models.Product: return new ObservableCollection<object>(dataBase.Products.ToList());
                default: throw new Exception();
            }
        }

        private UIElement UpdateEditor(Models models)
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

        private void DeleteItemFromDataBase(object item)
        {
            var dataManager = DataManager.GetInstance();

            var type = item.GetType().Name;
            var enumModel = Enum.Parse<Models>(type);

            switch (enumModel)
            {
                case Models.Individual:
                    dataManager.Individuals.Remove(item as Individual);
                    break;
                case Models.Entity:
                    dataManager.Entities.Remove(item as Entity);
                    break;
                case Models.Manager:
                    dataManager.Managers.Remove(item as Manager);
                    break;
                case Models.Product:
                    dataManager.Products.Remove(item as Product);
                    break;
                default: throw new Exception();
            }

            dataManager.SaveChanges();
        }

        private List<SortDescription> GetSortDescriptions()
        {
            var sortDescriptions = new List<SortDescription>
            {
                new SortDescription("Name", ListSortDirection.Ascending),
                new SortDescription("Manager.Name", ListSortDirection.Ascending),
                new SortDescription("Status", ListSortDirection.Ascending)
            };
            sortDescriptions.Add(new SortDescription("Name", System.ComponentModel.ListSortDirection.Ascending));

            return sortDescriptions;
        }
    }
}
