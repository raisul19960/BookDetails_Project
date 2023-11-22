using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BookDetails_Project
{
    public partial class ViewCategory : Form
    {
        public ViewCategory()
        {
            InitializeComponent();
        }

        private void ViewCategory_Load(object sender, EventArgs e)
        {
            using (SqlConnection con = new SqlConnection(ConnectionUtility.ConString))
            {
                using (SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM categories", con))
                {
                    DataTable dt1 = new DataTable();
                    da.Fill(dt1);
                    dataGridView1.DataSource = dt1;
                }
            }
        }
    }
}
