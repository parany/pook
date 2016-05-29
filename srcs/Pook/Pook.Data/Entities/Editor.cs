using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pook.Data.Entities
{
    [Table("Editor", Schema = "Editor")]
    public class Editor : Content
    {
        public Guid EditorId { get; set; }

        public string Description { get; set; }

        public string Address { get; set; }
    }
}
