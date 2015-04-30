using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

using AEPTexasUtilityManager;

namespace AEPTexasUserMngmnt
{
	public partial class Login : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
			{
				

			}
			else
			{
			}

			//username/password:  [lquinn_aept]
			//RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
			//byte[] buff = new byte[32];
			//rng.GetBytes(buff);
			//string saltstring = Convert.ToBase64String(buff);
		}

		protected void Login01_OnAuthenticate(object sender, AuthenticateEventArgs e)
		{
			System.Web.UI.WebControls.Login login = (System.Web.UI.WebControls.Login)sender;
			string username = login.UserName;
			string password = login.Password;
			try
			{
				UserIdentity.UserValidate(Server, username, password);
				string uiPassword = UserIdentity.Password;

			}
			catch (Exception ex)
			{
				
				throw;
			}

			e.Authenticated = UserIdentity.IsApproved;
		}

		protected void Login01_OnLoggedIn(object sender, EventArgs e)
		{
		}

		protected void Login01_OnLoggingIn(object sender, EventArgs e)
		{
			
		}

		protected void Login01_OnLoginError(object sender, EventArgs e)
		{
		}

	}
}