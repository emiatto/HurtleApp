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
    public class TelemetriaRepository : ITelemetriaRepository
    {
        private string _connectionString;
        public TelemetriaRepository(string cs)
        {
            _connectionString = cs;
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Telemetria> Get()
        {
            throw new NotImplementedException();
        }

        public Telemetria Get(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Telemetria> GetByIdAttivita(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                var query = @"SELECT [Id]
                                  ,[Latitudine]
                                  ,[Longitudine]
                                  ,[Istante]
                                  ,[IdRunner]
                                  ,[IdAttivita]
                                  ,[UriSelfie]
                              FROM [dbo].[Telemetria]
                              WHERE IdAttivita = @Id";

                return connection.Query<Telemetria>(query, new { Id = id }).ToList();
            }
        }

        public void Insert(Telemetria value)
        {
            throw new NotImplementedException();
        }

        public void Update(Telemetria value)
        {
            throw new NotImplementedException();
        }


    }
}
