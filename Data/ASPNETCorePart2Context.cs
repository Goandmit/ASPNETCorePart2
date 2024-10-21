using ASPNETCorePart2.Models;
using Microsoft.EntityFrameworkCore;

namespace ASPNETCorePart2.Data
{
    public class ASPNETCorePart2Context : DbContext
    {
        public DbSet<PhoneBookItem> PhoneBook => Set<PhoneBookItem>();

        public ASPNETCorePart2Context (DbContextOptions<ASPNETCorePart2Context> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
