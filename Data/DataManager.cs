using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using SoftTradePlusStore.Models;
using SoftTradePlusStore.Models.Clients;
using SoftTradePlusStore.Models.Products;

namespace SoftTradePlusStore.Data
{
    internal class DataManager : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<BoughtProduct> BoughtProducts { get; set; }
        public DbSet<Manager> Managers { get; set; }
        public DbSet<Individual> Individuals { get; set; }
        public DbSet<Entity> Entities { get; set; }

        public enum Models
        {
            Individual,
            Entity,
            Manager,
            Product,
            BoughtProduct
        }

        private static DataManager instance;

        public static DataManager GetInstance()
        {
            if (instance == null)
            {
                instance = new DataManager();
                instance.Initialize();
            }
            return instance;
        }
        private void Initialize()
        {
            Products.Load();
            BoughtProducts.Load();
            Managers.Load();
            Individuals.Load();
            Entities.Load();
        }
        public List<Product> GetProducts()
        {
            return Products.ToList();
        }
        public List<BoughtProduct> GetBoughtProducts()
        {
            return BoughtProducts.ToList();
        }
        public List<Manager> GetManagers()
        {
            return Managers.ToList();
        }

        public List<Individual> GetIndividuals()
        {
            return Individuals.ToList();
        }
        public List<Entity> GetEntities()
        {
            return Entities.ToList();
        }

        public void CreateItem(IHaveIdName item)
        {
            var enumModel = GetModelType(item);

            switch (enumModel)
            {
                case Models.Individual:
                    Individuals.Add(item as Individual);
                    break;
                case Models.Entity:
                    Entities.Add(item as Entity);
                    break;
                case Models.Manager:
                    Managers.Add(item as Manager);
                    break;
                case Models.Product:
                    Products.Add(item as Product);
                    break;
                    case Models.BoughtProduct:
                    BoughtProducts.Add(BoughtProducts.Add(item as BoughtProduct));
                    break;
            }

            SaveChanges();
        }

        public void DeleteItem(IHaveIdName item)
        {
            var enumModel = GetModelType(item);

            switch (enumModel)
            {
                case Models.Individual:
                    Individuals.Remove(item as Individual);
                    break;
                case Models.Entity:
                    Entities.Remove(item as Entity);
                    break;
                case Models.Manager:
                    Managers.Remove(item as Manager);
                    break;
                case Models.Product:
                    Products.Remove(item as Product);
                    break;
                case Models.BoughtProduct:
                    BoughtProducts.Remove(item as BoughtProduct);
                    break;
                default: throw new Exception();
            }

            SaveChanges();
        }

        private Models GetModelType(IHaveIdName item)
        {
            var type = item.GetType().Name;
            return Enum.Parse<Models>(type);
        }

        public bool Contains(IHaveIdName item) //TODO:doesnt work
        {
            var enumModel = GetModelType(item);

            switch (enumModel)
            {
                case Models.Individual:
                    return Individuals.Contains(item as Individual);
                case Models.Entity:
                    return Entities.Contains(item as Entity);
                case Models.Manager:
                    return Managers.Contains(item as Manager);
                case Models.Product:
                    return Products.Contains(item as Product);
                case Models.BoughtProduct:
                    return BoughtProducts.Contains(item as BoughtProduct);
                default: throw new Exception();
            }
        }

        
    }
}
