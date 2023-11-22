using BookDetails_Project.Reports;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BookDetails_Project
{
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();
        }

        private void viewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new FormStart { MdiParent = this }.Show();
        }

        private void addToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new AddBook { MdiParent= this }.Show();
        }

        private void adddToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new AddTOC { MdiParent= this }.Show();
        }

        private void editDeleteToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            new EditTOC { MdiParent = this }.Show();
        }

        private void viewToolStripMenuItem3_Click(object sender, EventArgs e)
        {
            new ViewTOC { MdiParent = this }.Show();
        }

        private void viewToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            new ViewPublsiher { MdiParent = this }.Show();
        }

        private void viewToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            new ViewCategory { MdiParent = this }.Show();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void addToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            new AddPublisher { MdiParent = this }.Show();
        }

        private void addToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            new AddCategory { MdiParent = this }.Show();
        }

        private void editDeleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new EditPublisher { MdiParent = this }.Show();
        }

        private void editDeleteToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            new EditCategory { MdiParent = this }.Show();
        }

        private void report1ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new FormBookRpt { MdiParent = this }.Show();
        }

        private void report2ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new FormBookGroupRpt { MdiParent = this }.Show();
        }

        private void report3ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new RptForm1 { MdiParent = this }.Show();
        }
    }
}
