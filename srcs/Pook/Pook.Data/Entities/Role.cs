using System;

namespace Pook.Data.Entities
{
    public class AuthorRole : Content
    {
        public Guid AuthorRoleId { get; set; }

        public string Title { get; set; }

        public string Desription { get; set; }
    }
}
