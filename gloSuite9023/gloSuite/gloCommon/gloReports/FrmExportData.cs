using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace gloReports
{
    public partial class FrmExportData : Form
    {
        private bool bCurrentPage;
        public bool CurrentPage
        {
            get { return bCurrentPage; }
            set { bCurrentPage = value; }
        }

        private bool bSelectAll;
        public bool SelectAll
        {
            get { return bSelectAll; }
            set { bSelectAll = value; }
        }

        private string sPageRange;
        public string PageRange
        {
            get { return sPageRange; }
            set { sPageRange = value; }
        }

        private String sFilePath;
        public string FilePath
        {
            get { return sFilePath; }
            set { sFilePath = value; }
        }

        bool bexport = false;
        public bool Export
        {
            get { return bexport; }
            set { bexport = value; }
        }

        Int32 iTotalPages = 0;

        public FrmExportData(Int32 _ItotalPage)
        {
            InitializeComponent();
            iTotalPages = _ItotalPage;
            CurrentPage = true;
        }

        private void tblExport_Click(object sender, EventArgs e)
        {
            if (txtFilepath.Text.Trim() == string.Empty)
            {
                MessageBox.Show("Please select folder path to export data.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            #region "Check if Valid page no."
            if (rd_PageRange.Checked)
            {
                try
                {
                    String[] iRange = txtPageRange.Text.Split(',');
                    String[] strRange;
                    for (Int32 j = 0; j < iRange.Length; j++)
                    {
                        String sSplit = iRange[j].ToString();
                        if ((sSplit.Contains("-")))
                        {
                            strRange = iRange[j].Split('-');

                            if ((strRange[0] == string.Empty) || (strRange[1] == string.Empty) || (Convert.ToInt32(strRange[0]) > Convert.ToInt32(strRange[1]))
                                || (Convert.ToInt32(strRange[0]) == 0) || (Convert.ToInt32(strRange[1]) == 0))
                            {
                                MessageBox.Show("Please enter the valid page range.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                lblpageRange.ForeColor = Color.Red;
                                txtPageRange.Focus();
                                return;
                            }
                        }
                        else
                        {
                            if ((sSplit.Trim() == string.Empty) || (Convert.ToInt32(sSplit) > iTotalPages) || (Convert.ToInt32(sSplit) <= 0))
                            {
                                MessageBox.Show("Please enter the valid page range.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                lblpageRange.ForeColor = Color.Red;
                                txtPageRange.Focus();
                                return;
                            }
                        }
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("Invalid page range.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    lblpageRange.ForeColor = Color.Red;
                    txtPageRange.Focus();
                    return;
                }

            }
            #endregion "Check if Valid page no."

            bexport = true;
            if (rd_PageRange.Checked)
                PageRange = txtPageRange.Text.Trim();
            this.Close();

        }

        private void toolStripButton6_Click(object sender, EventArgs e)
        {
            bexport = false;
            this.Close();
        }

        private void rd_CurrentPage_CheckedChanged(object sender, EventArgs e)
        {
            if (rd_CurrentPage.Checked)
            {
                rd_PageRange.Checked = false;
                rd_SelectAll.Checked = false;
                txtPageRange.Visible = false;
                CurrentPage = true;
                SelectAll = false;
                PageRange = String.Empty;
                lblpageRange.Visible = false;
            }
        }

        private void rd_SelectAll_CheckedChanged(object sender, EventArgs e)
        {
            if (rd_SelectAll.Checked)
            {
                rd_PageRange.Checked = false;
                rd_CurrentPage.Checked = false;
                txtPageRange.Visible = false;
                CurrentPage = false;
                SelectAll = true;
                PageRange = String.Empty;
                lblpageRange.Visible = false;
            }
        }

        private void rd_PageRange_CheckedChanged(object sender, EventArgs e)
        {
            if (rd_PageRange.Checked)
            {
                txtPageRange.Text = string.Empty;
                rd_SelectAll.Checked = false;
                rd_CurrentPage.Checked = false;
                txtPageRange.Visible = true;
                lblpageRange.Visible = true;
                CurrentPage = false;
                SelectAll = false;
                txtPageRange.Focus();
                lblpageRange.ForeColor = Color.FromArgb(31, 73, 125);
            }
        }

        private void txtPageRange_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!((e.KeyChar >= Convert.ToChar(48) & e.KeyChar <= Convert.ToChar(57)) | e.KeyChar == Convert.ToChar(8) | e.KeyChar == ',' | e.KeyChar == '-'))
            {
                e.Handled = true;
            }
        }

        private void btnbrwfile_Click(object sender, EventArgs e)
        {
            #region "select file"

            FolderBrowserDialog op = new FolderBrowserDialog();//select the path where user want to store.
            op.ShowDialog(this);
            if ((op.SelectedPath == string.Empty) || (op.SelectedPath == null))
            {
                op.Dispose();
                op = null;
                return;
            }
            else
            {
                FilePath = System.IO.Path.Combine(op.SelectedPath, "InterfaceReport-" + gloGlobal.clsFileExtensions.GetUniqueDateString("yyyyMMddHHmmssffff") + ".xls");
                txtFilepath.Text = FilePath;
                op.Dispose();
                op = null;
            }
            try
            {
                if (System.IO.File.Exists(sFilePath))
                {
                    System.IO.File.Delete(sFilePath);
                }

            }
            catch (Exception)
            {
                MessageBox.Show("This file is currently used by another person. Please close file and retry.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            #endregion "select file"
        }
    }
}
