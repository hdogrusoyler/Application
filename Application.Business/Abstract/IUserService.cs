using Application.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Business.Abstract
{
    public interface IUserService
    {
        List<User> GetAll();
        User GetById(int userId);
        User Login(string userName, string password);
        User Add(User user, string password);
        User Update(User user, string password);
        void Delete(User user);
        bool UserExists(string userName);
    }
}
