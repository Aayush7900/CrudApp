﻿using App.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Infrastructure.Data {
    public class ApplicationDbContext:DbContext {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) {
        }
        public DbSet<User>Users { get; set; }   
        public DbSet<Product>Products { get; set; }
    }
}
