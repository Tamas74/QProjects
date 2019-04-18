using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using gloPatient.Classes;

namespace gloPM.Classes
{
    class gloDataGridViewStyle : DataGridView
    {
        public static void Style(DataGridView DataGrid)
        {
            //AlternatingRowsDefaultCellStyle
            DataGrid.AlternatingRowsDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(222, 231, 250);
            DataGrid.AlternatingRowsDefaultCellStyle.Font = gloGlobal.clsgloFont.gFont_SMALL; //new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular);
            DataGrid.AlternatingRowsDefaultCellStyle.ForeColor = Color.FromArgb(31, 75, 125);
            DataGrid.AlternatingRowsDefaultCellStyle.SelectionBackColor = System.Drawing.Color.FromArgb(254, 207, 102);
            DataGrid.AlternatingRowsDefaultCellStyle.SelectionForeColor = Color.Black;
            

            // AutoSizeColumnsMode
            DataGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            // Background Color
            DataGrid.BackgroundColor = Color.White;

            // Border Style
            DataGrid.BorderStyle = BorderStyle.None;

            // ColumnHeaderDefaultCellStyle
            DataGrid.ColumnHeadersDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(86, 126, 211);
            DataGrid.ColumnHeadersDefaultCellStyle.Font = gloGlobal.clsgloFont.gFont_SMALL_BOLD ;//new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            DataGrid.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            DataGrid.ColumnHeadersDefaultCellStyle.SelectionBackColor = System.Drawing.Color.FromArgb(254, 207, 102);
            DataGrid.ColumnHeadersDefaultCellStyle.SelectionForeColor = Color.Black;

            // ColumnHeaderHeightSizeMode
            DataGrid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;

            DataGrid.ColumnHeadersHeight = 22;
            DataGrid.RowTemplate.Height = 18;

            // DefaultCellStyle
            DataGrid.DefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(240, 247, 255);
            DataGrid.DefaultCellStyle.Font = gloGlobal.clsgloFont.gFont_SMALL ;//new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular);
            DataGrid.DefaultCellStyle.ForeColor = Color.FromArgb(31, 75, 125);
            DataGrid.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.FromArgb(254, 207, 102);
            DataGrid.DefaultCellStyle.SelectionForeColor = Color.Black;

            // EnableHeaderVisualStyles
            DataGrid.EnableHeadersVisualStyles = false;

            // Grid Color
            DataGrid.GridColor = System.Drawing.Color.FromArgb(159, 181, 221);

            // RowHeaderVisible
            DataGrid.RowHeadersVisible = false;

            //RowsDefaultCellStyle
            DataGrid.RowsDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(240, 247, 255);
            DataGrid.RowsDefaultCellStyle.Font = gloGlobal.clsgloFont.gFont_SMALL ;//new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular);
            DataGrid.RowsDefaultCellStyle.ForeColor = Color.FromArgb(31, 75, 125);
            DataGrid.RowsDefaultCellStyle.SelectionBackColor = System.Drawing.Color.FromArgb(254, 207, 102);
            DataGrid.RowsDefaultCellStyle.SelectionForeColor = Color.Black;

            //Selection Mode
            DataGrid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

        }

        public static void DashBoardStyle(DataGridView DataGrid)
        {
            //AlternatingRowsDefaultCellStyle
            DataGrid.AlternatingRowsDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(222, 231, 250);
            DataGrid.AlternatingRowsDefaultCellStyle.Font = gloGlobal.clsgloFont.gFont_SMALL ;//new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular);
            DataGrid.AlternatingRowsDefaultCellStyle.ForeColor = Color.FromArgb(31, 75, 125);
            DataGrid.AlternatingRowsDefaultCellStyle.SelectionBackColor = System.Drawing.Color.FromArgb(254, 207, 102);
            DataGrid.AlternatingRowsDefaultCellStyle.SelectionForeColor = Color.Black;

            // AutoSizeColumnsMode
            DataGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            // Background Color
            DataGrid.BackgroundColor = Color.White;

            // Border Style
            DataGrid.BorderStyle = BorderStyle.None;

            // ColumnHeaderDefaultCellStyle
            DataGrid.ColumnHeadersDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(86, 126, 211);
            DataGrid.ColumnHeadersDefaultCellStyle.Font = gloGlobal.clsgloFont.gFont_SMALL_BOLD ;//new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            DataGrid.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            DataGrid.ColumnHeadersDefaultCellStyle.SelectionBackColor = System.Drawing.Color.FromArgb(254, 207, 102);
            DataGrid.ColumnHeadersDefaultCellStyle.SelectionForeColor = Color.Black;

            // ColumnHeaderHeightSizeMode
            DataGrid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;

            DataGrid.ColumnHeadersHeight = 22;
            DataGrid.RowTemplate.Height = 20;

            // DefaultCellStyle
            DataGrid.DefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(240, 247, 255);
            DataGrid.DefaultCellStyle.Font = gloGlobal.clsgloFont.gFont_SMALL ;//new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular);
            DataGrid.DefaultCellStyle.ForeColor = Color.FromArgb(31, 75, 125);
            DataGrid.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.FromArgb(254, 207, 102);
            DataGrid.DefaultCellStyle.SelectionForeColor = Color.Black;

            // EnableHeaderVisualStyles
            DataGrid.EnableHeadersVisualStyles = false;

            // Grid Color
            DataGrid.GridColor = System.Drawing.Color.FromArgb(159, 181, 221);

            // RowHeaderVisible
            DataGrid.RowHeadersVisible = false;

            //RowsDefaultCellStyle
            DataGrid.RowsDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(240, 247, 255);
            DataGrid.RowsDefaultCellStyle.Font = gloGlobal.clsgloFont.gFont_SMALL ;//new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular);
            DataGrid.RowsDefaultCellStyle.ForeColor = Color.FromArgb(31, 75, 125);
            DataGrid.RowsDefaultCellStyle.SelectionBackColor = System.Drawing.Color.FromArgb(254, 207, 102);
            DataGrid.RowsDefaultCellStyle.SelectionForeColor = Color.Black;

            //Selection Mode
            DataGrid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

           
        }
    }
}
