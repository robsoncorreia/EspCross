using System.Collections.Generic;
using System.Threading.Tasks;

namespace Esp.Services
{
    public interface IDataStore<T>
    {
        Task<bool> AddItemAsync(T item);

        Task<bool> UpdateItemAsync(T item);

        Task<bool> DeleteItemAsync(object obj);

        Task<T> GetItemAsync(int id);

        Task<IEnumerable<T>> GetItemsAsync(bool forceRefresh = false);
    }
}