using System;
using System.Collections.Generic;
using System.Linq;
using Pook.Data.Repositories.Interface;
using Pook.Service.Coordinator.Interface;
using DCategory = Pook.Data.Entities.Category;
using SCategory = Pook.Service.Models.Categories.Category;

namespace Pook.Service.Coordinator.Concrete
{
    public class CategoryService : ICategoryService
    {
        private IGenericRepository<DCategory> CategoryRepository { get; }

        public CategoryService(IGenericRepository<DCategory> categoryRepository)
        {
            CategoryRepository = categoryRepository;
        }

        public IList<SCategory> GetAll()
        {
            return CategoryRepository
                .GetAll()
                .Select(DtoS)
                .ToList();
        }

        public SCategory GetSingle(Guid id)
        {
            throw new NotImplementedException();
        }

        public void Add(SCategory author)
        {
            throw new NotImplementedException();
        }

        public void Update(SCategory author)
        {
            throw new NotImplementedException();
        }

        public void Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        private SCategory DtoS(DCategory category)
        {
            return new SCategory
            {
                Id = category.Id,
                Title = category.Title,
                Description = category.Description
            };
        }
    }
}
