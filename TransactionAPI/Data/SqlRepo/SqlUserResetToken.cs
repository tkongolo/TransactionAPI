using TransactionAPI.Data.IRepo;
using TransactionAPI.Models;

namespace TransactionAPI.Data.SqlRepo
{
    public class SqlUserResetToken : IUserResetToken
    {
        public UserResetToken GetResetTokenByUser(Users user)
        {
            throw new NotImplementedException();
        }

        public void SaveResetToken(string token)
        {
            throw new NotImplementedException();
        }

        public void UpdateResetToken(string token, Users user)
        {
            throw new NotImplementedException();
        }
    }
}
