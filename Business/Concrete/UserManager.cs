using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Business.Abstract;
using Business.Constants;
using Core.Entities.Concrete;
using Core.Utilities.Business;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using Core.Utilities.Security.Hashing;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;

namespace Business.Concrete
{
    public class UserManager : IUserService
    {
        IUserDal _userDal;

        public UserManager(IUserDal userDal)
        {
            _userDal = userDal;
        }

        public List<OperationClaim> GetClaims(User user)
        {
            return _userDal.GetClaims(user);
        }

        public void Add(User user)
        {
            _userDal.Add(user);
        }

        public IResult Update(UserForUpdateDto user)
        {
            var userToCheck = _userDal.Get(c => c.Email == user.Email);

            IResult result = BusinessRules.Run(CheckIfUserIdAndMail(user.Id, user.Email), CheckIfPassword(user.Password,user.Email));
            if (result != null)
            {
                return result;
            }

            var userModel = new User
            {
                Id = user.Id,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                PasswordHash = userToCheck.PasswordHash,
                PasswordSalt = userToCheck.PasswordSalt,
                Status = true
            };

            _userDal.Update(userModel);
            return new SuccessResult(Messages.UserUpdated);
        }

        public IResult ChangePassword(int userId,string password,string oldPassword)
        {
            var userToCheck = _userDal.Get(c => c.Id == userId);
            IResult result = BusinessRules.Run(CheckIfOldPassword(oldPassword, userId));
            if (result != null)
            {
                return result;
            }

            byte[] passwordHash, passwordSalt;
            HashingHelper.CreatePasswordHash(password, out passwordHash, out passwordSalt);
            var userModel = new User
            {
                Id = userId,
                Email = userToCheck.Email,
                FirstName = userToCheck.FirstName,
                LastName = userToCheck.LastName,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                Status = true
            };

            _userDal.Update(userModel);
            return new SuccessResult(Messages.PasswordUpdated);
        }

        public User GetByMail(string email)
        {
            return _userDal.Get(u => u.Email == email);
        }

        public IDataResult<User> GetById(int userId)
        {
            return new SuccessDataResult<User>(_userDal.Get(c => c.Id == userId));
        }

        public IDataResult<User> GetByUser(string eposta)
        {
            return new SuccessDataResult<User>(_userDal.Get(c => c.Email == eposta), "Kullanıcı Listelendi");
        }

        private IResult CheckIfUserIdAndMail(int userId, string email)
        {
            if (userId == 0)
            {
                return new ErrorResult(Messages.ErrorUserUpdated);
            }

            var result = _userDal.GetAll(c => c.Id == userId && c.Email == email).Any();
            if (!result)
            {
                return new ErrorResult(Messages.ErrorUserUpdated);
            }
            return new SuccessResult();
        }

        private IResult CheckIfPassword(string password,string email)
        {
            if (password == null)
            {
                return new ErrorResult(Messages.ErrorUserUpdated);
            }

            var userToCheck = _userDal.Get(c => c.Email == email);
            if (userToCheck == null)
            {
                return new ErrorDataResult<User>(Messages.UserNotFound);
            }

            if (!HashingHelper.VerifyPasswordHash(password, userToCheck.PasswordHash, userToCheck.PasswordSalt))
            {
                return new ErrorDataResult<User>(Messages.PasswordError);
            }

            return new SuccessResult();
        }

        private IResult CheckIfOldPassword(string oldPassword, int userId)
        {
            if (oldPassword == null)
            {
                return new ErrorResult(Messages.ErrorUserUpdated);
            }

            var userToCheck = _userDal.Get(c => c.Id == userId);
            if (userToCheck == null)
            {
                return new ErrorDataResult<User>(Messages.UserNotFound);
            }

            if (!HashingHelper.VerifyPasswordHash(oldPassword, userToCheck.PasswordHash, userToCheck.PasswordSalt))
            {
                return new ErrorDataResult<User>(Messages.OldPasswordError);
            }

            return new SuccessResult();
        }

    }
}
