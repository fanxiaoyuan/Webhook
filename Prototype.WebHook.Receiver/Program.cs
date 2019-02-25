using System;
using Microsoft.Owin.Hosting;

namespace Prototype.WebHook.Receiver
{
    class Program
    {
        static void Main(string[] args)
        {
            var localAddress = $"http://{IpAddressResolver.GetLocalIpAddress()}:1986";
            using (WebApp.Start<Startup>(localAddress))
            {
                Console.WriteLine($"{nameof(Receiver)} running on {localAddress}");
                Console.ReadLine();
            }
        }
    }
}
