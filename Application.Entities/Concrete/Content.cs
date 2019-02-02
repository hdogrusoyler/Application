using System;
using System.Collections.Generic;
using System.Text;
using Application.Core.Entities;

namespace Application.Entities.Concrete
{
    public class Content : IEntity
    {
        public Content()
        {
            Photos=new List<Photo>();
        }

        public int Id { get; set; }

        public string Title { get; set; }
        public string Description { get; set; }

        public int? CategoryId { get; set; }
        public Category Category { get; set; }

        public List<Photo> Photos { get; set; }
    }
}
