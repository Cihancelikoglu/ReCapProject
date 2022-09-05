using Business.Abstract;
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

        public List<Car> GetAll()
        {
            return _carDal.GetByAll();
        }

        public Car GetById(int carId)
        {
            return _carDal.Get(x => x.CarId == carId);
        }

        public List<CarDetailDto> GetCarDetail()
        {
            return _carDal.GetCarDetail();
        }

        public void Insert(Car car)
        {
            Console.WriteLine(car.CarId + " " + "Id Numarasına Ait Yeni Araç Eklendi");
            _carDal.Add(car);
        }

        public void Update(Car car)
        {
            Console.WriteLine(car.CarId + " " + "Id Numarasına Ait Araç Güncellendi");
            _carDal.Update(car);
        }

        public void Delete(Car car)
        {
            Console.WriteLine(car.CarId + " " + "Id Numarasına Ait Araç Silindi");
            _carDal.Delete(car);
        }
    }
}
