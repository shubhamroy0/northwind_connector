using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Web.UI;
using System.Web.UI.WebControls;

namespace Northwind_Connector
{
    public partial class ProuctConnect : System.Web.UI.Page
    {
        String conn_string = "Data Source = PC252061 ; Initial Catalog = NORTHWND ; Integrated Security =true";
        DataTable dt;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                loadproucts();
            }
        }
        private void loadproucts()
        {
            using(SqlConnection conn = new SqlConnection(conn_string))
            {
                SqlDataAdapter adapter = new SqlDataAdapter();
                SqlCommand scmd = new SqlCommand();
                scmd.Connection = conn;
                scmd.CommandText = "USP_PrintAllData";
                scmd.CommandType = CommandType.StoredProcedure;
                adapter.SelectCommand=scmd;
                dt = new DataTable();
                //adapter.FillSchema(dt,SchemaType.Source);
                adapter.Fill(dt);
                SqlCommandBuilder cbuilder = new SqlCommandBuilder(adapter);
                product_list.DataValueField ="ProductID";
                product_list.DataTextField = "ProductName";
                product_list.DataSource = dt;
                product_list.DataBind();
            }
        }

        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            using (SqlConnection conn = new SqlConnection(conn_string))
            {
                SqlDataAdapter adapter = new SqlDataAdapter();
                SqlCommand scmd = new SqlCommand();
                scmd.Connection = conn;
                scmd.CommandText = "USP_ExtractPid";
                scmd.CommandType = CommandType.StoredProcedure;
                scmd.Parameters.AddWithValue("@pid", product_list.SelectedItem.Value);
                adapter.SelectCommand = scmd;
                dt = new DataTable();
                adapter.Fill(dt);
                //DataTable newdt = from dt where "id" == product_list.SelectedValue
                             //     select ;
                //TextBox1.Text=newdt.GetType().ToString();
                Product_details.DataSource = dt;
                Product_details.DataBind();
            }
        }
    }
}