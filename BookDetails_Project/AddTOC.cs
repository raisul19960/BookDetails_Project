using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BookDetails_Project
{
    public partial class AddTOC : Form
    {
        public AddTOC()
        {
            InitializeComponent();
        }

        private void AddTOC_Load(object sender, EventArgs e)
        {
            GetId();
            LoadCombo();
        }

        private void GetId()
        {
            using (SqlConnection con = new SqlConnection(ConnectionUtility.ConString))
            {
                using (SqlCommand cmd = new SqlCommand("SELECT ISNULL(MAX(tocid), 0) FROM TOCs", con))
                {
                    
                    con.Open();
                    int i = (int)cmd.ExecuteScalar();
                    
                    con.Close();
                    this.textBox1.Text= (i + 1).ToString();
                }
            }
            
        }

        private void LoadCombo()
        {
            using (SqlConnection con = new SqlConnection(ConnectionUtility.ConString))
            {
                using (SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM books", con))
                {
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    comboBox1.DataSource = dt;
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (SqlConnection con = new SqlConnection(ConnectionUtility.ConString))
            {
                using (SqlCommand cmd = new SqlCommand(@"INSERT INTO TOCs
                                                     VALUES(@i, @bi, @n, @t, @p)", con))
                {
                    cmd.Parameters.AddWithValue("@i", textBox1.Text);
                    cmd.Parameters.AddWithValue("@bi", comboBox1.SelectedValue);
                    cmd.Parameters.AddWithValue("@n", numericUpDown1.Value);
                    cmd.Parameters.AddWithValue("@t", textBox2.Text);
                    cmd.Parameters.AddWithValue("@p", numericUpDown1.Value);
                    con.Open();
                    
                    if (cmd.ExecuteNonQuery()>0)
                    {
                        MessageBox.Show("Data Saved", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    }
                    else
                    {
                        MessageBox.Show($"Failed to save", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    con.Close();
                }
            }
        }
    }
}
