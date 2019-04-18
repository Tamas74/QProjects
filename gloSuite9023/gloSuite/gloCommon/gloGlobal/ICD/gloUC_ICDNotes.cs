using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml.Linq;


namespace gloGlobal.ICD
{
    public partial class gloICDNotes : UserControl
    {

        #region "Private Variables"
        //private string _MessageBoxCaption = "";
        public string _databaseconnectionstring = "";
        //System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;
        private XDocument xDoc = null;
        //private DataSet dsTree;
        #endregion

        #region "Public Properties"
       // public string ICD10Code { get; set; }

        #endregion


        public gloICDNotes()
        {
            InitializeComponent();

            #region " Retrieve MessageBoxCaption from AppSettings "

            //if (appSettings["MessageBOXCaption"] != null)
            //{
            //    if (appSettings["MessageBOXCaption"] != "")
            //    {
            //        _MessageBoxCaption = Convert.ToString(appSettings["MessageBOXCaption"]);
            //    }
            //    else
            //    {
            //        _MessageBoxCaption = "";
            //    }
            //}
            //else
            //{ _MessageBoxCaption = ""; }

            #endregion
        }

        #region "Notes functions"
     //   private bool IsnotesPresents = false;

        private class ICDNote
        {
            public enum NoteType
            {
                Other,
                Custom,
                Includes,
                Excludes1,
                Excludes2,
                InclusionTerm,
                CodeAlso,
                CodeFirst,
                UseAdditionalCode,
                SevenCharNote,
                Note
            }

            public ICDNote()
            {
                //AdditionalNotes = new List<string>();
            }

            public ICDNote(NoteType type, List<string> notes)
            {
                Type = type;
                Notes = notes;
                //AdditionalNotes = new List<string>();
            }

            public NoteType Type { get; set; }
            public List<string> Notes { get; set; }
            //public List<string> AdditionalNotes { get; set; }
        }

        private List<ICDNote> GetICDNotes(string code)
        {          
            List<ICDNote> notes = new List<ICDNote>();
            XElement element = null;

            try
            {
               // var xDoc = clsICD.LoadICDNotesXML();
                if (xDoc != null)
                {
                    element = xDoc.Descendants("diag").Where(x => x.Element("name").Value.Replace(".","") == code).FirstOrDefault();

                    if (element != null)
                    {
                        notes = ExtractNotes(element);
                    }
                }
                return notes;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception : " + ex.ToString());
                return null;
            }
            finally
            {
                element = null;
            }
        }

       
        private List<ICDNote> ExtractNotes(XElement element)
        {
            List<ICDNote> notes = new List<ICDNote>();
            XElement childElement = null;
            XElement tempElement = null;
            ICDNote note = null;
            try
            {
                foreach (XNode child in element.Nodes())
                {
                    childElement = (XElement)child;

                    var temp = from record in childElement.Elements("note")
                               select record.Value;

                    if (temp.FirstOrDefault() != null)
                    {
                        ICDNote.NoteType type = ICDNote.NoteType.Other;

                        if (childElement.Name.ToString() == "includes")
                        { type = ICDNote.NoteType.Includes; }
                        else if (childElement.Name.ToString() == "excludes1")
                        { type = ICDNote.NoteType.Excludes1; }
                        else if (childElement.Name.ToString() == "excludes2")
                        { type = ICDNote.NoteType.Excludes2; }
                        else if (childElement.Name.ToString() == "inclusionTerm")
                        { type = ICDNote.NoteType.InclusionTerm; }
                        else if (childElement.Name.ToString() == "codeAlso")
                        { type = ICDNote.NoteType.CodeAlso; }
                        else if (childElement.Name.ToString() == "codeFirst")
                        { type = ICDNote.NoteType.CodeFirst; }
                        else if (childElement.Name.ToString() == "useAdditionalCode")
                        { type = ICDNote.NoteType.UseAdditionalCode; }
                        else if (childElement.Name.ToString() == "notes")
                        { type = ICDNote.NoteType.Note; }

                        if (childElement.Name.ToString() == "sevenChrNote")
                        {
                            tempElement = (XElement)childElement.NextNode;
                            var temp7char = from record in tempElement.Elements("extension")
                                            select record;

                            type = ICDNote.NoteType.SevenCharNote;

                            //Adding a Seven Char Note
                            note = new ICDNote(type, temp.ToList<string>());

                            //Adding a Seven Char ExtensionNotes
                            foreach (XElement n in temp7char.ToList())
                            {
                                string s = n.Attribute("char").Value + " - " + n.Value;
                                note.Notes.Add(s);
                            }

                            notes.Add(note);

                        }
                        else
                        {
                            notes.Add(new ICDNote(type, temp.ToList<string>()));
                        }
                    }
                }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception : " + ex.ToString());
                return null;
            }
            finally
            {
                childElement = null;
                tempElement = null;
                note = null;
            }
            return notes;
        }

        #endregion
        
        public bool LoadControl(string ICDCode)
        {
            bool result = false;
            try
            {
                 xDoc = clsICD.LoadICDNotesXML();
                // Loads icd code notes
                LoadNotes(false, ICDCode);
                lblSelectedICD.Text = "ICD-10 Notes : " + ICDCode + "";

                int catlen = ICDCode.Length - 1;
                if (catlen > 3)
                {
                    string cat = ICDCode.Substring(0, ICDCode.Length - 1);

                    // Loads icd code category notes
                    LoadNotes(true, cat);
                    if (cat.Length == 4)
                    { cat = cat.Remove(cat.IndexOf('.')); }

                    lblCategoryNotes.Text = "Category Notes : " + cat + "";
                }
                else
                {
                    //panel6.Visible = false;
                }

                if (TrvNotes.Nodes.Count > 0)
                {
                    result = true;
                }
            }
            catch //(Exception ex)
            {

            }
            finally
            {
                if (xDoc != null)
                {
                    xDoc = null;
                }
            }
            return result;
        }

        public void LoadNotes(bool isCategory, string ICDCode)
        {
            List<ICDNote> lstNotes = new List<ICDNote>();
            try
            {
                if (ICDCode != null)
                {
                    lstNotes = GetICDNotes(ICDCode.Replace(".", ""));
                    if (lstNotes.Count > 0)
                    {
                        FillICDNotes(isCategory, lstNotes);                       
                    }
                   

                }
            }
            catch (Exception ex)
            {
                //TODO : Audit Trail
                MessageBox.Show("Exception : " + ex.ToString());
            }
            finally
            {
                lstNotes = null;
            }
        }
       
        private void FillICDNotes(bool isCategory, List<ICDNote> notes)
        {
            TreeNode node = null;
            TreeNode Td = null;
            try
            {
                if (isCategory)
                {
                    TrvCategoryNotes.Nodes.Clear();
                    TrvCategoryNotes.BeginUpdate();
                }
                else
                {
                    TrvNotes.Nodes.Clear();
                    TrvNotes.BeginUpdate();
                }

                foreach (ICDNote note in notes)
                {
                    node = new TreeNode();
                    node.NodeFont = gloGlobal.clsgloFont.gFont; //new System.Drawing.Font("Tahoma", 9.0F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, (byte)0);//new Font(TrvNotes.Font, FontStyle.Bold);
                    node.ForeColor = System.Drawing.Color.DarkBlue;

                    node.Text = note.Type.ToString();
                    node.ImageIndex = 4;
                    node.SelectedImageIndex = 4;
                    foreach (string n in note.Notes)
                    {
                        Td = new TreeNode();
                        Td.Text = n;
                        Td.ImageIndex = 1;
                        Td.SelectedImageIndex = 1;
                        node.Nodes.Add(Td);
                    }

                    if (isCategory)
                    {
                        TrvCategoryNotes.Nodes.Add(node);
                    }
                    else
                    {
                        TrvNotes.Nodes.Add(node);
                    }
                }
                if (isCategory)
                {
                    //TrvCategoryNotes.BeforeExpand -= new System.Windows.Forms.TreeViewCancelEventHandler(this.TrvCategoryNotes_BeforeExpand);
                    TrvCategoryNotes.ExpandAll();
                    //TrvCategoryNotes.BeforeExpand += new System.Windows.Forms.TreeViewCancelEventHandler(this.TrvCategoryNotes_BeforeExpand);

                }
                else
                {
                    //TrvNotes_BeforeCollapse
                    //TrvNotes.BeforeExpand -= new System.Windows.Forms.TreeViewCancelEventHandler(this.TrvNotes_BeforeExpand);
                    TrvNotes.ExpandAll();
                    //TrvNotes.BeforeExpand += new System.Windows.Forms.TreeViewCancelEventHandler(this.TrvNotes_BeforeExpand);                    
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception : " + ex.ToString());
            }
            finally
            {
                if (isCategory)
                {
                    TrvCategoryNotes.EndUpdate();
                }
                else
                {
                    TrvNotes.EndUpdate();
                }
                node = null;
                Td = null;
            }
        }

        public void ClearNotes()
        {
            try
            {
                TrvNotes.Nodes.Clear();
                TrvCategoryNotes.Nodes.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception : " + ex.ToString());
            }
        }      
      
        private void TrvNotes_BeforeCollapse(object sender, TreeViewCancelEventArgs e)
        {
            e.Cancel = true;
        }

        private void TrvCategoryNotes_BeforeCollapse(object sender, TreeViewCancelEventArgs e)
        {
            e.Cancel = true;
        }

    }
}
