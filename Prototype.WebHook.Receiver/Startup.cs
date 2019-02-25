using System.Web.Http;
using System.Web.Http.Dispatcher;
using Microsoft.AspNet.WebHooks;
using Owin;

namespace Prototype.WebHook.Receiver
{
    public class Startup
    {
        public void Configuration(IAppBuilder appBuilder)
        {
            var config = new HttpConfiguration();
            var assemblyResolver = new WebHookAssemblyResolver();
            config.Services.Replace(typeof(IAssembliesResolver), assemblyResolver);
            config.MapHttpAttributeRoutes();
            config.Routes.MapHttpRoute(
                "DefaultApi",
                "api/{controller}/{id}",
                new {id = RouteParameter.Optional});
            config.InitializeReceiveCustomWebHooks();
            appBuilder.UseWebApi(config);
        }
    }
}
