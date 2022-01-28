using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Controllers
{
    
    [Authorize]
    public class IdentityController : ControllerBase
    {
        DatabaseFunctions DBF = new DatabaseFunctions();
        static string connStr = "server = localhost; user = root; database = CSATM; port = 3306; password = SamSQL1555";
        MySqlConnection conn = new MySqlConnection(connStr);
        [Route("identity")]
        [HttpGet]
        public IActionResult Get()
        {
            return new JsonResult(from c in User.Claims select new { c.Type, c.Value });
        }
        [Route("Deposit")]
        [HttpPost]
        public async Task<IActionResult> Deposit()
        {
            Console.WriteLine("Deposit form data recieved");
            var a = HttpContext.Request.Form["DBox.DepositValue"];
            string UID = HttpContext.Request.Form["UID"];
            string CurrentBal = DBF.GetCurrentBalLocal(UID).ToString();
            if (Int32.TryParse(CurrentBal, out int NVal))
            {
                if (Int32.TryParse(a, out int SVal))
                {
                    conn.Open();
                    string sql = $"UPDATE Users SET Balance = '{NVal + SVal}' WHERE Username = '{UID}'";
                    MySqlCommand cmd = new MySqlCommand(sql, conn);
                    cmd.ExecuteNonQuery();
                    return Ok(NVal + SVal);
                }

            }
            return StatusCode(500);
        }
        [Route("Withdrawl")]
        [HttpPost]
        public async Task<IActionResult> Withdrawl()
        {
            Console.WriteLine("Withdrawl form data recieved");
            var a = HttpContext.Request.Form["WBox.WithdrawlValue"];
            string UID = HttpContext.Request.Form["UID"];
            string CurrentBal = DBF.GetCurrentBalLocal(UID).ToString();
            if (Int32.TryParse(CurrentBal, out int NVal))
            {
                if (Int32.TryParse(a, out int SVal))
                {
                    conn.Open();
                    string sql = $"UPDATE Users SET Balance = '{NVal - SVal}' WHERE Username = '{UID}'";
                    MySqlCommand cmd = new MySqlCommand(sql, conn);
                    cmd.ExecuteNonQuery();
                    return Ok(NVal - SVal);
                }
            }
            return StatusCode(500);
        }
    }
}

