using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Windows.Forms;
using gloEMRGeneralLibrary;
using System.IO;

namespace gloPMGeneral
{
   public class clsSplitScreenPM : IDisposable
    {   

       public object clsDMS {get; set; }
              
       public Form ParentFrm { get; set; }  

       public string _DatabaseConnectionString = "";

       Janus.Windows.UI.Dock.UIPanelManager UiSplitScreenPanelManager = null;
       Janus.Windows.UI.Dock.UIPanelGroup uiPanSplitScreen = null;
       gloUC_PastWordNotes_SplitControl pwnSplitDMS;
       Janus.Windows.UI.Dock.UIPanelInnerContainer contDMS = null;
       Janus.Windows.UI.Dock.UIPanel uiPanDMS = null;
     
       public clsSplitScreenPM()
       {           
           UiSplitScreenPanelManager = new Janus.Windows.UI.Dock.UIPanelManager();
           uiPanSplitScreen = new Janus.Windows.UI.Dock.UIPanelGroup();
           contDMS = new Janus.Windows.UI.Dock.UIPanelInnerContainer();
           uiPanDMS = new Janus.Windows.UI.Dock.UIPanel();
        
          
       }

       public Janus.Windows.UI.Dock.UIPanelGroup LoadSplitControl(Form objfrm,long m_PatientID, long nVisitId, String fromForm, long _clinicId, long _loginId) 
        {
           
            try
            {
                ParentFrm = objfrm;

               
                UiSplitScreenPanelManager.ContainerControl = ParentFrm;
                UiSplitScreenPanelManager.DefaultPanelSettings.CaptionFormatStyle.Font = gloGlobal.clsgloFont.gFont_BOLD;
                UiSplitScreenPanelManager.DefaultPanelSettings.CaptionStyle = Janus.Windows.UI.Dock.PanelCaptionStyle.Dark;
                //UiSplitScreenPanelManager.LargeImageList = Me.ImgLarge
                UiSplitScreenPanelManager.TabStripFormatStyle.Font = gloGlobal.clsgloFont.gFont_BOLD;
                UiSplitScreenPanelManager.VisualStyle = Janus.Windows.UI.Dock.PanelVisualStyle.Office2007;
                UiSplitScreenPanelManager.DockingStyleMode = Janus.Windows.UI.Dock.DockingStyleMode.Standard;


                uiPanSplitScreen.Id = new System.Guid("cd93dadf-3067-4964-b42a-50d4cf93e3cf");

                if (!LoadPrevLayout(fromForm, _clinicId, _loginId, ref uiPanDMS)) //If Lay out is present 
                {

                    UiSplitScreenPanelManager.ContainerControl = ParentFrm;
                    uiPanSplitScreen.CloseButtonVisible = Janus.Windows.UI.InheritableBoolean.False;
                    uiPanSplitScreen.GroupStyle = Janus.Windows.UI.Dock.PanelGroupStyle.VerticalTiles;
                    uiPanSplitScreen.Location = new System.Drawing.Point(3, 33);
                    uiPanSplitScreen.Name = "uiPanSplitScreen";
                    uiPanSplitScreen.SelectedPanel = uiPanDMS;
                    uiPanSplitScreen.Size = new System.Drawing.Size(295, 572);
                    uiPanSplitScreen.TabIndex = 1;
                    uiPanSplitScreen.Text = "View Documents";
                    uiPanSplitScreen.TabStateStyles.DisabledFormatStyle.Font = gloGlobal.clsgloFont.gFont;
                    uiPanSplitScreen.TabStateStyles.FormatStyle.Font = gloGlobal.clsgloFont.gFont;
                    uiPanSplitScreen.TabStateStyles.HotFormatStyle.Font = gloGlobal.clsgloFont.gFont;
                    uiPanSplitScreen.TabStateStyles.PressedFormatStyle.Font = gloGlobal.clsgloFont.gFont_BOLD;
                    uiPanSplitScreen.TabStateStyles.SelectedFormatStyle.Font = gloGlobal.clsgloFont.gFont_BOLD;
                    uiPanSplitScreen.StaticGroup = true;
                    uiPanSplitScreen.AutoHide = true;

                    //contDMS//
                    contDMS.Location = new System.Drawing.Point(1, 24);
                    contDMS.Name = "contDMS";
                    contDMS.Size = new System.Drawing.Size(289, 254);
                    contDMS.TabIndex = 4;

                    //uiPanDMS//

                    uiPanDMS.CaptionFormatStyle.Font = gloGlobal.clsgloFont.gFont_BOLD;
                    uiPanDMS.CaptionStyle = Janus.Windows.UI.Dock.PanelCaptionStyle.Dark;
                    uiPanDMS.Font = gloGlobal.clsgloFont.gFont_BOLD;
                    uiPanDMS.InnerContainer = contDMS;
                    uiPanDMS.Image = gloPMGeneral.Properties.Resources.ViewDoc16;
                    uiPanDMS.LargeImage = gloPMGeneral.Properties.Resources.ViewDoc24;
                    uiPanDMS.Location = new System.Drawing.Point(4, 0);
                    uiPanDMS.Name = "uiPanDMS";
                    uiPanDMS.Size = new System.Drawing.Size(291, 278);
                    uiPanDMS.TabIndex = 1;
                    uiPanDMS.Text = "View Documents";
                    uiPanDMS.CaptionVisible = Janus.Windows.UI.InheritableBoolean.False;
                    uiPanDMS.CloseButtonVisible = Janus.Windows.UI.InheritableBoolean.False;
                    uiPanDMS.TabStateStyles.DisabledFormatStyle.Font = gloGlobal.clsgloFont.gFont_BOLD;
                    uiPanDMS.TabStateStyles.FormatStyle.Font = gloGlobal.clsgloFont.gFont_BOLD;
                    uiPanDMS.TabStateStyles.HotFormatStyle.Font = gloGlobal.clsgloFont.gFont_BOLD;
                    uiPanDMS.TabStateStyles.PressedFormatStyle.Font = gloGlobal.clsgloFont.gFont_BOLD;
                    uiPanDMS.TabStateStyles.SelectedFormatStyle.Font = gloGlobal.clsgloFont.gFont_BOLD;
                    uiPanSplitScreen.Panels.Add(uiPanDMS);

                }
                else
                {
                    if (uiPanSplitScreen.DockState == Janus.Windows.UI.Dock.PanelDockState.Docked && uiPanSplitScreen.DockStyle == Janus.Windows.UI.Dock.PanelDockStyle.Fill)
                    {
                        contDMS.Size = new System.Drawing.Size(289, 254);
                        uiPanDMS.Size = new System.Drawing.Size(291, 278);
                        uiPanSplitScreen.Size = new System.Drawing.Size(295, 572);
                        uiPanSplitScreen.DockStyle = Janus.Windows.UI.Dock.PanelDockStyle.Left;
                        uiPanSplitScreen.AutoHide = true;
                    }
                }
               

                FillDocuments(m_PatientID,_clinicId);

                uiPanSplitScreen.SelectedPanel = uiPanDMS;

                UiSplitScreenPanelManager.Panels.Add(uiPanSplitScreen);

                //Register Control For Events
                //UiSplitScreenPanelManager.CurrentLayoutChanging += new System.ComponentModel.CancelEventHandler(UiSplitScreenPanelManager_CurrentLayoutChanging);
                //uiPanSplitScreen.DockChanged += new EventHandler(uiPanSplitScreen_DockChanged);
                //uiPanSplitScreen.Click += new EventHandler(uiPanSplitScreen_Click);
                //uiPanSplitScreen.ParentChanged += new EventHandler(uiPanSplitScreen_ParentChanged);

                uiPanSplitScreen.BringToFront();
                return uiPanSplitScreen;
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                
            }


           
        }

       private bool LoadPrevLayout(String fromForm, long _clinicId, long _loginId, ref Janus.Windows.UI.Dock.UIPanel uiPanDMS)
       {
           bool _bRsult = false;
           DataTable dtLastLayout = null;
           try
           {
               dtLastLayout = RetriveLastUIPanelLayout(_loginId, fromForm);

               if (dtLastLayout != null && dtLastLayout.Rows.Count > 0) //If Lay out is present 
               {
                   Byte[] oBytesArry = (Byte[])dtLastLayout.Rows[0]["iStyle"];
                   MemoryStream memStream = new MemoryStream(oBytesArry);
                   UiSplitScreenPanelManager.LoadLayoutFile(memStream);
                   memStream.Dispose();
                   memStream = null;

                   UiSplitScreenPanelManager.ContainerControl = ParentFrm;
                   uiPanSplitScreen = (Janus.Windows.UI.Dock.UIPanelGroup)UiSplitScreenPanelManager.Panels[0];
                   uiPanDMS = (Janus.Windows.UI.Dock.UIPanel)uiPanSplitScreen.Panels[0];
                   uiPanDMS.InnerContainer = contDMS;
                   _bRsult = true;
                   //if (uiPanSplitScreen.DockStyle == Janus.Windows.UI.Dock.PanelDockStyle.Fill)
                   //{
                   //    uiPanSplitScreen.DockStyle = Janus.Windows.UI.Dock.PanelDockStyle.Left;
                   //  //  uiPanSplitScreen.AutoHide = false;
                   //}
                   //uiPanSplitScreen.BringToFront();
               }
               else
               {
                   _bRsult = false;
               }
           }
           catch (Exception)
           {
               _bRsult = false;
           }
           finally
           {
               if (dtLastLayout != null)
               {
                   dtLastLayout.Dispose();
                   dtLastLayout = null;
               }
           }
           return _bRsult;
       }

       public void FillDocuments(long m_PatientID, long _clinicId)
       {
           try
           {
               pwnSplitDMS = new gloUC_PastWordNotes_SplitControl(m_PatientID, 0, "DMS", null, null, clsDMS, _clinicId, false);
               pwnSplitDMS.Dock = DockStyle.Fill;

               if (contDMS != null && contDMS.Controls != null) { contDMS.Controls.Clear(); }
               contDMS.Controls.Add(pwnSplitDMS);

           }
           catch (Exception ex)
           {
               throw ex;
           }
           finally
           { 
           
           }
       }

       private DataTable RetriveLastUIPanelLayout(long _loginId, string sModuleName)
        {
            DataTable _bResult = null;
            gloDatabaseLayer.DBLayer objDBLayer = null;
            gloDatabaseLayer.DBParameters objDBParameters = null;
            try
            {
                objDBParameters = new gloDatabaseLayer.DBParameters();
                objDBParameters.Add("@nUserID", _loginId, ParameterDirection.Input, SqlDbType.BigInt);
                objDBParameters.Add("@sMachineName", System.Windows.Forms.SystemInformation.ComputerName, ParameterDirection.Input, SqlDbType.VarChar);
                objDBParameters.Add("@sModuleName", sModuleName, ParameterDirection.Input, SqlDbType.VarChar);
                objDBLayer = new gloDatabaseLayer.DBLayer(_DatabaseConnectionString);                
                objDBLayer.Connect(false);
                objDBLayer.Retrive("INUP_SplitScreenDisplaySettings", objDBParameters, out _bResult);
                objDBLayer.Disconnect();
            }
            catch (Exception ex)
            {               
                throw ex;
            }
            finally
            {
                if (objDBLayer != null)
                {
                    objDBLayer.Dispose();
                    objDBLayer = null;
                }
                if (objDBParameters != null)
                {

                    objDBParameters.Dispose();
                    objDBParameters = null;
                }

            }
            return _bResult;
        }

       public void SaveControlDisplaySettings(long _UserId, string sModuleName)
        {
            MemoryStream memStream=null;
            gloDatabaseLayer.DBLayer ODBLayer = null;
            gloDatabaseLayer.DBParameters oDBParameters = null;
            try
            {
             
                if (UiSplitScreenPanelManager == null)
                    return;
                memStream = new MemoryStream();

                UiSplitScreenPanelManager.SaveLayoutFile(memStream);
                oDBParameters = new gloDatabaseLayer.DBParameters();
                oDBParameters.Add("@nUserID", _UserId, ParameterDirection.Input, SqlDbType.BigInt);
                oDBParameters.Add("@sMachineName", System.Windows.Forms.SystemInformation.ComputerName, ParameterDirection.Input, SqlDbType.VarChar);
                oDBParameters.Add("@sModuleName", sModuleName, ParameterDirection.Input, SqlDbType.VarChar);
                oDBParameters.Add("@iStyle", memStream.ToArray(), ParameterDirection.Input, SqlDbType.Image);
                ODBLayer = new gloDatabaseLayer.DBLayer(_DatabaseConnectionString);
                ODBLayer.Connect(false);
                ODBLayer.Execute("INUP_SplitScreenDisplaySettings", oDBParameters);
                ODBLayer.Disconnect();                
                memStream.Close();
            }
            catch (Exception ex)
            {
               gloAuditTrail.gloAuditTrail.ActivityLog("Error while Saving Split control Layout Error:" + ex.ToString());
               throw ex;
            }
            finally
            {
                if (memStream != null)
                {
                    memStream.Dispose();
                    memStream = null;
                }
                if (ODBLayer != null)
                {
                    ODBLayer.Disconnect();
                    ODBLayer.Dispose();
                    ODBLayer = null;
                }
                if (oDBParameters != null)
                {
                    oDBParameters.Dispose();
                    oDBParameters = null;
                }

            }

        }

       private void UiSplitScreenPanelManager_CurrentLayoutChanging(Object sender, System.ComponentModel.CancelEventArgs e)
       {
           try
           {
               if (UiSplitScreenPanelManager.CurrentLayout != null)
                   UiSplitScreenPanelManager.CurrentLayout.Update();
               
           }
           catch (Exception)
           {}
           
       }

       private void uiPanSplitScreen_DockChanged(Object sender, System.EventArgs e)
       {
           try
           {
               if (uiPanSplitScreen.DockStyle == Janus.Windows.UI.Dock.PanelDockStyle.Fill)               
                   uiPanSplitScreen.AutoHide = false;                
           }
           catch (Exception)
           {}
         
       }

       private void uiPanSplitScreen_Click(Object sender, System.EventArgs e)
       {

       }

       private void uiPanSplitScreen_ParentChanged(Object sender, System.EventArgs e)
        {


        }

        #region"Dispose"

        private bool disposed = false;
        public void Dispose()
        {
            
            if (UiSplitScreenPanelManager != null)
            {
                try{UiSplitScreenPanelManager.Panels.Clear();}catch (Exception){}
                
                UiSplitScreenPanelManager.Dispose();
                UiSplitScreenPanelManager = null;
            }

            if (uiPanSplitScreen != null) { uiPanSplitScreen.Dispose(); uiPanSplitScreen = null; }
            if (pwnSplitDMS != null) { pwnSplitDMS.Dispose(); pwnSplitDMS = null; }
            if (contDMS != null) { contDMS.Dispose(); contDMS = null; }
            if (uiPanDMS != null) { uiPanDMS.Dispose(); uiPanDMS = null; }

            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    try
                    {
                        gloGlobal.cEventHelper.RemoveAllEventHandlers(this);
                    }
                    catch
                    {
                    }
                    try
                    {
                        if (UiSplitScreenPanelManager != null)
                        {
                            UiSplitScreenPanelManager.Dispose();
                            UiSplitScreenPanelManager = null;
                        }
                    }
                    catch 
                    { 
                    }
                    try
                    {
                        if (uiPanSplitScreen != null)
                        {
                            uiPanSplitScreen.Dispose();
                            uiPanSplitScreen = null;
                        }
                    }
                    catch
                    {
                    }
                    try
                    {
                        if (pwnSplitDMS != null)
                        {
                            pwnSplitDMS.Dispose();
                            pwnSplitDMS = null;
                        }
                    }
                    catch
                    {
                    }
                    try
                    {
                        if (contDMS != null)
                        {
                            contDMS.Dispose();
                            contDMS = null;
                        }
                    }
                    catch
                    {
                    }
                    try
                    {
                        if (uiPanDMS != null)
                        {
                            uiPanDMS.Dispose();
                            uiPanDMS = null;
                        }
                    }
                    catch
                    {
                    }
                   
                }

            }
            disposed = true;
        }
              
        #endregion
    }
}
