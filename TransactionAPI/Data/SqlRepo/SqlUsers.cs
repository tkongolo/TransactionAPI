using TransactionAPI.Data.IRepo;
using TransactionAPI.Models;

namespace TransactionAPI.Data.SqlRepo
{
    public class SqlUsers : IUsers
    {
        public Users GetUserByEmail(string email)
        {
            throw new NotImplementedException();
        }

        public Users GetUserById(int id)
        {
            throw new NotImplementedException();
        }

        public void SaveUser(Users user)
        {
            throw new NotImplementedException();
        }
    }
}
