﻿using JobPortal.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace JobPortal.Data.Repositories.Interfaces
{
    public interface IJobPortalJobRepository : IJobPortalRepository<Job>
    {
        Task DeActivateJob(int jobId);
    }
}
