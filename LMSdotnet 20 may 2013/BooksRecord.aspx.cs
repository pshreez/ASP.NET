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
using System.Data.SqlClient;
using System.Text;
using System.IO;

public partial class BooksRecord : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            //BindTitle();
            //BindAuthor();
            //Session["iidfirst"] = 0;
            //Session["iidlast"] = 0;
            //Session["rowno"] = 1;
            //pagination();
            btnPrevious.Enabled = false;

            if (Request.QueryString.Get("bookid") == null)
            {
                PageLoad();
                BindTitle();
                BindAuthor();
                BindDayndYear();
            }
            else
            {
                PageLoad();
                BindTitle();
                BindAuthor();
                BindDayndYear();

                string bookid = Request.QueryString.Get("bookid");
                string query = string.Empty;
                query = @"select replace(convert(varchar,dtDateTime,106),' ','/') as entrydate,
                replace(convert(varchar,dtPurchaseDate,106),' ','/') as PurchaseDate,iTitleID,sAuthorIDs,
                sPublisherName,sISBNNumber,sIssueType,sBookCondition
                from tblBooksRecord where ibookid=" + bookid + "";
                string[] bookdetails = Class1.stringarray(query, 8);

                txtBookID.Text = bookid;
                //lblentrydate.Text = "Entry Date : " + bookdetails[0].ToString();
                txtPurchaseDate.Text = bookdetails[1].ToString();
                ddlTitle.SelectedValue = bookdetails[2].ToString();
                string[] authorids = bookdetails[3].Split(',');
                //ddlAuthorlist.Enabled = true;
                for (int i = 0; i < authorids.Length - 1; i++)
                {
                    ddlAuthorlist.SelectedValue = authorids[i];
                    lstboxAuthor.Items.Add(new ListItem(ddlAuthorlist.SelectedItem.Text, ddlAuthorlist.SelectedValue));
                }
                txtPublisher.Text = bookdetails[4].ToString();
                txtISBN.Text = bookdetails[5].ToString();
                ddlissuetype.SelectedValue = bookdetails[6].ToString();
                ddlBookCondition.SelectedValue = bookdetails[7].ToString();

                EnableDisableButtons(false, true, false, true, false);
            }
            //Session["authorids"] = string.Empty;
        }
    }

    protected void pagination()
    {
        double totalcount = 0;
        string sqlquery = string.Empty;
        sqlquery = "select count(ibookid) as bookcount from tblBooksRecord A left outer join tblTitleMaster B on A.iTitleID=B.iID where 1=1 ";
        if (txtsrchBookID.Text.Trim() != string.Empty)
        {
            sqlquery += " and iBookID like '" + txtsrchBookID.Text.Trim() + "%' ";
        }
        if (ddlsrchBookTitle.SelectedIndex > 0)
        {
            sqlquery += " and A.iTitleID = '" + ddlsrchBookTitle.SelectedValue + "' ";
        }
        if (txtsrchBookTitle.Text.Trim() != string.Empty)
        {
            sqlquery += " and B.sBookTitle like '" + txtsrchBookTitle.Text.Trim() + "%' ";
        }
        //sqlquery += " order by A.dtDateTime desc ";
        totalcount = Convert.ToInt32(Class1.GetString(sqlquery));
        
        double i = 0;
        i = totalcount / 3;

        if (i > Convert.ToInt32(i))
        {
            i = Convert.ToInt32(i) + 1;
        }
        lblpaginationincr.Text = "1";
        lblpaginationtot.Text = i.ToString();

        //Session["isndexx"] = string.Empty;
        Session["isndexx"] = new string[Convert.ToInt32(lblpaginationtot.Text) + 1];
        ((string[])Session["isndexx"])[0] = "0";
        //Session["isndexx"] = "0";
    }

    protected void PageLoad()
    {
        EnableDisableControls(false);
        //ddlAuthorlist.Enabled = true;
        EnableDisableButtons(true, false, false, true, false);
        
        Session["authorids"] = string.Empty;
        //ShowBooksRecord();
        Session["iidfirst"] = 0;
        Session["iidlast"] = 0;
        Session["rowno"] = 1;
        pagination();
        ShowBooksRecord();
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


    protected void ShowBooksRecord()
    {
        string strcondition = string.Empty;
        showdata.Text = "No record found!!";
        Session["rowcount"] = 0;
        //if (strcondition == "gt")
        //{
            strcondition = ((string[])Session["isndexx"])[Convert.ToInt32(lblpaginationincr.Text) - 1];
        //}
        //else
        //{
        //    strcondition = Session["iidfirst"].ToString();
        //}
        //lblpagination.Text = "";
        try
        {
            StringBuilder strbuild = new StringBuilder("");
            SqlConnection sqlcon = new SqlConnection(Class1.connstring);
            string query = string.Empty;
            query = @"select top 3 iBookID,replace(convert(varchar,A.dtDateTime,106),' ','/') as entrydate,
                replace(convert(varchar,dtPurchaseDate,106),' ','/') as PurchaseDate,B.sBookTitle,sAuthorIDs,
                sPublisherName,sISBNNumber,sIssueType,sBookCondition,sBookLogoName
                from tblBooksRecord A left outer join tblTitleMaster B on A.iTitleID=B.iID where ibookid > " + strcondition + " ";
            if (txtsrchBookID.Text.Trim() != string.Empty)
            {
                query += " and iBookID like '" + txtsrchBookID.Text.Trim() + "%' ";
            }
            if (ddlsrchBookTitle.SelectedIndex > 0)
            {
                query += " and A.iTitleID = '" + ddlsrchBookTitle.SelectedValue + "' ";
            }
            if (txtsrchBookTitle.Text.Trim() != string.Empty)
            {
                query += " and B.sBookTitle like '" + txtsrchBookTitle.Text.Trim() + "%' ";
            }
            query += " order by A.iBookID asc ";
            SqlCommand sqlcom = new SqlCommand(query, sqlcon);
            //sqlcom.CommandType = CommandType.Text;
            sqlcon.Open();
            SqlDataReader sqldr = sqlcom.ExecuteReader();
            if (sqldr.HasRows)
            {
                strbuild.Append("<table border='1'><tr><td>S.No.</td><td>Book ID</td><td>Entry Date</td><td>Pur Date</td><td>Book Title</td><td>Authors</td><td>Publisher</td><td>ISBN</td><td>Issue Type</td><td>Book Condition</td><td>Book Logo</td></tr>");
                //Session["rowno"] = 1;
                while (sqldr.Read())
                {
                    //if (Convert.ToInt32(Session["rowno"]) % 4 == 0 || Session["rowno"].ToString() == "1")
                    //{
                    //    Session["iidfirst"] = sqldr[0].ToString();
                    //}

                    strbuild.Append("<tr>");
                    strbuild.Append("<td>" + Session["rowno"] + "</td>");
                    strbuild.Append("<td>" + sqldr[0].ToString() + "</td>");
                    strbuild.Append("<td>" + sqldr[1].ToString() + "</td>");
                    strbuild.Append("<td>" + sqldr[2].ToString() + "</td>");
                    strbuild.Append("<td>" + sqldr[3].ToString() + "</td>");
                    
                    string authorsname = "";
                    if (sqldr[4].ToString() != string.Empty)
                    {
                        string[] authors = Class1.stringrowarray("select sAuthorName from tblAuthorMaster where iID in (" + sqldr[4].ToString().Substring(0, sqldr[4].ToString().Length - 1) + ")", sqldr[4].ToString().Split(',').Length - 1);//sqldr[4].ToString().Substring(0, sqldr[4].ToString().Length - 1).ToString().Split(',').Length);
                        authorsname = authors[0];
                        for (int i = 1; i < authors.Length; i++)
                        {
                            authorsname += "," + authors[i];
                        }
                    }
                    strbuild.Append("<td>" + authorsname + "</td>");
                    strbuild.Append("<td>" + sqldr[5].ToString() + "</td>");
                    strbuild.Append("<td>" + sqldr[6].ToString() + "</td>");
                    strbuild.Append("<td>" + sqldr[7].ToString() + "</td>");
                    strbuild.Append("<td>" + sqldr[8].ToString() + "</td>");
                    strbuild.Append("<td><img height='100px' width='100px' src='BooksLogo/" + sqldr[9].ToString() + "' /></td>");
                    strbuild.Append("<td><input type='button' id='btnEdit' name='btnEdit' value='Edit' onclick='openeditwindow(" + sqldr[0].ToString() + ",\"" + sqldr[5].ToString() + "\");' /></td>");
                    strbuild.Append("</tr>");

                    ((string[])Session["isndexx"])[Convert.ToInt32(lblpaginationincr.Text)] = sqldr[0].ToString();
                    //string dfdf = Session["iid"].ToString();
                    Session["rowno"] = Convert.ToInt32(Session["rowno"]) + 1;
                    Session["rowcount"] = Convert.ToInt32(Session["rowcount"]) + 1;

                }
                sqldr.Dispose();
                    sqlcom.Dispose();
                    sqlcon.Close();
                strbuild.Append("</table>");
                showdata.Text = strbuild.ToString();
            }
        }
        catch
        {
            

        }
        
    }

    protected void EnableDisableControls(bool val)
    {
        txtBookID.Enabled = val;
        ddlTitle.Enabled = val;
        txtPurchaseDate.Enabled = val;
        ddlAuthorlist.Enabled = val;
        lstboxAuthor.Enabled = val;
        txtPublisher.Enabled = val;
        txtPubEmail.Enabled = val;
        txtISBN.Enabled = val;
        ddlissuetype.Enabled = val;
        ddlBookCondition.Enabled = val;
        btnRemove.Enabled = val;
        fupBookLogo.Enabled = val;
    }

    protected void Clear()
    {
        txtBookID.Text="";
        ddlTitle.SelectedIndex = 0;
        txtPurchaseDate.Text="";
        ddlAuthorlist.SelectedIndex = 0;
        lstboxAuthor.Items.Clear();
        txtPublisher.Text="";
        txtPubEmail.Text="";
        txtISBN.Text="";
        ddlissuetype.SelectedIndex = 0;
        ddlBookCondition.SelectedIndex = 0;
       // Session["authorids"] = string.Empty;
        //btnRemove.Enabled = false;
        //lblmsg.Text = "";
    }

    protected void EnableDisableButtons(bool newval, bool editval, bool saveval, bool delval, bool cancelval)
    {
        btnNew.Enabled = newval;
        btnEdit.Enabled = editval;
        btnSave.Enabled = saveval;
        btnDel.Disabled = delval;
        btnCancel.Disabled = cancelval;
    }


    protected void BindTitle()
    {
        string query = string.Empty;
        query = "select iid,sbooktitle from tbltitlemaster where sstatus='A'";
        Class1.BindInDDL(query, "iid", "sbooktitle", ddlTitle);
        Class1.BindInDDL(query, "iid", "sbooktitle", ddlsrchBookTitle);
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
        //if (hdnflag.Value == "validated")
        //{
            try
            {
                //checking duplicacy in database
           //////     string dupquery = "", id = string.Empty;
           //////     dupquery = "select iID from tbltitlemaster where sstatus='A' " +
           //////" and sBookTitle='" + txtTitleName.Text.Trim().Replace("'", "''") + "'";
           //////     id = Class1.GetString(dupquery);
           //////     if (id != string.Empty)
           //////     {
           //////         lblmsg.Text = "Duplicate!! This title already exists!!";
           //////         return;
           //////     }
                //

                ////string connstring = ConfigurationManager.ConnectionStrings["connid"].ToString(); //ConfigurationManager.AppSettings["connid"];
                ////SqlConnection sqlcon = new SqlConnection(connstring);
                string query = "";
                //////if (lbliID.Text == string.Empty)
                //////{
                string authorids = string.Empty;
                for (int i = 0; i < lstboxAuthor.Items.Count; i++)
                {
                    authorids += lstboxAuthor.Items[i].Value + ",";
                }

                DateTime dtpurchasedate1 = DateTime.ParseExact(txtPurchaseDate.Text.Trim(), "dd/MMM/yyyy", CultureInfo.InvariantCulture);
                DateTime dtpurchasedate2 = DateTime.ParseExact(ddlday.SelectedValue + "/" + ddlmonth.SelectedValue + "/" + ddlyear.SelectedValue, "dd/MM/yyyy", CultureInfo.InvariantCulture);

                string sessdfdfd = Session["authorids"].ToString();
                string filename = string.Empty;
                if (fupBookLogo.HasFile)
                {
                    string fileextension = string.Empty;
                    fileextension = Path.GetExtension(fupBookLogo.FileName);
                    filename = System.DateTime.Now.ToString("MM/dd/yyyy hh:mm:ss:ms").Replace("/", "").Replace(" ", "").Replace(":", "") + fileextension;
                }
                //return;
                query = "insert into tblBooksRecord(iBookID,dtPurchaseDate,iTitleID,sAuthorIDs,sPublisherName,sISBNNumber,sIssueType,sBookCondition,sBookLogoName) " +
                        " values('" + txtBookID.Text.Trim().Replace("'", "''") + "','" + dtpurchasedate1.ToString("MM/dd/yyyy") + "','" + ddlTitle.SelectedValue + "','" + authorids + "','" + txtPublisher.Text.Trim().Replace("'", "''") + "','" + txtISBN.Text.Trim().Replace("'", "''") + "','" + ddlissuetype.SelectedValue + "','" + ddlBookCondition.SelectedValue + "','" + filename + "')";

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
                    if (fupBookLogo.HasFile)
                    {
                        string savingpath = Server.MapPath("BooksLogo/") + filename;
                        fupBookLogo.SaveAs(savingpath);
                    }
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
        //}
        //divprogress.Style.Add("visibility", "hidden");
        //divmain.Style.Add("visibility", "visible");
    }
    protected void btnNew_Click(object sender, EventArgs e)
    {
        EnableDisableControls(true);
        EnableDisableButtons(false, false, true, true, false);
        txtBookID.Focus();

        lblmsg.Text = "";
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        //Clear();
        PageLoad();
        Clear();
    }

    protected void btnDel_clik(object sender, EventArgs e)
    {
 
    }
    //protected void btnSearch_Click(object sender, EventArgs e)
    //{
    //    ShowBooksRecord();
    //}
    protected void txtsrchBookID_TextChanged(object sender, EventArgs e)
    {
        
        ShowBooksRecord();
        pagination();
    }
    protected void btnNext_Click(object sender, EventArgs e)
    {
        lblpaginationincr.Text =(Convert.ToInt32(lblpaginationincr.Text) + 1).ToString();
        ShowBooksRecord();
        if (Convert.ToInt32(lblpaginationincr.Text) == Convert.ToInt32(lblpaginationtot.Text))
        {
            btnNext.Enabled = false;
            btnPrevious.Enabled = true;
        }
        else
        {
            btnNext.Enabled = true;
            btnPrevious.Enabled = true;
        }
    }
    protected void btnPrevious_Click(object sender, EventArgs e)
    {
        Session["rowno"] = Convert.ToInt32(Session["rowno"]) - (3 + Convert.ToInt32(Session["rowcount"]));
        lblpaginationincr.Text = (Convert.ToInt32(lblpaginationincr.Text) - 1).ToString();
        ShowBooksRecord();

        if (Convert.ToInt32(lblpaginationincr.Text) !=1)
        {
            btnNext.Enabled = true;
            btnPrevious.Enabled = true;
        }
        else
        {
            btnNext.Enabled = true;
            btnPrevious.Enabled = false;
        }
    }
}
