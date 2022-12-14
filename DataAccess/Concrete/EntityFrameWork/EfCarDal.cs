using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace DataAccess.Concrete.EntityFrameWork
{
    public class EfCarDal : EfEntityRepositoryBase<Car, RentACarContext>, ICarDal
    {
        public List<CarDetailDto> GetCarDetail(Expression<Func<CarDetailDto, bool>> filter = null)
        {
            using (RentACarContext context = new RentACarContext())
            {
                var result = from c in context.Cars
                             join b in context.Brands on c.BrandId equals b.Id
                             join a in context.Colors on c.ColorId equals a.Id
                             select new CarDetailDto
                             {
                                 CarId = c.CarId,
                                 BrandId = b.Id,
                                 ColorId = a.Id,
                                 CarName = c.CarName,
                                 BrandName = b.BrandName,
                                 ColorName = a.ColorName,
                                 DailyPrice = c.DailyPrice,
                                 ModelYear = c.ModelYear,
                                 Description = c.Description,
                                 Findex = c.Findex,
                                 CarImages = (from carimg in context.CarImages where carimg.CarId == c.CarId select carimg.ImagePath).FirstOrDefault()
                             };
                return filter != null 
                    ? result.Where(filter).ToList() 
                    : result.ToList();
            }
        }
    }
}
