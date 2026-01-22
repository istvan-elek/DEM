using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity.ModelConfiguration.Configuration;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace DCAnalyser
{
    public partial class frmWorkersTree : Form
    {
        public frmWorkersTree(List<string> wNodes, string title)
        {
            InitializeComponent();
            wNodes.Sort();
            this.Text = "Workers' tree from #" + title.Split('#')[1] + " generation";
            InitializeTreeView(wNodes);
        }

        private void InitializeTreeView(List<string> wNodes)
        {
            string[] items = wNodes.Select(i => i.ToString()).ToArray();
            foreach (var item in items)
            {
                AddToTreeView(treeView1.Nodes, item.Split(','));
            }
        }

        private void AddToTreeView(TreeNodeCollection nodes, string[] parts)
        {
            TreeNode currentNode = null;
            foreach (var part in parts)
            {
                currentNode = FindOrCreateNode(nodes, part, currentNode);
                nodes = currentNode.Nodes;
            }
        }

        private TreeNode FindOrCreateNode(TreeNodeCollection nodes, string text, TreeNode parentNode)
        {
            foreach (TreeNode node in nodes)
            {
                if (node.Text == text)
                {
                    return node;
                }
            }
            TreeNode newNode = parentNode != null ? parentNode.Nodes.Add(text) : treeView1.Nodes.Add(text);
            return newNode;
        }

        private void expandAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            treeView1.BeginUpdate();
            treeView1.ExpandAll();
            treeView1.EndUpdate();
        }

        private void collapseAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            treeView1.CollapseAll();
        }
    }
}
