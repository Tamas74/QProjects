using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QSurvey.Models;
using QSurvey.Serialization;

namespace QSurvey.Controllers
{
    public class VR12Controller : Controller
    {
        public ActionResult VR12Survey()
        {
            return View();
        }

        [HttpPost]
        public ActionResult VR12SurveyView(VR12Model model)
        {
            ViewBag.ReadOnly = true;
            return this.VR12SurveyEdit(model);
        }

        [HttpPost]        
        public ActionResult VR12SurveyEdit(VR12Model vr12Model)
        {
            return View("VR12Survey", vr12Model);
        }

        [HttpPost]
        [OutputCache(Duration = 86400)]
        public ActionResult VR12Survey(VR12Model vr12Model)
        {            
            return View(vr12Model);
        }

        [HttpPost]
        public ActionResult PostSurvey(VR12Model vr12Model)
        {
            Serializer<VR12Model> s = new Serializer<VR12Model>();

            try
            {
                vr12Model.Save = "true"; 
                s.Save(vr12Model.RequestID, vr12Model);
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
            Serializer<VR12Model> s = new Serializer<VR12Model>();

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
