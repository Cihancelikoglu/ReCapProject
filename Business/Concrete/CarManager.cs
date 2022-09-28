using Business.Abstract;
using Business.Constants;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Text;
using Business.BusinessAspect.Aspect;
using Business.ValidationRules.FluentValidation;
using Core.Aspect.Autofac.Caching;
using Core.Aspect.Autofac.Validation;

namespace Business.Concrete
{
    public class CarManager : ICarService
    {
        private readonly ICarDal _carDal;
        public CarManager(ICarDal carDal)
        {
            _carDal = carDal;
        }

        [CacheAspect]
        public IDataResult<List<Car>> GetAll()
        {
            if (DateTime.Now.Hour == 11)
            {
                return new ErrorDataResult<List<Car>>(Messages.MaintenanceTime);
            }

            return new SuccessDataResult<List<Car>>(_carDal.GetAll(), Messages.CarsListed);
        }

        [CacheAspect]
        public IDataResult<Car> GetById(int carId)
        {
            //if (DateTime.Now.Hour == 12)
            //{
            //    return new ErrorDataResult<Car>(Messages.MaintenanceTime);
            //}

            return new SuccessDataResult<Car>(_carDal.Get(c => c.CarId == carId), Messages.CarListed);
        }

        public IDataResult<List<CarDetailDto>> GetCarDetail()
        {
            return new SuccessDataResult<List<CarDetailDto>>(_carDal.GetCarDetail(), Messages.CarListed);
        }

        [SecuredOperation("product.add,admin")]
        [ValidationAspect(typeof(CarValidator))]
        [CacheRemoveAspect("ICarService.Get")]
        public IResult Add(Car car)
        {
            _carDal.Add(car);
            return new Result(true, Messages.CarAdded);
        }

        [CacheRemoveAspect("ICarService.Get")]
        public IResult Update(Car car)
        {
            if (car.CarId < 0)
            {
                return new ErrorResult(Messages.ErrorCarUpdated);
            }
            _carDal.Update(car);
            return new Result(true, Messages.CarUpdated);
        }

        public IResult Delete(Car car)
        {
            if (car.CarId < 0)
            {
                return new ErrorResult(Messages.ErrorCarDeleted);
            }
            _carDal.Delete(car);
            return new Result(true, Messages.CarDeleted);
        }

        public IDataResult<List<CarDetailDto>> GetAllByColor(int colorId)
        {
            return new SuccessDataResult<List<CarDetailDto>>(_carDal.GetCarDetail(x => x.ColorId == colorId), "Renk'e göre listelendi");
        }

        public IDataResult<List<CarDetailDto>> GetAllByBrand(int brandId)
        {
            return new SuccessDataResult<List<CarDetailDto>>(_carDal.GetCarDetail(x => x.BrandId == brandId), "Markaya göre listelendi");
        }

        public IDataResult<List<CarDetailDto>> GetByCarId(int carId)
        {
            return new SuccessDataResult<List<CarDetailDto>>(_carDal.GetCarDetail(c => c.CarId == carId), Messages.CarListed);
        }

        public IDataResult<List<CarDetailDto>> GetCarFilter(int colorId, int brandId)
        {
            return new SuccessDataResult<List<CarDetailDto>>(_carDal.GetCarDetail(c => c.ColorId == colorId && c.BrandId == brandId));
        }
    }
}
