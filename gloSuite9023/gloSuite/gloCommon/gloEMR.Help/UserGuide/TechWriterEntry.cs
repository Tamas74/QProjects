using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
  
namespace gloEMR.Help
{
	/// <summary>
	/// Help editor form.
	/// </summary>
    public partial class TechWriterEntry : Form
	{
		#region Constructor & destructor
		/// <summary>
		/// Initializes a new instance of the <see cref="HelpEditor"/> class.
		/// </summary>
		private TechWriterEntry() { }

		/// <summary>
		/// Initializes a new instance of the <see cref="HelpEditor"/> class.
		/// </summary>
		/// <param name="Control">The control.</param>
        public TechWriterEntry(Control Control)
        {
            InitializeComponent();

            InitializeControls();
            InitializePath();
            InitializeTreeForControl(Control);
        }
		#endregion

		#region Properties
		/// <summary>
		/// Help description.
		/// </summary>
		private readonly HelpDescription HelpDescription = ResourceHelper.HelpDescription;
		#endregion

		#region Methods
		/// <summary>
		/// Initializes the controls.
		/// </summary>
		private void InitializeControls()
		{
			// Load all existing help files to make it easier for the editor
			try
			{
				// List files
				string[] files = Directory.GetFiles(Path.Combine(Application.StartupPath, PathHelper.HelpDirectory), "*.chm");

				// Add the files
				foreach (string file in files)
				{
					FileInfo fileInfo = new FileInfo(file);
					helpFileComboBox.Items.Add(fileInfo.Name);
				}
			}
			catch (Exception)
			{
				// Unfortunately we cannot help the user by automatically filling the combobox
			}

            foreach (HelpNavigator v in Enum.GetValues(typeof(HelpNavigator)))
            {
                cbNavigator.Items.Add(v);
            }
		    cbNavigator.SelectedItem = HelpNavigator.Topic;
		}

		/// <summary>
		/// Initializes the path.
		/// </summary>
		private void InitializePath()
        {
            helpFileComboBox.Text = HelpDescription.HelpFile;
            if (helpFileComboBox.Text.Trim() == string.Empty)
            {
                // Select the first value from the combobox automatically
                string text = (helpFileComboBox.Items.Count > 0) ? helpFileComboBox.Items[0] as string : string.Empty;
                helpFileComboBox.Text = text ?? string.Empty;
            }
            appendExtensionCheckBox.Checked = HelpDescription.AppendExtension;
        }

		/// <summary>
		/// Initializes the tree for control.
		/// </summary>
		/// <param name="control">The control.</param>
        private void InitializeTreeForControl(Control control)
        {
            List<Control> controlTree = UserHelper.GetControlTree(control);

            TreeNode Node = null;
            foreach (Control c in controlTree)
            {
                TreeNode cNode = new TreeNode(UserHelper.GetControlDescription(c));
                cNode.Tag = c;

                SetNodeProperties(cNode);

                if (Node == null)
                {
                    tvNodes.Nodes.Add(cNode);
                }
                else
                {
                    Node.Nodes.Add(cNode);
                }

                Node = cNode;
            }

            tvNodes.ExpandAll();
        }

		/// <summary>
		/// Sets the node properties.
		/// </summary>
		/// <param name="node">The node.</param>
        private void SetNodeProperties(TreeNode node)
        {
            if (node.Tag is Control)
            {
                ControlHelpDescription exactDescription = HelpDescription.FindExactDescription(node.Tag as Control,AppendFormName.Checked,UserHelper.GetFormName( node.Tag  as Control) );
                node.ForeColor = (exactDescription == null) || string.IsNullOrEmpty(exactDescription.HelpKeyword) ? Color.Red : SystemColors.WindowText;
            }
        }

		/// <summary>
		/// Initializes the description for control.
		/// </summary>
		/// <param name="control">The control.</param>
        private void InitializeDescriptionForControl(Control control)
        {
            ControlHelpDescription description = HelpDescription.FindExactDescription(control, true, UserHelper.GetFormName(control));
            if (description == null)
            {
                 description = HelpDescription.FindExactDescription(control, false, UserHelper.GetFormName(control));
        
            }

            if (description != null)
            {
                txtCategory.Text = description.HelpKeyword;
                cbNavigator.SelectedItem = description.HelpNavigator;
                cbShowHelp.Checked = description.ShowHelp;
                fromApplication = true;
                AppendFormName.Checked = description.AppendFormFlag;
                fromApplication = false;
            }
            else
            {
                txtCategory.Text = string.Empty;
                cbNavigator.SelectedItem = HelpNavigator.Topic;
                cbShowHelp.Checked = true;
                AppendFormName.Checked = false;
            }
        }
        #endregion

        #region Event handlers
        private void txtCategory_Validated(object sender, EventArgs e)
        {
            if (SelectedDescription != null)
            {
                SelectedDescription.HelpKeyword = txtCategory.Text;
            }
            SetNodeProperties(tvNodes.SelectedNode);
        }

        private void cbNavigator_Validated(object sender, EventArgs e)
        {
            if (SelectedDescription != null)
            {
                SelectedDescription.HelpNavigator = (HelpNavigator) cbNavigator.SelectedItem;
            }
            SetNodeProperties(tvNodes.SelectedNode);
        }

        private void cbShowHelp_Validated(object sender, EventArgs e)
        {
            if (SelectedDescription != null)
            {
                SelectedDescription.ShowHelp = cbShowHelp.Checked;
            }
            SetNodeProperties(tvNodes.SelectedNode);
        }

        private void appendExtensionCheckBox_Validated(object sender, EventArgs e)
        {
            HelpDescription.AppendExtension = appendExtensionCheckBox.Checked;
        }

        private ControlHelpDescription SelectedDescription
        {
            get
            {
                if (tvNodes.SelectedNode.Tag is Control)
                {
                    Control control = tvNodes.SelectedNode.Tag as Control;
                    return HelpDescription.CreateExactDescription(control, AppendFormName.Checked, UserHelper.GetFormName(control));
                }

                return null;
            }
        }

        private void btSave_Click(object sender, EventArgs e)
        {
            //try
            //{
            //    HelpDescription.HelpFile = helpFileComboBox.Text;
            //    ResourceHelper.SaveHelpDescription(HelpDescription);
            //    Close();
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show("An error occurred while saving the help mapping file.\r\n\r\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}
        }

        private void tvNodes_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (e.Node.Tag is Control)
            {
                gbProperties.Enabled = true;
                InitializeDescriptionForControl(e.Node.Tag as Control);
            }
            else
            {
                gbProperties.Enabled = false;
            }
        }
        #endregion
        static Boolean fromApplication = false;
        private void AppendFormName_CheckedChanged(object sender, EventArgs e)
        {
            if (fromApplication)
                return;
            if (SelectedDescription != null)
            {
                SelectedDescription.HelpKeyword = txtCategory.Text;
            }
            SetNodeProperties(tvNodes.SelectedNode);
        }

        private void tlsbtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                HelpDescription.HelpFile = helpFileComboBox.Text;
                if (tvNodes.SelectedNode != null)
                {
                    for (int len = 0; len < HelpDescription.TopicDescription.Count; len++)
                    {
                        if (HelpDescription.TopicDescription[len].Name == tvNodes.SelectedNode.Text)
                        {
                            HelpDescription.TopicDescription[len].HelpKeyword = txtCategory.Text;
                            break;
                        }
                    }




                    //IEnumerable<HelpDescription.TopicDescription> existingpat = (from c in HelpDescription.TopicDescription 
                    //              where
                    //                   HelpDescription.TopicDescription.Contains("Ramesh")
                    //              select c).FirstOrDefault();
       

                
                
                
                }
                //tvNodes.SelectedNode
               
                
                
                ResourceHelper.SaveHelpDescription(HelpDescription);
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while saving the help mapping file.\r\n\r\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
    }
}