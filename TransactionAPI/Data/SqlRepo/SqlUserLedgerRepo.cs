using TransactionAPI.Data.IRepo;
using TransactionAPI.Models;

namespace TransactionAPI.Data.SqlRepo
{
    public class SqlUserLedgerRepo : IUserLedgerRepo
    {
        private readonly TransactionContext _ctx;

        public SqlUserLedgerRepo(TransactionContext ctx)
        {
            _ctx = ctx;
        }
        public UserLedger GetLatestTransactionByUser(Users user)
        {
            UserLedger? res = _ctx.UserLedgers.Last(p => p.User == user);
            return res;
        }

        public void SaveUserLedger(UserLedger ledger)
        {
            _ctx.UserLedgers.Add(ledger);
            _ctx.SaveChanges();
        }
    }
}
