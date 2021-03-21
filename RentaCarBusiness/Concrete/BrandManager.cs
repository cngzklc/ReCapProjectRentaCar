﻿using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Validation;
using Core.Constants;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using RentaCarBusiness.Abstract;
using RentaCarBusiness.BusinessAspect.Autofac;
using RentaCarBusiness.ValidationRules.FluentValidation;
using RentaCarDataAccess.Abstract;
using RentaCarEntities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace RentaCarBusiness.Concrete
{
    public class BrandManager : IBrandService
    {
        IBrandDal _brandDal;
        public BrandManager(IBrandDal brandDal)
        {
            _brandDal = brandDal;
        }

        [SecuredOperation("add, admin")]
        [ValidationAspect(typeof(BrandValidator))]
        [CacheRemoveAspect("IBrandService.Get")]
        public IResult Add(Brand brand)
        {
            _brandDal.Add(brand);
            return new SuccessResult(Messages.Added(brand));
        }

        [CacheRemoveAspect("IBrandService.Get")]
        public IResult Delete(int id)
        {
            Brand brand = _brandDal.Get(b => b.Id == id);
            _brandDal.Delete(brand);
            return new SuccessResult(Messages.Added(brand));
        }

        [ValidationAspect(typeof(BrandValidator))]
        [CacheRemoveAspect("IBrandService.Get")]
        public IResult Update(int id)
        {
            Brand brand = _brandDal.Get(b => b.Id == id);
            _brandDal.Update(brand);
            return new SuccessResult(Messages.Added(brand));
        }
        [CacheAspect]
        public IDataResult<List<Brand>> GetAll()
        {
            return new SuccessDataResult<List<Brand>>( _brandDal.GetAll());
        }

        [CacheAspect]
        public IDataResult<Brand> GetById(int brandId)
        {
            return new SuccessDataResult<Brand>(_brandDal.Get(p => p.Id == brandId));
        }

    }
}
