using System;
using System.Collections.Generic;
using System.Text;
using Application.Core.Entities;

namespace Application.Entities.Concrete
{
    public class Message : IEntity
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public string Email { get; set; }
        public string Description { get; set; }
    }
}
