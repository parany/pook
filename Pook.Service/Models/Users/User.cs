using System.ComponentModel;

namespace Pook.Service.Models.Users
{
    public class User
    {
        public string Id { get; set; }

        [DisplayName("First Name")]
        public string FirstName { get; set; }

        [DisplayName("Last Name")]
        public string LastName { get; set; }

        public string Description { get; set; }

        public string Address { get; set; }

        public string Email { get; set; }

        [DisplayName("User Name")]
        public string UserName { get; set; }

        public static User DtoS(Data.Entities.User user)
        {
            return new User
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Description = user.Description,
                Address = user.Address,
                Email = user.Email,
                UserName = user.UserName
            };
        }

        public static Data.Entities.User StoD(User user)
        {
            return new Data.Entities.User
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Description = user.Description,
                Address = user.Address,
                Email = user.Email,
                UserName = user.UserName
            };
        }
    }
}
