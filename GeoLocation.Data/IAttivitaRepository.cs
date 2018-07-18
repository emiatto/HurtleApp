using GeoLocation.Data;
using Hurtle.Data.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hurtle.Data
{
    public interface IAttivitaRepository : IRepository<Attivita, int>
    {
        IEnumerable<Attivita> GetByUser(int idrunner);
    }
}
