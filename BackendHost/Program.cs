﻿using Backend;
using Backend.Models;
using Backend.Services;
using Castle.Facilities.WcfIntegration;
using Castle.Windsor;
using System;
using System.IO;
using System.ServiceModel;

namespace BackendHost
{
    class Program
    {
        static void Main()
        {
            using (WindsorContainer container = new WindsorContainer())
            {
                string dbDir = 
                    Directory.GetParent(Directory.GetParent(Directory.GetParent(Directory.GetCurrentDirectory()).FullName).FullName).FullName;
                ApplicationDbContext.ConnectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;" +
                                                       $"AttachDbFilename={dbDir}\\Backend\\App_Data\\POSDb.mdf;" +
                                                        "Integrated Security=True";
                container.AddFacility<WcfFacility>();
                container.Install(new DependencyInjection());
                DefaultServiceHostFactory s = new DefaultServiceHostFactory(container.Kernel);
                ServiceHostBase userHost = s.CreateServiceHost(typeof(UserService).AssemblyQualifiedName, new Uri[0]);
                userHost.Open();
                Console.WriteLine("UserService On");
                Console.ReadLine();
                userHost.Close();
            }
        }
    }
}
