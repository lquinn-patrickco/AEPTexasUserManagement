using System;
using System.Collections;
using System.Data;
using System.Web;
using System.Web.Security;
using AEPTexasDataUtility;

namespace AEPTexasUtilityCode
{
	public class RoleUtility: RoleProvider
	{
		public RoleUtility()
		{
			
		}

		public override void AddUsersToRoles(string[] usernames, string[] roleNames)
		{
			throw new NotImplementedException();
		}

		public override string ApplicationName
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		public override void CreateRole(string roleName)
		{
			throw new NotImplementedException();
		}

		public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
		{
			throw new NotImplementedException();
		}

		public override string Description
		{
			get
			{
				return base.Description;
			}
		}

		public override string[] FindUsersInRole(string roleName, string usernameToMatch)
		{
			throw new NotImplementedException();
		}

		public override string[] GetAllRoles()
		{
			throw new NotImplementedException();
		}

		public string[] GetAllRoles(HttpServerUtility serverUtil)
		{
			string[] allRoles = {""};
			DataAccessUtility dau = new DataAccessUtility();
			string getpassProcKey = "SqlConnections//SqlConnection//SqlProcedures//SqlProcedure[Key = \"GetAllRoles\"]";
			string xmlGetPassProc = dau.GetKeyValue(serverUtil, getpassProcKey);
			string xmlAppPath = "ApplicationSettings//ApplicationSetting[Key = \"ApplicationName\"]";
			string xmlAppName = dau.GetKeyValue(serverUtil, xmlAppPath);
			ArrayList getroleArray = new ArrayList();
			ArrayList alApp = new ArrayList();
			alApp.Add("@ApplicationName");
			alApp.Add(xmlAppName);
			alApp.Add(SqlDbType.NVarChar);
			getroleArray.Add(alApp);
			DataView roleView = dau.ExecuteProcedureDataView(serverUtil, "SecurityConnectionString", xmlGetPassProc, getroleArray);
			allRoles = new string[roleView.Table.Rows.Count];
			for (int i = 0; i < roleView.Table.Rows.Count; i++)
			{
				allRoles[i] = roleView.Table.Rows[i].ItemArray[0].ToString();
			}
			return allRoles;
		}

		public override string[] GetRolesForUser(string username)
		{
			throw new NotImplementedException();
		}

		public override string[] GetUsersInRole(string roleName)
		{
			throw new NotImplementedException();
		}

		public override void Initialize(string name, System.Collections.Specialized.NameValueCollection config)
		{
			base.Initialize(name, config);
		}

		public override bool IsUserInRole(string username, string roleName)
		{
			throw new NotImplementedException();
		}

		public override string Name
		{
			get
			{
				return base.Name;
			}
		}

		public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
		{
			throw new NotImplementedException();
		}

		public override bool RoleExists(string roleName)
		{
			throw new NotImplementedException();
		}

	}
}