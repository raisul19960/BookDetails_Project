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
    public partial class EditCategory : Form
    {
        public EditCategory()
        {
            InitializeComponent();
        }

        private void EditCategory_Load(object sender, EventArgs e)
        {
            LoadCombo();
        }
        private void LoadCombo()
        {
            using (SqlConnection con = new SqlConnection(ConnectionUtility.ConString))
            {
                using (SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM categories", con))
                {
                    DataTable dt1 = new DataTable();
                    da.Fill(dt1);

                    comboBox1.DataSource = dt1;

                }
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            using (SqlConnection con = new SqlConnection(ConnectionUtility.ConString))
            {
                using (SqlCommand cmd = new SqlCommand("SELECT * FROM categories WHERE categoryid =@i", con))
                {

                    cmd.Parameters.AddWithValue("@i", this.comboBox1.SelectedValue);
                    con.Open();
                    var dr = cmd.ExecuteReader();
                    if (dr.Read())
                    {
                        textBox2.Text = dr.GetSqlString(1).ToString();


                    }
                    con.Close();
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (SqlConnection con = new SqlConnection(ConnectionUtility.ConString))
            {
                using (SqlCommand cmd = new SqlCommand(@"UPDATE categories 
                        SET categoryname=@n
                        WHERE categoryid =@i", con))
                {

                    cmd.Parameters.AddWithValue("@n", textBox2.Text);
                    cmd.Parameters.AddWithValue("@i", comboBox1.SelectedValue);

                    con.Open();

                    if (cmd.ExecuteNonQuery() > 0)
                    {
                        MessageBox.Show("Data Saved", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        con.Close();
                        LoadCombo();
                    }
                    else
                    {
                        MessageBox.Show($"Failed to save", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    if (con.State == ConnectionState.Open) con.Close();

                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            using (SqlConnection con = new SqlConnection(ConnectionUtility.ConString))
            {
                using (SqlCommand cmd = new SqlCommand(@"DELETE categories 
                        WHERE categoryid =@i", con))
                {


                    cmd.Parameters.AddWithValue("@i", comboBox1.SelectedValue);

                    con.Open();

                    if (cmd.ExecuteNonQuery() > 0)
                    {
                        MessageBox.Show("Data deleted", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        con.Close();
                        LoadCombo();
                    }
                    else
                    {
                        MessageBox.Show($"Failed to delete", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    if (con.State == ConnectionState.Open) con.Close();

                }
            }
        }
    }
}
