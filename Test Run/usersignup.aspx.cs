using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Test_Run
{
    public partial class usersignup : System.Web.UI.Page
    {

        string strcon = ConfigurationManager.ConnectionStrings["con"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (CheckUserID())
            {
                Response.Write("<script>alert('ID Already Exist')</script>");
            }
            else
            {
                SignUpUser();
            }
           
        }

        bool CheckUserID()
        {
            try
            {
                SqlConnection con = new SqlConnection(strcon);
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                SqlCommand cmd = new SqlCommand("SELECT * from member_master_tbl where member_id='"+SignUpID.Text.Trim()+"'", con);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();

                da.Fill(dt);

                if (dt.Rows.Count >= 1)
                {
                    return true;
                }
                else return false;

                con.Close();
            }
            catch(Exception ex)
            {
                Response.Write("<script>alert('Failed " + ex.Message + "')</script>");
            }

            return false;
        
        }

        void SignUpUser()
        {
            try
            {
                SqlConnection con = new SqlConnection(strcon);
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                SqlCommand cmd = new SqlCommand("INSERT INTO member_master_tbl(full_name,dob,contact_no,email,state,city,pincode,full_address,member_id,password,account_status) values (@full_name,@dob,@contact_no,@email,@state,@city,@pincode,@full_address,@member_id,@password,@account_status)", con);

                cmd.Parameters.AddWithValue("@full_name", SignUpName.Text.Trim());
                cmd.Parameters.AddWithValue("@dob", SignUpDate.Text.Trim());
                cmd.Parameters.AddWithValue("@contact_no", SignUpNo.Text.Trim());
                cmd.Parameters.AddWithValue("@email", SignUpEmail.Text.Trim());
                cmd.Parameters.AddWithValue("@state", " ");
                cmd.Parameters.AddWithValue("@city", " ");
                cmd.Parameters.AddWithValue("@pincode", " ");
                cmd.Parameters.AddWithValue("@full_address", " ");
                cmd.Parameters.AddWithValue("@member_id", SignUpID.Text.Trim());
                cmd.Parameters.AddWithValue("@password", SignUpPassword.Text.Trim());
                cmd.Parameters.AddWithValue("@account_status", "pending");

                cmd.ExecuteNonQuery();
                con.Close();

                Response.Write("<script>alert('Success')</script>");

            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('Failed " + ex.Message + "')</script>");
            }

        }
    }
}