using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using TransactionAPI.Data.IRepo;
using TransactionAPI.Dtos;
using TransactionAPI.Models;

namespace TransactionAPI.Controllers
{
    [Route("api/transaction-api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUsers _user;
        private readonly IUserLoginToken _userlogToken;
        private readonly IUserResetToken _userResetToken;
        private readonly IUserLedgerRepo _userLedger;

        public AuthController(IUsers user, IUserLoginToken userlogToken, IUserResetToken userResetToken, IUserLedgerRepo userLedger)
        {
            _user = user;
            _userlogToken = userlogToken;
            _userResetToken = userResetToken;
            _userLedger = userLedger;
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromForm] LoginDto model)
        {
            Users selectedUser = _user.GetUserByEmail(model.Email);
            if(selectedUser != null)
            {
                //compare password
                if(model.Password == selectedUser.Password)
                {
                    var authClaims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Email, selectedUser.Email)
                    };

                    var token = CreateToken(authClaims);
                    var tokString = new JwtSecurityTokenHandler().WriteToken(token);
                    var refresh_token = GenerateRefreshToken();

                    //update logtoken token
                    var refTok = new UserLoginToken();
                    refTok.User = selectedUser;
                    refTok.AccessToken = tokString;
                    _userlogToken.SaveLoginToken(refTok);

                    //update refresh token
                    var refrefTok = new UserResetToken();
                    refrefTok.User = selectedUser;
                    refrefTok.ResetToken = refresh_token;
                    _userResetToken.SaveResetToken(refrefTok);

                    return Ok(new
                    {
                        AccessToken = tokString,
                        RefreshToken = refresh_token
                    });


                }
            }

            return Unauthorized();
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromForm] RegisterDto model)
        {
            Users selectedUser = _user.GetUserByEmail(model.Email);
            if (selectedUser == null)
            {
                var newUser = new Users();
                newUser.Name = model.Name;
                newUser.Email = model.Email;
                newUser.Password = model.Password;

                //save user
                _user.SaveUser(newUser);

                //initialize ledger
                var ledge = new UserLedger();
                ledge.User = newUser;
                ledge.TransactionType = "INIT";
                ledge.PreviousBalance = 0;
                ledge.Amount = 0;
                ledge.FinalBalance = 0;

                _userLedger.SaveUserLedger(ledge);

                ResponseDto res = new ResponseDto();
                res.Status = "201";
                res.Message = "New User Created";

                return Ok(res);
            }
            else
            {
                return Conflict("Email already exists in System");
            }
        }

        private JwtSecurityToken CreateToken(List<Claim> authClaims)
        {
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("JWTRefreshTokenHIGHsecuredPasswordVVVp1OH7Xzyr"));
            _ = int.TryParse("10", out int tokenValidityInMinutes);

            var token = new JwtSecurityToken(
                expires: DateTime.Now.AddMinutes(tokenValidityInMinutes),
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256),
                claims: authClaims
                );

            return token;
        }

        private static string GenerateRefreshToken()
        {
            var randomNumber = new byte[64];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }
    }
}
