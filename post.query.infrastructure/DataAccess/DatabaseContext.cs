using Microsoft.EntityFrameworkCore;
using post.query.domain.Entities;

namespace post.query.infrastructure.DataAccess
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions options): base(options) { }    
        public DbSet<PostEntity> Posts { get; set; }
        public DbSet<CommentEntity> Comments { get; set; }
    }
}
