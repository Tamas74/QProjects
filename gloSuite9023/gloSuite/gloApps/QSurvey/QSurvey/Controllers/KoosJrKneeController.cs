using System.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QSurvey.Models;
using QSurvey.Classes;
using Newtonsoft.Json;
using QSurvey.Serialization;


namespace QSurvey.Controllers
{
    public class KoosJrKneeController : Controller
    {        
        public ActionResult KoosJRKneeSurvey()
        {
            return View();
        }

        [HttpPost]
        public ActionResult KoosJRKneeSurveyView(KoosJrKneeModel model)
        {
            ViewBag.ReadOnly = true;
            return this.KoosJRKneeSurveyEdit(model);
        }

        [HttpPost]        
        public ActionResult KoosJRKneeSurveyEdit(KoosJrKneeModel kooskneeModel)
        {
            return View("KoosJRKneeSurvey", kooskneeModel);
        }

        [HttpPost]
        [OutputCache(Duration = 86400)]
        public ActionResult KoosJRKneeSurvey(KoosJrKneeModel kooskneeModel)
        {
            return View(kooskneeModel);
        }
      
        [HttpPost]
        public ActionResult PostSurvey(KoosJrKneeModel koosKneeModel)
        {
            Serializer<KoosJrKneeModel> s = new Serializer<KoosJrKneeModel>();

            try
            {
                koosKneeModel.Save = "true";
                s.Save(koosKneeModel.RequestID, koosKneeModel); 
            }
            catch (Exception ex)
            {
                Logger.Log(ex.ToString());
            }
            finally { s = null; }

            return View("CompletedSurvey");
        }
        
        [HttpPost]
        public JsonResult GetModel(Int64 RequestID)
        {
            string sModelContent = string.Empty;
            JsonResult jsonResult = new JsonResult() { JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            Serializer<KoosJrKneeModel> s = new Serializer<KoosJrKneeModel>();

            try
            {                
                jsonResult.Data = s.GetModelReference(RequestID);
            }
            catch (Exception ex)
            {
                Logger.Log(ex.ToString());                
            }
            finally
            {                
                s = null;
            }
            
            return jsonResult;
        }

    }
}
