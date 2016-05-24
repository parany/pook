using System;

namespace Pook.Data.Entities
{
    public class Category : Content
    {
        public Guid CategoryId { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }
    }
}
