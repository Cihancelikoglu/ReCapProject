using Core.Utilities.Results.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;
using Core.Entities.Concrete;
using Entities.DTOs;

namespace Business.Abstract
{
    public interface IUserService
    {
        List<OperationClaim> GetClaims(User user);
        void Add(User user);
        IResult Update(UserForUpdateDto user);
        IResult ChangePassword(int userId, string password, string oldPassword);
        User GetByMail(string email);
        IDataResult<User> GetByUser(string eposta);
        IDataResult<User> GetById(int userId);
    }
}
