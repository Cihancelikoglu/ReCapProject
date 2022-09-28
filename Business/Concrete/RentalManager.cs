using Business.Abstract;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspect.Autofac.Validation;
using Core.Utilities.Business;
using Entities.DTOs;

namespace Business.Concrete
{
    public class RentalManager : IRentalService
    {
        IRentalDal _rentalDal;
        public RentalManager(IRentalDal rentalDal)
        {
            _rentalDal = rentalDal;
        }

        public IDataResult<List<Rental>> GetAll()
        {
            return new SuccessDataResult<List<Rental>>(_rentalDal.GetAll(), "Kiralık Araçlar Listelendi");
        }

        public IDataResult<Rental> GetById(int rentalId)
        {
            return new SuccessDataResult<Rental>(_rentalDal.Get(x => x.Id == rentalId), "Araç Listelendi");

        }

        public IDataResult<List<RentalDetailDto>> GetRentalDetail()
        {
            return new SuccessDataResult<List<RentalDetailDto>>(_rentalDal.GetRentalDetail(), Messages.CarListed);
        }

        [ValidationAspect(typeof(RentalValidator))]
        public IResult RentalDateControl(Rental rental)
        {
            IResult result = BusinessRules.Run(CheckIfRentalDate(rental));

            if (result != null)
            {
                return result;
            }
            return new Result(true, "Araç Kiralanabilir");
        }

        public IResult Add(Rental rental)
        {
            var result = _rentalDal.GetAll(c => c.CarId == rental.CarId && (c.ReturnDate == null || c.ReturnDate >= rental.RentDate));
            if (rental.ReturnDate < rental.RentDate)
            {
                return new Result(false, "Dönüş Tarihi Kiralama Tarihinden Küçük Olamaz");
            }
            else
            {
                if (result.Count > 0)
                {
                    return new ErrorResult("Bu Tarikler Arası Aracı Kiralayamızsınız.");
                }
                else
                {
                    _rentalDal.Add(rental);
                    return new Result(true, "Araç Kiralandı");
                }
            }
        }

        public IResult Update(Rental rental)
        {
            throw new NotImplementedException();
        }

        public IResult Delete(Rental rental)
        {
            throw new NotImplementedException();
        }

        private IResult CheckIfRentalDate(Rental rental)
        {
            var result = _rentalDal.GetAll(c => c.CarId == rental.CarId && (c.ReturnDate == null || c.ReturnDate >= rental.RentDate)).Any();
            if (result)
            {
                return new ErrorResult("Bu Tarihler Arası Aracı Kiralayamızsınız.");
            }
            else
            {
                return new SuccessResult("Araç Kiralanabilir");
            }
        }

    }
}
