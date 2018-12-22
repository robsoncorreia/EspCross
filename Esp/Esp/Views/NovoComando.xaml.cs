using ConfigurationFlexCloudHubBlaster.Service;
using Esp.Models;
using System;
using System.Diagnostics;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Esp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NovoComando : ContentPage
    {
        private IUdpService udpService;
        private ITcpService tcpService;

        public NovoComando()
        {
            InitializeComponent();

            udpService = new UdpService();

            tcpService = new TcpService();

            Comando = new Comando
            {
                Send = "@ALTERNAR#",
                Port = 9999,
                IP = "192.168.1.200"
            };

            BindingContext = this;
        }

        public Comando Comando { get; set; }

        private async void Save_Clicked(object sender, EventArgs e)
        {
            MessagingCenter.Send(this, "AddItem", Comando);
            try
            {
                TimeSpan duration = TimeSpan.FromMilliseconds(80);
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
            await Navigation.PopModalAsync();
        }

        private async void Cancel_Clicked(object sender, EventArgs e)
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
            await Navigation.PopModalAsync();
        }

        private async void Enviar_UDP_Clicked(object sender, EventArgs e)
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
                Enviar_UDP.IsEnabled = false;
                Enviar_TCP.IsEnabled = false;
                Comando.Receive = await udpService.SendAsync(Comando.IP, (int)Comando.Port, Comando.Send);
                Enviar_UDP.IsEnabled = true;
                Enviar_TCP.IsEnabled = true;
            }
            catch (Exception ex)
            {
                Enviar_UDP.IsEnabled = true;
                Enviar_TCP.IsEnabled = true;
                Debug.WriteLine(ex.Message);
            }
        }

        private async void Enviar_TCP_Clicked(object sender, EventArgs e)
        {
            MessagingCenter.Send(this, "AddItem", Comando);
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
                Enviar_UDP.IsEnabled = false;
                Enviar_TCP.IsEnabled = false;
                Comando.Receive = await tcpService.SendAsync(Comando.IP, Comando.Port, Comando.Send);
                Enviar_UDP.IsEnabled = true;
                Enviar_TCP.IsEnabled = true;
            }
            catch (Exception ex)
            {
                Enviar_UDP.IsEnabled = true;
                Enviar_TCP.IsEnabled = true;
                Debug.WriteLine(ex.Message);
            }
        }
    }
}