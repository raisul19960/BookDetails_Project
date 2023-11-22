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
    public partial class EditTOC : Form
    {
        public EditTOC()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (SqlConnection con = new SqlConnection(ConnectionUtility.ConString))
            {
                using (SqlCommand cmd = new SqlCommand(@"UPDATE TOCs 
                        SET bookid=@bi,chapterno=@n, chaptertitle=@t,totalpages=@p
                        WHERE tocid =@i", con))
                {
                    cmd.Parameters.AddWithValue("@i", comboBox2.SelectedValue);
                    cmd.Parameters.AddWithValue("@bi", comboBox1.SelectedValue);
                    cmd.Parameters.AddWithValue("@n", numericUpDown1.Value);
                    cmd.Parameters.AddWithValue("@t", textBox2.Text);
                    cmd.Parameters.AddWithValue("@p", numericUpDown1.Value);
                    con.Open();

                    if (cmd.ExecuteNonQuery() > 0)
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
        private void LoadCombo()
        {
            using (SqlConnection con = new SqlConnection(ConnectionUtility.ConString))
            {
                using (SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM books", con))
                {
                    DataTable dt1 = new DataTable();
                    da.Fill(dt1);

                    comboBox1.DataSource = dt1;
                    da.SelectCommand.CommandText = "SELECT * FROM TOCs";
                    DataTable dt2 = new DataTable();
                    da.Fill(dt2);

                    comboBox2.DataSource = dt2;
                }
            }
        }

        private void EditTOC_Load(object sender, EventArgs e)
        {
            LoadCombo();
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            using (SqlConnection con = new SqlConnection(ConnectionUtility.ConString))
            {
                using (SqlCommand cmd = new SqlCommand("SELECT * FROM TOCs WHERE tocid =@i", con))
                {
                    var x = this.comboBox2.SelectedValue;
                    cmd.Parameters.AddWithValue("@i", this.comboBox2.SelectedValue);
                    con.Open();
                    var dr = cmd.ExecuteReader();
                    if (dr.Read())
                    {
                        comboBox1.SelectedValue = dr.GetInt32(1).ToString();
                        numericUpDown1.Value = dr.GetInt32(2);
                        textBox2.Text = dr.GetString(3);
                        numericUpDown2.Value = dr.GetInt32(4);

                    }
                    con.Close();
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //using (SqlConnection con = new SqlConnection(ConnectionUtility.ConString))
            //{
            //    using (SqlCommand cmd = new SqlCommand("DELETE FROM TOCs WHERE tocid =@i", con))
            //    {
            //        cmd.Parameters.AddWithValue("@i", this.comboBox2.SelectedValue);
            //        con.Open();
                    
            //        if (cmd.ExecuteNonQuery()>0)
            //        {
            //            con.Close();
            //            LoadCombo();

            //        }
            //        else
            //        {
            //            con.Close();
            //        }
                    
            //    }
            //}
        }
    }
}
