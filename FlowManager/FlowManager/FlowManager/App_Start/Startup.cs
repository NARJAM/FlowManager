using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Owin;
using Microsoft.Owin;
[assembly: OwinStartup(typeof(WebApplication4.Startup))]

namespace WebApplication4
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            Microsoft.AspNet.SignalR.HubConfiguration config = new Microsoft.AspNet.SignalR.HubConfiguration();
            config.EnableJSONP = false;
            config.EnableJavaScriptProxies = false;
            app.MapSignalR(config);
   
        }
        
    }
}