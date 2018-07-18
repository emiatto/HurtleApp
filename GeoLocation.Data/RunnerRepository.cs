using Dapper;
using Hurtle.Data.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace GeoLocation.Data
{
    public class RunnerRepository : IRunnerRepository
    {
        private string _connectionString;
        public RunnerRepository(string cs)
        {
            _connectionString = cs;
        }

        public void Delete(int idRunner)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                string query =
                    @"DELETE FROM [dbo].[Runner]
                    WHERE Id = @Id";

                connection.Query(query, new { id = idRunner });
            }
        }

        public IEnumerable<Runner> Get()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                var query = @"SELECT [Id]
                                  ,[Username]
                                  ,[Nome]
                                  ,[Cognome]
                                  ,[DataDiNascita]
                                  ,[Sesso]
                                  ,[FotoUri]
                              FROM [dbo].[Runner]";

                return connection.Query<Runner>(query).ToList();
            }
        }

        public Runner Get(int idRunner)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                var query = @"SELECT [Id]
                                  ,[Username]
                                  ,[Nome]
                                  ,[Cognome]
                                  ,[DataDiNascita]
                                  ,[Sesso]
                                  ,[FotoUri]
                              FROM [dbo].[Runner]
                              WHERE Id = @Id";

                return connection.QueryFirstOrDefault<Runner>(query, new { id = idRunner });
            }
        }

        public Runner GetUserByUsername(string username)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                var query = @"SELECT [Id]
                                  ,[Username]
                                  ,[Nome]
                                  ,[Cognome]
                                  ,[DataDiNascita]
                                  ,[Sesso]
                                  ,[FotoUri]
                              FROM [dbo].[Runner]
                              WHERE Username = @username";

                return connection.QueryFirstOrDefault<Runner>(query, new { username = username });
            }
        }

        public void Insert(Runner value)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                string query =
                    @"
                        INSERT INTO [dbo].[Runner]
                                ([Username]
                                ,[Nome]
                                ,[Cognome]
                                ,[DataDiNascita]
                                ,[Sesso]
                                ,[FotoUri])
                         VALUES
                               (@Username
                               ,@Nome
                               ,@Cognome
                               ,@DataDiNascita
                               ,@Sesso
                               ,@FotoUri)";

                connection.Query(query, value);
            }

        }

        public void Update(Runner value)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                string query =
                    @"UPDATE [dbo].[Runner]
                       SET [Username] = @Username
                          ,[Nome] = @Nome
                          ,[Cognome] = @Cognome
                          ,[DataDiNascita] = @DataDiNascita
                          ,[Sesso] = @Sesso
                          ,[FotoUri] = @FotoUri
                     WHERE Id = @Id";

                connection.Query(query, value);
            }
        }
    }
}
