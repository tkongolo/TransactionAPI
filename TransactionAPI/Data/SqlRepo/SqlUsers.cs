using TransactionAPI.Data.IRepo;
using TransactionAPI.Models;

namespace TransactionAPI.Data.SqlRepo
{
    public class SqlUsers : IUsers
    {
        private readonly TransactionContext _ctx;

        public SqlUsers(TransactionContext ctx)
        {
            _ctx = ctx;
        }
        public Users GetUserByEmail(string email)
        {
            return _ctx.Users.FirstOrDefault(p => p.Email == email);
        }

        public Users GetUserById(int id)
        {
            return _ctx.Users.FirstOrDefault(p => p.Id == id);
        }

        public void SaveUser(Users user)
        {
            Users tok = GetUserByEmail(user.Email);
            if (tok == null)
            {
                _ctx.Users.Add(tok);
            }
            else
            {
                _ctx.Users.Update(tok);
            }
            _ctx.SaveChanges();
        }
    }
}
