using System;
using System.Collections.Generic;
using System.Text;
using Application.Core.DataAccess.EntityFramework;
using Application.DataAccess.Abstract;
using Application.Entities.Concrete;

namespace Application.DataAccess.Concrete.EntityFramework
{
    public class EfPhotoDal : EfEntityRepositoryBase<Photo, DataContext>, IPhotoDal
    {
        public EfPhotoDal(DataContext dbContext) : base(dbContext)
        {
            
        }
    }
}
