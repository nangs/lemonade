﻿using Lemonade.Domain.Infrastructure;

namespace Lemonade.Domain.Events
{
    public class ConfigurationHasBeenUpdated : IDomainEvent
    {
        public int ConfigurationId { get; private set; }
        public string Name { get; private set; }

        public ConfigurationHasBeenUpdated(int configurationId, string name)
        {
            ConfigurationId = configurationId;
            Name = name;
        }
    }
}