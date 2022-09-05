using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract
{
    public interface ICarService
    {
        List<Car> GetAll();
        Car GetById(int carId);
        List<CarDetailDto> GetCarDetail();
        public void Insert(Car car);
        public void Update(Car car);
        public void Delete(Car car);
    }
}
