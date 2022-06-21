using FabrikamFiber.Web.App_Start;
using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FabrikamFiber.Web
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.UseNinject(NinjectMVC3.CreateKernel);
        }
    }
}