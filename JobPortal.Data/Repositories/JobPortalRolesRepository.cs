﻿using JobPortal.Data.Models;
using JobPortal.Data.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace JobPortal.Data.Repositories
{
    public class JobPortalRolesRepository : JobPortalRepository<Role>, IJobPortalRoleRepository
    {
        public JobPortalRolesRepository(JobDbContext jobDbContext) : base(jobDbContext)
        {
        }
    }
}
