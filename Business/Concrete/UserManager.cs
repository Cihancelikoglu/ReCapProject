using System;
using System.Collections.Generic;
using System.Text;
using Business.Abstract;
using Core.Utilities.Business;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using DataAccess.Abstract;
using Entities.Concrete;

namespace Business.Concrete
{
    internal class UserManager : IUserService
    {
        private readonly IUserDal _userDal;
        public UserManager(IUserDal userDal)
        {
            _userDal = userDal;
        }

        public IDataResult<List<User>> GetAll()
        {
            var result = BusinessRules.Run(CheckIfUserCount());
            if (result != null)
            {
                return new ErrorDataResult<List<User>>(_userDal.GetAll());

            }
            return new SuccessDataResult<List<User>>(_userDal.GetAll());
        }

        public IDataResult<User> GetById(int userId)
        {
            throw new NotImplementedException();
        }

        public IResult Add(User user)
        {
            throw new NotImplementedException();
        }

        public IResult Update(User user)
        {
            throw new NotImplementedException();
        }

        public IResult Delete(User user)
        {
            throw new NotImplementedException();
        }

        private IResult CheckIfUserCount()
        {
            var result = _userDal.GetAll().Count;
            if (result <= 0)
            {
                return new ErrorResult();
            }

            return new SuccessResult();
        }
    }
}
