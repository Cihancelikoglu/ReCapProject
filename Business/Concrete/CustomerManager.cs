using Business.Abstract;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Concrete
{
    public class CustomerManager : ICustomerService
    {
        ICustomerDal _customerDal;
        public CustomerManager(ICustomerDal customerDal)
        {
            _customerDal = customerDal;
        }


        public IDataResult<List<Customer>> GetAll()
        {
            return new SuccessDataResult<List<Customer>>(_customerDal.GetAll());
        }

        public IDataResult<Customer> GetById(int customerId)
        {
            return new SuccessDataResult<Customer>(_customerDal.Get(x => x.CustomerId == customerId));
        }

        public IResult Add(Customer customer)
        {
            if (customer.CompanyName != null)
            {
                return new ErrorResult("Müşteri Eklenemedi");
            }
            _customerDal.Add(customer);
            return new Result(true, "Müşteri Eklendi");
        }

        public IResult Update(Customer customer)
        {
            throw new NotImplementedException();
        }

        public IResult Delete(Customer customer)
        {
            throw new NotImplementedException();
        }
    }
}
