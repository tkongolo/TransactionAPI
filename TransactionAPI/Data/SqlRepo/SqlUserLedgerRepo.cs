using TransactionAPI.Data.IRepo;
using TransactionAPI.Models;

namespace TransactionAPI.Data.SqlRepo
{
    public class SqlUserLedgerRepo : IUserLedgerRepo
    {
        public UserLedger GetLatestTransactionByUser(Users user)
        {
            throw new NotImplementedException();
        }

        public void SaveUserLedger(UserLedger ledger)
        {
            throw new NotImplementedException();
        }
    }
}
