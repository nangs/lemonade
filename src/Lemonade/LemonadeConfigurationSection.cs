﻿using System;
using System.Configuration;

namespace Lemonade
{
    public class LemonadeConfigurationSection : ConfigurationSection
    {
        public static readonly LemonadeConfigurationSection Current = GetSection();

        [ConfigurationProperty("FeatureResolver", IsRequired = false)]
        public string FeatureResolver => this["FeatureResolver"] as string;

        [ConfigurationProperty("ConfigurationResolver", IsRequired = false)]
        public string ConfigurationResolver => this["ConfigurationResolver"] as string;

        [ConfigurationProperty("ResourceResolver", IsRequired = false)]
        public string ResourceResolver => this["ResourceResolver"] as string;

        [ConfigurationProperty("CacheProvider", IsRequired = false)]
        public string CacheProvider => this["CacheProvider"] as string;

        [ConfigurationProperty("CacheExpiration", IsRequired = false)]
        public double? CacheExpiration => this["CacheExpiration"] as double? ?? 0;

        [ConfigurationProperty("ApplicationName", IsRequired = false)]
        public string ApplicationName => this["ApplicationName"] as string ?? AppDomain.CurrentDomain.FriendlyName.Replace(".exe", string.Empty);

        [ConfigurationProperty("RetryPolicy", IsRequired = false)]
        public string RetryPolicy => this["RetryPolicy"] as string;

        [ConfigurationProperty("MaximumAttempts", IsRequired = false)]
        public int? MaximumAttempts => this["MaximumAttempts"] as int? ?? 3;

        private static LemonadeConfigurationSection GetSection()
        {
            var section = (LemonadeConfigurationSection)ConfigurationManager.GetSection("Lemonade");
            return section ?? new LemonadeConfigurationSection();
        }
    }
}