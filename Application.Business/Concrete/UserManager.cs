using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Application.Business.Abstract;
using Application.DataAccess.Abstract;
using Application.Entities.Concrete;

namespace Application.Business.Concrete
{
    public class UserManager : IUserService
    {
        private IUserDal _userDal;

        public UserManager(IUserDal userDal)
        {
            _userDal = userDal;
        }

        public List<User> GetAll()
        {
            return _userDal.GetList();
        }

        public User GetById(int userId)
        {
            return _userDal.Get(u => u.Id == userId);
        }

        public User Login(string userName, string password)
        {
            var user = _userDal.Get(u => u.Name == userName);
            if (user == null)
            {
                return null;
            }

            if (!VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
            {
                return null;
            }

            return user;
        }

        private bool VerifyPasswordHash(string password, byte[] userpasswordHash, byte[] userpasswordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(userpasswordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != userpasswordHash[i])
                    {
                        return false;
                    }
                }

                return true;
            }
        }

        public User Add(User user, string password)
        {
            byte[] passwordHash, passwordSalt;
            CreatePasswordHash(password, out passwordHash, out passwordSalt);

            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

            _userDal.Add(user);

            return user;
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        public User Update(User user, string password)
        {
            byte[] passwordHash, passwordSalt;
            CreatePasswordHash(password, out passwordHash, out passwordSalt);

            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

            _userDal.Update(user);

            return user;
        }

        public void Delete(User user)
        {
            _userDal.Delete(user);
        }

        public bool UserExists(string userName)
        {
            if (_userDal.Get(u => u.Name == userName) != null)
            {
                return true;
            }

            return false;
        }
    }
}
