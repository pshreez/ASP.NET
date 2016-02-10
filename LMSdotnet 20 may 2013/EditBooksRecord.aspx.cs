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
using System.Globalization;

public partial class EditBooksRecord : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            PageLoad();
            BindTitle();
            BindAuthor();
            BindDayndYear();

            string bookid = Request.QueryString.Get("id");
            string query = string.Empty;
            query = @"select replace(convert(varchar,dtDateTime,106),' ','/') as entrydate,
                replace(convert(varchar,dtPurchaseDate,106),' ','/') as PurchaseDate,iTitleID,sAuthorIDs,
                sPublisherName,sISBNNumber,sIssueType,sBookCondition
                from tblBooksRecord where ibookid=" + bookid + "";
            string[] bookdetails = Class1.stringarray(query, 8);

            txtBookID.Text = bookid;
            lblentrydate.Text = "Entry Date : " + bookdetails[0].ToString();
            txtPurchaseDate.Text = bookdetails[1].ToString();
            ddlTitle.SelectedValue = bookdetails[2].ToString();
            string[] authorids = bookdetails[3].Split(',');
            for (int i = 0; i < authorids.Length - 1; i++)
            {
                ddlAuthorlist.SelectedValue = authorids[i];
                lstboxAuthor.Items.Add(new ListItem(ddlAuthorlist.SelectedItem.Text, ddlAuthorlist.SelectedValue));
            }
            txtPublisher.Text = bookdetails[4].ToString();
            txtISBN.Text = bookdetails[5].ToString();
            ddlissuetype.SelectedValue = bookdetails[6].ToString();
            ddlBookCondition.SelectedValue = bookdetails[7].ToString();

            //BindTitle();
            //BindAuthor();
            
            //Session["authorids"] = string.Empty;
        }
    }

    protected void PageLoad()
    {
        
        Session["authorids"] = string.Empty;
        
    }

    protected void BindDayndYear()
    {
        for (int i = 1; i <= 31; i++)
        {
            string val = string.Empty;
            if (i.ToString().Length == 1)
            {
                val = "0";
            }
            ddlday.Items.Add(new ListItem(val + i.ToString()));
        }

        for (int i = 1900; i <= 2020; i++)
        {
            ddlyear.Items.Add(i.ToString());
        }
    }

    protected void BindTitle()
    {
        string query = string.Empty;
        query = "select iid,sbooktitle from tbltitlemaster where sstatus='A'";
        Class1.BindInDDL(query, "iid", "sbooktitle", ddlTitle);
    }

    protected void BindAuthor()
    {
        string query = string.Empty;
        query = "select iid,sauthorname from tblauthormaster where sstatus='A'";
        Class1.BindInDDL(query, "iid", "sauthorname", ddlAuthorlist);
    }
    protected void ddlAuthorlist_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblmsg.Text = "";
        if (ddlAuthorlist.SelectedIndex > 0)
        {
            for (int i = 0; i < lstboxAuthor.Items.Count; i++)
            {
                if (lstboxAuthor.Items[i].Value == ddlAuthorlist.SelectedValue)
                {
                    lblmsg.Text = "This Author already listed!!";
                    return;
                }
            }
            lstboxAuthor.Items.Add(new ListItem(ddlAuthorlist.SelectedItem.Text, ddlAuthorlist.SelectedValue));
            Session["authorids"] = Session["authorids"].ToString() + ddlAuthorlist.SelectedValue + ",";
        }
    }
    protected void btnRemove_Click(object sender, EventArgs e)
    {
        if (lstboxAuthor.SelectedIndex >= 0)
        {
            Session["authorids"] = Session["authorids"].ToString().Replace((lstboxAuthor.SelectedValue + ","), "");
            lstboxAuthor.Items.RemoveAt(lstboxAuthor.SelectedIndex);

            string dfdfd = Session["authorids"].ToString();
            ddlAuthorlist.SelectedIndex = 0;
        }

    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        //ScriptManager.RegisterStartupScript(Page, typeof(string), "hello", "closepage();", true);
        //return;
        try
        {
            string query = "";
            string authorids = string.Empty;
            for (int i = 0; i < lstboxAuthor.Items.Count; i++)
            {
                authorids += lstboxAuthor.Items[i].Value + ",";
            }

            DateTime dtpurchasedate1 = DateTime.ParseExact(txtPurchaseDate.Text.Trim(), "dd/MMM/yyyy", CultureInfo.InvariantCulture);
            DateTime dtpurchasedate2 = DateTime.ParseExact(ddlday.SelectedValue + "/" + ddlmonth.SelectedValue + "/" + ddlyear.SelectedValue, "dd/MM/yyyy", CultureInfo.InvariantCulture);

            string sessdfdfd = Session["authorids"].ToString();
            //query = "insert into tblBooksRecord(iBookID,dtPurchaseDate,iTitleID,sAuthorIDs,sPublisherName,sISBNNumber,sIssueType,sBookCondition) " +
                    //" values('" + txtBookID.Text.Trim().Replace("'", "''") + "','" + dtpurchasedate1.ToString("MM/dd/yyyy") + "','" + ddlTitle.SelectedValue + "','" + authorids + "','" + txtPublisher.Text.Trim().Replace("'", "''") + "','" + txtISBN.Text.Trim().Replace("'", "''") + "','" + ddlissuetype.SelectedValue + "','" + ddlBookCondition.SelectedValue + "')";
            query = "update tblBooksRecord set iBookID='" + txtBookID.Text.Trim().Replace("'", "''") + "',dtPurchaseDate='" + dtpurchasedate1.ToString("MM/dd/yyyy") + "',iTitleID='" + ddlTitle.SelectedValue + "',sAuthorIDs='" + authorids + "',sPublisherName='" + txtPublisher.Text.Trim().Replace("'", "''") + "',sISBNNumber='" + txtISBN.Text.Trim().Replace("'", "''") + "',sIssueType='" + ddlissuetype.SelectedValue + "',sBookCondition='" + ddlBookCondition.SelectedValue + "' where ibookid='" + Request.QueryString.Get("id") + "'";
            ////}
            ////else
            ////{
            ////    query = "update tblTitleMaster set sBookTitle='" + txtTitleName.Text.Trim().Replace("'", "''") + "' where iID='" + lbliID.Text + "'";
            ////}
            ////SqlCommand sqlcom = new SqlCommand(query, sqlcon);
            //////sqlcom.CommandType = CommandType.Text;
            ////sqlcon.Open();

            ////sqlcom.ExecuteNonQuery();
            string retvalue = Class1.InsUpdDel(query);
            if (retvalue == "true")
            {
                //lblmsg.Text = "Successfully saved!!";
                //btnCancel_Click(sender, e);
                //ScriptManager.RegisterClientScriptBlock(Page, typeof(string), "hello", "closepage();", true);
                ClientScript.RegisterStartupScript(GetType(), "aa", "<script>closepage();</script>");
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
}
