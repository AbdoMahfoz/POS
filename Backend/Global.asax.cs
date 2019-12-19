using Castle.Facilities.WcfIntegration;
using Castle.Windsor;
using System;
using System.Web;

namespace Backend
{
    public class Global : HttpApplication
    {
        static IWindsorContainer container;
        protected void Application_Start(object sender, EventArgs e)
        {
            container = new WindsorContainer();
            container.AddFacility<WcfFacility>();
            container.Install(new DependencyInjection());
        }
    }
}