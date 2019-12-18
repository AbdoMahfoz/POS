using Backend;
using Backend.Models;
using Backend.Services;
using Castle.Facilities.WcfIntegration;
using Castle.Windsor;
using System;
using System.ServiceModel;

namespace BackendHost
{
    class Program
    {
        static void Main(string[] args)
        {
            using (WindsorContainer container = new WindsorContainer())
            {
                container.AddFacility<WcfFacility>();
                container.Install(new DependencyInjection());
                DefaultServiceHostFactory s = new DefaultServiceHostFactory(container.Kernel);
                ServiceHostBase host = s.CreateServiceHost(typeof(TestService).AssemblyQualifiedName, new Uri[0]);
                host.Open();
                Console.WriteLine("On");
                Console.ReadLine();
                host.Close();
            }
        }
    }
}
