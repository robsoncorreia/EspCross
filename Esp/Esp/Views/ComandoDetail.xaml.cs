using ConfigurationFlexCloudHubBlaster.Service;
using Esp.Models;
using Esp.Services;
using Esp.ViewModels;
using System;
using System.Diagnostics;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Esp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ComandoDetail : ContentPage
    {
        private ComandoDetailViewModel viewModel;

        private IUdpService udpService;

        private ITcpService tcpService;

        private IDataStore<Comando> dataStore;

        public Comando Comando { get; set; }

        public ComandoDetail()
        {
            InitializeComponent();

            Comando = new Comando
            {
                Send = "@ALTERNAR",
                Receive = "Tem alguem ai?"
            };

            dataStore = new MockDataStore();

            udpService = new UdpService();

            tcpService = new TcpService();

            viewModel = new ComandoDetailViewModel(Comando);

            BindingContext = viewModel;
        }

        public ComandoDetail(ComandoDetailViewModel viewModel)
        {
            InitializeComponent();

            udpService = new UdpService();

            tcpService = new TcpService();

            dataStore = new MockDataStore();

            BindingContext = this.viewModel = viewModel;
        }

        private async void Reenviar_Clicked(object sender, EventArgs e)
        {
            try
            {
                TimeSpan duration = TimeSpan.FromMilliseconds(20);
                Vibration.Vibrate(duration);
            }
            catch (FeatureNotSupportedException)
            {
                // Feature not supported on device
            }
            catch (Exception)
            {
                // Other error has occurred.
            }
            try
            {
                Reenviar.IsEnabled = false;
                await udpService.SendAsync(viewModel.Comando.IP, viewModel.Comando.Port, viewModel.Comando.Send);
                Reenviar.IsEnabled = true;
            }
            catch (Exception ex)
            {
                Reenviar.IsEnabled = true;
                Debug.WriteLine(ex.Message);
            }
        }

        private async void Apagar_Clicked(object sender, EventArgs e)
        {
            await App.Database.DeleteItemAsync(viewModel.Comando);
            await Navigation.PushModalAsync(new NavigationPage(new ComandosPage()));
        }
    }
}