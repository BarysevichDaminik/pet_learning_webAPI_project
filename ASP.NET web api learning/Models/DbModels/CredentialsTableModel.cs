using System.ComponentModel.DataAnnotations;

namespace ASP.NET_web_api_learning.models.DbModels
{
    public class Credentials
    {
        [Key]
        public int id { get; set; }
        [Required]
        public string username { get; set; }
        [Required]
        public string password { get; set; }
        public Credentials(Credentials creds)
        {
            this.id = creds.id;
            this.username = creds.username;
            this.password = creds.password;
        }
    }
}
