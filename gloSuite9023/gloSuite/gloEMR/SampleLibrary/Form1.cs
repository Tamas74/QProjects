using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SampleLibrary
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            DirectoryTree DirTree = new DirectoryTree();
            DirTree.Size = new Size(this.Width - 30, this.Height - 60);
            DirTree.Location = new Point(5, 5);
            DirTree.Drive = "C";
            DirTree.Drive = "D";
            DirTree.CollapseAll();
            DirTree.Drive = "E";
            DirTree.Drive = "F";
            DirTree.Drive = "G";
            this.Controls.Add(DirTree);
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        
    }
}