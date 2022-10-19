using Newtonsoft.Json;
using System.Web.Http;
using System.Web.Http.Cors;
using WebApiCRUD.Helpers;

namespace WebApiCRUD
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.MapHttpAttributeRoutes();

            config.Formatters.JsonFormatter.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;

            //string ip1 = "192.168.1.1";
            //string ip2 = "192.168.1.2";
            //var cors = new EnableCorsAttribute($"{ip1},{ip2}", "*", "*");
            var cors = new EnableCorsAttribute($"*", "*", "*");
            config.EnableCors(cors);

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            config.Filters.Add(new MyModelValidationAttribute());
            config.Filters.Add(new MyErrorAttribute());
        }
    }
}
