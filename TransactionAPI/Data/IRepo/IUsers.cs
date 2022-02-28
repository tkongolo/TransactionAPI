using TransactionAPI.Models;

namespace TransactionAPI.Data.IRepo
{
    public interface IUsers
    {
        Users GetUserById(int id);
        Users GetUserByEmail(string email);
        void SaveUser(Users user);
    }
}
