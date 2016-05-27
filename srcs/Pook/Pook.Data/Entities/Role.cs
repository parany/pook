using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pook.Data.Entities
{
    [Table("AuthorRole", Schema = "Book")]
    public class AuthorRole : Content
    {
        public Guid AuthorRoleId { get; set; }

        public string Title { get; set; }

        public string Desription { get; set; }
    }
}
