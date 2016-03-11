using BootcampTrack.Core.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BootcampTrack.Data.Infrastructure
{
    public class DatabaseFactory : Disposable, IDatabaseFactory
    {
        private readonly BootcampTrackDataContext _dataContext;

        public BootcampTrackDataContext GetDataContext()
        {
            return _dataContext ?? new BootcampTrackDataContext();
        }

        public DatabaseFactory()
        {
            _dataContext = new BootcampTrackDataContext();
        }

        protected override void DisposeCore()
        {
            if (_dataContext != null) _dataContext.Dispose();
        }
    }
}
