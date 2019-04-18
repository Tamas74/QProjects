using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QSurvey.Models;
using QSurvey.Serialization;

namespace QSurvey.Controllers
{
    public class PHQAController : Controller
    {
        public ActionResult PHQASurvey()
        {
            return View();
        }

        [HttpPost]
        public ActionResult PHQASurveyView(PHQAModel model)
        {
            ViewBag.ReadOnly = true;
            return this.PHQASurveyEdit(model);
        }

        [HttpPost]        
        public ActionResult PHQASurveyEdit(PHQAModel PHQAModel)
        {
            return View("PHQASurvey", PHQAModel);
        }

        [HttpPost]
        [OutputCache(Duration = 86400)]
        public ActionResult PHQASurvey(PHQAModel phqAModel)
        {            
            return View(phqAModel);
        }

        [HttpPost]
        public ActionResult PostSurvey(PHQAModel phqAModel)
        {
            Serializer<PHQAModel> s = new Serializer<PHQAModel>();

            try
            {
                phqAModel.Save = "true"; 
                s.Save(phqAModel.RequestID, phqAModel);
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
            Serializer<PHQAModel> s = new Serializer<PHQAModel>();

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
