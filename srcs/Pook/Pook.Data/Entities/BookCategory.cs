using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pook.Data.Entities
{
    public class BookCategory : Content
    {
        public Guid BookCategoryId { get; set; }

        public Guid BookId { get; set; }

        public Guid CategoryId { get; set; }

        public Book Book { get; set; }

        public Category Category { get; set; }

        public ICollection<BookCategory> BookCategories { get; set; }
    }
}
