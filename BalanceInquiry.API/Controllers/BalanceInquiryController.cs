using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BalanceInquiry.Micorservice.Controllers
{
    //[Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class BalanceInquiryController : ControllerBase
    {
        readonly IConfiguration _configuration;
        public BalanceInquiryController(IConfiguration configuration) 
        {
            _configuration = configuration;
        }

        [HttpGet("GetBalanceInquire")]
        public JsonResult GetBalanceInqure()
        {
            BalanceInquiryModels acctA = new()
            {
                ID = "A",
                Account = "A123456",
                Balance = 1000
            };

            return new JsonResult(acctA);
             

        }


        [HttpPost("BalanceInquire")]
        public JsonResult BalanceInqure([FromBody] string ID)
        {
            BalanceInquiryModels acctA = new()
            {
                ID = "A",
                Account = "A123456",
                Balance = 1000
            };

            BalanceInquiryModels acctB = new()
            {
                ID = "B",
                Account = "B7890",
                Balance = 3000
            };

            if (ID == "A")
                return new JsonResult(acctA);
            else if (ID == "B")
                return new JsonResult(acctB);
            else 
                return new JsonResult(new BalanceInquiryModels { ID = "", Account = "", Balance = 0 });
            
        }
    }


    public class BalanceInquiryModels
    {
        public string ID { get; set; }
        public string Account { get; set; } 
        public int Balance { get; set; }

    }
}
