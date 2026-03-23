using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistance.Context
{
    public class BlogDbContextFactory : IDesignTimeDbContextFactory<BlogDbContext>
    {
        public BlogDbContext CreateDbContext(string[] args)
        {
            var optionsbuilder = new DbContextOptionsBuilder<BlogDbContext>();
            optionsbuilder.UseSqlServer("Server=localhost,1433;Database=Blog;User Id=sa;Password=Manoj@123;Encrypt=True;TrustServerCertificate=True;");

            return new BlogDbContext(optionsbuilder.Options);
        }
    }
}
