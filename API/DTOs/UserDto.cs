using System.ComponentModel.DataAnnotations;

namespace API.DTOs
{
    public class UserDto
    {

        public string Email { get; set; }

        public string Displayname { get; set; }
        public string Token { get; set; }
    }
}
