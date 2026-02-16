using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SqlServer.Bussiness;
using System.Threading.Tasks;

namespace SqlServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OTPController : ControllerBase
    {
        private readonly OtpBussiness _otpBussiness;
        public OTPController(OtpBussiness otpBussiness)
        {
            _otpBussiness = otpBussiness;
        }

        [HttpPost]
        [Route("OTPS")]
        public async Task<IActionResult> sentOtp(string name) 
        {
            var checkOtp= await _otpBussiness.generateOtp(name);

            if (checkOtp!=null) 
            {
                return Ok(checkOtp);
            }
            return NotFound();


        }

        [HttpGet]
        [Route("VerifyOtp")]
        public async Task<IActionResult> verifyOtp(string otp) 
        {
            var result = await _otpBussiness.verifyOtp(otp);
            return Ok(new { message=result });
        }

        [HttpPost]
        [Route("UpdateDetails")]
        public async Task<IActionResult> updateDetail(int id, string name) 
        {
            var result =  await _otpBussiness.UpdateDetails(id,name);

            if (result != null)
            {
                return Ok(result);
            }
            else 
            {
                return NotFound(new {message="something error"});
            }


        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDetails(int id) 
        {
            var result=await _otpBussiness.DeleteData(id);
            
            if (!result) 
            {
                return NotFound(new {message="NO Datas Found"});
            }
            return Ok(new { message=$"{id} Record Deleted successfully"});  
        }

        #region GetAllDetails
        [HttpGet]
        [Route("GetAllDetails")]
        public async Task<IActionResult> GetAllDatas() 
        {
            var result = await _otpBussiness.GetAll();

            if (result==null) 
            {
                return NotFound(new {message="No datas"});
            }
            return Ok(new { message = result });
        }
        #endregion

        #region VerifyData
        [HttpGet]
        [Route("GetAllDatas")]
        public async Task<IActionResult> verifyData(string name) 
        {
            var result = await _otpBussiness.verifyData(name);

            if (result!=null) 
            {
                return Ok(new {message=result});
            }
            return NotFound(new { message = "Not found"});
        }
        #endregion
    }
}
