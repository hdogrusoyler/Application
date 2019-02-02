using System;
using System.Collections.Generic;
using System.Text;
using Application.Core.DataAccess;
using Application.Entities.Concrete;

namespace Application.DataAccess.Abstract
{
    public interface ICategoryDal : IEntityRepository<Category>
    {
        //Custom Operations
    }
}
