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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace BookDetails_Project
{
    public partial class ViewPublsiher : Form
    {
        public ViewPublsiher()
        {
            InitializeComponent();
        }

        private void ViewPublsiher_Load(object sender, EventArgs e)
        {
            using (SqlConnection con = new SqlConnection(ConnectionUtility.ConString))
            {
                using (SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM publishers", con))
                {
                    DataTable dt1 = new DataTable();
                    da.Fill(dt1);
                    dataGridView1.DataSource = dt1;
                }
            }
        }
    }
}
