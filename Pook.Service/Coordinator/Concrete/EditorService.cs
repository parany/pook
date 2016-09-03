using System;
using System.Collections.Generic;
using System.Linq;
using Pook.Data.Repositories.Interface;
using Pook.Service.Coordinator.Interface;
using DEditor = Pook.Data.Entities.Editor;
using SEditor = Pook.Service.Models.Editors.Editor;

namespace Pook.Service.Coordinator.Concrete
{
    public class EditorService : IEditorService
    {
        private IGenericRepository<DEditor> EditorRepository { get; }

        public EditorService(IGenericRepository<DEditor> editorRepository)
        {
            EditorRepository = editorRepository;
        }


        public IList<SEditor> GetAll()
        {
            return EditorRepository
                .GetAll()
                .Select(DtoS)
                .ToList();
        }

        public SEditor GetSingle(Guid id)
        {
            var editor = EditorRepository.GetSingle(id);
            return DtoS(editor);
        }

        public void Add(SEditor editor)
        {
            EditorRepository.Add(StoD(editor));
        }

        public void Update(SEditor editor)
        {
            EditorRepository.Update(StoD(editor));
        }

        public void Delete(Guid id)
        {
            EditorRepository.Delete(id);
        }

        private SEditor DtoS(DEditor editor)
        {
            return new SEditor
            {
                Id = editor.Id,
                Title = editor.Title,
                Description = editor.Description,
                Address = editor.Address
            };
        }

        private DEditor StoD(SEditor editor)
        {
            return new DEditor
            {
                Id = editor.Id,
                Title = editor.Title,
                Description = editor.Description,
                Address = editor.Address
            };
        }
    }
}
