using System;
using System.Collections.Generic;
using System.Text;
using Application.Core.DataAccess.EntityFramework;
using Application.DataAccess.Abstract;
using Application.Entities.Concrete;

namespace Application.DataAccess.Concrete.EntityFramework
{
    public class EfMessageDal : EfEntityRepositoryBase<Message, DataContext>, IMessageDal
    {
        public EfMessageDal(DataContext dbContext) : base(dbContext)
        {
            
        }
    }
}
