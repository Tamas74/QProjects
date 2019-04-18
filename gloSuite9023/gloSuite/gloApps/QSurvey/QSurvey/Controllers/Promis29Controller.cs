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
    public class Promis29Controller : Controller
    {        
        public ActionResult Promis29Survey()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Promis29SurveyView(Promis29Model model)
        {
            ViewBag.ReadOnly = true;
            return this.Promis29SurveyEdit(model);
        }

        [HttpPost]        
        public ActionResult Promis29SurveyEdit(Promis29Model promis29Model)
        {
            return View("Promis29Survey", promis29Model);
        }

        [HttpPost]
        [OutputCache(Duration = 86400)]
        public ActionResult Promis29Survey(Promis29Model kooskneeModel)
        {
            return View(kooskneeModel);
        }
      
        [HttpPost]
        public ActionResult PostSurvey(Promis29Model koosKneeModel)
        {
            Serializer<Promis29Model> s = new Serializer<Promis29Model>();

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
            Serializer<Promis29Model> s = new Serializer<Promis29Model>();

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
