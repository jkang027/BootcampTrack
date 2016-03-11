using BootcampTrack.Core.Domain;
using BootcampTrack.Core.Repository;
using BootcampTrack.Data.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BootcampTrack.Data.Repository
{
    public class SubmissionRepository : Repository<Submission>, ISubmissionRepository
    {
        public SubmissionRepository(IDatabaseFactory databaseFactory) : base(databaseFactory)
        {
        }
    }
}
