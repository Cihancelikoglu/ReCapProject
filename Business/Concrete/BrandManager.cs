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
    internal class BrandManager : IBrandService
    {
        private IBrandDal _brandDal;

        public BrandManager(IBrandDal brandDal)
        {
            _brandDal = brandDal;
        }

        public IDataResult<List<Brand>> GetAll()
        {
            if (DateTime.Now.Hour == 11)
            {
                return new ErrorDataResult<List<Brand>>(Messages.MaintenanceTime);
            }

            return new SuccessDataResult<List<Brand>>(_brandDal.GetAll(), Messages.BrandsListed);
        }

        public IDataResult<Brand> GetById(int brandId)
        {
            if (DateTime.Now.Hour == 12)
            {
                return new ErrorDataResult<Brand>(Messages.MaintenanceTime);
            }

            return new SuccessDataResult<Brand>(_brandDal.Get(c => c.Id == brandId), Messages.BrandListed);
        }

        public IResult Add(Brand brand)
        {
            _brandDal.Add(brand);
            return new Result(true, Messages.BrandAdded);
        }

        public IResult Update(Brand brand)
        {
            IResult result = BusinessRules.Run(CheckIfBrandId(brand.Id));
            if (result != null)
            {
                return result;
            }

            _brandDal.Update(brand);
            return new SuccessResult(Messages.BrandUpdated);
        }

        public IResult Delete(Brand brand)
        {
            IResult result = BusinessRules.Run(CheckIfBrandId(brand.Id));
            if (result != null)
            {
                return result;
            }

            _brandDal.Delete(brand);
            return new SuccessResult(Messages.BrandDeleted);
        }

        private IResult CheckIfBrandId(int brandId)
        {
            if (brandId == 0)
            {
                return new ErrorResult(Messages.ErrorCarUpdated);
            }

            var result = _brandDal.GetAll(c => c.Id == brandId).Any();
            if (!result)
            {
                return new ErrorResult(Messages.ErrorCarUpdated);
            }
            return new SuccessResult();
        }
    }
}
