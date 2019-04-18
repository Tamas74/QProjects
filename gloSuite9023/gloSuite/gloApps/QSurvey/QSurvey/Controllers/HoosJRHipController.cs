using System;
using System.Web.Mvc;
using QSurvey.Models;
using QSurvey.Serialization;

namespace QSurvey.Controllers
{
    public class HoosJRHipController : Controller
    {        
        public ActionResult HoosJRHipSurvey()
        {
            return View();
        }

        [HttpPost]
        public ActionResult HoosJRHipSurveyView(HoosJRHipModel model)
        {
            ViewBag.ReadOnly = true;
            return this.HoosJRHipSurveyEdit(model);
        }

        [HttpPost]        
        public ActionResult HoosJRHipSurveyEdit(HoosJRHipModel model)
        {
            return View("HoosJRHipSurvey", model);
        }

        [HttpPost]
        [OutputCache(Duration = 86400)]
        public ActionResult HoosJRHipSurvey(HoosJRHipModel model)
        {
            return View(model);
        }

        [HttpPost]
        public ActionResult PostSurvey(HoosJRHipModel hoosJRHipModel)
        {
            Serializer<HoosJRHipModel> s = new Serializer<HoosJRHipModel>();

            try
            {
                hoosJRHipModel.Save = "true";
                s.Save(hoosJRHipModel.RequestID, hoosJRHipModel);
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
            Serializer<HoosJRHipModel> s = new Serializer<HoosJRHipModel>();

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
