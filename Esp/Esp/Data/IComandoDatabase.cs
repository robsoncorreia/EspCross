using System.Collections.Generic;
using System.Threading.Tasks;
using Esp.Models;

namespace Esp
{
    public interface IComandoDatabase
    {
        Task<int> DeleteItemAsync(Comando item);

        Task<Comando> GetItemAsync(int id);

        Task<List<Comando>> GetItemsAsync();

        Task<int> SaveItemAsync(Comando item);
    }
}