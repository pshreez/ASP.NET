using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using CrystalDecisions.CrystalReports.Engine;


public partial class crystrep : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //Binddata();
    }

    protected void Binddata()
    {
        DataSet1TableAdapters.tblBooksRecordTableAdapter tbladp = new DataSet1TableAdapters.tblBooksRecordTableAdapter();
        ReportDocument rptdoc = new ReportDocument();
        rptdoc.Load(Server.MapPath("~/CrystalReport.rpt"));
        rptdoc.SetDataSource((DataTable)tbladp.GetData(Convert.ToInt64(TextBox1.Text.Trim())));
        CrystalReportViewer1.ReportSource = rptdoc;
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        Binddata();
    }
}
