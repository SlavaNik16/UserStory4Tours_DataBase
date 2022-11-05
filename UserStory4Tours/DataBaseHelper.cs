using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace UserStory4Tours
{
    public static class DataBaseHelper
    {
        public static DbContextOptions<ApplicationContext> Option()
        {
            var builder = new ConfigurationBuilder();
            builder.SetBasePath(Directory.GetCurrentDirectory());
            builder.AddJsonFile("appsettings.json");
            var config = builder.Build();
            string connectionString = config.GetConnectionString("DefaultConnection");
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationContext>();
            return  optionsBuilder
                    .UseSqlServer(connectionString)
                    .Options;
        }
    }
}
