using System;
using System.Collections.Generic;
using System.Text;
using Application.Core.Entities;

namespace Application.Entities.Concrete
{
    public class Category : IEntity
    {
        public Category()
        {
            Contents = new List<Content>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public List<Content> Contents { get; set; }
    }
}
