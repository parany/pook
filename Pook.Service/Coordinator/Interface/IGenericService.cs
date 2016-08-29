using System;
using System.Collections.Generic;
using Pook.Service.Models.Author;

namespace Pook.Service.Coordinator.Interface
{
    public interface IGenericService<T>
    {
        IList<T> GetAll();

        T GetSingle(Guid id);

        void Add(T author);

        void Update(T author);

        void Delete(Guid id);
    }
}
