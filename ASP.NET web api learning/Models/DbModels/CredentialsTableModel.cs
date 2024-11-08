using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ASP.NET_web_api_learning.models.DbModels
{
    public class Credentials
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        [Required]
        public string username { get; set; }
        [Required]
        public string password { get; set; }
        public Credentials()
        {
        }
        public Credentials(Credentials credentials)
        {
            this.username = credentials.username;
            this.password = credentials.password;
        }
    }
}
