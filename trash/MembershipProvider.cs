using System;
using System.Collections.Generic;
using System.Collections.Specialized;
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
    public class XmlMembershipProvider : MembershipProvider
    {
        private string _appName = null;
        private string _providerName = null;

        private string _userStore;

        public override void Initialize(string name, System.Collections.Specialized.NameValueCollection config)
        {
            if (config == null)
                throw new ArgumentNullException("config");

            if (String.IsNullOrEmpty(name))
                name = "CustomProvider.XmlMembershipProvider";

            base.Initialize(name, config);
            _providerName = name;

            string path = config["FileName"];
            if (String.IsNullOrEmpty(path))
                path = "C:\\temp\\IdentityStore.xml";
            else
                _userStore = path;     

            FileIOPermission permission = new FileIOPermission(FileIOPermissionAccess.Write,_userStore);
            
            permission.Demand();
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

        public override bool ChangePassword(string username, string oldPassword, string newPassword)
        {
            throw new NotImplementedException();
        }

        public override bool ChangePasswordQuestionAndAnswer(string username, string password, string newPasswordQuestion, string newPasswordAnswer)
        {
            return true;
        }

        public override MembershipUser CreateUser(string username, string password, string email, string passwordQuestion, string passwordAnswer, bool isApproved, object providerUserKey, out MembershipCreateStatus status)
        {
            MembershipUser newUser = new MembershipUser(
                _providerName,
                username,
                null,
                email,
                "Secret Question",
                username,
                true,
                false,
                DateTime.Now,               // creationDate
                DateTime.Now,               // lastLoginDate
                DateTime.Now,               // lastActivityDate
                DateTime.Now,               // lastPasswordChangedDate
                new DateTime(2000, 1, 1)    // lastLockoutDate
                );

            XmlDocument _usersList = ReadUsersFromStore();

            XmlNode newUserNode = _usersList.CreateNode(XmlNodeType.Element, "User", null);

            XmlNode newUserInfoNode = _usersList.CreateElement("UserName");
            newUserInfoNode.InnerText = username;
            newUserNode.AppendChild(newUserInfoNode);

            newUserInfoNode = _usersList.CreateElement("Password");
            newUserInfoNode.InnerText = password;
            newUserNode.AppendChild(newUserInfoNode);

            newUserInfoNode = _usersList.CreateElement("FullName");
            newUserInfoNode.InnerText = username;
            newUserNode.AppendChild(newUserInfoNode);

            newUserInfoNode = _usersList.CreateElement("Email");
            newUserInfoNode.InnerText = email;
            newUserNode.AppendChild(newUserInfoNode);

            newUserInfoNode = _usersList.CreateElement("Roles");
            newUserNode.AppendChild(newUserInfoNode);

            _usersList.DocumentElement.AppendChild(newUserNode);

            _usersList.Save(_userStore);
                  
            status = MembershipCreateStatus.Success;

            return newUser;
            
        }

        public override bool DeleteUser(string username, bool deleteAllRelatedData)
        {
            XmlDocument _usersList = ReadUsersFromStore();

            XmlNode nodeToDelete = _usersList.SelectSingleNode(string.Format("//*[UserName=\"{0}\"]", username));

            if (nodeToDelete != null)
            {
                _usersList.FirstChild.RemoveChild(nodeToDelete);
                _usersList.Save(_userStore);
                return true;
            }
            else
                return false;
        }

        public override bool EnablePasswordReset
        {
            get { return true; }
        }

        public override bool EnablePasswordRetrieval
        {
            get { return true; }
        }

        public override MembershipUserCollection FindUsersByEmail(string emailToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }

        public override MembershipUserCollection FindUsersByName(string usernameToMatch, int pageIndex, int pageSize, out int totalRecords)
        {

            MembershipUserCollection usersColl = new MembershipUserCollection();
            XmlNodeList matchingNodes = null;

            XmlDocument _usersList = ReadUsersFromStore();
            matchingNodes = _usersList.SelectNodes(string.Format("//*[contains(UserName,\"{0}\")]", usernameToMatch));

            totalRecords = 0;

            if (matchingNodes != null && matchingNodes.Count != 0)
            {
                totalRecords = matchingNodes.Count;

                foreach (XmlNode node in matchingNodes)
                {
                    MembershipUser newUser = new MembershipUser(
                        _providerName,
                        node["UserName"].InnerText,
                        null,
                        node["Email"].InnerText,
                        "",
                        node["UserName"].InnerText,
                        true,
                        false,
                        DateTime.Now,               // creationDate
                        DateTime.Now,               // lastLoginDate
                        DateTime.Now,               // lastActivityDate
                        DateTime.Now,               // lastPasswordChangedDate
                        new DateTime(2000, 1, 1)    // lastLockoutDate
                        );

                    usersColl.Add(newUser);
                }
            }

            return usersColl;
        }

        public override MembershipUserCollection GetAllUsers(int pageIndex, int pageSize, out int totalRecords)
        {

            MembershipUserCollection usersColl = new MembershipUserCollection();

            XmlDocument _usersList = ReadUsersFromStore();

            XmlNodeList userNodes = _usersList.GetElementsByTagName("User");

            foreach (XmlNode node in userNodes)
            {
                MembershipUser newUser = new MembershipUser(
                    _providerName,
                    node["UserName"].InnerText,
                    null,
                    node["Email"].InnerText,
                    "Secret Question",
                    node["UserName"].InnerText,
                    true,
                    false,
                    DateTime.Now,               // creationDate
                    DateTime.Now,               // lastLoginDate
                    DateTime.Now,               // lastActivityDate
                    DateTime.Now,               // lastPasswordChangedDate
                    new DateTime(2000, 1, 1)    // lastLockoutDate
                    );

                usersColl.Add(newUser);                 
            }

            totalRecords = userNodes.Count;

            return usersColl;                  
        }

        public override int GetNumberOfUsersOnline()
        {
            throw new NotImplementedException();
        }

        public override string GetPassword(string username, string answer)
        {
            throw new NotImplementedException();
        }

        public override MembershipUser GetUser(string username, bool userIsOnline)
        {
            XmlDocument _usersList = ReadUsersFromStore();

            MembershipUser user = null;

            XmlNode node = _usersList.SelectSingleNode(string.Format("//*[UserName=\"{0}\"]", username));

            if (node != null)
            {

                user = new MembershipUser(
                    _providerName,
                    node["UserName"].InnerText,
                    null,
                    node["Email"].InnerText,
                    "Secret Question",
                    node["UserName"].InnerText,
                    true,
                    false,
                    DateTime.Now,               // creationDate
                    DateTime.Now,               // lastLoginDate
                    DateTime.Now,               // lastActivityDate
                    DateTime.Now,               // lastPasswordChangedDate
                    new DateTime(2000, 1, 1)    // lastLockoutDate
                    );
            }

            return user;

        }

        public override MembershipUser GetUser(object providerUserKey, bool userIsOnline)
        {
            throw new NotImplementedException();
        }

        public override string GetUserNameByEmail(string email)
        {
            throw new NotImplementedException();
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
            XmlDocument _usersList = ReadUsersFromStore();

            String username = user.UserName;
            XmlNode nodeToUpdate = _usersList.SelectSingleNode(string.Format("//*[UserName=\"{0}\"]", username));

            if (nodeToUpdate != null)
            {
                nodeToUpdate["Email"].InnerText = user.Email;
                _usersList.Save(_userStore);
            }
            
        }

        public override bool ValidateUser(string username, string password)
        {
            XmlDocument _usersList = ReadUsersFromStore();

            XmlNode node = _usersList.SelectSingleNode(string.Format("//*[UserName=\"{0}\"]", username));

            if (node != null && username.Equals(node["UserName"].InnerText) && password.Equals(node["Password"].InnerText))
                return true;
            else
                return false;
        }

        private XmlDocument ReadUsersFromStore()
        {
            XmlDocument _usersList = new XmlDocument();
            _usersList.Load(_userStore);
            return _usersList;
        }

    } /* XmlFileMembershipProvider */
}
