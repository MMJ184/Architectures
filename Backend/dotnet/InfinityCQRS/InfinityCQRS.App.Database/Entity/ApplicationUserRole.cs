﻿using Microsoft.AspNetCore.Identity;

namespace InfinityCQRS.Backend.Contracts
{
    public class ApplicationUserRole : IdentityUserRole<string>
    {
        public virtual ApplicationUser User { get; set; }
        public virtual ApplicationRole Role { get; set; }
    }
}
