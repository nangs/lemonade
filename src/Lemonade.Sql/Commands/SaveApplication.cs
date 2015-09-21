using System.Data.Common;
using Dapper;
using Lemonade.Data.Commands;
using Lemonade.Data.Entities;
using Lemonade.Data.Exceptions;

namespace Lemonade.Sql.Commands
{
    public class SaveApplication : LemonadeConnection, ISaveApplication
    {
        public SaveApplication()
        {
        }

        public SaveApplication(string connectionStringName) : base(connectionStringName)
        {
        }

        public void Execute(Application application)
        {
            using (var cnn = CreateConnection())
            {
                try
                {
                    cnn.Execute("INSERT INTO Application (Name) VALUES (@Name)", new { application.Name });
                }
                catch (DbException exception)
                {
                    throw new SaveApplicationException(application.Name, exception);
                }
            }
        }
    }
}