using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pook.Data.Entities
{
    [Table("Author", Schema = "Book")]
    public class Author
    {
        public Guid AuthorId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        public string Email { get; set; }

        public string Address { get; set; }
    }
}
