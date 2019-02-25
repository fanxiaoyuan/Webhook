using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.WebHooks;
using Newtonsoft.Json.Linq;

namespace Prototype.WebHook.Receiver
{
    public class CustomReceiver : WebHookHandler
    {
        public override Task ExecuteAsync(string receiver, WebHookHandlerContext context)
        {
            var notifications = context.GetDataOrDefault<JObject>()["Notifications"];
            var result = notifications.ToObject<CustomResult[]>();
            Console.WriteLine($"================ received {string.Join(";", result.Select(x=>x.ToString()))}");
            return Task.FromResult(true);
        }
    }
}
