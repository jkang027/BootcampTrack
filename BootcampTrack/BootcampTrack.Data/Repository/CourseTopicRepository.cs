﻿using BootcampTrack.Core.Domain;
using BootcampTrack.Core.Repository;
using BootcampTrack.Data.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BootcampTrack.Data.Repository
{
    public class CourseTopicRepository : Repository<CourseTopic>, ICourseTopicRepository
    {
        public CourseTopicRepository(IDatabaseFactory databaseFactory) : base(databaseFactory)
        {
        }
    }
}