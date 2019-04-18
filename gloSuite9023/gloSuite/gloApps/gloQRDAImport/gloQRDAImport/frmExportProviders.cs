using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using gloCCDLibrary;
using System.IO;
using UnZipFileIonic;
using System.IO.Compression;
using Excel=  Microsoft.Office.Interop.Excel;





namespace gloQRDAImport
{
    public partial class frmExportProviders : Form
    {

        //string[] files;
        public frmExportProviders()
        {
            InitializeComponent();
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            btnExport.Enabled = false;
            gloCCDLibrary.gloQRDAReader qrdareader = new gloQRDAReader();
            ReconcileList oreconsilelist = null;
            gloCCDLibrary.gloQRDARegPatient qrdaregpatient = new gloQRDARegPatient();
            Excel.Application xlApp;
            Excel.Workbook xlWorkBook;
            Excel.Worksheet xlWorkSheet;
            object misValue = System.Reflection.Missing.Value;

            xlApp = new Excel.Application();
            xlWorkBook = xlApp.Workbooks.Add(misValue);
            xlWorkSheet = (Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);
             DataTable dtProviderList = new DataTable();
             DataTable dtfinalList = new DataTable();
             bool isxml = false;
             try
             {
                 if (txtPath.Text == "")
                 {
                     MessageBox.Show("Please select Folder");
                     return;
                 }
                 if (txtPath.Text != "")
                 {

                     //FileInfo fie = new FileInfo("C:\\QRDAProviderList.xls");
                     //if (fie.Exists)
                     //{
                     //    bool inuse = IsFileInUse("C:\\QRDAProviderList.xls");
                     //    if (inuse == true)
                     //    {
                     //        MessageBox.Show("The file 'QRDAproviderList.xls' is in use, please close the file");
                     //        return;
                     //    }
                         dtfinalList.Columns.Add("No.");
                         dtfinalList.Columns.Add("CMSID");
                         dtfinalList.Columns.Add("Provider Name");
                         dtfinalList.Rows.Add("No.", "CMSID", "Provider Name");
                         dtfinalList.Rows.Add("", "CMS146", "");
                         dtfinalList.Rows.Add("", "CMS165", "");
                         dtfinalList.Rows.Add("", "CMS138", "");
                         dtfinalList.Rows.Add("", "CMS124", "");
                         dtfinalList.Rows.Add("", "CMS153", "");
                         dtfinalList.Rows.Add("", "CMS130", "");
                         dtfinalList.Rows.Add("", "CMS117", "");
                         dtfinalList.Rows.Add("", "CMS147", "");
                         dtfinalList.Rows.Add("", "CMS127", "");
                         dtfinalList.Rows.Add("", "CMS166", "");
                         dtfinalList.Rows.Add("", "CMS131", "");
                         dtfinalList.Rows.Add("", "CMS123", "");
                         dtfinalList.Rows.Add("", "CMS122", "");
                         dtfinalList.Rows.Add("", "CMS134", "");
                         dtfinalList.Rows.Add("", "CMS163", "");
                         dtfinalList.Rows.Add("", "CMS164", "");
                         dtfinalList.Rows.Add("", "CMS139", "");
                         dtfinalList.Rows.Add("", "CMS2", "");
                         dtfinalList.Rows.Add("", "CMS68", "");
                         dtfinalList.Rows.Add("", "CMS69", "");
                         dtfinalList.Rows.Add("", "CMS22", "");
                         dtfinalList.Rows.Add("", "CMS56", "");
                         dtfinalList.Rows.Add("", "CMS66", "");
                         dtfinalList.Rows.Add("", "CMS90", "");
                         dtfinalList.Rows.Add("", "CMS125", "");
                         dtProviderList.Columns.Add("No.");
                         dtProviderList.Columns.Add("CMSID");
                         dtProviderList.Columns.Add("Provider Name");
                        dtProviderList.Columns.Add("Patient Name");
                         dtProviderList.Rows.Add("No.", "CMSID", "Provider Name","Patient Name");
                         string zipPath = txtPath.Text;
                         //string extractPath = @"c:\example\extract";

                         string extension= Path.GetExtension(zipPath);
                         if (extension== ".zip")
                         {
                             string FinalDirectory = clsExtractFile.ExtractZipFile(zipPath);
                             string[] Directories = Directory.GetDirectories(FinalDirectory);
                             if (Directories.Length == 0)
                             {
                                 string[] filePaths = Directory.GetFiles(FinalDirectory);
                                 int srno = 1;
                                 foreach (string file in filePaths)
                                 {
                                     if (System.IO.Path.GetExtension(file) == ".zip")
                                     {
                                         string finalXMLdirectory = clsExtractFile.ExtractZipFile(file);
                                         string[] xmlfiles = Directory.GetFiles(finalXMLdirectory);
                                         if (xmlfiles.Length > 0)
                                         {
                                             //take only 1 file as the provider is same in all the files
                                             string qrdafile = xmlfiles[0];
                                             if (System.IO.Path.GetExtension(qrdafile) != ".xsl")
                                             {
                                                 if (System.IO.Path.GetExtension(qrdafile) == ".xml")
                                                 {
                                                     qrdareader.ExportproviderList = true;
                                                     oreconsilelist = qrdareader.ExtractCDA(qrdafile, 0, true);
                                                     if (oreconsilelist != null)
                                                     {
                                                         string provider_name = oreconsilelist.mPatient.PatientCareTeam.get_Item(0).PersonName.FirstName + " " + oreconsilelist.mPatient.PatientCareTeam.get_Item(0).PersonName.MiddleName + " " + oreconsilelist.mPatient.PatientCareTeam.get_Item(0).PersonName.LastName;
                                                         dtProviderList.Rows.Add(srno, "CMS" + oreconsilelist.CMSID, provider_name);
                                                         dtfinalList.Rows[srno][0] = srno;
                                                         srno++;
                                                     }
                                                 }
                                             }
                                         }
                                     }
                                     else if (System.IO.Path.GetExtension(file) == ".xml")
                                     {
                                         isxml = true;
                                         qrdareader.ExportproviderList = true;
                                         oreconsilelist = qrdareader.ExtractCDA(file, 0, true);
                                         if (oreconsilelist != null)
                                         {
                                             
                                             string provider_name = oreconsilelist.mPatient.PatientCareTeam.get_Item(0).PersonName.FirstName + " " + oreconsilelist.mPatient.PatientCareTeam.get_Item(0).PersonName.MiddleName + " " + oreconsilelist.mPatient.PatientCareTeam.get_Item(0).PersonName.LastName;
                                             string patient_name = oreconsilelist.mPatient.PatientDemographics.DemographicsDetail.PatientLastName + " " + oreconsilelist.mPatient.PatientDemographics.DemographicsDetail.PatientFirstName + " " + oreconsilelist.mPatient.PatientDemographics.DemographicsDetail.PatientMiddleName;
                                             dtProviderList.Rows.Add(srno, "CMS" + oreconsilelist.CMSID, provider_name,patient_name);
                                             //dtfinalList.Rows[srno][0] = srno;
                                             srno++;

                                         }
                                     }
                                     //string finalXMLdirectory = clsExtractFile.ExtractZipFile(file);
                                     //string[] xmlfiles = Directory.GetFiles(finalXMLdirectory);
                                     //if (xmlfiles.Length > 0)
                                     //{
                                     //    //take only 1 file as the provider is same in all the files
                                     //    string qrdafile = xmlfiles[0];
                                     //    if (System.IO.Path.GetExtension(qrdafile) != ".xsl")
                                     //    {
                                     //        if (System.IO.Path.GetExtension(qrdafile) == ".xml")
                                     //        {
                                     //            qrdareader.ExportproviderList = true;
                                     //            oreconsilelist = qrdareader.ExtractCDA(qrdafile, 0, true);
                                     //            if (oreconsilelist != null)
                                     //            {
                                     //                string provider_name = oreconsilelist.mPatient.PatientCareTeam.get_Item(0).PersonName.FirstName + " " + oreconsilelist.mPatient.PatientCareTeam.get_Item(0).PersonName.MiddleName + " " + oreconsilelist.mPatient.PatientCareTeam.get_Item(0).PersonName.LastName;
                                     //                dtProviderList.Rows.Add(srno, "CMS" + oreconsilelist.CMSID, provider_name);
                                     //                dtfinalList.Rows[srno][0] = srno;
                                     //                srno++;
                                     //            }
                                     //        }
                                     //    }
                                     //}

                                 }


                                 if (isxml == true)
                                 {
                                     for (int i = 0; i < dtProviderList.Rows.Count; i++)
                                     {
                                         for (int j = 0; j < dtProviderList.Columns.Count; j++)
                                         {
                                             xlWorkSheet.Cells[i + 1, j + 1] = dtProviderList.Rows[i][j];
                                         }
                                     }
                                 }
                                 else
                                 {
                                     foreach (DataRow row in dtfinalList.Rows)
                                     {
                                         DataRow rowsToUpdate = dtProviderList.AsEnumerable().FirstOrDefault(r => r.Field<string>("CMSID") == row.Field<string>("CMSID"));

                                         if (rowsToUpdate!=null)
                                         {
                                             row.SetField("Provider Name", rowsToUpdate.Field<string>("Provider Name"));
                                         }
                                         
                                     }
                                     for (int i = 0; i < dtfinalList.Rows.Count; i++)
                                     {
                                         for (int j = 0; j < dtfinalList.Columns.Count; j++)
                                         {
                                             xlWorkSheet.Cells[i + 1, j + 1] = dtfinalList.Rows[i][j];
                                         }
                                     }
                                 }
                                 //foreach (DataRow row in dtfinalList.Rows)
                                 //{
                                 //    DataRow rowsToUpdate = dtProviderList.AsEnumerable().FirstOrDefault(r => r.Field<string>("CMSID") == row.Field<string>("CMSID"));
                                 //    row.SetField("Provider Name", rowsToUpdate.Field<string>("Provider Name"));
                                 //}

                                 //for (int i = 0; i < dtfinalList.Rows.Count; i++)
                                 //{
                                 //    for (int j = 0; j < dtfinalList.Columns.Count; j++)
                                 //    {
                                 //        xlWorkSheet.Cells[i + 1, j + 1] = dtfinalList.Rows[i][j];
                                 //    }
                                 //}

                                 // fie=null;
                                 // fie = new FileInfo("C:\\QRDAProviderList.xls");
                                 //if (fie.Exists)
                                 //{

                                 //    fie.Delete();
                                 //}
                                 SaveFileDialog saveFileDialog1 = new SaveFileDialog();
                                 saveFileDialog1.Filter = "Excel Workbook |*.xls";
                                 saveFileDialog1.Title = "Save File";
                                 saveFileDialog1.ShowDialog();
                                 string filename = "";
                                 if (saveFileDialog1.FileName != "")
                                 {
                                     filename = saveFileDialog1.FileName;
                                     xlWorkBook.SaveAs(saveFileDialog1.FileName, Excel.XlFileFormat.xlWorkbookNormal, misValue, misValue, misValue, misValue, Excel.XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue, misValue);
                                     xlWorkBook.Close(true, misValue, misValue);

                                     //FileInfo excel = new FileInfo(filename);

                                     //if (excel.Exists)
                                     //{
                                     //    System.Diagnostics.Process.Start(@"" + filename + "");
                                     //}
                                     //else
                                     //{
                                     //    MessageBox.Show("File does not exist");
                                     //    //file doesn't exist
                                     //}
                                 }
                                 else
                                 {
                                     MessageBox.Show("Please select the path to save the file");
                                 }

                                 saveFileDialog1.Dispose();
                                 xlApp.Quit();
                                 releaseObject(xlWorkSheet);
                                 releaseObject(xlWorkBook);
                                 releaseObject(xlApp);

                                 MessageBox.Show("Excel file created , you can find the file at " + filename + "");
                             }
                             else
                             {

                                 int srno = 1;
                                 foreach (string Dirfile in Directories)
                                 {
                                     string[] filePaths = Directory.GetFiles(Dirfile);

                                     string qrdafile = filePaths[0];
                                     if (System.IO.Path.GetExtension(qrdafile) != ".xsl")
                                     {
                                         if (System.IO.Path.GetExtension(qrdafile) == ".xml")
                                         {
                                             qrdareader.ExportproviderList = true;
                                             oreconsilelist = qrdareader.ExtractCDA(qrdafile, 0, true);
                                             if (oreconsilelist != null)
                                             {
                                                 string provider_name = oreconsilelist.mPatient.PatientCareTeam.get_Item(0).PersonName.FirstName + " " + oreconsilelist.mPatient.PatientCareTeam.get_Item(0).PersonName.MiddleName + " " + oreconsilelist.mPatient.PatientCareTeam.get_Item(0).PersonName.LastName;
                                                 dtProviderList.Rows.Add(srno, "CMS" + oreconsilelist.CMSID, provider_name);
                                                 dtfinalList.Rows[srno][0] = srno;
                                                 srno++;
                                             }
                                         }

                                     }
                                 }

                                 foreach (DataRow row in dtfinalList.Rows)
                                 {
                                     DataRow rowsToUpdate = dtProviderList.AsEnumerable().FirstOrDefault(r => r.Field<string>("CMSID") == row.Field<string>("CMSID"));
                                     if (rowsToUpdate!=null)
                                     {
                                         row.SetField("Provider Name", rowsToUpdate.Field<string>("Provider Name"));
                                     }
                                     
                                 }

                                 for (int i = 0; i < dtfinalList.Rows.Count; i++)
                                 {
                                     for (int j = 0; j < dtfinalList.Columns.Count; j++)
                                     {
                                         xlWorkSheet.Cells[i + 1, j + 1] = dtfinalList.Rows[i][j];
                                     }
                                 }

                                 //fie = null;
                                 //fie = new FileInfo("C:\\QRDAProviderList.xls");
                                 //if (fie.Exists)
                                 //{
                                 //    fie.Delete();
                                 //}
                                 //Stream stream = new FileStream("C:\\QRDAProviderList.xls", FileMode.Open);
                                 //if (stream != null)
                                 //{
                                 //    stream.Close();
                                 //}



                                 SaveFileDialog saveFileDialog2 = new SaveFileDialog();
                                 saveFileDialog2.Filter = "Excel Workbook |*.xls";
                                 saveFileDialog2.Title = "Save File";
                                 saveFileDialog2.ShowDialog();
                                 string filename1 = "";
                                 if (saveFileDialog2.FileName != "")
                                 {
                                     filename1 = saveFileDialog2.FileName;
                                     xlWorkBook.SaveAs(saveFileDialog2.FileName, Excel.XlFileFormat.xlWorkbookNormal, misValue, misValue, misValue, misValue, Excel.XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue, misValue);
                                     xlWorkBook.Close(true, misValue, misValue);
                                     //FileInfo excel = new FileInfo(filename1);

                                     //if (excel.Exists)
                                     //{
                                     //    System.Diagnostics.Process.Start(@"" + filename1 + "");
                                     //}
                                     //else
                                     //{
                                     //    MessageBox.Show("File does not exist");
                                     //    //file doesn't exist
                                     //}
                                 }
                                 else
                                 {
                                     MessageBox.Show("Please select the path to save the file");
                                 }
                                 saveFileDialog2.Dispose();
                                 xlApp.Quit();
                                 releaseObject(xlWorkSheet);
                                 releaseObject(xlWorkBook);
                                 releaseObject(xlApp);

                                 MessageBox.Show("Excel file created , you can find the file at " + filename1 + "");
                             }
                         }
                         else
                         {
                             MessageBox.Show("Please select directory having extension '.zip'");
                         }
                        
                         //files = Directory.GetFiles(txtPath.Text);
                     }

                 }
          
             catch (Exception ex)
             {

                 MessageBox.Show(ex.Message);
             }
            finally
            {
                if (qrdareader != null)
                {
                    qrdareader.Dispose();
                }
                if (oreconsilelist != null)
                {
                    oreconsilelist.Dispose();
                }
                if (dtfinalList!=null)
                {
                    dtfinalList.Dispose();

                }
                if (dtProviderList!=null)
                {
                    dtProviderList.Dispose();
                }
                this.Cursor = Cursors.Default;
                btnExport.Enabled = true;
            
            }
        }
        public bool IsFileInUse(string path)
        {
            if (string.IsNullOrEmpty(path))
                throw new ArgumentException("'path' cannot be null or empty.", "path");

            try
            {
                using (var stream = new FileStream(path, FileMode.Open, FileAccess.Read)) { }
            }
            catch (IOException)
            {
                return true;
            }

            return false;
        }
        private void releaseObject(object obj)
        {
            try
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(obj);
                obj = null;
            }
            catch (Exception ex)
            {
                obj = null;
                MessageBox.Show("Exception Occured while releasing object " + ex.ToString());
            }
            finally
            {
                GC.Collect();
            }
        }

        private void cmdBrowse_Click(object sender, EventArgs e)
        {
            OpenFileDialog fbd = new OpenFileDialog();
            try
            {

                DialogResult result = fbd.ShowDialog();

                if (fbd.FileName != "")
                {
                    //files = Directory.GetFiles(fbd.SelectedPath);
                    txtPath.Text = fbd.FileName;
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
            finally
            {
                if (fbd != null)
                {
                    fbd.Dispose();
                }

            }
        }

       
    }
}
