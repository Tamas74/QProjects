using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QSurvey.Classes
{
    public class JSONConverter<T>
    {
        public T GetModel(object obj)
        {
            T returned = default(T);
            JsonResult jsonResult = new JsonResult();
            jsonResult.JsonRequestBehavior = JsonRequestBehavior.AllowGet;

            try
            {
                if (obj is T){ returned = (T)obj; }
            }
            catch (Exception)
            {
                
                throw;
            }

            return returned;
        }
    }
}