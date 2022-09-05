using Business.Concrete;
using DataAccess.Concrete.EntityFrameWork;
using DataAccess.Concrete.InMemory;
using Entities.Concrete;
using System;

namespace ConsoleUI
{
    class Program
    {
        static void Main(string[] args)
        {
            CarManagerTest();

        }

        private static void CarManagerTest()
        {
            Console.WriteLine("-----------GetByAll------------");
            CarManager carManager = new CarManager(new EfCarDal());
            foreach (var car in carManager.GetAll())
            {
                Console.WriteLine("Açıklama: " + car.Description + " " + "Fiyat: " + car.DailyPrice);
            }

            Console.WriteLine("-----------GetById------------");
            Console.WriteLine(carManager.GetById(2).Description);

            Console.WriteLine("-----------CarDetailDto------------");
            foreach (var carDetail in carManager.GetCarDetail())
            {
                Console.WriteLine(carDetail.CarId + " " + carDetail.BrandName + " / " + carDetail.ColorName + " / " + carDetail.DailyPrice);
            }

            Console.WriteLine("-----------Insert------------");
            Car car1 = new Car { BrandId = 1, ColorId = 4, ModelYear = new DateTime(2021, 12, 12), DailyPrice = 1500, Description = "Mercedes E200" };
            if (car1.DailyPrice > 0 && car1.Description.Length >= 2)
            {
                carManager.Insert(car1);
            }

            //Console.WriteLine("-----------Delete------------");
            //Car car2 = new Car { CarId = 6};
            //carManager.Delete(car2);

            Console.WriteLine("-----------Update------------");
            Car car3 = new Car { CarId = 2, BrandId = 1, ColorId = 2, ModelYear = new DateTime(2021, 09, 12), DailyPrice = 800, Description = "BMW 535" };
            carManager.Update(car3);
        }
    }
}
