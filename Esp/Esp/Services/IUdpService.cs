using System.Threading.Tasks;

namespace ConfigurationFlexCloudHubBlaster.Service
{
    public interface IUdpService
    {
        Task Broadcast(int port = 9999, string comando = "oi");

        void Send(string ip, string buf, int port = 6666);

        Task<string> SendAsync(string ip, int port, string Comando);
    }
}