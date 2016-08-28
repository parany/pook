using System;
using System.ComponentModel.DataAnnotations;

namespace Pook.Service.Models
{
    public class Author
    {
        public Guid Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string FullName => string.Concat(FirstName, " ", LastName);

        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        public string Email { get; set; }

        public string Address { get; set; }
    }
}
