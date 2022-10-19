using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Business.Abstract;
using Business.Constants;
using Core.Utilities.Business;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using DataAccess.Abstract;
using Entities.Concrete;

namespace Business.Concrete
{
    public class UserFindexManager : IUserFindexService
    {
        private IUserFindexDal _userFindexDal;

        public UserFindexManager(IUserFindexDal userFindexDal)
        {
            _userFindexDal = userFindexDal;
        }

        public IDataResult<List<UserFindex>> GetAll()
        {
            return new SuccessDataResult<List<UserFindex>>(_userFindexDal.GetAll());
        }

        public IDataResult<UserFindex> GetByUserId(int userId)
        {
            return new SuccessDataResult<UserFindex>(_userFindexDal.Get(x => x.UserId == userId));
        }

        public IResult FindexControl(int userfindex, int carFindex)
        {
            IResult result = BusinessRules.Run(CheckFindex(userfindex,carFindex));

            if (result != null)
            {
                return result;
            }
            return new Result(true, "Findex Değeriniz Doğrulandı");
        }

        private IResult CheckFindex(int userId, int carFindex)
        {
            var result = _userFindexDal.GetAll(c => c.UserId == userId && c.Findex > carFindex).Any();
            if (result)
            {
                return new SuccessResult("Findex Değeriniz Doğrulandı");
            }
            else
            {
                return new ErrorResult("Findex Değeriniz Düşük");
            }
        }
    }
}
