using Microsoft.EntityFrameworkCore;
using SqlServer.Controllers;
using SqlServer.DataBase;
using SqlServer.Model;
using System.Security.Cryptography;

namespace SqlServer.Bussiness
{
    public class OtpBussiness
    {
        private readonly ApplicationDbContext _context;

        public OtpBussiness(ApplicationDbContext context)
        {
           _context = context;
        }

        #region OTP
            public async Task<DetailsDTO> generateOtp(string name) 
            {
                try
                {
                    var generateOtp = RandomNumberGenerator.GetInt32(1000,10000).ToString();
                    var message = "OTP sent Successfully";


                var otpEntity = new Details
                {
                    otp = generateOtp,
                    ExpiryTime = DateTime.Now.AddMinutes(1),
                    name = name,
                };
                    _context.OTP.Add(otpEntity);
                    await _context.SaveChangesAsync();
                
                    

                return new DetailsDTO 
                {
                    otp=generateOtp,
                    message= message,
                    
                };
                 
                }

                catch (Exception ex)
                {
                    return new DetailsDTO
                    {
                        otp = null,
                        message = "OTP not sent"
                    };
                }
            }
        #endregion

        #region VerifyOtp
        public async Task<string> verifyOtp(string otp) 
        {
            var verifyOtp = await _context.OTP
                            .Where(p => p.otp == otp)
                            .FirstOrDefaultAsync();

            if (DateTime.Now > verifyOtp.ExpiryTime) 
            {
                await _context.SaveChangesAsync();
                //await generateOtp(string name);
                return "Otp Expired new otp sent";
            }
            if (verifyOtp!=null) 
            {
                return "Verified successfully";
            }
            return "wrong otp";
        }
        #endregion

        #region UpdateData
        public async Task<string> UpdateDetails(int id, string name) 
        {
            var findData = await _context.OTP.FindAsync(id);

            if (findData == null) 
            {
                return $"NO datas in this {id}";
            }

             findData.name = name;
            await _context.SaveChangesAsync();
            return "update successfully";
        }
        #endregion

        #region DeleteDatas
        public async Task<bool> DeleteData(int id) 
        {
            var findDetails = await _context.OTP.FindAsync(id);

            if (findDetails==null) 
            {
                return false;
            }
            _context.OTP.Remove(findDetails);
            _context.SaveChanges();
            return true;

        }
        #endregion

        #region GetAllDetails
        public async Task<List<Details>> GetAll() 
        {
            var result = await _context.OTP.ToListAsync();

            if (result!=null) 
            {
                return result;
            }
            return new List<Details>();
        }
        #endregion

        #region VerifyName
        public async Task<string> verifyData(string name) 
        {
            var result = await _context.OTP.FirstOrDefaultAsync(p => p.name == name);

            if (result==null) 
            {
                return "No data";
            }
            return "name is available";
        }
        #endregion
    }
}
