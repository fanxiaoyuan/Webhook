using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;
using Microsoft.AspNet.WebHooks;
using Microsoft.AspNet.WebHooks.Diagnostics;

namespace Prototype.WebHook.Publisher
{
    public class CustomSender : DataflowWebHookSender, IHookSender
    {
        public CustomSender(ILogger logger) : base(logger)
        {
        }

        public CustomSender(ILogger logger, IEnumerable<TimeSpan> retryDelays, ExecutionDataflowBlockOptions options) : base(logger, retryDelays, options)
        {
        }

        public void LaunchHook(string uri, string secret, string action, object data)
        {
            Task.Run(() =>
            {
                var notifications = new[] {new NotificationDictionary(action, data)};
                var webHook = new Microsoft.AspNet.WebHooks.WebHook
                {
                    Description = action,
                    WebHookUri = new Uri(uri),
                    Secret = secret
                };
                var workItem = new WebHookWorkItem(webHook, notifications);
                SendWebHookWorkItemsAsync(new[] {workItem});
            });
        }
    }
}
