using Hurtle.Data.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace GeoLocation.Data
{
    public interface IRunnerRepository : IRepository<Runner, int>
    {
        Runner GetUserByUsername(string username);
    }
}
