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

namespace Business.Concrete
{
    public class CarManager : ICarService
    {
        ICarDal _carDal;
        public CarManager(ICarDal carDal)
        {
            _carDal = carDal;
        }

        public IDataResult<List<Car>> GetAll()
        {
            return new SuccessDataResult<List<Car>>(_carDal.GetByAll(), Messages.CarListed);
        }

        public IDataResult<Car> GetById(int carId)
        {
            return new SuccessDataResult<Car>(_carDal.Get(c => c.CarId == carId), Messages.CarListed);
        }

        public IDataResult<List<CarDetailDto>> GetCarDetail()
        {
            return new SuccessDataResult<List<CarDetailDto>>(_carDal.GetCarDetail(), Messages.CarListed);
        }

        public IResult Add(Car car)
        {
            if (car.BrandId < 0)
            {
                return new ErrorResult(Messages.ErrorCarAdded);
            }
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
