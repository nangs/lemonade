﻿using System.Data.Common;
using System.Linq;
using Dapper;
using Lemonade.Data.Commands;
using Lemonade.Data.Exceptions;

namespace Lemonade.Sql.Commands
{
    public class CreateFeature : LemonadeConnection, ICreateFeature
    {
        public CreateFeature()
        {
        }

        public CreateFeature(string connectionStringName) : base(connectionStringName)
        {
        }

        public void Execute(Data.Entities.Feature feature)
        {
            using (var cnn = CreateConnection())
            {
                try
                {
                    feature.FeatureId = cnn.Query<int>(@"INSERT INTO Feature (IsEnabled, Name, ApplicationId)
                                                         VALUES (@IsEnabled, @Name, @ApplicationId);
                                                         SELECT SCOPE_IDENTITY();", new
                    {
                        feature.IsEnabled,
                        feature.Name,
                        feature.ApplicationId
                    }).First();
                }
                catch (DbException exception)
                {
                    throw new CreateFeatureException(exception);
                }
            }
        }
    }
}