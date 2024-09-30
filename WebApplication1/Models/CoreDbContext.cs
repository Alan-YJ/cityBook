using Microsoft.EntityFrameworkCore;

namespace WebApplication1.Models
{
    public class CoreDbContext: DbContext
    {
        public virtual DbSet<Book> Book { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured) {
                optionsBuilder.UseMySql(@"Server=47.108.165.93;port=3306;database=bookcity;uid=yijing;pwd=123456;", new MySqlServerVersion(new Version(8,0,2)));
            }
        }

        /// <summary>
        /// 重写OnModelCreating方法，配置映射表
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Book>().ToTable("book");
            base.OnModelCreating(modelBuilder);
        }
    }
}
