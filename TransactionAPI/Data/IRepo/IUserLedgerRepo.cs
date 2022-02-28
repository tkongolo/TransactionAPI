using TransactionAPI.Models;

namespace TransactionAPI.Data.IRepo
{
    public interface IUserLedgerRepo
    {
        void SaveUserLedger(UserLedger ledger);
        UserLedger GetLatestTransactionByUser(Users user);
    }
}
