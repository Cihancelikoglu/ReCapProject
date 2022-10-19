using Core.Utilities.Results.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract
{
    public interface IUserFindexService
    {
        IDataResult<List<UserFindex>> GetAll();
        IDataResult<UserFindex> GetByUserId(int userId);
        IResult FindexControl(int userfindex, int carFindex);
    }
}
