using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Telegenic.Repository;

namespace Telegenic.Web.Plumbing.CastleWindsor.Installers
{
    public class RepositoriesInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Classes.FromAssemblyInThisApplication()
                .Where(Component.IsInSameNamespaceAs<SeriesRepository>())
                .WithService.DefaultInterfaces()
                .LifestyleTransient());
        }
    }
}