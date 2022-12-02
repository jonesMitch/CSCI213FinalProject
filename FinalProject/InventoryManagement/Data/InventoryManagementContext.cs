using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using InventoryManagement.Models;

namespace InventoryManagement.Data
{
    public class InventoryManagementContext : DbContext
    {
        public InventoryManagementContext (DbContextOptions<InventoryManagementContext> options)
            : base(options)
        {
        }

        public DbSet<InventoryManagement.Models.Computer> Computer { get; set; } = default!;
    }
}
