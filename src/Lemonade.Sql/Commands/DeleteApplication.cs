using Dapper;
using Lemonade.Data.Commands;

namespace Lemonade.Sql.Commands
{
    public class DeleteApplication : LemonadeConnection, IDeleteApplication
    {
        public DeleteApplication()
        {
        }

        public DeleteApplication(string connectionStringName) : base(connectionStringName)
        {
        }

        public void Execute(int applicationId)
        {
            using (var cnn = CreateConnection())
            {
                cnn.Query("DELETE FROM Application WHERE ApplicationId = @applicationId", new { applicationId });
            }
        }
    }
}