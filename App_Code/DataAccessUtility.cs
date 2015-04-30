using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Xml;

namespace AEPTexasDataUtility
{
	public class DataAccessUtility
	{
		public DataAccessUtility()
		{

		}

		private SqlConnectionStringBuilder BuildConnectionString(HttpServerUtility server, string ConnectionName)
		{
			XmlNode sqlNode = XmlConnectionSettings(server, ConnectionName);
			SqlConnectionStringBuilder stringbuild = new SqlConnectionStringBuilder();
			//stringbuild.ApplicationIntent = "";
			//stringbuild.ApplicationName = sqlNode.SelectSingleNode("ApplicationName").InnerText;
			stringbuild.AsynchronousProcessing = Convert.ToBoolean(sqlNode.SelectSingleNode("//AsynchronousProcessing").InnerText);
			stringbuild.ConnectTimeout = Convert.ToInt32(sqlNode.SelectSingleNode("//ConnectTimeout").InnerText);
			stringbuild.ContextConnection = Convert.ToBoolean(sqlNode.SelectSingleNode("//ContextConnection").InnerText);
			stringbuild.DataSource = sqlNode.SelectSingleNode("//DataSource").InnerText;
			stringbuild.Encrypt = Convert.ToBoolean(sqlNode.SelectSingleNode("//Encrypt").InnerText);
			stringbuild.Enlist = Convert.ToBoolean(sqlNode.SelectSingleNode("//Enlist").InnerText);
			if (sqlNode.SelectSingleNode("FailoverPartner").InnerText.Length > 0)
			{
				stringbuild.FailoverPartner = sqlNode.SelectSingleNode("FailoverPartner//").InnerText;
			} 
			stringbuild.InitialCatalog = sqlNode.SelectSingleNode("//InitialCatalog").InnerText;
			stringbuild.IntegratedSecurity = Convert.ToBoolean(sqlNode.SelectSingleNode("//IntegratedSecurity").InnerText);
			//stringbuild.MultipleActiveResultSets = false;
			stringbuild.Password = sqlNode.SelectSingleNode("//Password").InnerText;
			stringbuild.PersistSecurityInfo = Convert.ToBoolean(sqlNode.SelectSingleNode("//PersistSecurityInfo").InnerText);
			stringbuild.Pooling = Convert.ToBoolean(sqlNode.SelectSingleNode("//Pooling").InnerText);
			//stringbuild.TransactionBinding
			//stringbuild.TrustServerCertificate = true;
			stringbuild.UserID = sqlNode.SelectSingleNode("//UserID").InnerText;
			return stringbuild;
		}

		public DataView ExecuteProcedureDataView(HttpServerUtility ServerUtility, string ConnectionStringName, string Procedure, ArrayList paramList)
		{
			string connects = GetConnectionString(ServerUtility, ConnectionStringName);
			RetrieveUtility retrieveutil = new RetrieveUtility();
			DataSet retrievData = retrieveutil.RetrieveRowsWithParametersDataSet(connects, Procedure, paramList);
			DataView retrievView;
			if (retrievData.Tables.Count > 0)
			{
				retrievView = new DataView(retrievData.Tables[0]);
			}
			else
			{
				retrievView = new DataView();
			}
			return retrievView;
		}

		public DataSet ExecuteProcedureDataSet(HttpServerUtility ServerUtility, string ConnectionStringName, string Procedure, ArrayList paramList, ArrayList returnList)
		{
			DataSet retrieveSet = new DataSet();
			RetrieveUtility retrieveutil = new RetrieveUtility();
			string connects = GetConnectionString(ServerUtility, ConnectionStringName);
			//retrieveSet =	retrieveutil.RetrieveRowsWithParameters(connects, Procedure, paramList, returnList);
			return retrieveSet;
		}

		public Guid ExecuteProcedureGuid(HttpServerUtility ServerUtility, string ConnectionStringName, string Procedure, ArrayList paramList, string guidName)
		{
			Guid retrnGuid = Guid.Empty;
			RetrieveUtility retrieveutil = new RetrieveUtility();
			string connects = GetConnectionString(ServerUtility, ConnectionStringName);
			retrnGuid = retrieveutil.RetrieveRowsWithParametersGuid(connects, Procedure, paramList, guidName);
			return retrnGuid;
		}

		private string GetConnectionString(HttpServerUtility HttpServer, string ConnectName)
		{
			SqlConnectionStringBuilder builder = BuildConnectionString(HttpServer, ConnectName);
			return builder.ToString();
		}

		public string GetKeyValue(HttpServerUtility utilityServer, string nodePath)
		{
			string keyvalue = "";
			keyvalue = XmlKeyValue(utilityServer, nodePath);
			return keyvalue;
		}

		private string XmlKeyValue(HttpServerUtility serverUtility, string nodepath)
		{
			string xfilename = "/App_Data/AEPTexasApplicationSettings.xml";
			string xfilepath = serverUtility.MapPath(xfilename);
			XmlDocument xDoc = new XmlDocument();
			xDoc.Load(xfilepath);
			XmlElement rootNode = xDoc.DocumentElement;
			XmlNode connexnode = rootNode.SelectSingleNode(nodepath);
			string xmlValue = connexnode.ChildNodes[1].InnerText;
			return xmlValue;
		}

		private XmlNode XmlConnectionSettings(HttpServerUtility serverUtility, string ConnectName)
		{
			string xfilename = "./App_Data/AEPTexasApplicationSettings.xml";
			string xfilepath = serverUtility.MapPath(xfilename);
			string nodepath = "SqlConnections//SqlConnection//SqlConnectionString[SqlConnectionStringName = \"" + ConnectName + "\"]";
			XmlDocument xDoc = new XmlDocument();
			xDoc.Load(xfilepath);
			XmlElement rootNode = xDoc.DocumentElement;
			XmlNode connexnode = rootNode.SelectSingleNode(nodepath);
			return connexnode;
		}

	}
}