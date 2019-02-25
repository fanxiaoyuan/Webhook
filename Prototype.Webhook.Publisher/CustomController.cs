using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Results;
using Prototype.Webhook.Publisher;

namespace Prototype.WebHook.Publisher
{
    public class CustomController : ApiController
    {
        public IHttpActionResult Get()
        {
            return Ok();
        }

        public IEnumerable<string> Put()
        {
            return new[] {"value1", "value2"};
        }

        public IEnumerable<string> Delete()
        {
            return new[] { "value1", "value2" };
        }

        public IHttpActionResult Post(Request request)
        {
            Task.Delay(500).ContinueWith(t =>
            {
                var result = new ProcessResult {FirstName = "Toto", LastName = "Bean"};
                SingletonSender.Instance.Sender.LaunchHook(request.CallbackUri, request.Secret, "process completed", result);
                Console.WriteLine($"========={request.CallbackUri}, Secret={request.Secret} completed=========");
            });
            return new OkNegotiatedContentResult<string>("processing", this);
        }
    }
}
