using System;
using System.Collections;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AEPTexasUtilityManager;

namespace AEPTexasUserMngmnt
{
	public partial class CreateUser : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
			{
				string[] rolestrings = UserIdentity.GetAllRoles(Server);
				roleList.Items.Add("Select role");
				foreach (string role in rolestrings)
				{
					roleList.Items.Add(role);
				}
				ViewState["EmailAddress"] = "";
				ViewState["FirstName"] = "";
				ViewState["LastName"] = "";
				ViewState["Password1"] = "";
				ViewState["Password2"] = "";
				btnCreateUser.Enabled = false;
			}
		}

		protected void btnCreateUser_Click(object sender, EventArgs e)
		{
			bool Success;
			ArrayList uservalueList = new ArrayList();
			uservalueList.Add(tbxEmailAddress.Text);
			uservalueList.Add(tbxFirstName.Text);
			uservalueList.Add(tbxLastName.Text);
			uservalueList.Add(tbxPassword1.Text);
			uservalueList.Add(tbxPassword2.Text);
			uservalueList.Add(roleList.Text);
			try
			{
				Success = UserIdentity.CreateUser(Server, uservalueList);
			}
			catch (Exception ex)
			{
				//log ex
				Success = false;
			}
			if (Success == true)
			{
				Server.Transfer("~/Default.aspx");
			}
			else
			{ 
				//error message
			}
		}

		protected void EnableCreateUserButton()
		{
			if (
					(ViewState["EmailAddress"].ToString() == "True") &&
					(ViewState["FirstName"].ToString() == "True") &&
					(ViewState["LastName"].ToString() == "True") &&
					(ViewState["Password1"].ToString() == "True") &&
					(ViewState["Password2"].ToString() == "True") &&
					(roleList.Text != "Select role")
				)
			{
				btnCreateUser.Enabled = true;
			}
			else
			{
				btnCreateUser.Enabled = false;
			}

		}

		protected void tbxEmailAddress_TextChanged(object sender, EventArgs e)
		{
			if (tbxEmailAddress.Text.Length > 0)
			{
				ViewState["EmailAddress"] = true;
			}
			else
			{
				ViewState["EmailAddress"] = false;
			}
			EnableCreateUserButton();
		}

		protected void tbxFirstName_TextChanged(object sender, EventArgs e)
		{
			if (tbxFirstName.Text.Length > 0)
			{
				ViewState["FirstName"] = true;
			}
			else
			{
				ViewState["FirstName"] = false;
			}
			EnableCreateUserButton();
		}

		protected void tbxLastName_TextChanged(object sender, EventArgs e)
		{
			if (tbxLastName.Text.Length > 0)
			{
				ViewState["LastName"] = true;
			}
			else
			{
				ViewState["LastName"] = false;
			}
			EnableCreateUserButton();
		}

		protected void tbxPassword1_TextChanged(object sender, EventArgs e)
		{
			if (tbxPassword1.Text.Length > 0)
			{
				ViewState["Password1"] = true;
			}
			else
			{
				ViewState["Password1"] = false;
			}
			EnableCreateUserButton();
		}

		protected void tbxPassword2_TextChanged(object sender, EventArgs e)
		{
			if (tbxPassword2.Text.Length > 0)
			{
				ViewState["Password2"] = true;
			}
			else
			{
				ViewState["Password2"] = false;
			}
			EnableCreateUserButton();
		}

		protected void roleList_TextChanged(object sender, EventArgs e)
		{
			if (roleList.Text != "Select role")
			{
				ViewState["RoleList"] = true;
			}
			else
			{
				ViewState["RoleList"] = false;
			}
			EnableCreateUserButton();
		}
	}
}