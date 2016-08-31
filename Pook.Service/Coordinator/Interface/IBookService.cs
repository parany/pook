using System;
using System.Collections.Generic;
using Pook.Data.Entities;
using Pook.Service.Models.Books;
using Book = Pook.Service.Models.Books.Book;

namespace Pook.Service.Coordinator.Interface
{
    public interface IBookService : IGenericService<Book>
    {
        IList<BookList> GetList(string userId);

        IList<BookList> GetListByStatus(string userId, Func<Progression, bool> filter);

        BookDetails GetDetails(Guid id);

        BookCreate GetBookCreate();
    }
}
