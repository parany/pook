using System;

namespace Pook.Data.Entities
{
    public class Editor : Content
    {
        public Guid EditorId { get; set; }

        public string Description { get; set; }

        public string Address { get; set; }
    }
}
