using Esp.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Esp.Services
{
    public class MockDataStore : IDataStore<Comando>
    {
        private List<Comando> items = new List<Comando>();

        public MockDataStore()
        {
        }

        public async Task<bool> AddItemAsync(Comando item)
        {
            Console.WriteLine("Creating database, if it doesn't already exist");
            string dbPath = Path.Combine(
                 Environment.GetFolderPath(Environment.SpecialFolder.Personal),
                 "comandos.db3");
            var db = new SQLiteConnection(dbPath);
            db.CreateTable<Comando>();
            db.Insert(item);
            items.Add(item);
            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateItemAsync(Comando item)
        {
            var oldItem = items.Where((Comando arg) => arg.Id == item.Id).FirstOrDefault();
            items.Remove(oldItem);
            items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(object obj)
        {
            string dbPath = Path.Combine(
                 Environment.GetFolderPath(Environment.SpecialFolder.Personal),
                 "comandos.db3");
            var db = new SQLiteConnection(dbPath);

            Comando comando = (Comando)obj;

            db.Delete(comando);

            return await Task.FromResult(true);
        }

        public async Task<Comando> GetItemAsync(int id)
        {
            return await Task.FromResult(items.FirstOrDefault(s => s.Id == id));
        }

        public async Task<IEnumerable<Comando>> GetItemsAsync(bool forceRefresh = false)
        {
            string dbPath = Path.Combine(
                 Environment.GetFolderPath(Environment.SpecialFolder.Personal),
                 "comandos.db3");
            var db = new SQLiteConnection(dbPath);
            db.CreateTable<Comando>();
            return await Task.FromResult(db.Table<Comando>().ToList<Comando>());
        }
    }
}