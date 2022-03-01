using TransactionAPI.Data.IRepo;
using TransactionAPI.Models;

namespace TransactionAPI.Data.SqlRepo
{
    public class SqlUserResetToken : IUserResetToken
    {
        private readonly TransactionContext _ctx;

        public SqlUserResetToken(TransactionContext ctx)
        {
            _ctx = ctx;
        }

        public UserResetToken GetResetTokenByUser(Users user)
        {
            return _ctx.UserResetTokens.FirstOrDefault(p => p.User == user);
        }

        public void SaveResetToken(UserResetToken token)
        {
            UserResetToken tok = GetResetTokenByUser(token.User);
            if (tok == null)
            {
                _ctx.UserResetTokens.Add(token);
            }
            else
            {
                tok.ResetToken = token.ResetToken;
                _ctx.UserResetTokens.Update(tok);
            }
            _ctx.SaveChanges();
        }
    }
}
