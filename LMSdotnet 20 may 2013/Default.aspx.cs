using System;
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
using System.Data.SqlClient;

public partial class _Default : System.Web.UI.Page 
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Page.IsPostBack == false)
        {
            txtUserId.Focus();
        }
    }
    protected void btnLogIn_Click(object sender, EventArgs e)
    {
        try
        {
            //string connstring = ConfigurationManager.AppSettings["connid"];
            //SqlConnection sqlcon = new SqlConnection(connstring);

            string sqlquery = "";
            sqlquery = "select sPassword from tblLogin where sStatus='A' and sUserId='" + txtUserId.Text + "'";

            //SqlCommand sqlcom = new SqlCommand(sqlquery, sqlcon);
            //sqlcom.CommandType = CommandType.Text;
            //sqlcon.Open();
            //SqlDataReader sqldr = sqlcom.ExecuteReader();


            //while (sqldr.Read())
            //{

            //}
            //sqldr.Read();
            //string pwd = "";
            //try
            //{
            //    pwd = sqldr["sPassword"].ToString();
            //}
            //catch
            //{
            //    lblmsg.Text = "Invalid User Id!!!";
            //    return;
            //}

            string pwd = "";
            pwd = Class1.GetString(sqlquery);
            //string[] field = { "@userid" };
            //string[] row={txtUserId.Text.Trim()};
            //pwd = Class1.FindStringfromprocedure("authorizepassword", field, row);
            if (pwd == "")
            {
                lblmsg.Text = "Invalid User Id!!!";
                return;
            }
            if (pwd == txtPassword.Text)
            {
                Response.Redirect("Home.aspx");
            }
            else
            {
                lblmsg.Text = "Invalid password!!";
                txtPassword.Focus();
            }
        }
        catch (Exception ex)
        {
            lblmsg.Text = "Something gone wrong!! " + ex.Message.ToString();
        }

    }
}
