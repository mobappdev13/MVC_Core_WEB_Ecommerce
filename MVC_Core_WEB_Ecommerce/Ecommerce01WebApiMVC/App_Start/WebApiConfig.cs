using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Web.Http;

namespace Ecommerce01WebApiMVC
{
    public class CustomJsonFormatter : JsonMediaTypeFormatter
    {
        public CustomJsonFormatter()
        {
            this.SupportedMediaTypes.Add
                (new System.Net.Http.Headers.MediaTypeHeaderValue("text/html"));
        }

        public override void SetDefaultContentHeaders(Type type, HttpContentHeaders headers, MediaTypeHeaderValue mediaType)
        {
            base.SetDefaultContentHeaders(type, headers, mediaType);
            headers.ContentType = new MediaTypeHeaderValue("application/json");
        }
    }
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            

            // Servizi e configurazione dell'API Web

            // Route dell'API Web
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );


            // config.Formatters.Remove(config.Formatters.XmlFormatter);
            var json = config.Formatters.JsonFormatter;
            json.SerializerSettings.PreserveReferencesHandling = Newtonsoft.Json.PreserveReferencesHandling.Objects;
            //response only json (1)
            //config.Formatters.Remove(config.Formatters.XmlFormatter);
            //response only xml (2)
            //config.Formatters.Remove(config.Formatters.JsonFormatter);
            //thinking to browsers (3)
            //config.Formatters.JsonFormatter.SupportedMediaTypes.Add
            //    (new System.Net.Http.Headers.MediaTypeHeaderValue("text/html"));
            config.Formatters.Add(new CustomJsonFormatter());   

        }

    }
}
//GlobalConfiguration.Configuration.Formatters.JsonFormatter.MediaTypeMappings.Add(
//new QueryStringMapping("type", "json", new MediaTypeHeaderValue("application/json")));