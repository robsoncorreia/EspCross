using Esp.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Threading.Tasks;

namespace Esp.Services
{
    public class MockDataStore : IDataStore<Comando>
    {
        private readonly string dbPath = Path.Combine(
     Environment.GetFolderPath(Environment.SpecialFolder.Personal),
     "comando.db3");

        public MockDataStore()
        {
        }

        public async Task<int> AddItemAsync(Comando item)
        {
            SQLiteAsyncConnection db = new SQLiteAsyncConnection(dbPath);

            await db.CreateTableAsync<Comando>();

            return await db.InsertAsync(item);
        }

        public async Task<int> UpdateItemAsync(Comando item)
        {
            SQLiteAsyncConnection db = new SQLiteAsyncConnection(dbPath);

            await db.CreateTableAsync<Comando>();

            Comando comandoVelho = await db.GetAsync<Comando>(item.Id);

            await db.DeleteAsync(comandoVelho);

            return await db.InsertAsync(item);
        }

        public async Task<int> DeleteItemAsync(object obj)
        {
            SQLiteAsyncConnection db = new SQLiteAsyncConnection(dbPath);

            await db.CreateTableAsync<Comando>();

            Comando comando = (Comando)obj;

            return await db.DeleteAsync(comando);
        }

        public async Task<Comando> GetItemAsync(int id)
        {
            SQLiteAsyncConnection db = new SQLiteAsyncConnection(dbPath);

            await db.CreateTableAsync<Comando>();

            return await db.Table<Comando>().FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<Comando>> GetItemsAsync(bool forceRefresh = false)
        {
            SQLiteAsyncConnection db = new SQLiteAsyncConnection(dbPath);

            await db.CreateTableAsync<Comando>();

            return await db.Table<Comando>().ToListAsync();
        }
    }
}