using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using connectiondatabase;
using System.Data.SqlClient;

namespace withoutNTier
{
	public partial class AddressBook : System.Web.UI.Page
	{
		Class1 c1 = new Class1();
		SqlConnection con;
		SqlCommand cmd;
		protected void Page_Load(object sender, EventArgs e)
		{
			

		}

		protected void btnInsert_Click(object sender, EventArgs e)
		{
			using (con = c1.GetConnection())
			{
				cmd = new SqlCommand("insertIntoAddressBook", con);
				cmd.CommandType = System.Data.CommandType.StoredProcedure;

				cmd.Parameters.AddWithValue("@firstname", txtFirstName.Text);
				cmd.Parameters.AddWithValue("@lastname", txtLastName.Text);
				cmd.Parameters.AddWithValue("@email", txtEmail.Text);
				cmd.Parameters.AddWithValue("@mobno", txtMoblieNo.Text);
				cmd.Parameters.AddWithValue("@address", txtAddress.Text);
			
				SqlParameter flag = new SqlParameter("@flag", System.Data.SqlDbType.Int);
				flag.Direction = System.Data.ParameterDirection.ReturnValue;
				con.Open();
				cmd.ExecuteNonQuery();
				if (Convert.ToInt32(flag) == 0)
					lblShow.Text = "Record Not Inserted";
				else
					lblShow.Text = "Record Inserted Successfully";
				con.Close();

			}
		}

		protected void btnUpdate_Click(object sender, EventArgs e)
		{
			using (con = c1.GetConnection())
			{
				cmd = new SqlCommand("updateAddressBook", con);
				cmd.CommandType = System.Data.CommandType.StoredProcedure;

				cmd.Parameters.AddWithValue("@addressid", txtAddressId.Text);
				cmd.Parameters.AddWithValue("@email", txtEmail.Text);

				SqlParameter flag = new SqlParameter("@flag", System.Data.SqlDbType.Int);
				flag.Direction = System.Data.ParameterDirection.ReturnValue;
				con.Open();
				cmd.ExecuteNonQuery();
				if (Convert.ToInt32(flag) == 0)
					lblShow.Text = "Record Not updated or not exsist";
				else
					lblShow.Text = "Record updated Successfully";
				con.Close();
			}

		}

		protected void txtDelete_Click(object sender, EventArgs e)
		{
			using (con = c1.GetConnection())
			{
				cmd = new SqlCommand("deleteFromAddressBook", con);
				cmd.CommandType = System.Data.CommandType.StoredProcedure;

				cmd.Parameters.AddWithValue("@addressid", txtAddressId.Text);


				SqlParameter flag = new SqlParameter("@flag", System.Data.SqlDbType.Int);
				flag.Direction = System.Data.ParameterDirection.ReturnValue;
				con.Open();
				cmd.ExecuteNonQuery();
				if (Convert.ToInt32(flag) == 0)
					lblShow.Text = "Record Not deleted or not exsist";
				else
					lblShow.Text = "Record deleted Successfully";
				con.Close();
			}
		}

		protected void txtSearch_Click(object sender, EventArgs e)
		{
			using (con = c1.GetConnection())
			{
				cmd = new SqlCommand("select * from addressBook", con);
				con.Open();
				SqlDataReader dr = cmd.ExecuteReader();
				if(dr.HasRows)
				{
					txtAddressId.Text = dr[0].ToString();
					txtFirstName.Text = dr[1].ToString();
					txtLastName.Text = dr[2].ToString();
					txtMoblieNo.Text = dr[3].ToString();
					txtAddress.Text = dr[4].ToString();
				}
				else
				{
					lblShow.Text = "No Record Found";
				}
			}
			}
	}
}