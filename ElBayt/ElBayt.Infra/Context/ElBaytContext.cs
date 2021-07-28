using ElBayt.Infra.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace ElBayt.Infra.Context
{
    public class ElBaytContext : IdentityDbContext
    {
        public ElBaytContext(DbContextOptions<ElBaytContext> options)
          : base(options)
        {
        }

        public virtual DbSet<ProductModel> Products { get; set; }
    }
}
