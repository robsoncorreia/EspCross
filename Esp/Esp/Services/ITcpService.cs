using System.Threading.Tasks;

namespace ConfigurationFlexCloudHubBlaster.Service
{
    public interface ITcpService
    {
        Task<string> SendAsync(string ip, int port, string command);
    }
}