using System.Collections;
using System.Configuration.Provider;
using System.Linq;
using System.Text;
using System.Web.Security;
using System.Web.Hosting;
using System.Web.Management;
using System.Web;
using System.Security.Permissions;
using System.Xml;
using System.IO;

namespace AEPTexasUserMngmntUtilityCode
{
	public class XmlRoleProvider : RoleProvider
	{
		private string _appName = null;
		private string _providerName = null;

		private string _roleStore = null;
		XmlDocument _xmlRoleList = null;

		public override void Initialize(string name, System.Collections.Specialized.NameValueCollection config)
		{
			if (config == null)
				throw new ArgumentNullException("config");

			if (String.IsNullOrEmpty(name))
				name = "CustomProvider.XmlRoleProvider";

			base.Initialize(name, config);
			_providerName = name;

			string path = config["FileName"];
			if (String.IsNullOrEmpty(path))
				path = "C:\\temp\\IdentityStore.xml";
			else
				_roleStore = path;

			FileIOPermission permission = new FileIOPermission(FileIOPermissionAccess.Write, _roleStore);

			permission.Demand();
		}

		public override void AddUsersToRoles(string[] usernames, string[] roleNames)
		{
			XmlDocument userRoleDoc = ReadUserRolesFromStore();
			foreach (string user in usernames)
			{
				XmlNode userRoleNode = userRoleDoc.SelectSingleNode(string.Format("//*[UserName=\"{0}\"]", user));

				String roleList = userRoleNode["Roles"].InnerText;

				foreach (string role in roleNames)
				{
					if (roleList.Equals(""))
						roleList += role;
					else
						roleList += "," + role;
				}

				userRoleNode.RemoveChild(userRoleNode.LastChild);

				XmlNode newRoleListNode = userRoleDoc.CreateElement("Roles");
				newRoleListNode.InnerText = roleList;
				userRoleNode.AppendChild(newRoleListNode);
			}
			userRoleDoc.Save(_roleStore);
		}

		public override string ApplicationName
		{
			get
			{
				return _appName;
			}
			set
			{
				_appName = value;
			}
		}

		public override void CreateRole(string roleName)
		{
			ReadRolesFromStore();

			XmlNode newRoleNode = _xmlRoleList.CreateNode(XmlNodeType.Element, "Role", null);

			XmlNode newUserInfoNode = _xmlRoleList.CreateElement("RoleName");
			newUserInfoNode.InnerText = roleName;
			newRoleNode.AppendChild(newUserInfoNode);

			_xmlRoleList.DocumentElement.AppendChild(newRoleNode);

			_xmlRoleList.Save(_roleStore);
		}

		public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
		{
			ReadRolesFromStore();

			XmlNode nodeToDelete = _xmlRoleList.SelectSingleNode(string.Format("//*[RoleName=\"{0}\"]", roleName));

			if (nodeToDelete != null)
			{
				_xmlRoleList.FirstChild.RemoveChild(nodeToDelete);
				_xmlRoleList.Save(_roleStore);
				return true;
			}
			else
				return false;
		}

		public override string[] FindUsersInRole(string roleName, string usernameToMatch)
		{
			throw new NotImplementedException();
		}

		public override string[] GetAllRoles()
		{
			ArrayList roleArrayList = ReadRolesFromStore();

			return (string[])roleArrayList.ToArray(typeof(string));
		}

		public override string[] GetRolesForUser(string username)
		{
			XmlDocument xmlUserRolesList = ReadUserRolesFromStore();

			XmlNode userRoleNode = xmlUserRolesList.SelectSingleNode(string.Format("//*[UserName=\"{0}\"]", username));

			String[] roleList = userRoleNode["Roles"].InnerText.Split(',');

			if (roleList[0] == "")
				return new string[0];
			else
				return roleList;
		}

		public override string[] GetUsersInRole(string roleName)
		{
			ArrayList userList = new ArrayList();

			XmlDocument xmlUserRolesList = ReadUserRolesFromStore();

			XmlNodeList userRoleNodes = xmlUserRolesList.GetElementsByTagName("User");

			if (userRoleNodes != null)
			{
				int numRoles = userRoleNodes.Count;
				foreach (XmlNode node in userRoleNodes)
				{
					String[] roleList = node["Roles"].InnerText.Split(',');

					foreach (string item in roleList)
					{
						if (item.Equals(roleName))
						{
							String username = node["UserName"].InnerText;
							userList.Add(username);
							break;
						}
					}
				}
			}

			return (string[])userList.ToArray(typeof(string));
		}

		public override bool IsUserInRole(string username, string roleName)
		{
			throw new NotImplementedException();
		}

		public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
		{
			XmlDocument userRoleDoc = ReadUserRolesFromStore();
			foreach (string user in usernames)
			{
				XmlNode userRoleNode = userRoleDoc.SelectSingleNode(string.Format("//*[UserName=\"{0}\"]", user));

				ArrayList roleList = new ArrayList();
				roleList.AddRange(userRoleNode["Roles"].InnerText.Split(','));

				foreach (string role in roleNames)
				{
					if (roleList.Contains(role))
						roleList.Remove(role);
				}

				userRoleNode.RemoveChild(userRoleNode.LastChild);

				XmlNode newRoleListNode = userRoleDoc.CreateElement("Roles");
				if (roleList.Count > 0)
					newRoleListNode.InnerText = string.Join(",", (string[])roleList.ToArray(Type.GetType("System.String")));
				userRoleNode.AppendChild(newRoleListNode);
			}
			userRoleDoc.Save(_roleStore);
		}

		public override bool RoleExists(string roleName)
		{
			throw new NotImplementedException();
		}

		private ArrayList ReadRolesFromStore()
		{
			_xmlRoleList = new XmlDocument();
			_xmlRoleList.Load(_roleStore);

			XmlNodeList roleNodes = _xmlRoleList.GetElementsByTagName("Role");

			ArrayList roleArrayList = null;

			if (roleNodes != null)
			{
				roleArrayList = new ArrayList();

				foreach (XmlNode node in roleNodes)
				{
					roleArrayList.Add(node["RoleName"].InnerText);
				}
			}
			return roleArrayList;
		}

		private XmlDocument ReadUserRolesFromStore()
		{
			XmlDocument xmlUserRolesList = new XmlDocument();
			xmlUserRolesList.Load(_roleStore);

			return xmlUserRolesList;
		}
	}
}
