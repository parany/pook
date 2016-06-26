using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pook.Data.Entities
{
    [Table("Author", Schema = "Book")]
    public class Author : Content
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        public string Email { get; set; }

        public string Address { get; set; }

        public string FullName => string.Concat(FirstName, " ", LastName.ToUpper());
    }
}
