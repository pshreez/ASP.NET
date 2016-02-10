using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Data.SqlClient;

/// <summary>
/// Summary description for Class1
/// </summary>
public class Class1
{
	public Class1()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public static string connstring = ConfigurationManager.ConnectionStrings["connid"].ToString(); //ConfigurationManager.AppSettings["connid"];
    public static string GetString(string query)
    {
        string retvalue = "";
        SqlConnection sqlcon = new SqlConnection(connstring);
        SqlCommand sqlcom = new SqlCommand(query, sqlcon);
        //sqlcom.CommandType = CommandType.Text;
        sqlcon.Open();
        SqlDataReader sqldr = sqlcom.ExecuteReader();
        sqldr.Read();
        try
        {
            retvalue = sqldr[0].ToString();
            sqldr.Dispose();
            sqlcom.Dispose();
            sqlcon.Close();
        }
        catch
        {
            sqldr.Dispose();
            sqlcom.Dispose();
            sqlcon.Close();
            return "";
        }
        return retvalue;
    }

    public static string InsUpdDel(string query)
    {
        SqlConnection sqlcon = new SqlConnection(connstring);

        SqlCommand sqlcom = new SqlCommand(query, sqlcon);
        //sqlcom.CommandType = CommandType.Text;
        sqlcon.Open();
        try
        {
            // string connstring = ConfigurationManager.ConnectionStrings["connid"].ToString(); //ConfigurationManager.AppSettings["connid"];
            

            sqlcom.ExecuteNonQuery();

            query = "";
            sqlcom.Dispose();
            sqlcon.Close();
            return "true";
        }
        catch(Exception prajwol)
        {
            query = "";
            sqlcom.Dispose();
            sqlcon.Close();
            return prajwol.Message.ToString();
        }
    }

    public static void datatable(string query,GridView gv)
    {
        DataTable dt = new DataTable();
        SqlConnection sqlcon = new SqlConnection(connstring);
        SqlDataAdapter sqlda = new SqlDataAdapter(query, sqlcon);
        sqlcon.Open();
        sqlda.Fill(dt);
        sqlda.Dispose();
        sqlcon.Close();

        //return dt;
        gv.DataSource = dt;
        gv.DataBind();
    }

    public static void BindInDDL(string query, string valuefield, string textfield, DropDownList ddl)
    {
        DataTable dt = new DataTable();
        SqlConnection sqlcon = new SqlConnection(connstring);
        SqlDataAdapter sqlda = new SqlDataAdapter(query, sqlcon);
        sqlcon.Open();
        sqlda.Fill(dt);

        if (dt.Rows.Count > 0)
        {
            DataRow drow = dt.NewRow();
            drow[valuefield] = 0;
            drow[textfield] = "--Select--";
            dt.Rows.InsertAt(drow, 0);

            ddl.DataSource = dt;
            ddl.DataTextField = textfield;
            ddl.DataValueField = valuefield;
            ddl.DataBind();
        }
        sqlda.Dispose();
        sqlcon.Close();
    }

    public static string[] stringarray(string query, int columncount)
    {
        string[] retvalue = new string[columncount];
        SqlConnection sqlcon = new SqlConnection(connstring);
        SqlCommand sqlcom = new SqlCommand(query, sqlcon);
        //sqlcom.CommandType = CommandType.Text;
        sqlcon.Open();
        SqlDataReader sqldr = sqlcom.ExecuteReader();
        sqldr.Read();
        try
        {
            int i = 0;
            while (i < columncount)
            {
                retvalue[i] = sqldr[i].ToString();
                i++;
            }
            sqldr.Dispose();
            sqlcom.Dispose();
            sqlcon.Close();
        }
        catch
        {
            sqldr.Dispose();
            sqlcom.Dispose();
            sqlcon.Close();
            //return "";
        }

        return retvalue;
    }

    public static string[] stringrowarray(string query,int rowcount)
    {
        string[] retvalue = new string[rowcount];
        SqlConnection sqlcon = new SqlConnection(connstring);
        SqlCommand sqlcom = new SqlCommand(query, sqlcon);
        //sqlcom.CommandType = CommandType.Text;
        sqlcon.Open();
        SqlDataReader sqldr = sqlcom.ExecuteReader();
        //sqldr.Read();
        try
        {
            int i = 0;
            //while (i < columncount)
            //{
            while (sqldr.Read())
            {
                retvalue[i] += sqldr[0].ToString();
                i++;
            }
            //}
            sqldr.Dispose();
            sqlcom.Dispose();
            sqlcon.Close();
        }
        catch
        {
            sqldr.Dispose();
            sqlcom.Dispose();
            sqlcon.Close();
            //return "";
        }

        return retvalue;
    }

    public static string FindStringfromprocedure(string procedurename, string[] fieldid, string[] fieldvalue)
    {
        string result = string.Empty;
        
        SqlConnection conn = new SqlConnection(connstring);
        SqlCommand cmd = new SqlCommand(procedurename, conn);
        cmd.CommandType = CommandType.StoredProcedure;

        for (int i = 0; i < fieldid.Length; i++)
            cmd.Parameters.Add(fieldid[i], fieldvalue[i]);

        SqlDataReader reader;
        try
        {
            conn.Open();
            reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    result = reader[0].ToString().Trim();
                }
            }
        }
        catch (Exception ex)
        {
            //throw;
            ex.Message.ToString();
        }
        finally
        {
            conn.Close();
            cmd.Dispose();
        }
        return result;
    }


    public static string[,] Findmultiarraystr2(string sql, int fieldcount)//, int rowcount)
    {
        string[,] result={{},{}};
        //result = {};//new string[rowcount, fieldcount];
        //string sql = "select Top 1 iEmployeeid from tblEmployeeMaster order by iEmployeeid desc";
        string connStr = ConfigurationManager.AppSettings["SqlConn"].ToString();
        SqlConnection conn = new SqlConnection(connStr);
        SqlCommand cmd = new SqlCommand(sql, conn);
        cmd.CommandType = CommandType.Text;
        SqlDataReader reader;
        try
        {
            conn.Open();
            reader = cmd.ExecuteReader();
            int i = 0;
            //reader.Read();
            //for (i = 0; i < rowcount; i++)
            //{
            while (reader.Read())
            {
                int j = 0;
                for (j = 0; j < fieldcount; j++)
                {
                    result[i, j] = reader[j].ToString();
                }
                i++;
            }
            //    reader.Read();
            //}
        }
        catch (Exception ex)
        {
            //throw;
            ex.Message.ToString();
        }
        finally
        {
            conn.Close();
            cmd.Dispose();
        }
        return result;
    }
}
