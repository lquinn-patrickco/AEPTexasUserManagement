using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Net;

//using ESRI.ArcGIS.SOAP;



namespace AGSSOAPUtility
{

	class AuthenticateProxy
	{

		public static System.Web.Services.Protocols.SoapHttpClientProtocol

			Authenticate(System.Web.Services.Protocols.SoapHttpClientProtocol serviceproxy,

			string username, string password, string domain, int timeout)
		{

			string url_401 = serviceproxy.Url;

			if (serviceproxy.Url.Contains("?"))

				url_401 = serviceproxy.Url.Substring(0, serviceproxy.Url.IndexOf("?"));

			url_401 += "?wsdl";

			HttpWebRequest webRequest_401 = null;

			webRequest_401 = (HttpWebRequest)HttpWebRequest.Create(url_401);

			webRequest_401.ContentType = "text/xml;charset=\"utf-8\"";

			webRequest_401.Method = "GET";

			webRequest_401.Accept = "text/xml";

			HttpWebResponse webResponse_401 = null;

			while (webResponse_401 == null || webResponse_401.StatusCode != HttpStatusCode.OK)
			{

				try
				{

					webResponse_401 = (HttpWebResponse)webRequest_401.GetResponse();

				}

				catch (System.Net.WebException webex)
				{

					HttpWebResponse webexResponse = (HttpWebResponse)webex.Response;

					if (webexResponse.StatusCode == HttpStatusCode.Unauthorized)
					{

						if (webRequest_401.Credentials == null)
						{

							webRequest_401 = (HttpWebRequest)HttpWebRequest.Create(url_401);

							webRequest_401.ContentType = "text/xml;charset=\"utf-8\"";

							webRequest_401.Method = "GET";

							webRequest_401.Accept = "text/xml";

							webRequest_401.Credentials = new NetworkCredential(username, password, domain);

						}

						else
						{

							// if original credentials not accepted, throw exception

							throw webex;

						}

					}

					// 499 - token required, 498 - invalid token

					else if (webexResponse.StatusCode.ToString() == "499" ||

						webexResponse.StatusCode.ToString() == "498")
					{

						string tokenServiceUrl = "";

						ServiceCatalogProxy myCatalog = new ServiceCatalogProxy();

						myCatalog.Url = serviceproxy.Url.Substring(0, serviceproxy.Url.IndexOf("/services") + 9);

						if (myCatalog.RequiresTokens())

							tokenServiceUrl = myCatalog.GetTokenServiceURL();

						else

							throw new Exception("Service does not require token but status code 499 returned");

						if (string.IsNullOrEmpty(tokenServiceUrl))

							throw new Exception("Token service url unavailable");

						string url = tokenServiceUrl +
						  string.Format("?request=getToken&username={0}&password={1}&timeout={2}",
						  username, password, timeout);

						System.Net.HttpWebRequest request = (HttpWebRequest)System.Net.WebRequest.Create(url);

						System.Net.WebResponse response = request.GetResponse();

						System.IO.Stream responseStream = response.GetResponseStream();

						System.IO.StreamReader readStream = new System.IO.StreamReader(responseStream);


						string theToken = readStream.ReadToEnd();

						webRequest_401 = (HttpWebRequest)HttpWebRequest.Create(url_401 + "&token=" + theToken);

					}

					else
					{

						// if status code unrecognized, throw exception   

						throw webex;

					}

				}

				catch (Exception ex) { throw ex; }

			}

			if (webResponse_401 != null)

				webResponse_401.Close();

			if (webRequest_401.Credentials != null)

				serviceproxy.Credentials = webRequest_401.Credentials;

			if (webRequest_401.RequestUri.ToString().Contains("token"))
			{

				string myToken =
					ParseStringIntoNameValueCollection(webRequest_401.RequestUri.ToString())["token"];

				string baseServiceProxyUrl = serviceproxy.Url;

				if (serviceproxy.Url.Contains("?"))

					baseServiceProxyUrl = serviceproxy.Url.Substring(0, serviceproxy.Url.IndexOf("?"));



				serviceproxy.Url = baseServiceProxyUrl + "?token=" + myToken;

			}

			return serviceproxy;

		}


		private static System.Collections.Specialized.NameValueCollection

			ParseStringIntoNameValueCollection(string argumentValues)
		{

			System.Collections.Specialized.NameValueCollection keyValColl =

				new System.Collections.Specialized.NameValueCollection();

			string[] keyValuePairs = argumentValues.Split(new char[] { '&' },

				StringSplitOptions.RemoveEmptyEntries);

			for (int i = 0; i < keyValuePairs.Length; i++)
			{

				string keyval = keyValuePairs[i];

				int index = keyval.IndexOf('=');

				if (index >= 0)
				{

					string key = keyval.Substring(0, index);

					string val = keyval.Substring(index + 1);

					keyValColl.Add(key, val);

				}

			}

			return keyValColl;

		}

	}
}