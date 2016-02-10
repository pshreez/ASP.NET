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
using System.Data.SqlClient;

public partial class TitleMaster : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Page.IsPostBack == false)
        {
            PageLoad();
        }
    }

    protected void PageLoad()
    {
        EnableDisableControls(false);
        EnableDisableButtons(true, false, false, false, false);
        BindTitle();
    }

    protected void EnableDisableControls(bool val)
    {
        txtTitleName.Enabled = val;
    }

    protected void EnableDisableButtons(bool newval, bool editval, bool saveval, bool delval, bool cancelval)
    {
        btnNew.Enabled = newval;
        btnEdit.Enabled = editval;
        btnSave.Enabled = saveval;
        btnDel.Enabled = delval;
        btnCancel.Enabled = cancelval;
    }
    protected void btnNew_Click(object sender, EventArgs e)
    {
        EnableDisableControls(true);
        EnableDisableButtons(false, false, true, false, true);
        txtTitleName.Focus();

        lblmsg.Text = "";
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Clear();
        PageLoad();
    }
    protected void Clear()
    {
        txtTitleName.Text = string.Empty;//""
        lbliID.Text = string.Empty;
        //lblmsg.Text = "";
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            //checking duplicacy in database
            string dupquery = "",id=string.Empty;
            dupquery = "select iID from tbltitlemaster where sstatus='A' "+
       " and sBookTitle='" + txtTitleName.Text.Trim().Replace("'", "''") + "'";
            id = Class1.GetString(dupquery);
            if (id != string.Empty)
            {
                lblmsg.Text = "Duplicate!! This title already exists!!";
                return;
            }
            //

            ////string connstring = ConfigurationManager.ConnectionStrings["connid"].ToString(); //ConfigurationManager.AppSettings["connid"];
            ////SqlConnection sqlcon = new SqlConnection(connstring);
            string query = "";
            if (lbliID.Text == string.Empty)
            {
                query = "insert into tblTitleMaster(sBookTitle,sUserID) " +
                    " values('" + txtTitleName.Text.Trim().Replace("'", "''") + "','1')";
            }
            else
            {
                query = "update tblTitleMaster set sBookTitle='" + txtTitleName.Text.Trim().Replace("'", "''") + "' where iID='" + lbliID.Text + "'";
            }
            ////SqlCommand sqlcom = new SqlCommand(query, sqlcon);
            //////sqlcom.CommandType = CommandType.Text;
            ////sqlcon.Open();

            ////sqlcom.ExecuteNonQuery();
            string retvalue= Class1.InsUpdDel(query);
            if (retvalue == "true")
            {
                lblmsg.Text = "Successfully saved!!";
                btnCancel_Click(sender, e);
            }
            else
            {
                lblmsg.Text = retvalue;
            }
        }
        catch (Exception ex)
        {
            lblmsg.Text = "Something gone wrong!! " + ex.Message.ToString();
        }
    }

    protected void BindTitle()
    {
        string sqlquery = string.Empty;
        sqlquery = @"select A.iID,sBookTitle,replace(convert(varchar,dtDateTime,106),' ','/') as
                    dtDateTime,B.sUserId,(case when A.sStatus='A' then 'Active' else 'Delete' end) as Status,A.sStatus from tbltitlemaster A left outer join tblLogin B
                    on A.sUserID=B.iID where A.sStatus in ('A','D')";

        //DataTable dt = new DataTable();
        //dt = Class1.datatable(sqlquery,gvTitle);
        Class1.datatable(sqlquery,gvTitle);

        //gvTitle.DataSource = dt;
        //gvTitle.DataBind();
    }
    protected void gvTitle_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        //e.NewPageIndex = true;
        gvTitle.PageIndex = e.NewPageIndex;
        BindTitle();
        //gvTitle.DataBind();
    }

    protected void chkselect_CheckedChange(object sender, EventArgs e)
    {
        string itemvalue = Request.Form["__EVENTTARGET"];

        string[] checkBox = itemvalue.Split('$');

        int chkboxlength = checkBox.Length;
        string chkboxvalue = checkBox[chkboxlength - 2];
        int index = Convert.ToInt32(chkboxvalue.Replace("ctl",""));

        //int index = int.Parse(checkBox[chkboxlength - 2].Substring(3, (checkBox[chkboxlength - 2].Length - 3)));

        int checkindex = 1;
        bool flag = false;
        foreach (GridViewRow gvrow in gvTitle.Rows)
        {
            checkindex++;
            CheckBox chkbox = (CheckBox)gvrow.FindControl("chkselect");
            if (checkindex != index)
            {

                chkbox.Checked =  false;

            }
            if (chkbox.Checked == true)
            {
                flag = true;
                Label lbltitle = (Label)gvrow.FindControl("lblgvTitle");
                Label lbliid = (Label)gvrow.FindControl("lblgvID");

                txtTitleName.Text = lbltitle.Text;
                lbliID.Text = lbliid.Text;

                EnableDisableButtons(false, true, false, true, true);
                //return;
            }
        }
        if (flag == false)
        {
            btnCancel_Click(sender, e);

        }
    }

    protected void btnEdit_Click(object sender, EventArgs e)
    {
        EnableDisableControls(true);
        EnableDisableButtons(false, false, true, false, true);
    }
    protected void btnDel_Click(object sender, EventArgs e)
    {
        string query = "";
        query = "update tbltitlemaster set sstatus='D' where iid=" + lbliID.Text + "";
        string returnvalue = Class1.InsUpdDel(query);
        if (returnvalue == "true")
        {
            lblmsg.Text = "Successfully deleted!!";
            btnCancel_Click(sender, e);
        }
        else
        {
            lblmsg.Text = returnvalue;
        }
    }
    protected void gvTitle_RowEditing(object sender, GridViewEditEventArgs e)
    {
        gvTitle.EditIndex = e.NewEditIndex;
        GridViewRow gvrow = (GridViewRow)gvTitle.Rows[e.NewEditIndex];
        Label lblstatus = (Label)gvrow.FindControl("lblgvStatus");
        DropDownList ddlstatus = (DropDownList)gvrow.FindControl("ddlgvStatus");
        //if (lblstatus.Text == "Delete")
        //    ddlstatus.SelectedValue = "D";
        BindTitle();
    }
    protected void gvTitle_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        GridViewRow gvrow = (GridViewRow)gvTitle.Rows[e.RowIndex];

        Label lblid = (Label)gvrow.FindControl("lblgvID");
        TextBox txttitle = (TextBox)gvrow.FindControl("txtgvTitle");
        DropDownList ddlstatus = (DropDownList)gvrow.FindControl("ddlgvStatus");

        string query = "";
        query = "update tblTitleMaster set sBookTitle='" + txttitle.Text.Trim().Replace("'", "''") + "',sstatus='" + ddlstatus.SelectedValue + "' where iID='" + lblid.Text + "'";
        string retvalue = Class1.InsUpdDel(query);
        if (retvalue == "true")
        {
            gvTitle.EditIndex = -1;
            BindTitle();
        }
        else
        {
            lblmsg.Text = retvalue;
        }
    }
    protected void gvTitle_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        gvTitle.EditIndex = -1;
        BindTitle();
    }
    protected void gvTitle_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void gvTitle_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        GridViewRow gvrow = (GridViewRow)gvTitle.Rows[e.RowIndex];
        Label lblid = (Label)gvrow.FindControl("lblgvID");
        

        string query = "";
        query = "update tblTitleMaster set sstatus='D' where iID='" + lblid.Text + "'";
        string retvalue = Class1.InsUpdDel(query);
        if (retvalue == "true")
        {
            //gvTitle.EditIndex = -1;
            BindTitle();
        }
        else
        {
            lblmsg.Text = retvalue;
        }
    }
    protected void gvTitle_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        //if (e.Row.RowType == DataControlRowType.DataRow)
        //{
        //    DropDownList ddl = (DropDownList)e.Row.FindControl("ddlgvStatus");
        //    Label lblEmpid = ((Label)e.Row.FindControl("lblgvStatus"));
        //    ddl.SelectedValue = "D";
        //}
    }
}
