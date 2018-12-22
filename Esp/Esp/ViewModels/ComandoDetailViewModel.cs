using Esp.Models;

namespace Esp.ViewModels
{
    public class ComandoDetailViewModel : BaseViewModel
    {
        public Comando Comando { get; set; }
        public string Titulo { get; set; }

        public ComandoDetailViewModel(Comando comando = null)
        {
            Comando = comando;
            Titulo = comando.IP;
        }
    }
}