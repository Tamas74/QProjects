using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QSurvey.Models;
using QSurvey.Serialization;

namespace QSurvey.Controllers
{
    public class PHQ9Controller : Controller
    {
        [HttpPost]
        public ActionResult PHQ9SurveyView(PHQ9Model model)
        {
            ViewBag.ReadOnly = true;
            return this.PHQ9SurveyEdit(model);
        }

        [HttpPost]        
        public ActionResult PHQ9SurveyEdit(PHQ9Model PHQ9Model)
        {
            return View("PHQ9Survey", PHQ9Model);
        }
        
        public ActionResult PHQ9SurveyGet(PHQ9Model phq9Model)
        {
            return View("PHQ9Survey", phq9Model);
        }

        [HttpPost]
        [OutputCache(Duration = 86400)]
        public ActionResult PHQ9Survey(PHQ9Model phq9Model)
        {            
            return View(phq9Model);
        }

        [HttpPost]
        public ActionResult PostSurvey(PHQ9Model phq9Model)
        {
            Serializer<PHQ9Model> s = new Serializer<PHQ9Model>();

            try
            {
                phq9Model.Save = "true"; 
                s.Save(phq9Model.RequestID, phq9Model);
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
            Serializer<PHQ9Model> s = new Serializer<PHQ9Model>();

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
