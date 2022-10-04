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
    public class ColorManager : IColorService
    {
        private IColorDal _colorDal;
        public ColorManager(IColorDal colorDal)
        {
            _colorDal = colorDal;
        }

        public IDataResult<List<Color>> GetAll()
        {
            var result = BusinessRules.Run(CheckIfColorCount());
            if (result != null)
            {
                return new ErrorDataResult<List<Color>>(_colorDal.GetAll());
            }
            return new SuccessDataResult<List<Color>>(_colorDal.GetAll());
        }

        public IDataResult<Color> GetById(int colorId)
        {
            return new SuccessDataResult<Color>(_colorDal.Get(x => x.Id == colorId));
        }

        public IResult Add(Color color)
        {
           _colorDal.Add(color);
           return new SuccessResult(Messages.ColorAdded);
        }

        public IResult Update(Color color)
        {
            IResult result = BusinessRules.Run(CheckIfColorId(color.Id));
            if (result != null)
            {
                return result;
            }

            _colorDal.Update(color);
            return new SuccessResult(Messages.ColorUpdated);
        }

        public IResult Delete(Color color)
        {
            IResult result = BusinessRules.Run(CheckIfColorId(color.Id));
            if (result != null)
            {
                return result;
            }

            _colorDal.Delete(color);
            return new SuccessResult(Messages.ColorDeleted);
        }

        private IResult CheckIfColorCount()
        {
            var result = _colorDal.GetAll().Count;
            if (result == 0)
            {
                return new ErrorResult();
            }
            return new SuccessResult();
        }

        private IResult CheckIfColorId(int colorId)
        {
            if (colorId == 0)
            {
                return new ErrorResult(Messages.ErrorColorUpdated);
            }

            var result = _colorDal.GetAll(c => c.Id == colorId).Any();
            if (!result)
            {
                return new ErrorResult(Messages.ErrorColorUpdated);
            }
            return new SuccessResult();
        }
    }
}
