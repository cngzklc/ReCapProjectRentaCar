﻿using RentaCarDataAccess.Abstract;
using RentaCarEntities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace RentaCarDataAccess.Concrete.EntityFramework
{
    public class EfColorDal :BaseEntityRepository<Color>, IColorDal
    {

    }
}
