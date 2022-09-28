using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entities.DTOs;

namespace DataAccess.Concrete.EntityFrameWork
{
    public class EfRentalDal : EfEntityRepositoryBase<Rental, RentACarContext>, IRentalDal
    {
        public List<RentalDetailDto> GetRentalDetail()
        {
            using (RentACarContext context = new RentACarContext())
            {
                var result = from r in context.Rentals
                             join cu in context.Customers on r.CustomerId equals cu.CustomerId
                             join u in context.Users on cu.UserId equals u.Id
                             join c in context.Cars on r.CarId equals c.CarId
                             join b in context.Brands on c.BrandId equals b.Id
                             select new RentalDetailDto
                             {
                                 CarId = r.CarId,
                                 CarName = c.CarName,
                                 BrandName = b.BrandName,
                                 CustomerName = u.FirstName + " " + u.LastName,
                                 RentDate = r.RentDate,
                                 ReturnDate = r.ReturnDate,
                                 TotalDay = r.TotalDay,
                                 TotalPrice = r.TotalPrice

                             };
                return result.ToList();
            }
        }
    }
}
