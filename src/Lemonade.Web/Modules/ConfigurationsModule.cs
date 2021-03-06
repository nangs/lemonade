﻿using System;
using System.Collections.Generic;
using Lemonade.Web.Contracts;
using Lemonade.Web.Core.Commands;
using Lemonade.Web.Core.Queries;
using Lemonade.Web.Core.Services;
using Lemonade.Web.Infrastructure;
using Nancy;
using Nancy.ModelBinding;

namespace Lemonade.Web.Modules
{
    public class ConfigurationsModule : NancyModule
    {
        public ConfigurationsModule(ICommandDispatcher commandDispatcher, IQueryDispatcher queryDispatcher)
        {
            _commandDispatcher = commandDispatcher;
            _queryDispatcher = queryDispatcher;
            Get["/api/configurations"] = p => GetConfigurations();
            Get["/api/configuration"] = p => GetConfiguration();
            Post["/api/configurations"] = p => PostConfiguration();
            Put["/api/configurations"] = p => PutConfiguration();
            Delete["/api/configurations"] = p => DeleteConfiguration();
        }

        private Configuration GetConfiguration()
        {
            var configurationName = Request.Query["configuration"].Value as string;
            var applicationName = Request.Query["application"].Value as string;
            var configuration = _queryDispatcher.Dispatch(new GetConfigurationByNameAndApplicationQuery(applicationName, configurationName));

            return configuration;
        }

        private IList<Configuration> GetConfigurations()
        {
            int applicationId;
            int.TryParse(Request.Query["applicationId"].Value as string, out applicationId);

            var configurations = _queryDispatcher.Dispatch(new GetAllConfigurationsByApplicationIdQuery(applicationId));

            return configurations;
        }

        private Response PostConfiguration()
        {
            try
            {
                var configuration = this.Bind<Configuration>();
                _commandDispatcher.Dispatch(new CreateConfigurationCommand(configuration.ApplicationId, configuration.Name, configuration.Value));

                return HttpStatusCode.OK;
            }
            catch (Exception ex)
            {
                return Responses.BadRequest(ex);
            }
        }

        private Response PutConfiguration()
        {
            try
            {
                var configuration = this.Bind<Configuration>();
                _commandDispatcher.Dispatch(new UpdateConfigurationCommand(configuration.ConfigurationId, configuration.Name, configuration.Value));

                return HttpStatusCode.OK;
            }
            catch (Exception ex)
            {
                return Responses.BadRequest(ex);
            }
        }

        private Response DeleteConfiguration()
        {
            int configurationId;
            int.TryParse(Request.Query["id"].Value as string, out configurationId);

            try
            {
                _commandDispatcher.Dispatch(new DeleteConfigurationCommand(configurationId));
                return HttpStatusCode.OK;
            }
            catch (Exception ex)
            {
                return Responses.BadRequest(ex);
            }
        }

        private readonly ICommandDispatcher _commandDispatcher;
        private readonly IQueryDispatcher _queryDispatcher;
    }
}