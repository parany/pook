using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pook.Data.Entities
{
    [Table("Editor", Schema = "Editor")]
    public class Editor : Content
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public string Address { get; set; }
    }
}
