using System;
using System.Collections;
using System.Data;
using System.Web;

using AEPTexasUtilityCode;

namespace AEPTexasUtilityManager
{
	public static class UserIdentity
	{
		#region METHODS

		public static bool CreateUser(HttpServerUtility server, ArrayList userValues)
		{
			bool createSuccess = false;
			MembersUtility mu = new MembersUtility();
			bool boolUser = mu.CreateUser(server, userValues);


			return createSuccess;
		}

		public static void DeleteUser (HttpServerUtility httpServer, string Username)
		{
			
		}

		public static string[] GetAllRoles(HttpServerUtility server)
		{
			string[] roleList;
			RoleUtility ru = new RoleUtility();
			roleList = ru.GetAllRoles(server);
			return roleList;
		}

		public static void UserValidate(HttpServerUtility serverUtility, string username, string password)
		{
			MembersUtility memberutil = new MembersUtility();
			memberutil.ValidateUser(serverUtility, username, password);
			return;
		}

		#endregion

		#region PROPERTIES

		private static string comment;
		public static string Comment
		{
			get
			{
				return comment;
			}
			set
			{
				comment = value;
			}
		}

		private static DateTime createdate;
		public static DateTime CreateDate
		{
			get
			{
				return createdate;
			}
			set
			{
				createdate = value;
			}
		}

		private static int failedpasswordanswerattemptcount;
		public static int FailedPasswordAnswerAttemptCount
		{
			get
			{
				return failedpasswordanswerattemptcount;
			}
			set
			{
				failedpasswordanswerattemptcount = value;
			}
		}

		private static int failedpasswordattemptcount;
		public static int FailedPasswordAttemptCount
		{
			get
			{
				return failedpasswordattemptcount;
			}
			set
			{
				failedpasswordattemptcount = value;
			}
		}

		private static bool isapproved;
		public static bool IsApproved
		{
			get
			{
				return isapproved;
			}
			set
			{
				isapproved = value;
			}
		}

		private static bool islockedout;
		public static bool IsLockedOut
		{
			get
			{
				return islockedout;
			}
			set
			{

				islockedout = value;
			}
		}

		private static bool isloggedin;
		public static bool IsLoggedIn
		{
			get
			{
				return isloggedin;
			}
			set
			{
				isloggedin = value;
			}
		}

		private static DateTime lastactivitydate;
		public static DateTime LastActivityDate
		{
			get
			{
				return lastactivitydate;
			}
			set
			{
				lastactivitydate = value;
			}
		}

		private static DateTime lastlockoutdate;
		public static DateTime LastLockoutDate
		{
			get
			{
				return lastlockoutdate;
			}
			set
			{
				lastlockoutdate = value;
			}
		}

		private static DateTime lastlogindate;
		public static DateTime LastLoginDate
		{
			get
			{
				return lastlogindate;
			}
			set
			{
				lastlogindate = value;
			}
		}

		private static DateTime lastpasswordchangeddate;
		public static DateTime LastPasswordChangedDate
		{
			get
			{
				return lastpasswordchangeddate;
			}
			set
			{
				lastpasswordchangeddate = value;
			}
		}

		private static string password;
		public static string Password
		{
			get
			{
				return password;
			}
			set
			{
				password = value;
			}
		}

		private static int passwordformat;
		public static int PasswordFormat
		{
			get
			{
				return passwordformat;
			}
			set
			{
				passwordformat = value;
			}
		}

		private static string passwordquestion;
		public static string PasswordQuestion
		{
			get
			{
				return passwordquestion;
			}
			set
			{
				passwordquestion = value;
			}
		}

		private static string passwordsalt;
		public static string PasswordSalt
		{
			get
			{
				return passwordsalt;
			}
			set
			{
				passwordsalt = value;
			}
		}

		private static string useremail;
		public static string UserEmail
		{
			get
			{
				return useremail;
			}
			set
			{
				useremail = value;
			}
		}

		private static Guid useridentification;
		public static Guid UserIdentification
		{
			get
			{
				return useridentification;
			}
			set
			{
				useridentification = value;
			}
		}

		private static string username;
		public static string Username
		{
			get
			{
				return username;
			}
			set
			{
				username = value;
			}
		}

		private static string userroles;
		public static string UserRoles
		{
			get
			{
				return userroles;
			}
			set
			{
				userroles = value;
			}
		}
		#endregion
	}
}