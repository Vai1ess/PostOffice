using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PostOffice.Models;

namespace PostOffice
{
    public class PostOfficeContext : DbContext
    {
        public DbSet<Client> Clients { get; set; }
        public DbSet<Parcel> Parcels { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=localhost;Initial Catalog=DbPostOffice; User Id=sa; Password=sa; TrustServerCertificate=true");
        }
    }
}