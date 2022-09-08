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
using Business.ValidationRules.FluentValidation;
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

        public IDataResult<List<Car>> GetAll()
        {
            if (DateTime.Now.Hour == 11)
            {
                return new ErrorDataResult<List<Car>>(Messages.MaintenanceTime);
            }

            return new SuccessDataResult<List<Car>>(_carDal.GetAll(), Messages.CarsListed);
        }

        public IDataResult<Car> GetById(int carId)
        {
            if (DateTime.Now.Hour == 12)
            {
                return new ErrorDataResult<Car>(Messages.MaintenanceTime);
            }

            return new SuccessDataResult<Car>(_carDal.Get(c => c.CarId == carId), Messages.CarListed);
        }

        public IDataResult<List<CarDetailDto>> GetCarDetail()
        {
            if (DateTime.Now.Hour == 12)
            {
                return new ErrorDataResult<List<CarDetailDto>>(Messages.MaintenanceTime);
            }

            return new SuccessDataResult<List<CarDetailDto>>(_carDal.GetCarDetail(), Messages.CarListed);
        }

        [ValidationAspect(typeof(CarValidator))]
        public IResult Add(Car car)
        {
            _carDal.Add(car);
            return new Result(true, Messages.CarAdded);
        }

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
    }
}
