using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using Dapper;
using GeoLocation.Data;
using Hurtle.Data.Model;

namespace Hurtle.Data
{
    public class AttivitaRepository : IAttivitaRepository
    {
        private string _connectionString;
        public AttivitaRepository(string cs)
        {
            _connectionString = cs;
        }

        public void Delete(int idAttivita)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                string query =
                    @"DELETE FROM [dbo].[Attivita]
                    WHERE Id = @Id";

                connection.Query(query, new { id = idAttivita });
            }
        }

        public IEnumerable<Attivita> Get()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                var query = @"SELECT [Id]
                                  ,[Titolo]
                                  ,[IdRunner]
                                  ,[DataCreazione]
                                  ,[Localita]
                                  ,[Tipologia]
                                  ,[UriGara]
                              FROM [dbo].[Attivita]";

                return connection.Query<Attivita>(query).ToList();
            }
        }

        public Attivita Get(int idAttivita)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                var query = @"SELECT [Id]
                                  ,[Titolo]
                                  ,[IdRunner]
                                  ,[DataCreazione]
                                  ,[Localita]
                                  ,[Tipologia]
                                  ,[UriGara]
                              FROM [dbo].[Attivita]
                              WHERE Id = @Id";

                return connection.QueryFirstOrDefault<Attivita>(query, new { id = idAttivita });
            }
        }

        public IEnumerable<Attivita> GetByUser(int idrunner)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                var query = @"SELECT [Id]
                                  ,[Titolo]
                                  ,[IdRunner]
                                  ,[DataCreazione]
                                  ,[Localita]
                                  ,[Tipologia]
                                  ,[UriGara]
                              FROM [dbo].[Attivita]
                              WHERE IdRunner = @Id";

                return connection.Query<Attivita>(query, new { id = idrunner }).ToList();
            }
        }

        public void Insert(Attivita value)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                string query =
                    @"
                        INSERT INTO [dbo].[Attivita]
                                ([Titolo]
                                ,[IdRunner]
                                ,[DataCreazione]
                                ,[Localita]
                                ,[Tipologia]
                                ,[UriGara])
                         VALUES
                               (@Titolo
                               ,@IdRunner
                               ,@DataCreazione
                               ,@Localita
                               ,@Tipologia
                               ,@UriGara)";

                connection.Query(query, value);
            }

        }

        public void Update(Attivita value)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                string query =
                    @"UPDATE [dbo].[Attivita]
                       SET [Titolo] = @Titolo
                          ,[IdRunner] = @IdRunner
                          ,[DataCreazione] = @DataCreazione
                          ,[Localita] = @Localita
                          ,[Tipologia] = @Tipologia
                          ,[UriGara] = @UriGara
                     WHERE Id = @Id";

                connection.Query(query, value);
            }
        }
    }
}
