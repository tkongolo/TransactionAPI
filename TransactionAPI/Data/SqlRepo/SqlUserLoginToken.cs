using TransactionAPI.Data.IRepo;
using TransactionAPI.Models;

namespace TransactionAPI.Data.SqlRepo
{
    public class SqlUserLoginToken : IUserLoginToken
    {
        public UserLoginToken GetResetTokenByUser(Users user)
        {
            throw new NotImplementedException();
        }

        public void SaveLoginToken(string token)
        {
            throw new NotImplementedException();
        }

        public void UpdateLoginToken(string token, Users user)
        {
            throw new NotImplementedException();
        }
    }
}
