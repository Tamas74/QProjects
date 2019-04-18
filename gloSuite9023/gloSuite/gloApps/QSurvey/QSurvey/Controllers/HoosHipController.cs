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
    public class HoosHipController : Controller
    {        
        public ActionResult HoosHipSurvey()
        {
            return View();
        }

        [HttpPost]
        public ActionResult HoosHipSurveyView(HoosHipModel hoosHipModel)
        {
            ViewBag.ReadOnly = true;            
            return this.HoosHipSurveyEdit(hoosHipModel);
        }

        [HttpPost]
        public ActionResult HoosHipSurveyEdit(HoosHipModel hoosHipModel)
        {
            return View("HoosHipSurvey", hoosHipModel);            
        }

        [HttpPost]
        [OutputCache(Duration = 86400)]
        public ActionResult HoosHipSurvey(HoosHipModel hoosHipModel)
        {           
            return View(hoosHipModel);
        }
      
        [HttpPost]
        public ActionResult PostSurvey(HoosHipModel hoosHipModel)
        {
            Serializer<HoosHipModel> s = new Serializer<HoosHipModel>();

            try
            {
                hoosHipModel.Save = "true";
                
                s.Save(hoosHipModel.RequestID, hoosHipModel); 
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
            Serializer<HoosHipModel> s = new Serializer<HoosHipModel>();

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
