﻿using Core.Aspects.Autofac.Validation;
using Core.Constants;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using RentaCarBusiness.Abstract;
using RentaCarBusiness.ValidationRules.FluentValidation;
using RentaCarDataAccess.Abstract;
using RentaCarDataAccess.DTOs;
using RentaCarEntities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace RentaCarBusiness.Concrete
{
    public class RentalManager : IRentalService
    {
        IRentalDal _rentalDal;
        public RentalManager(IRentalDal rentalDal)
        {
            _rentalDal = rentalDal;
        }

        [ValidationAspect(typeof(RentalValidator))]
        public IResult Add(Rental rental)
        {
            var result = RentableCar(rental.CarId);
            if (result.Success)
            {
                _rentalDal.Add(rental);
                return new SuccessResult(RentableCar(rental.CarId).Message);
            }
            else
            {
                return new ErrorResult(RentableCar(rental.CarId).Message);
            }
            //_rentalDal.Add(rental);
            //return new SuccessResult(Messages.Added(rental));
        }

        public IResult Delete(int id)
        {
            Rental rental = _rentalDal.Get(r => r.Id == id);
            _rentalDal.Delete(rental);
            return new SuccessResult(Messages.Deleted(rental));
        }

        public IDataResult<List<Rental>> GetAllCustomerId(int customerId)
        {
            return new SuccessDataResult<List<Rental>>(_rentalDal.GetAll(r => r.CustomerId == customerId));
        }

        public IDataResult<List<Rental>> GetAll()
        {
            return new SuccessDataResult<List<Rental>>(_rentalDal.GetAll());
        }
        public IDataResult<Rental> GetById( int id)
        {
            return new SuccessDataResult<Rental>(_rentalDal.Get(r=> r.Id == id));
        }

        public IResult RentableCar(int carId)
        {
            var result = _rentalDal.GetAll(r => r.CarId == carId && r.ReturnDate == null);
            if (result.Count > 0)
            {
                return new ErrorResult(Messages.NotRentableCar);
            }
            else
            {
                return new SuccessResult(Messages.RentableCar);
            }
        }

        public IResult Update(int id)
        {
            Rental rental = _rentalDal.Get(r => r.Id == id);
            _rentalDal.Update(rental);
            return new SuccessResult(Messages.Updated(rental));
        }

        public IDataResult<List<NotRentableCarDto>> GetNotRentableCarDetails()
        {
            return new SuccessDataResult<List<NotRentableCarDto>>(_rentalDal.GetNotRentableCarDetails());
        }
    }
}
