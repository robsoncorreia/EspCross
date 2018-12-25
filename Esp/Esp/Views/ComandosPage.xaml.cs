using Esp.Models;
using Esp.ViewModels;
using System;
using System.Diagnostics;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Esp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ComandosPage : ContentPage
    {
        private ComandosViewModel viewModel;

        public ComandosPage()
        {
            InitializeComponent();

            BindingContext = viewModel = new ComandosViewModel();

            Appearing += AppearingPage;
        }

        private void AppearingPage(object sender, EventArgs e)
        {
            viewModel.LoadItemsCommand.Execute(null);
        }

        private async void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            var item = args.SelectedItem as Comando;
            if (item == null)
                return;

            await Navigation.PushAsync(new ComandoDetail(new ComandoDetailViewModel(item)));

            // Manually deselect item.
            ItemsListView.SelectedItem = null;
        }

        private async void AddItem_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new NavigationPage(new NovoComando()));
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (viewModel.Comandos.Count == 0)
                viewModel.LoadItemsCommand.Execute(null);
        }
    }
}