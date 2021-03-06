﻿using System;
using System.IO;
using System.Linq;
using System.Reflection;
using Lemonade.Web.Core.CommandHandlers;
using Lemonade.Web.Core.EventHandlers;
using Lemonade.Web.Core.QueryHandlers;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Infrastructure;
using Nancy;
using Nancy.Bootstrapper;
using Nancy.Conventions;
using Nancy.Hosting.Aspnet;
using Nancy.Responses;
using Nancy.TinyIoc;
using Nancy.ViewEngines;

namespace Lemonade.Web.Infrastructure
{
    public class LemonadeBootstrapper : DefaultNancyBootstrapper
    {
        protected override IRootPathProvider RootPathProvider => new AspNetRootPathProvider();

        protected override void ConfigureConventions(NancyConventions conventions)
        {
            base.ConfigureConventions(conventions);

            foreach (var assembly in AppDomainAssemblyTypeScanner.Assemblies)
            {
                MapResourcesFromAssembly(conventions, assembly);
            }
        }

        protected override void ConfigureApplicationContainer(TinyIoCContainer container)
        {
            base.ConfigureApplicationContainer(container);

            container.RegisterImplementations(typeof(IDomainEventHandler<>));
            container.RegisterImplementations(typeof(IQueryHandler<,>));
            container.RegisterImplementations(typeof(ICommandHandler<>));
            container.Register(GetConnectionManager());

            ConfigureDependencies(container);
        }

        protected override NancyInternalConfiguration InternalConfiguration
        {
            get { return NancyInternalConfiguration.WithOverrides(nic => nic.ViewLocationProvider = typeof(ResourceViewLocationProvider)); }
        }

        protected virtual IConnectionManager GetConnectionManager()
        {
            return GlobalHost.ConnectionManager;
        }

        protected virtual void ConfigureDependencies(TinyIoCContainer container)
        {
        }

        private static void MapResourcesFromAssembly(NancyConventions conventions, Assembly assembly)
        {
            var resourceNames = assembly.GetManifestResourceNames();

            conventions.StaticContentsConventions.Add((ctx, p) =>
            {
                var filename = Path.GetFileName(ctx.Request.Path);
                var directoryName = Path.GetDirectoryName(ctx.Request.Path);
                var path = assembly.GetName().Name + directoryName?
                    .Replace(Path.DirectorySeparatorChar, '.')
                    .Replace("-", "_")
                    .TrimEnd('.');

                var name = string.Concat(path, ".", filename);

                return resourceNames.Any(r => r.Equals(name, StringComparison.InvariantCultureIgnoreCase))
                    ? new EmbeddedFileResponse(assembly, path, filename)
                    : null;
            });
        }
    }
}