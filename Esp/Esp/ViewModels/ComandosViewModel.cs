using ConfigurationFlexCloudHubBlaster.Service;
using Esp.Models;
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

        private readonly IUdpService udpService;

        public ComandosViewModel()
        {
            udpService = new UdpService();

            Title = "Comando";
            Comandos = new ObservableCollection<Comando>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());
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