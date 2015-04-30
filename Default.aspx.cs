using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AEPTexasUserMngmnt
{
	public partial class Default : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
			{
				//FormsIdentity id = (FormsIdentity)User.Identity;
				//FormsAuthenticationTicket ticket = id.Ticket;
				//Response.Write("Hello, " + Server.HtmlEncode(User.Identity.Name));
				//Response.Write("<p/>TicketName: " + ticket.Name);
				//Response.Write("<br/>Cookie Path: " + ticket.CookiePath);
				//Response.Write("<br/>Ticket Expiration: " + ticket.Expiration.ToString());
				//Response.Write("<br/>Expired: " + ticket.Expired.ToString());
				//Response.Write("<br/>Persistent: " + ticket.IsPersistent.ToString());
				//Response.Write("<br/>IssueDate: " + ticket.IssueDate.ToString());
				//Response.Write("<br/>UserData: " + ticket.UserData);
				//Response.Write("<br/>Version: " + ticket.Version.ToString());
			}
			else
			{
			}

		}
	}
}