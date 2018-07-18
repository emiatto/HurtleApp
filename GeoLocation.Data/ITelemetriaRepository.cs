using GeoLocation.Data;
using Hurtle.Data.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hurtle.Data
{
    public interface ITelemetriaRepository : IRepository<Telemetria, int>
    {
        IEnumerable<Telemetria> GetByIdAttivita(int id);
    }
}
