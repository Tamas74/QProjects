using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QSurvey.Models;
using QSurvey.Serialization;

namespace QSurvey.Controllers
{
    public class VR36Controller : Controller
    {
        public ActionResult VR36Survey()
        {
            return View();
        }

        [HttpPost]
        public ActionResult VR36SurveyView(VR36Model model)
        {
            ViewBag.ReadOnly = true;
            return this.VR36SurveyEdit(model);
        }

        [HttpPost]        
        public ActionResult VR36SurveyEdit(VR36Model vr36Model)
        {
            return View("VR36Survey", vr36Model);
        }

        [HttpPost]
        [OutputCache(Duration = 86400)]
        public ActionResult VR36Survey(VR36Model vr36Model)
        {            
            return View(vr36Model);
        }

        [HttpPost]
        public ActionResult PostSurvey(VR36Model vr36Model)
        {
            Serializer<VR36Model> s = new Serializer<VR36Model>();

            try
            {
                vr36Model.Save = "true"; 
                s.Save(vr36Model.RequestID, vr36Model);
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
            Serializer<VR36Model> s = new Serializer<VR36Model>();

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
