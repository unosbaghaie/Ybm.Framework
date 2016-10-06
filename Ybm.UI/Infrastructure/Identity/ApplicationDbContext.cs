﻿using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ybm.UI.Infrastructure.Identity
{
    public class ApplicationDbContext :IdentityDbContext<CustomUser>
    {

        public ApplicationDbContext()
            :base()
        {

        }
        public static ApplicationDbContext Create()
        {

            return new ApplicationDbContext();

        }

    }
}