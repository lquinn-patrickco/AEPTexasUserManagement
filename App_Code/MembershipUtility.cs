using System;
using System.Collections;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Security;

using AEPTexasDataUtility;
using AEPTexasUtilityManager;

namespace AEPTexasUtilityCode
{
	public class MembersUtility : System.Web.Security.MembershipProvider
	{
		//https://msdn.microsoft.com/en-us/library/system.web.security.membership(v=vs.110).aspx
		public override bool ChangePassword(string username, string oldPassword, string newPassword)
		{
			throw new NotImplementedException();
		}

		public override bool ChangePasswordQuestionAndAnswer(string username, string password, string newPasswordQuestion, string newPasswordAnswer)
		{
			throw new NotImplementedException();
		}

		private ArrayList CreateParamArray(string paramKey, Guid uniqueId)
		{
			ArrayList paramList = new ArrayList();
			paramList.Add(paramKey);
			paramList.Add(uniqueId.ToString());
			paramList.Add(SqlDbType.UniqueIdentifier);
			return paramList;
		}

		private ArrayList CreateParamArray(string paramKey, int number)
		{
			ArrayList paramList = new ArrayList();
			paramList.Add(paramKey);
			paramList.Add(number);
			paramList.Add(SqlDbType.UniqueIdentifier);
			return paramList;
		}

		private ArrayList CreateParamArray(string paramKey, string paramValue)
		{
			ArrayList paramList = new ArrayList();
			paramList.Add(paramKey);
			paramList.Add(paramValue);
			paramList.Add(SqlDbType.NVarChar);
			return paramList;
		}

		public bool CreateUser(HttpServerUtility httpServer, ArrayList valueList)
		{
			//uservalueList.Add(ViewState["EmailAddress"].ToString());
			//uservalueList.Add(ViewState["FirstName"].ToString());
			//uservalueList.Add(ViewState["LastName"].ToString());
			//uservalueList.Add(ViewState["Password1"].ToString());
			//uservalueList.Add(ViewState["Password2"].ToString());
			DataAccessUtility dau = new DataAccessUtility();
			ArrayList paramAppList = new ArrayList();
			DateTime current = DateTime.Now;
			string xmlAppPath = "ApplicationSettings//ApplicationSetting[Key = \"ApplicationName\"]";
			string xmlAppName = dau.GetKeyValue(httpServer, xmlAppPath);
			string getappProcKey = "SqlConnections//SqlConnection//SqlProcedures//SqlProcedure[Key = \"GetApplicationId\"]";
			string xmlGetAppidProc = dau.GetKeyValue(httpServer, getappProcKey);
			paramAppList.Add(CreateParamArray("@ApplicationName", xmlAppName));
			paramAppList.Add(CreateParamArray("@ApplicationId", ""));

			Guid resultGuid = dau.ExecuteProcedureGuid(httpServer, "SecurityConnectionString", xmlGetAppidProc, paramAppList, "@ApplicationId");
			//DataView resultView = dau.ExecuteProcedure(httpServer, "SecurityConnectionString", xmlGetAppidProc, paramAppList);
			
			//string getUserCreateProcKey = "SqlConnections//SqlConnection//SqlProcedures//SqlProcedure[Key = \"CreateUsersUser\"]";
			//string xmlGetUserCreateProc = dau.GetKeyValue(httpServer, getUserCreateProcKey);
			//Guid appidGuid = (Guid)resultView.Table.Rows[0].ItemArray[0];
			//ArrayList createUserList = new ArrayList();
			//createUserList.Add(CreateParamArray("@ApplicationId", appidGuid));
			//createUserList.Add(CreateParamArray("@UserName", valueList[0].ToString()));
			//createUserList.Add(CreateParamArray("@IsUserAnonymous", 0));
			//createUserList.Add(CreateParamArray("@LastActivityDate", current.ToString()));
			//createUserList.Add(CreateParamArray("@UserId", Guid.Empty));
			//DataView userView = dau.ExecuteProcedure(httpServer, "SecurityConnectionString", xmlGetUserCreateProc, createUserList);






			return false;
		}

		public override MembershipUser CreateUser(string username, string password, string email, string passwordQuestion, string passwordAnswer, bool isApproved, object providerUserKey, out MembershipCreateStatus status)
		{
			throw new NotImplementedException();
		}

		protected override byte[] DecryptPassword(byte[] encodedPassword)
		{
			return base.DecryptPassword(encodedPassword);
		}

		public override bool DeleteUser(string username, bool deleteAllRelatedData)
		{
			//paramVals1.Add("@UserName", valueList[0].ToString());
			//paramVals1.Add("@IsUserAnonymous", 0);
			//paramVals1.Add("@LastActivityDate", current);
			//paramVals1.Add("@UserId", null);
			throw new NotImplementedException();
		}

		protected override byte[] EncryptPassword(byte[] password)
		{
			RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
			byte[] buff = new byte[32];
			rng.GetBytes(buff);
			string saltstring = Convert.ToBase64String(buff);

			return base.EncryptPassword(password);
		}

		public override MembershipUserCollection FindUsersByEmail(string emailToMatch, int pageIndex, int pageSize, out int totalRecords)
		{
			throw new NotImplementedException();
		}

		public override MembershipUserCollection FindUsersByName(string usernameToMatch, int pageIndex, int pageSize, out int totalRecords)
		{
			throw new NotImplementedException();
		}

		public override MembershipUserCollection GetAllUsers(int pageIndex, int pageSize, out int totalRecords)
		{
			throw new NotImplementedException();
		}

		public override int GetNumberOfUsersOnline()
		{
			throw new NotImplementedException();
		}

		public override string GetPassword(string username, string answer)
		{
			throw new NotImplementedException();
		}

		public override MembershipUser GetUser(object providerUserKey, bool userIsOnline)
		{
			throw new NotImplementedException();
		}

		public override MembershipUser GetUser(string username, bool userIsOnline)
		{
			throw new NotImplementedException();
		}

		public override string GetUserNameByEmail(string email)
		{
			throw new NotImplementedException();
		}

		public override string ResetPassword(string username, string answer)
		{
			throw new NotImplementedException();
		}

		public override bool UnlockUser(string userName)
		{
			throw new NotImplementedException();
		}

		public override void UpdateUser(MembershipUser user)
		{
			throw new NotImplementedException();
		}

		public override bool ValidateUser(string username, string password)
		{
			throw new NotImplementedException();
		}

		public void ValidateUser(HttpServerUtility httpServer, string username, string password)
		{
			bool IsUserValid = false;
			string xmlAppPath = "ApplicationSettings//ApplicationSetting[Key = \"ApplicationName\"]";
			string getuserProcKey = "SqlConnections//SqlConnection//SqlProcedures//SqlProcedure[Key = \"GetUserByName\"]";
			DataAccessUtility dau = new DataAccessUtility();
			string xmlAppName = dau.GetKeyValue(httpServer, xmlAppPath);
			string xmlGetUserProc = dau.GetKeyValue(httpServer, getuserProcKey);
			DateTime currentUtc = new DateTime();
			currentUtc = DateTime.UtcNow;
			int updateActvty = 1;
			ArrayList getuserArray = new ArrayList();
			ArrayList alApp = new ArrayList();
			alApp.Add("@ApplicationName");
			alApp.Add(xmlAppName);
			alApp.Add(SqlDbType.NVarChar);
			getuserArray.Add(alApp);
			ArrayList alUser = new ArrayList();
			alUser.Add("@UserName");
			alUser.Add(username);
			alUser.Add(SqlDbType.NVarChar);
			getuserArray.Add(alUser);
			ArrayList alUtc = new ArrayList();
			alUtc.Add("@CurrentTimeUtc");
			alUtc.Add(currentUtc);
			alUtc.Add(SqlDbType.DateTime);
			getuserArray.Add(alUtc);
			ArrayList alAct = new ArrayList();
			alAct.Add("@UpdateLastActivity");
			alAct.Add(updateActvty);
			alAct.Add(SqlDbType.Int);
			getuserArray.Add(alAct);
			DataView userView = dau.ExecuteProcedureDataView(httpServer, "SecurityConnectionString", xmlGetUserProc, getuserArray);
			if (!(userView == null))
			{
				UserIdentity.Comment = userView.Table.Rows[0].ItemArray[2].ToString();
				UserIdentity.CreateDate = Convert.ToDateTime(userView.Table.Rows[0].ItemArray[4]);
				UserIdentity.UserEmail = userView.Table.Rows[0].ItemArray[0].ToString();
				UserIdentity.IsLockedOut = Convert.ToBoolean(userView.Table.Rows[0].ItemArray[9]);
				UserIdentity.LastLockoutDate = Convert.ToDateTime(userView.Table.Rows[0].ItemArray[10]);
				UserIdentity.LastPasswordChangedDate = Convert.ToDateTime(userView.Table.Rows[0].ItemArray[7]);
				UserIdentity.PasswordQuestion = userView.Table.Rows[0].ItemArray[1].ToString();
				UserIdentity.UserIdentification = (Guid)userView.Table.Rows[0].ItemArray[8];
			}
			string getpassProcKey = "SqlConnections//SqlConnection//SqlProcedures//SqlProcedure[Key = \"GetPasswordWithFormat\"]";
			string xmlGetPassProc = dau.GetKeyValue(httpServer, getpassProcKey);
			getuserArray.Remove(alAct);
			ArrayList alLog = new ArrayList();
			alLog.Add("@UpdateLastLoginActivityDate");
			alLog.Add(updateActvty);
			alLog.Add(SqlDbType.Int);
			getuserArray.Add(alLog);
			DataView passView = dau.ExecuteProcedureDataView(httpServer, "SecurityConnectionString", xmlGetPassProc, getuserArray);
			if (!(passView == null))
			{
				UserIdentity.FailedPasswordAnswerAttemptCount = Convert.ToInt32(passView.Table.Rows[0].ItemArray[4]);
				UserIdentity.FailedPasswordAttemptCount = Convert.ToInt32(passView.Table.Rows[0].ItemArray[3]);
				UserIdentity.IsApproved = Convert.ToBoolean(passView.Table.Rows[0].ItemArray[5]);
				UserIdentity.LastActivityDate = Convert.ToDateTime(passView.Table.Rows[0].ItemArray[7]);
				UserIdentity.LastLoginDate = Convert.ToDateTime(passView.Table.Rows[0].ItemArray[6]);
				UserIdentity.Password = passView.Table.Rows[0].ItemArray[0].ToString();
				UserIdentity.PasswordFormat = Convert.ToInt32(passView.Table.Rows[0].ItemArray[1]);
				UserIdentity.PasswordSalt = passView.Table.Rows[0].ItemArray[2].ToString();
			}
			//**********************unencrypt db password and validate against user password;

			string getroleProcKey = "SqlConnections//SqlConnection//SqlProcedures//SqlProcedure[Key = \"GetRolesForUser\"]";
			string xmlGetRoleProc = dau.GetKeyValue(httpServer, getroleProcKey);
			ArrayList getroleArray = new ArrayList();
			getroleArray.Add(alApp);
			getroleArray.Add(alUser);
			DataView roleView = dau.ExecuteProcedureDataView(httpServer, "SecurityConnectionString", xmlGetRoleProc, getroleArray);
			if (!(roleView == null))
			{
				StringBuilder sb = new StringBuilder();
				for(int i = 0; i < roleView.Table.Rows.Count; i++)
				{
					if (sb.Length > 0)
					{
						sb.Append(", ");
					}
					sb.Append(roleView.Table.Rows[0].ItemArray[i].ToString());
				}
				UserIdentity.UserRoles = sb.ToString();
			}
			return;
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

		public override bool EnablePasswordReset
		{
			get { throw new NotImplementedException(); }
		}

		public override bool EnablePasswordRetrieval
		{
			get { throw new NotImplementedException(); }
		}

		public override int MaxInvalidPasswordAttempts
		{
			get { throw new NotImplementedException(); }
		}

		public override int MinRequiredNonAlphanumericCharacters
		{
			get { throw new NotImplementedException(); }
		}

		public override int MinRequiredPasswordLength
		{
			get { throw new NotImplementedException(); }
		}

		protected override void OnValidatingPassword(ValidatePasswordEventArgs e)
		{
			base.OnValidatingPassword(e);
		}

		public override int PasswordAttemptWindow
		{
			get { throw new NotImplementedException(); }
		}

		public override MembershipPasswordFormat PasswordFormat
		{
			get { throw new NotImplementedException(); }
		}

		public override string PasswordStrengthRegularExpression
		{
			get { throw new NotImplementedException(); }
		}

		public override bool RequiresQuestionAndAnswer
		{
			get { throw new NotImplementedException(); }
		}

		public override bool RequiresUniqueEmail
		{
			get { throw new NotImplementedException(); }
		}

	}
}