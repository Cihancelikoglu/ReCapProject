using Business.Abstract;
using Core.Utilities.Business;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Business.Concrete
{
    public class CreditCardManager : ICreditCardService
    {
        ICreditCardDal _creditCartDal;
        public CreditCardManager(ICreditCardDal creditCartDal)
        {
            _creditCartDal = creditCartDal;
        }

        public IDataResult<List<CreditCard>> GetAll()
        {
            return new SuccessDataResult<List<CreditCard>>(_creditCartDal.GetAll());
        }

        public IResult CheckCreditCart(CreditCard creditCard)
        {
            IResult result = BusinessRules.Run(CheckIfCreditCartNumber(creditCard), CheckIfCreditCartBalance(creditCard));

            if (result != null)
            {
                return result;
            }
            return new Result(true, "Ödeme Başarılı");
        }

        private IResult CheckIfCreditCartNumber(CreditCard creditCard)
        {
            var result = _creditCartDal.GetAll(c => c.CardNumber == creditCard.CardNumber && c.ExpireYearMonth == creditCard.ExpireYearMonth
            && c.Cvv == creditCard.Cvv && c.CardHolder == creditCard.CardHolder).Any();
            if (!result)
            {
                return new ErrorResult("Kart Bilgileri Hatalı");
            }
            else
            {
                return new SuccessResult("Ödeme Başarılı");
            }
        }

        private IResult CheckIfCreditCartBalance(CreditCard creditCard)
        {
            var result = _creditCartDal.GetAll(c => c.CardNumber == creditCard.CardNumber && c.Balance > creditCard.Balance).Any();
            if (!result)
            {
                return new ErrorResult("Kart Limiti Yetersiz");
            }
            else
            {
                return new SuccessResult("Ödeme Başarılı");
            }
        }
    }
}
