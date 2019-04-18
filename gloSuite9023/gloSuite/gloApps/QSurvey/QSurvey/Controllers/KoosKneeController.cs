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
    public class KoosKneeController : Controller
    {        
        public ActionResult KoosKneeSurvey()
        {
            return View();
        }

        [HttpPost]
        public ActionResult KoosKneeSurveyView(KoosKneeModel model)
        {
            ViewBag.ReadOnly = true;
            return this.KoosKneeSurveyEdit(model);
        }

        [HttpPost]        
        public ActionResult KoosKneeSurveyEdit(KoosKneeModel kooskneeModel)
        {
            return View("KoosKneeSurvey", kooskneeModel);
        }

        [HttpPost]
        [OutputCache(Duration = 86400)]
        public ActionResult KoosKneeSurvey(KoosKneeModel kooskneeModel)
        {
            return View(kooskneeModel);
        }
      
        [HttpPost]
        public ActionResult PostSurvey(KoosKneeModel koosKneeModel)
        {
            Serializer<KoosKneeModel> s = new Serializer<KoosKneeModel>();

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
            Serializer<KoosKneeModel> s = new Serializer<KoosKneeModel>();

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
