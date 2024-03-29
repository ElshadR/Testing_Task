﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Formatting;
using System.Web.Http;

namespace Task.Api
{
    public static class WebApiConfig
    {
        
        public static string UrlPrefix { get { return "api"; } }
        public static string UrlPrefixRelative { get { return "~/api"; } }
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            config.Formatters.Clear();
            config.Formatters.Add(new JsonMediaTypeFormatter());
            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                 name: "DefaultApi",
                 routeTemplate: WebApiConfig.UrlPrefix + "/{controller}/{id}",
                 defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
