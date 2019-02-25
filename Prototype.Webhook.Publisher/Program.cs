using System;
using Microsoft.AspNet.WebHooks.Diagnostics;
using Microsoft.Owin.Hosting;

namespace Prototype.WebHook.Publisher
{
    public class Program
    {
        static void Main(string[] args)
        {
            var localAddress = "http://localhost:9000";
            SingletonSender.Instance.SetSender(new CustomSender(new TraceLogger()));
            using (WebApp.Start<SwaggerConfig>(localAddress))
            {
                Console.WriteLine($"{nameof(Publisher)} running on {localAddress}");
                Console.ReadLine();
            }
        }
    }
}
