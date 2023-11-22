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

namespace BookDetails_Project.Reports
{
    public partial class RptForm1 : Form
    {
        public RptForm1()
        {
            InitializeComponent();
        }

        private void RptForm1_Load(object sender, EventArgs e)
        {
            DataSet ds = new DataSet();
            using (SqlConnection con = new SqlConnection(ConnectionUtility.ConString))
            {
                using (SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM books", con))
                {
                    da.Fill(ds, "books");
                    ds.Tables["books"].Columns.Add(new DataColumn("image", typeof(System.Byte[])));
                    //for (var i = 0; i < ds.Tables["booksi"].Rows.Count; i++)
                    //{
                    //    ds.Tables["booksi"].Rows[i]["image"] = File.ReadAllBytes(Path.Combine(Path.GetFullPath(@"..\..\Pictures"), ds.Tables["booksi"].Rows[i]["coverpage"].ToString()));
                    //}
                    da.SelectCommand.CommandText = "SELECT * FROM TOCs";
                    da.Fill(ds, "TOCs");
                    CrystalReport2 rpt = new CrystalReport2();
                    rpt.SetDataSource(ds);
                    crystalReportViewer1.ReportSource = rpt;
                    rpt.Refresh();
                    crystalReportViewer1.Refresh();
                }
            }
        }
    }
}
