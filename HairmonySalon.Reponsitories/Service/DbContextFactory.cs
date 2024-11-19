using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Harmony.Repositories.Service
{
    public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
    {
        public ApplicationDbContext CreateDbContext(string[] args)
        {
            // Tạo builder cho DbContextOptions
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();

            // Cấu hình sử dụng SQL Server với chuỗi kết nối
            optionsBuilder.UseSqlServer("Data Source=.;Initial Catalog=HairHarmonySalon;Integrated Security=True;Trust Server Certificate=True");

            // Trả về một instance của ApplicationDbContext
            return new ApplicationDbContext(optionsBuilder.Options);
        }
    }
}
