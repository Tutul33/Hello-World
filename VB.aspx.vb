Imports System.Data
Imports System.Configuration
Imports System.Data.SqlClient
Imports CrystalDecisions.CrystalReports.Engine

Partial Class VB
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim crystalReport As New ReportDocument()
        crystalReport.Load(Server.MapPath("~/CustomerReport.rpt"))
        Dim dsCustomers As Customers = GetData("select * from customers")
        crystalReport.SetDataSource(dsCustomers)
        CrystalReportViewer1.ReportSource = crystalReport
    End Sub

    Private Function GetData(query As String) As Customers
        Dim conString As String = ConfigurationManager.ConnectionStrings("constr").ConnectionString
        Dim cmd As New SqlCommand(query)
        Using con As New SqlConnection(conString)
            Using sda As New SqlDataAdapter()
                cmd.Connection = con

                sda.SelectCommand = cmd
                Using dsCustomers As New Customers()
                    sda.Fill(dsCustomers, "DataTable1")
                    Return dsCustomers
                End Using
            End Using
        End Using
    End Function
End Class
