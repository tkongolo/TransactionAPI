using TransactionAPI.Models;

namespace TransactionAPI.Data.IRepo
{
    public interface IUserResetToken
    {
        void SaveResetToken(UserResetToken token);
        UserResetToken GetResetTokenByUser(Users user);
    }
}
