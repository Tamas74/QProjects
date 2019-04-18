using System;
using System.Collections.Generic;
using System.Text;


    public   static  class clsRxGeneral
    {

        public static void UpdateLogForRx(string strLogMessage)
        {
            try
            {

                System.IO.StreamWriter objFile = new System.IO.StreamWriter(System.Windows.Forms.Application.StartupPath + "\\gloRx.log", true);
                objFile.WriteLine(System.DateTime.Now + ":" + System.DateTime.Now.Millisecond + "\\t" + strLogMessage);
                objFile.Close();
                objFile = null;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.CreatePrescription, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
            }
        }
    }

