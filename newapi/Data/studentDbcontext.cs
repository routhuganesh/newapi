using Microsoft.EntityFrameworkCore;
using newapi.Models;

namespace newapi.Data
{
    public class studentDbcontext : DbContext
    {
        public studentDbcontext(DbContextOptions<studentDbcontext> options) : base(options)
        {
        }
        public DbSet<student> student { get; set; }
    }
}
