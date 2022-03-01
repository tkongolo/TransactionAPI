using Microsoft.EntityFrameworkCore;
using TransactionAPI.Models;

namespace TransactionAPI.Data
{
    public class TransactionContext : DbContext
    {
        public TransactionContext(DbContextOptions<TransactionContext>opt): base(opt)
        {

        }

        public DbSet<UserLedger>? UserLedgers { get; set; }
        public DbSet<UserLoginToken>? UserLoginTokens { get; set; }
        public DbSet<UserResetToken>? UserResetTokens { get; set; }
        public DbSet<Users>? Users { get; set; }
    }
}
