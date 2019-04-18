using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QSurvey.Models;
using QSurvey.Serialization;

namespace QSurvey.Controllers
{
    public class PHQ2Controller : Controller
    {
        public ActionResult PHQ2Survey()
        {
            return View();
        }

        [HttpPost]
        public ActionResult PHQ2SurveyView(PHQ2Model model)
        {
            ViewBag.ReadOnly = true;
            return this.PHQ2SurveyEdit(model);
        }

        [HttpPost]        
        public ActionResult PHQ2SurveyEdit(PHQ2Model PHQ2Model)
        {
            return View("PHQ2Survey", PHQ2Model);
        }

        [HttpPost]
        [OutputCache(Duration = 86400)]
        public ActionResult PHQ2Survey(PHQ2Model PHQ2Model)
        {
            return View(PHQ2Model);
        }

        [HttpPost]
        public ActionResult PostSurvey(PHQ2Model PHQ2Model)
        {
            Serializer<PHQ2Model> s = new Serializer<PHQ2Model>();
                        
            try
            {
                PHQ2Model.Save = "true";
                s.Save(PHQ2Model.RequestID, PHQ2Model);

                if (PHQ2Model.IsNegativeScreening == false)
                {
                    PHQ9Model p = new PHQ9Model(PHQ2Model.AgeInYears) { RequestID = PHQ2Model.RequestID + 1, IsPHQ2Completed = true };
                    return RedirectToAction("PHQ9SurveyGet", "PHQ9", p);
                }
                else
                {
                    return View("CompletedSurvey"); 
                }

            }
            catch (Exception ex)
            {                
                Logger.Log(ex.ToString());
                return View();
            }
            finally { s = null; }            
        }

        [HttpPost]
        public JsonResult GetModel(Int64 RequestID)
        {
            string sModelContent = string.Empty;
            JsonResult jsonResult = new JsonResult() { JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            Serializer<PHQ2Model> s = new Serializer<PHQ2Model>();

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
