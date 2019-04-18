using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace SampleLibrary
{
    class DirectoryTree : TreeView
    {
        public event DirectorySelectedEventHandler DirectorySelected;
        public delegate void DirectorySelectedEventHandler(object sender, DirectorySelectedEventArgs e);
        private string _Drive;
        public string Drive
        {
            get { return _Drive; }
            set
            {
                _Drive = value;
                RefreshDisplay();
            }
        }
        public void RefreshDisplay()
        {
            // this.Nodes.Clear();
            TreeNode RootNode = new TreeNode(_Drive + ":\\");
            this.Nodes.Add(RootNode);
            Fill(RootNode);
            this.Nodes[0].Expand();
        }
        private void Fill(TreeNode DirNode)
        {
            DirectoryInfo DirObj = new DirectoryInfo(DirNode.FullPath);
            DirectoryInfo[] Dirs = DirObj.GetDirectories();

            // DirectoryInfo Dir = new DirectoryInfo(DirNode.FullPath);
            //DirectoryInfo DirItem = default(DirectoryInfo);
            foreach (DirectoryInfo DirItem in DirObj.GetDirectories())
            {
                TreeNode NewNode = new TreeNode(DirItem.Name);
                DirNode.Nodes.Add(NewNode);
                NewNode.Nodes.Add("*");
            }
        }
        protected override void OnBeforeExpand(TreeViewCancelEventArgs e)
        {
            base.OnBeforeExpand(e);
            if (e.Node.Nodes[0].Text == "*")
            {
                e.Node.Nodes.Clear();
                Fill(e.Node);
            }
        }
        protected override void OnAfterSelect(TreeViewEventArgs e)
        {
            base.OnAfterSelect(e);
            base.OnAfterSelect(e);
            if (e.Node.FullPath != null
                && e.Node.FullPath.Length > 0)
            {
                SampleLibrary.Program.Dir = e.Node.FullPath.ToString();
            }
            if (DirectorySelected != null)
            {
                DirectorySelected(this, new DirectorySelectedEventArgs(e.Node.FullPath));
            }
            // return e.Node.FullPath;
        }
    }
    public class DirectorySelectedEventArgs : EventArgs
    {
        public string DirectoryName;
        public DirectorySelectedEventArgs(string DirectoryName)
        {
            this.DirectoryName = DirectoryName;
        }
    }
}
