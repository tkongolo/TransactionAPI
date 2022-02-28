using TransactionAPI.Models;

namespace TransactionAPI.Data.IRepo
{
    public interface IUserResetToken
    {
        void SaveResetToken(string token);
        void UpdateResetToken(string token, Users user);

        UserResetToken GetResetTokenByUser(Users user);
    }
}
