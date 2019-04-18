using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DMSImport;
using System.IO;

namespace DMSImport
{
    public partial class DMSImportReport : Form
    {

       // bool bIsV1toV3;
        //To Take the File Path..
        string _FilePath = "";
       

        public string sFilePath
        {
            get { return _FilePath; }
            set { _FilePath = value; }
        }
                       
        public DMSImportReport()
        {
           // bIsV1toV3 = blnIsV1toV3;
            InitializeComponent();
        }

        private void DMSImportReport_Load(object sender, System.EventArgs e) 
        {


            //setting of the Scrollbar == true..
            pnlGrid.HorizontalScroll.Enabled = true; 
            pnlGrid.VerticalScroll.Enabled = true;
            _FilePath = System.Windows.Forms.Application.StartupPath + "\\DocumentExportLog.csv";

            

            //Nos of the Rows and columns to check..
            C1Flex.Cols.Count = 5; 
            C1Flex.Cols.Fixed = 0; 
            C1Flex.Rows.Count = 1; 
            
            
             
            //Setting of the Header 
            C1Flex.SetData(0, 0, "Date");
            C1Flex.SetData(0, 1, "Document Name");
            C1Flex.SetData(0, 2, "Document Path");
            C1Flex.SetData(0, 3, "Result");
            C1Flex.SetData(0, 4, "Comment");
            
            //Setting of the Width..
            int nWidth = 0; 
            nWidth = pnlFill.Width; 
            { 
               C1Flex.ExtendLastCol = true; 
               C1Flex.Cols[0].Width =Convert.ToInt32( nWidth * 0.14);
                C1Flex.Cols[1].Width = Convert.ToInt32(nWidth * 0.14); 
                C1Flex.Cols[2].Width =Convert.ToInt32( nWidth * 0.14);
                C1Flex.Cols[3].Width = Convert.ToInt32(nWidth * 0.14); 
                C1Flex.Cols[4].Width = Convert.ToInt32(nWidth * 0.07);
                
                
                C1Flex.Cols[0].Sort = C1.Win.C1FlexGrid.SortFlags.Ascending; 
                //'.Cols(4).Sort = C1.Win.C1FlexGrid.SortFlags.Ascending 
                
                C1Flex.AllowEditing = false; 
            } 
            
            //if (File.Exists(_FilePath)== true )
            //C1Flex.LoadGrid(_FilePath, C1.Win.C1FlexGrid.FileFormatEnum.TextComma, C1.Win.C1FlexGrid.FileFlags.AsDisplayed, System.Text.Encoding.Default); 
            
            
            //Loading the Grid...
            LoadGrid();

            System.Windows.Forms.Application.DoEvents(); 
            
            lblColumName.Text = "Date: "; 
        }
            
        private void C1Flex_AfterSort(object sender, C1.Win.C1FlexGrid.SortColEventArgs e) 
        {
            //Sorting according to the flexgrid column heading...
            lblColumName.Text =Convert.ToString( C1Flex.GetData(0, e.Col)) + ": "; 
            C1Flex.Col = e.Col; 
            txtvalue.Text = ""; 
            
        } 

        private void txtvalue_TextChanged(object sender, System.EventArgs e) 
        { 
            //used to Find the rows from the *.csv that has been loaded..
            C1Flex.Row = C1Flex.FindRow(txtvalue.Text, 1, C1Flex.Col, false, false, false); 
        } 
             
        private void  tlpRefresh_Click(object sender, System.EventArgs e) 
        {
            //To Reload the Grid...
            LoadGrid();
            C1Flex.Refresh();

            //It will set the selected node to the first location On Refresh
            C1Flex.Row = C1Flex.FindRow(txtvalue.Text , 1, C1Flex.Col, false, false, false);
           
            
            

        } 

        private void  tlpClose_Click(object sender, System.EventArgs e) 
        { 
            //it is used to Close
            this.Close(); 
        }

        private void LoadGrid()
        {
            try
            {
                txtvalue.Text = "";
                //Setting the FilePath..
                _FilePath = System.Windows.Forms.Application.StartupPath + "\\DocumentExportLog.csv";
                //checking weather the file Exist or not
                if (File.Exists(_FilePath) == true)
                {
                    C1Flex.LoadGrid(_FilePath, C1.Win.C1FlexGrid.FileFormatEnum.TextComma, C1.Win.C1FlexGrid.FileFlags.AsDisplayed, System.Text.Encoding.Default);
                }
            
                else
                {
                    C1Flex.Rows.Count = 1;
                }

                C1Flex.Cols[0].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                C1Flex.Cols[1].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                C1Flex.Cols[2].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                C1Flex.Cols[3].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                C1Flex.Cols[4].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR : " + ex.Message,gloEDocumentV3.gloEDocV3Admin.gMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;

            }
             
        }

       
              
    }//class
}

