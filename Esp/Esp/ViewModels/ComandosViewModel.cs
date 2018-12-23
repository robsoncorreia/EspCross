using Esp.Models;
using Esp.Views;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Esp.ViewModels
{
    public class ComandosViewModel : BaseViewModel
    {
        public ObservableCollection<Comando> Comandos { get; set; }
        public Command LoadItemsCommand { get; set; }

        public ComandosViewModel()
        {
            Title = "Browse";
            Comandos = new ObservableCollection<Comando>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());

            MessagingCenter.Subscribe<NovoComando, Comando>(this, "AddItem", async (obj, item) =>
            {
                var newItem = item as Comando;
                Comandos.Add(newItem);
                try
                {
                    await DataStore.AddItemAsync(newItem);
                }
                catch (Exception e)
                {
                    Debug.WriteLine(e);
                }
            });

            MessagingCenter.Subscribe<ComandoDetail, Comando>(this, "DeleteItem", async (obj, item) =>
            {
                var newItem = item as Comando;
                Comandos.Remove(newItem);
                await DataStore.DeleteItemAsync(newItem);
            });
        }

        private async Task ExecuteLoadItemsCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                Comandos.Clear();
                var items = await DataStore.GetItemsAsync(true);
                foreach (var item in items)
                {
                    Comandos.Add(item);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}