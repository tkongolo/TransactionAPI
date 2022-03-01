using TransactionAPI.Models;

namespace TransactionAPI.Data.IRepo
{
    public interface IUserLoginToken
    {

        void SaveLoginToken(UserLoginToken token);
        UserLoginToken GetLoginTokenByUser(Users user);
    }
}
