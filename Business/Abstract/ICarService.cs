using Core.Utilities.Results.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract
{
    public interface ICarService
    {
        IDataResult<List<Car>> GetAll();
        IDataResult<Car> GetById(int carId);
        IDataResult<List<CarDetailDto>> GetCarDetail();
        IResult Add(Car car);
        IResult Update(Car car);
        IResult Delete(Car car);
        IDataResult<List<CarDetailDto>> GetAllByColor(int colorId);
        IDataResult<List<CarDetailDto>> GetAllByBrand(int brandId);
        IDataResult<List<CarDetailDto>> GetByCarId(int carId);
        IDataResult<List<CarDetailDto>> GetCarFilter(int colorId,int brandId);
    }
}
