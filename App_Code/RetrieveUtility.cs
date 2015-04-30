using System;
using System.Collections.Generic;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace AEPTexasDataUtility
{
	public class RetrieveUtility
	{
		public DataSet RetrieveRows(string connectionString, string queryName)
		{
			DataSet dataRows = new DataSet();

			using (SqlConnection connection = new SqlConnection(connectionString))
			{
				SqlCommand command = new SqlCommand(queryName, connection);
				SqlDataAdapter adapter = new SqlDataAdapter(command);
				adapter.Fill(dataRows);
			}
			return dataRows;
		}

		public DataSet RetrieveRowsWithParametersDataSet(string connectionString, string queryName, ArrayList paramArray)
		{
			DataSet dataRows = new DataSet();
			using (SqlConnection connection = new SqlConnection(connectionString))
			{
				SqlCommand command = new SqlCommand(queryName, connection);
				command.CommandType = CommandType.StoredProcedure;
				SqlDataAdapter adapter = new SqlDataAdapter(command);
				if (!(paramArray == null))
				{
					for (int i = 0; i < paramArray.Count; i++)
					{
						ArrayList current = (ArrayList)paramArray[i];
						SqlParameter param = new SqlParameter(current[0].ToString(), current[2]);
						param.Value = current[1];
						command.Parameters.Add(param);
					}
				}
				adapter.Fill(dataRows);
			}
			return dataRows;
		}

		public Guid RetrieveRowsWithParametersGuid(string connectionString, string queryName, ArrayList paramArray, string guidParamName)
		{
			Guid returnGuid = Guid.Empty;
			using (SqlConnection connection = new SqlConnection(connectionString))
			{
				SqlCommand command = new SqlCommand(queryName, connection);
				command.CommandType = CommandType.StoredProcedure;
				SqlDataAdapter adapter = new SqlDataAdapter(command);
				if (!(paramArray == null))
				{
					for (int i = 0; i < paramArray.Count; i++)
					{
						ArrayList current = (ArrayList)paramArray[i];
						SqlParameter param = new SqlParameter(current[0].ToString(), current[2]);
						param.Value = current[1];
						command.Parameters.Add(param);
					}
				}
				try
				{
					connection.Open();
					returnGuid = (Guid)command.ExecuteScalar();
					//command.ExecuteNonQuery();
					//string returnval = command.Parameters[guidParamName].Value.ToString();
					//if (returnval.Length > 0)
					//{
					//	returnGuid = new Guid(returnval);
					//}

				}
				catch (Exception)
				{

					throw;
				}
				finally
				{
					connection.Close();
				}
			}
			return returnGuid;
		}

	}
}