using Application.Core.DataAccess;
using Application.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.DataAccess.Abstract
{
    public interface IContentDal : IEntityRepository<Content>
    {
        //Custom Operations
    }
}
