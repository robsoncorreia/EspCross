using System.Collections.Generic;
using System.Threading.Tasks;

namespace Esp.Services
{
    public interface IDataStore<T>
    {
        Task<int> AddItemAsync(T item);

        Task<int> UpdateItemAsync(T item);

        Task<int> DeleteItemAsync(object obj);

        Task<T> GetItemAsync(int id);

        Task<IEnumerable<T>> GetItemsAsync(bool forceRefresh = false);
    }
}