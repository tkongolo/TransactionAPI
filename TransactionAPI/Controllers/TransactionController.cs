using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TransactionAPI.Data.IRepo;
using TransactionAPI.Dtos;
using TransactionAPI.Models;

namespace TransactionAPI.Controllers
{
    [Route("api/transaction-api/transactions")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private readonly IUserLedgerRepo _userledge;
        private readonly IUsers _usersRepo;

        public TransactionController(IUserLedgerRepo userledge,IUsers users)
        {
            _userledge = userledge;
            _usersRepo = users;
        }

        [Authorize]
        [HttpPost]
        [Route("topup")]
        public async Task<IActionResult> AddAmount([FromBody] AmountDto model)
        {
            var currentUser = HttpContext.User;
            if(currentUser.HasClaim(c => c.Type == "Email"))
            {
                string emailVar = currentUser.Claims.FirstOrDefault(c => c.Type == "Email").Value;
                if (emailVar != null)
                {
                    //get user
                    var selectedUser = _usersRepo.GetUserByEmail(emailVar);
                    if (selectedUser != null)
                    {
                        //add new transaction
                        var previousBalanceLedge = _userledge.GetLatestTransactionByUser(selectedUser);

                        var newLedgerRecord = new UserLedger();
                        newLedgerRecord.User = selectedUser;
                        newLedgerRecord.PreviousBalance = previousBalanceLedge.FinalBalance;
                        newLedgerRecord.Amount = model.Amount;
                        newLedgerRecord.FinalBalance = newLedgerRecord.PreviousBalance + newLedgerRecord.Amount;

                        _userledge.SaveUserLedger(newLedgerRecord);

                        //response
                        var resp = new TransactionDto();
                        resp.Name = selectedUser.Name;
                        resp.PreviousBalance = newLedgerRecord.PreviousBalance;
                        resp.NewBalance = newLedgerRecord.FinalBalance;

                        return Ok(resp);
                    }
                }
            }

            return Unauthorized();
        }

        [Authorize]
        [HttpPost]
        [Route("deduct")]
        public async Task<IActionResult> DeductAmount([FromBody] AmountDto model)
        {
            var currentUser = HttpContext.User;
            if (currentUser.HasClaim(c => c.Type == "Email"))
            {
                string emailVar = currentUser.Claims.FirstOrDefault(c => c.Type == "Email").Value;
                if (emailVar != null)
                {
                    //get user
                    var selectedUser = _usersRepo.GetUserByEmail(emailVar);
                    if (selectedUser != null)
                    {
                        //add new transaction
                        var previousBalanceLedge = _userledge.GetLatestTransactionByUser(selectedUser);

                        var newLedgerRecord = new UserLedger();
                        newLedgerRecord.User = selectedUser;
                        newLedgerRecord.PreviousBalance = previousBalanceLedge.FinalBalance;
                        newLedgerRecord.Amount = model.Amount;
                        newLedgerRecord.FinalBalance = newLedgerRecord.PreviousBalance - newLedgerRecord.Amount;

                        _userledge.SaveUserLedger(newLedgerRecord);

                        //response
                        var resp = new TransactionDto();
                        resp.Name = selectedUser.Name;
                        resp.PreviousBalance = newLedgerRecord.PreviousBalance;
                        resp.NewBalance = newLedgerRecord.FinalBalance;

                        return Ok(resp);
                    }
                }
            }

            return Unauthorized();
        }

        [Authorize]
        [HttpGet]
        [Route("currentAmount")]
        public async Task<IActionResult> ShowCurrentAmount()
        {
            var currentUser = HttpContext.User;
            if (currentUser.HasClaim(c => c.Type == "Email"))
            {
                string emailVar = currentUser.Claims.FirstOrDefault(c => c.Type == "Email").Value;
                if (emailVar != null)
                {
                    //get user
                    var selectedUser = _usersRepo.GetUserByEmail(emailVar);
                    if (selectedUser != null)
                    {
                        //add new transaction
                        var previousBalanceLedge = _userledge.GetLatestTransactionByUser(selectedUser);

                        //response
                        var resp = new TransactionDto();
                        resp.Name = selectedUser.Name;
                        resp.PreviousBalance = previousBalanceLedge.PreviousBalance;
                        resp.NewBalance = previousBalanceLedge.FinalBalance;
                        

                        return Ok(resp);
                    }
                }
            }

            return Unauthorized();
        }
    }
}
