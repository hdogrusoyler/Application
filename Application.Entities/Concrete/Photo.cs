using System;
using System.Collections.Generic;
using System.Text;
using Application.Core.Entities;

namespace Application.Entities.Concrete
{
    public class Photo : IEntity
    {
        public int Id { get; set; }

        public string Path { get; set; }
        public bool IsMain { get; set; }

        public int ContentId { get; set; }
        public Content Content { get; set; }
    }
}
