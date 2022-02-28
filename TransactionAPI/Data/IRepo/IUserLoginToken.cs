using TransactionAPI.Models;

namespace TransactionAPI.Data.IRepo
{
    public interface IUserLoginToken
    {
        void SaveLoginToken(string token);
        void UpdateLoginToken(string token, Users user);

        UserLoginToken GetResetTokenByUser(Users user);
    }
}
