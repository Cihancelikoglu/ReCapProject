using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace DataAccess.Concrete.InMemory
{
    public class InMemoryCarDal : ICarDal
    {
        List<Car> _car;
        public InMemoryCarDal()
        {
            _car = new List<Car> {
            new Car { CarId = 1, BrandId = 2, ColorId = 1, ModelYear = new DateTime(2020, 05, 09), DailyPrice = 120, Description = "Mercedes A180" },
            new Car { CarId = 2, BrandId = 2, ColorId = 2, ModelYear = new DateTime(2021, 05, 09), DailyPrice = 150, Description = "Mercedes C180" },
            new Car { CarId = 3, BrandId = 2, ColorId = 3, ModelYear = new DateTime(2021, 05, 09), DailyPrice = 180, Description = "BMW 520d" },
            new Car { CarId = 4, BrandId = 2, ColorId = 4, ModelYear = new DateTime(2019, 05, 09), DailyPrice = 100, Description = "Fiat Egea" },
            new Car { CarId = 5, BrandId = 2, ColorId = 5, ModelYear = new DateTime(2018, 05, 09), DailyPrice = 130, Description = "BMW 320" }
            };
        }

        public void Add(Car car)
        {
            _car.Add(car);
        }

        public void Delete(Car car)
        {
            Car carDelete = _car.SingleOrDefault(x => x.CarId == car.CarId);
            _car.Remove(carDelete);
        }

        public Car Get(Expression<Func<Car, bool>> filter)
        {
            throw new NotImplementedException();
        }

        public List<Car> GetByAll()
        {
            return _car;
        }

        public List<Car> GetAll(Expression<Func<Car, bool>> filter = null)
        {
            throw new NotImplementedException();
        }

        public List<Car> GetById(int carId)
        {
            return _car.Where(x => x.CarId == carId).ToList();
        }

        public List<CarDetailDto> GetCarDetail()
        {
            throw new NotImplementedException();
        }

        public void Update(Car car)
        {
            Car carUpdate = _car.SingleOrDefault(x => x.CarId == car.CarId);
            carUpdate.ColorId = car.ColorId;
            carUpdate.DailyPrice = car.DailyPrice;
            carUpdate.Description = car.Description;

        }
    }
}
