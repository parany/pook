using System;
using System.Collections.Generic;
using Pook.Service.Models;

namespace Pook.Service.Coordinator.Interface
{
    public interface IAuthorService
    {
        IList<Author> GetAll();

        Author GetSingle(Guid id);

        void Add(Author author);

        void Update(Author author);
    }
}
