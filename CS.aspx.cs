using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;
using CrystalDecisions.CrystalReports.Engine;

public partial class CS : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        ReportDocument crystalReport = new ReportDocument();
        crystalReport.Load(Server.MapPath("~/CustomerReport.rpt"));
        Customers dsCustomers = GetData("select * from customers");
        crystalReport.SetDataSource(dsCustomers);
        CrystalReportViewer1.ReportSource = crystalReport;
    }

    private Customers GetData(string query)
    {
        string conString = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
        SqlCommand cmd = new SqlCommand(query);
        using (SqlConnection con = new SqlConnection(conString))
        {
            using (SqlDataAdapter sda = new SqlDataAdapter())
            {
                cmd.Connection = con;

                sda.SelectCommand = cmd;
                using (Customers dsCustomers = new Customers())
                {
                    sda.Fill(dsCustomers, "DataTable1");
                    return dsCustomers;
                }
            }
        }
    }
}