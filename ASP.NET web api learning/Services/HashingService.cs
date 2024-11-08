using ASP.NET_web_api_learning.models.DbModels;
using ASP.NET_web_api_learning.models.ProjectModels;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.Intrinsics.Arm;
using System.Security.Cryptography;
using System.Text;

namespace ASP.NET_web_api_learning.Services
{
    public class HashingService
    {
        public HashingService() { }
        public string HashPassword(string password)
        {
            using (var sha3 = SHA3_512.Create())
            {
                var bytes = Encoding.UTF8.GetBytes(password);
                var hashBytes = sha3.ComputeHash(bytes);
                return Convert.ToHexString(hashBytes);
            }
        }
        public bool VerifyPassword(Credentials hashedCredentials, Credentials credentials)
        {
            var hashOfInput = HashPassword(credentials.password);
            return hashedCredentials.password != null ? hashedCredentials.password.Equals(hashOfInput) : false;
        }
    }
}

