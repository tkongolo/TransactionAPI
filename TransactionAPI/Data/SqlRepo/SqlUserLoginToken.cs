using TransactionAPI.Data.IRepo;
using TransactionAPI.Models;

namespace TransactionAPI.Data.SqlRepo
{
    public class SqlUserLoginToken : IUserLoginToken
    {
        private readonly TransactionContext _ctx;

        public SqlUserLoginToken(TransactionContext ctx)
        {
            _ctx = ctx;
        }
        public UserLoginToken GetLoginTokenByUser(Users user)
        {
            return _ctx.UserLoginTokens.FirstOrDefault(p => p.User == user);
        }

        public void SaveLoginToken(UserLoginToken token)
        {
            UserLoginToken tok = GetLoginTokenByUser(token.User);
            if(tok == null)
            {
                _ctx.UserLoginTokens.Add(token);
            }
            else
            {
                tok.AccessToken = token.AccessToken;
                _ctx.UserLoginTokens.Update(tok);
            }
            _ctx.SaveChanges();
        }
    }
}
