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
    public class Promis10Controller : Controller
    {        
        public ActionResult Promis10Survey()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Promis10SurveyView(Promis10Model model)
        {
            ViewBag.ReadOnly = true;
            return this.Promis10SurveyEdit(model);
        }

        [HttpPost]        
        public ActionResult Promis10SurveyEdit(Promis10Model promis10Model)
        {
            return View("Promis10Survey", promis10Model);
        }

        [HttpPost]
        [OutputCache(Duration = 86400)]
        public ActionResult Promis10Survey(Promis10Model promis10Model)
        {
            return View(promis10Model);
        }
      
        [HttpPost]
        public ActionResult PostSurvey(Promis10Model promis10Model)
        {
            Serializer<Promis10Model> s = new Serializer<Promis10Model>();

            try
            {
                promis10Model.Save = "true"; 
                s.Save(promis10Model.RequestID, promis10Model); 
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
            Serializer<Promis10Model> s = new Serializer<Promis10Model>();

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
