using ASP.NET_web_api_learning.models.DbModels;
using System.Runtime.Intrinsics.Arm;
using System.Security.Cryptography;
using System.Text;

namespace ASP.NET_web_api_learning.Services
{
    public class HashingService
    {
        public string HashPassword(string password)
        {
            using (var sha3 = SHA3_512.Create())
            {
                var bytes = Encoding.UTF8.GetBytes(password);
                var hashBytes = sha3.ComputeHash(bytes);
                return Convert.ToHexString(hashBytes);
            }
        }
        public bool VerifyPassword(Credentials credentials)
        {

            var hashOfInput = HashPassword(credentials.password);
            return hashedPassword.Equals(hashOfInput, StringComparison.OrdinalIgnoreCase);
        }
    }
}

