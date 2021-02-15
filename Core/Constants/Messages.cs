﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Constants
{
    public static class Messages
    {
        public static string Added(object entity) { return string.Format("{0} added!", entity.GetType().Name); }
        public static string Invalid(object entity) { return string.Format("Invalid! {0}", entity.GetType().Name); }
        public static string ProductNameInvalid = "Ürün ismi geçersiz!";
        public static string Listed(object entity) { return string.Format("{0} Listed!", entity.GetType().Name); }
        public static string NotListed(object entity) { return string.Format("{0} Not Listed!", entity.GetType().Name); }
        public static string MaintenanceTime = "Sistem bakımda!";

        //public static string RentableCar(object entity) { return string.Format("Rentable {0}!", entity.GetType().Name); }
        //public static string NotRentableCar(object entity) { return string.Format("Not a rentable {0}!", entity.GetType().Name); }
        public static string NotRentableCar = "Kirada olan araç gelmeden kiralanamaz!";
        public static string RentableCar = "Kiralanabilir araç";

        public static string Updated(object entity) { return string.Format("{0} updated!", entity.GetType().Name); }
        public static string Deleted(object entity) { return string.Format("{0} deleted!", entity.GetType().Name); }
    }
}
