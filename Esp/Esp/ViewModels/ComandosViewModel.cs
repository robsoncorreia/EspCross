using ConfigurationFlexCloudHubBlaster.Service;
using Esp.Models;
using Esp.Services;
using Esp.Views;
using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Esp.ViewModels
{
    public class ComandosViewModel : BaseViewModel
    {
        public ObservableCollection<Comando> Comandos { get; set; }
        public Command LoadItemsCommand { get; set; }

        private IDataStore<Comando> dataStore;

        private readonly IUdpService udpService;

        public ComandosViewModel()
        {
            udpService = new UdpService();

            dataStore = new MockDataStore();

            Title = "Browse";
            Comandos = new ObservableCollection<Comando>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());

            MessagingCenter.Subscribe<NovoComando, Comando>(this, "AddItem", async (obj, item) =>
            {
                var newItem = item as Comando;
                try
                {
                    int rows = await DataStore.AddItemAsync(newItem);
                    if (rows == 1)
                        Comandos.Add(newItem);
                }
                catch (Exception e)
                {
                    Debug.WriteLine(e);
                }
            });

            MessagingCenter.Subscribe<ComandoDetail, Comando>(this, "DeleteItem", async (obj, item) =>
            {
                var newItem = item as Comando;

                try
                {
                    int rows = await DataStore.DeleteItemAsync(newItem);
                    if (rows == 1)
                        Comandos.Remove(newItem);
                }
                catch (Exception e)
                {
                    Debug.WriteLine(e);
                }
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
                var items = await App.Database.GetItemsAsync();
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