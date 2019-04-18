using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using C1.Win.C1FlexGrid;

namespace gloGlobal.Common
{
    public class TriarqFormWithFocusListner : Form
    {
        private Control _SelectedControl = null;
        private Control _FormDefaultControl = null;
        private int gridRowSel = 0;
        private int gridColSel = 0;
        //private string callingformname = string.Empty;
        private Form.ControlCollection callingfromcontrol = null;

        public Control SelectedControl
        {
            get { return _SelectedControl; }
            set { _SelectedControl = value; }
        }

        public Control FormDefaultControl
        {
            get { return _FormDefaultControl; }
            set { _FormDefaultControl = value; }
        }

        public Form.ControlCollection CallingFormControl
        { get { return callingfromcontrol; } }

        //public string CallingFormName
        //{ get { return callingformname; } }


        public TriarqFormWithFocusListner()
        {
        }

        public void AddGotFocusListener(Control ctrl)
        {
            try
            {
                if (callingfromcontrol == null)
                {
                    callingfromcontrol = (Form.ControlCollection)ctrl.Controls;
                }
                foreach (Control c in ctrl.Controls)
                {
                    if (c.GetType() == typeof(C1.Win.C1FlexGrid.C1FlexGrid) ||
                        c.GetType() == typeof(TextBox ) ||
                        c.GetType() == typeof(ComboBox) ||
                        c.GetType() == typeof(DateTimePicker) ||
                        c.GetType() == typeof(RichTextBox ) ||
                        c.GetType() == typeof(MaskedTextBox) ||
                        c.GetType() == typeof(CheckBox)||
                        c.GetType() == typeof(DataGrid))
                    {
                        c.GotFocus += new EventHandler(Control_GotFocus);
                    }
                        if (c.Controls.Count > 0)
                        {
                            AddGotFocusListener(c);
                        }
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
        }

        public void RemoveGotFocusListener(Control ctrl)
        {
            try
            {
                foreach (Control c in ctrl.Controls)
                {
                    if (c.GetType() == typeof(C1.Win.C1FlexGrid.C1FlexGrid) ||
                        c.GetType() == typeof(TextBox) ||
                        c.GetType() == typeof(ComboBox) ||
                        c.GetType() == typeof(DateTimePicker) ||
                        c.GetType() == typeof(RichTextBox) ||
                        c.GetType() == typeof(MaskedTextBox) ||
                        c.GetType() == typeof(CheckBox) ||
                        c.GetType() == typeof(DataGrid))
                    {
                        c.GotFocus -= new EventHandler(Control_GotFocus);
                    }
                    if (c.Controls.Count > 0)
                    {
                        RemoveGotFocusListener(c);
                    }
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
        }

        public virtual void Control_GotFocus(object obj, EventArgs e)
        {
            // Set focused control here
            Control oCntrl = null;
            try
            {
                oCntrl = (Control)obj;
                if (oCntrl.GetType() == typeof(C1.Win.C1FlexGrid.C1FlexGrid) ||
                    oCntrl.GetType() == typeof(TextBox) ||
                    oCntrl.GetType() == typeof(ComboBox) ||
                    oCntrl.GetType() == typeof(DateTimePicker) ||
                    oCntrl.GetType() == typeof(RichTextBox) ||
                    oCntrl.GetType() == typeof(MaskedTextBox) ||
                    oCntrl.GetType() == typeof(CheckBox) ||
                    oCntrl.GetType() == typeof(DataGrid))
                {
                    _SelectedControl = oCntrl;
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                oCntrl = null;
            }
        }

        public virtual void GetControlSelection()
        {
            Control[] oSeleced = null;
            C1FlexGrid oFlex = null;
            try
            {
                if ((SelectedControl == null) || (String.IsNullOrEmpty(SelectedControl.Name)))
                {
                    return;
                }
                oSeleced = callingfromcontrol.Find(SelectedControl.Name, true);
                if (oSeleced.Length > 0)
                {
                    if (SelectedControl.GetType() == typeof(C1.Win.C1FlexGrid.C1FlexGrid))
                    {
                        oFlex = (C1FlexGrid)oSeleced[0];
                        gridRowSel = oFlex.RowSel;
                        gridColSel = oFlex.ColSel;
                    }
                    else
                    {
                        gridRowSel = 0;
                        gridColSel = 0;
                    }
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                oSeleced = null;
                oFlex = null;
            }
        }

        public virtual void SetControlSelection()
        {
            Control[] oSeleced = null;
            C1FlexGrid oFlex = null;

            try
            {
                if ((SelectedControl == null) || (String.IsNullOrEmpty(SelectedControl.Name)))
                {
                    return;
                }
                oSeleced = callingfromcontrol.Find(SelectedControl.Name, true);
                if (oSeleced.Length > 0)
                {
                    oSeleced[0].Focus();
                    //START:To Select Flexgrid Row
                    if (SelectedControl.GetType() == typeof(C1.Win.C1FlexGrid.C1FlexGrid))
                    {
                        oFlex = (C1FlexGrid)oSeleced[0];
                        if (oFlex.Rows.Count <= 1)
                        {
                            if (FormDefaultControl != null)
                            {
                                FormDefaultControl.Focus();
                            }
                        }
                        else
                        {
                            if (gridRowSel == 0 && gridColSel == 0)
                            {
                                oFlex.Select(oFlex.RowSel, oFlex.ColSel, true);
                            }
                            else
                            {
                                if (oFlex.Rows.Count > gridRowSel)
                                {
                                    oFlex.Select(gridRowSel, gridColSel, true);
                                }
                            }
                        }
                    }
                    //END
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                oSeleced = null;
                oFlex = null;
            }
        }


        public virtual void SetControlSelection_EMR( )
        {
            Control[] oSeleced = null;
            C1FlexGrid oFlex = null;

            try
            {
                //if (_IsFormClosing == false)
                //{
                //    return;
                //}
                if (gridRowSel == 0)
                {
                    return;
                }
                if ((SelectedControl == null) || (String.IsNullOrEmpty(SelectedControl.Name)))
                {
                    return;
                }
                oSeleced = callingfromcontrol.Find(SelectedControl.Name, true);
                if (oSeleced.Length > 0)
                {
                    oSeleced[0].Focus();
                    //START:To Select Flexgrid Row
                    if (SelectedControl.GetType() == typeof(C1.Win.C1FlexGrid.C1FlexGrid))
                    {
                        oFlex = (C1FlexGrid)oSeleced[0];
                        if (oFlex.Rows.Count <= 1)
                        {
                            if (FormDefaultControl != null)
                            {
                                FormDefaultControl.Focus();
                            }
                        }
                        else
                        {
                            if (gridRowSel == 0 && gridColSel == 0)
                            {
                                oFlex.Select(oFlex.RowSel, oFlex.ColSel, true);
                                //if (_isDashboardMenu == false)
                                //{
                                gridRowSel = 0;
                                gridColSel = 0;
                                // }
                            }
                            else
                            {
                                if (oFlex.Rows.Count > gridRowSel)
                                {
                                    oFlex.Select(gridRowSel, gridColSel, true);
                                    //if (_isDashboardMenu == false)
                                    //{
                                    gridRowSel = 0;
                                    gridColSel = 0;
                                    // }
                                }
                            }
                        }
                    }
                    //END
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                oSeleced = null;
                oFlex = null;
            }
        }
    }
}
