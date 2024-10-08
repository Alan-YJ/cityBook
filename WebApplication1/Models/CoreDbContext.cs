using Microsoft.EntityFrameworkCore;

namespace WebApplication1.Models
{
    public partial class CoreDbContext: DbContext
    {
        public virtual DbSet<Book> Book { get; set; }

        public CoreDbContext(DbContextOptions<CoreDbContext> options) : base(options) { 
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
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
