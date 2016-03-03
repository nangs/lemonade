﻿using System.Collections.Generic;
using System.Linq;
using Lemonade.Data.Commands;
using Lemonade.Data.Exceptions;
using Lemonade.Data.Queries;
using Lemonade.Web.Contracts;
using Lemonade.Web.Events;
using Lemonade.Web.Infrastructure;
using Lemonade.Web.Mappers;
using Nancy;
using Nancy.ModelBinding;

namespace Lemonade.Web.Modules
{
    public class ResourcesModule : NancyModule
    {
        public ResourcesModule(IDomainEventDispatcher eventDispatcher, IGetResource getResource, IGetAllResourcesByApplicationId getAllResourcesbyApplicationId, ICreateResource createResource, IDeleteResource deleteResource, IUpdateResource updateResource)
        {
            _eventDispatcher = eventDispatcher;
            _getResource = getResource;
            _getAllResourcesbyApplicationId = getAllResourcesbyApplicationId;
            _createResource = createResource;
            _deleteResource = deleteResource;
            _updateResource = updateResource;
            Get["/api/resource"] = r => GetResource();
            Get["/api/resources"] = r => GetResources();
            Post["/api/resources"] = r => CreateResource();
            Put["/api/resources"] = r => UpdateResource();
            Delete["api/resources"] = r => DeleteResource();
        }

        private Resource GetResource()
        {
            var application = Request.Query["application"].Value as string;
            var resourceSet = Request.Query["resourceSet"].Value as string;
            var resourceKey = Request.Query["resourceKey"].Value as string;
            var locale = Request.Query["locale"].Value as string;
            var resource = _getResource.Execute(application, resourceSet, resourceKey, locale);

            return resource.ToContract();
        }

        private IList<Resource> GetResources()
        {
            int applicationId;
            int.TryParse(Request.Query["applicationId"].Value as string, out applicationId);

            var resources = _getAllResourcesbyApplicationId.Execute(applicationId);

            return resources.Select(r => r.ToContract()).ToList();
        }

        private HttpStatusCode CreateResource()
        {
            try
            {
                var resource = this.Bind<Resource>().ToEntity();
                _createResource.Execute(resource);
                _eventDispatcher.Dispatch(new ResourceHasBeenCreated(resource.ResourceId, resource.ApplicationId, resource.ResourceSet, resource.ResourceKey, resource.Locale, resource.Value));

                return HttpStatusCode.OK;
            }
            catch (CreateResourceException exception)
            {
                _eventDispatcher.Dispatch(new ResourceErrorHasOccurred(exception.Message));
                return HttpStatusCode.BadRequest;
            }
        }

        private HttpStatusCode UpdateResource()
        {
            try
            {
                var resource = this.Bind<Resource>();
                _updateResource.Execute(resource.ToEntity());
                _eventDispatcher.Dispatch(new ResourceHasBeenUpdated(resource.ResourceId, resource.ApplicationId, resource.ResourceSet, resource.ResourceKey, resource.Locale, resource.Value));

                return HttpStatusCode.OK;
            }
            catch (UpdateFeatureException exception)
            {
                _eventDispatcher.Dispatch(new ResourceErrorHasOccurred(exception.Message));
                return HttpStatusCode.BadRequest;
            }
        }

        private HttpStatusCode DeleteResource()
        {
            int resourceId;
            int.TryParse(Request.Query["id"].Value as string, out resourceId);

            try
            {
                _deleteResource.Execute(resourceId);
                _eventDispatcher.Dispatch(new ResourceHasBeenDeleted(resourceId));
                return HttpStatusCode.OK;
            }
            catch (DeleteResourceException exception)
            {
                _eventDispatcher.Dispatch(new ResourceErrorHasOccurred(exception.Message));
                return HttpStatusCode.BadRequest;
            }
        }

        private readonly IDomainEventDispatcher _eventDispatcher;
        private readonly IGetResource _getResource;
        private readonly IGetAllResourcesByApplicationId _getAllResourcesbyApplicationId;
        private readonly ICreateResource _createResource;
        private readonly IDeleteResource _deleteResource;
        private readonly IUpdateResource _updateResource;
    }
}