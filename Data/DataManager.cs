using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using SoftTradePlusStore.Models;

namespace SoftTradePlusStore.Data
{
    internal class DataManager : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Manager> Managers { get; set; }
        public DbSet<Individual> Individuals { get; set; }
        public DbSet<Entity> Entities { get; set; }

        private static DataManager instance;

        public static DataManager GetInstance()
        {
            if (instance == null)
                instance = new DataManager();
            return instance;
        }
        public List<Product> GetProducts()
        {
            var dataBase = new DataManager();

            return dataBase.Products.ToList();
        }
        public List<Manager> GetManagers()
        {
            var dataBase = new DataManager();

            return dataBase.Managers.ToList();
        }

        public List<Individual> GetIndividuals()
        {
            var dataBase = new DataManager();

            return dataBase.Individuals.ToList();
        }
        public List<Entity> GetEntities()
        {
            var dataBase = new DataManager();

            return dataBase.Entities.ToList();
        }
    }
}
