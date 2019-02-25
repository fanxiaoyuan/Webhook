using System.Linq;
using System.Net;
using System.Net.Sockets;

namespace Prototype.WebHook.Receiver
{
    public class IpAddressResolver
    {
        public static string GetLocalIpAddress()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            return host.AddressList.FirstOrDefault(ip => ip.AddressFamily == AddressFamily.InterNetwork)?.ToString();
        }
    }
}
