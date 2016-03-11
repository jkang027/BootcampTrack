using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BootcampTrack.Data.Infrastructure
{
    public interface IDatabaseFactory : IDisposable
    {
        BootcampTrackDataContext GetDataContext();
    }
}
