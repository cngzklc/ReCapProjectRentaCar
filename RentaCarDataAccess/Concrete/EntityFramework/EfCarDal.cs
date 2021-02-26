﻿using Core.DataAccess.EntityFramework;
using RentaCarDataAccess.Abstract;
using RentaCarDataAccess.Comparer;
using RentaCarDataAccess.DTOs;
using RentaCarEntities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RentaCarDataAccess.Concrete.EntityFramework
{
    public class EfCarDal : EfEntityRepositoryBase<Car, RentaCarContext>, ICarDal
    {
        public List<CarDetailDto> GetCarDetails()
        {
            using (RentaCarContext context = new RentaCarContext())
            {
                var resault = from c in context.Cars
                              join b in context.Brands on c.BrandId equals b.BrandId
                              join o in context.Colors on c.ColorId equals o.ColorId

                              select new CarDetailDto
                              {
                                  CarName = c.CarName,
                                  BrandName = b.BrandName,
                                  ColorName = o.ColorName,
                                  DailyPrice = c.DailyPrice,
                                  Description = c.Description
                              };
                return resault.ToList();
            }
        }
        public List<Car> GetRentableCars()
        {
            return GetAll().Where(c => !GetNotRentableCars().Any(nrc => nrc.CarId == c.CarId)).ToList();
        }
        public List<CarDetailDto> GetRentableCarsDto()
        {
            using (RentaCarContext context = new RentaCarContext())
            {
                var resault = from c in GetAll().Where(c => !GetNotRentableCars().Any(nrc => nrc.CarId == c.CarId)).ToList()
                              join b in context.Brands on c.BrandId equals b.BrandId
                              join o in context.Colors on c.ColorId equals o.ColorId

                              select new CarDetailDto
                              {
                                  CarName = c.CarName,
                                  BrandName = b.BrandName,
                                  ColorName = o.ColorName,
                                  DailyPrice = c.DailyPrice,
                                  Description = c.Description
                              };
                return resault.ToList();
            }
        }
        public List<Car> GetNotRentableCars()
        {
            using (RentaCarContext context = new RentaCarContext())
            {
                var result = from c in context.Cars
                              join r in context.Rentals on c.CarId equals r.CarId where r.ReturnDate == null
                              join o in context.Colors on c.ColorId equals o.ColorId

                              select new Car
                              {
                                  CarId = c.CarId,
                                  BrandId = c.BrandId,
                                  ColorId = c.ColorId,
                                  CarName = c.CarName,
                                  ModelYear = c.ModelYear,
                                  DailyPrice = c.DailyPrice,
                                  Description = c.Description
                              };
                return result.ToList();
            }
        }

        public List<Car> GetRentableCars2()
        {
            return GetAll().Except(GetNotRentableCars(), new CarComparer()).ToList();
        }
    }
}
