using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace apiclient.Models.Users
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public int Role { get; set; }

        [JsonIgnore]
        public string PasswordHash { get; set; }
    }
}
