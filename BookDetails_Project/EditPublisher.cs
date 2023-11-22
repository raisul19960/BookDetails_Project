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
    public partial class EditPublisher : Form
    {
        public EditPublisher()
        {
            InitializeComponent();
        }

        private void EditPublisher_Load(object sender, EventArgs e)
        {
            LoadCombo();
        }
        private void LoadCombo()
        {
            using (SqlConnection con = new SqlConnection(ConnectionUtility.ConString))
            {
                using (SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM publishers", con))
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
                using (SqlCommand cmd = new SqlCommand("SELECT * FROM publishers WHERE publisherid =@i", con))
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
                using (SqlCommand cmd = new SqlCommand(@"UPDATE publishers 
                        SET publishername=@n
                        WHERE publisherid =@i", con))
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
                using (SqlCommand cmd = new SqlCommand(@"DELETE publishers 
                        WHERE publisherid =@i", con))
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
