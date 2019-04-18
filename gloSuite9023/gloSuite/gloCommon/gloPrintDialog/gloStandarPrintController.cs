using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing.Printing;
using System.Drawing;

namespace gloPrintDialog
{
    //access modifier change to public to use this class in gloEMR for batch printing
    public class gloStandardPrintController : StandardPrintController
    {

        public volatile bool IsErrors;// { get; set; }
        public volatile bool IsRestart; //{ get; set; }
        public volatile bool IsCancel; //{ get; set; }
        public gloStandardPrintController()
        {
            IsErrors = false;
            IsRestart = false;
            IsCancel = false;
        }

        public override void OnStartPrint(PrintDocument document, PrintEventArgs e)
        {
            if (IsCancel || IsErrors)
            {
                if (e != null)
                {
                    e.Cancel = true;
                }
                else
                {
                    gloAuditTrail.gloAuditTrail.ExceptionLog("OnStartPrint Event is null", false);
                }

            }
            IsErrors = false;
            try
            {
                if (document != null && e != null)
                {
                    base.OnStartPrint(document, e);
                }
                else
                {
                    gloAuditTrail.gloAuditTrail.ExceptionLog("OnStartPrint base Event" + (document == null ? " Document is Null" : "") + (e == null ? " Event is Null" : ""), false);
                }

            }
            catch (System.ComponentModel.Win32Exception winEx)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(winEx, false);
                if (winEx.NativeErrorCode == 0x3F)
                {
                    System.Windows.Forms.MessageBox.Show("Your file waiting to be printed was deleted.");
                }
                else
                {
                    //System.Windows.Forms.MessageBox.Show(winEx.ToString());
                    gloAuditTrail.gloAuditTrail.ExceptionLog(winEx.ToString(), false);
                }
                winEx = null;
                IsErrors = true;
                if (e != null)
                {
                    e.Cancel = true;
                }
                else
                {
                    gloAuditTrail.gloAuditTrail.ExceptionLog("OnStartPrint Windows Catch Event is null", false);
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex, false);
                //System.Windows.Forms.MessageBox.Show(ex.ToString());
                ex = null;
                IsErrors = true;
                if (e != null)
                {
                    e.Cancel = true;
                }
                if (e != null)
                {
                    e.Cancel = true;
                }
                else
                {
                    gloAuditTrail.gloAuditTrail.ExceptionLog("OnStartPrint Catch Event is null", false);
                }

            }
        }

        public override Graphics OnStartPage(PrintDocument document, PrintPageEventArgs e)
        {
            Graphics o = null;
            if (IsCancel || IsErrors)
            {
                if (e != null)
                {
                    e.Cancel = true;
                }
                else
                {
                    gloAuditTrail.gloAuditTrail.ExceptionLog("OnStartPage Event is null", false);
                }

            }
            try
            {
                if (document != null && e != null)
                {
                    o = base.OnStartPage(document, e);
                }
                else
                {
                    gloAuditTrail.gloAuditTrail.ExceptionLog("OnStartPage base Event" + (document == null ? " Document is Null" : "") + (e == null ? " Event is Null" : ""), false);
                }

            }
            catch (System.ComponentModel.Win32Exception winEx)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(winEx, false);
                if (winEx.NativeErrorCode == 0x3F)
                {
                    System.Windows.Forms.MessageBox.Show("Your file waiting to be printed was deleted.");
                }
                //else
                //{
                //    System.Windows.Forms.MessageBox.Show(winEx.ToString());
                //}
                winEx = null;
                IsErrors = true;
                if (e != null)
                {
                    e.HasMorePages = false;
                    e.Cancel = true;
                }
                else
                {
                    gloAuditTrail.gloAuditTrail.ExceptionLog("OnStartPage Windows Catch Event is null", false);
                }


            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex, false);
                //System.Windows.Forms.MessageBox.Show(ex.ToString());
                ex = null;
                IsErrors = true; e.HasMorePages = false;
                if (e != null)
                {
                    e.HasMorePages = false;
                    e.Cancel = true;
                }
                else
                {
                    gloAuditTrail.gloAuditTrail.ExceptionLog("OnStartPage Catch Event is null", false);
                }

            }
            return o;
        }

        public override void OnEndPage(PrintDocument document, PrintPageEventArgs e)
        {
            if (IsCancel || IsErrors)
            {
                if (e != null)
                {
                    e.Cancel = true;
                }
                else
                {
                    gloAuditTrail.gloAuditTrail.ExceptionLog("OnEndPage Event is null", false);
                }


            }
            else
            {
                try
                {
                    if (document != null && e != null)
                    {
                        base.OnEndPage(document, e);
                    }
                    else
                    {
                        gloAuditTrail.gloAuditTrail.ExceptionLog("OnEndPage base Event" + (document == null ? " Document is Null" : "") + (e == null ? " Event is Null" : ""), false);
                    }

                }
                catch (System.ComponentModel.Win32Exception winEx)
                {
                    gloAuditTrail.gloAuditTrail.ExceptionLog(winEx, false);
                    if (winEx.NativeErrorCode == 0x3F)
                    {
                        System.Windows.Forms.MessageBox.Show("Your file waiting to be printed was deleted.");
                    }
                    //else
                    //{
                    //    System.Windows.Forms.MessageBox.Show("Some Problem with Printer File Writing, Check Exception Log for more information");
                    //}
                    winEx = null;
                    IsErrors = true;
                    if (e != null)
                    {
                        e.HasMorePages = false;
                        e.Cancel = true;
                    }
                    else
                    {
                        gloAuditTrail.gloAuditTrail.ExceptionLog("OnEndPage Windows Catch Event is null", false);
                    }

                }
                catch (Exception ex)
                {
                    gloAuditTrail.gloAuditTrail.ExceptionLog(ex, false);
                    //System.Windows.Forms.MessageBox.Show(ex.ToString());
                    ex = null;
                    IsErrors = true;
                    if (e != null)
                    {
                        e.HasMorePages = false;
                        e.Cancel = true;
                    }
                    else
                    {
                        gloAuditTrail.gloAuditTrail.ExceptionLog("OnEndPage Catch Event is null", false);
                    }

                }
            }

        }

        public override void OnEndPrint(PrintDocument document, PrintEventArgs e)
        {
            if (IsCancel || IsErrors)
            {
                if (e != null)
                {
                    e.Cancel = true;
                }
                else
                {
                    gloAuditTrail.gloAuditTrail.ExceptionLog("OnEndPrint Event is null", false);
                }

            }
            else
            {
                try
                {
                    if (document != null && e != null)
                    {
                        base.OnEndPrint(document, e);
                    }
                    else
                    {
                        gloAuditTrail.gloAuditTrail.ExceptionLog("OnEndPrint base Event" + (document == null ? " Document is Null" : "") + (e == null ? " Event is Null" : ""), false);
                    }

                }
                catch (System.ComponentModel.Win32Exception winEx)
                {
                    gloAuditTrail.gloAuditTrail.ExceptionLog(winEx, false);
                    if (winEx.NativeErrorCode == 0x3F)
                    {
                        System.Windows.Forms.MessageBox.Show("Your file waiting to be printed was deleted.");
                    }
                    winEx = null;
                    IsErrors = true;
                    if (e != null)
                    {
                        e.Cancel = true;
                    }
                    else
                    {
                        gloAuditTrail.gloAuditTrail.ExceptionLog("OnEndPrint Windows Catch Event is null", false);
                    }

                }
                catch (Exception ex)
                {
                    gloAuditTrail.gloAuditTrail.ExceptionLog(ex, false);
                    //System.Windows.Forms.MessageBox.Show(ex.ToString());
                    ex = null;
                    IsErrors = true;
                    if (e != null)
                    {
                        e.Cancel = true;
                    }
                    else
                    {
                        gloAuditTrail.gloAuditTrail.ExceptionLog("OnEndPrint Catch Event is null", false);
                    }
                }
            }

        }
    }


}
