using System;
using System.Data;
using System.Xml;
using System.Xml.XPath;

using System.Collections;
using System.ComponentModel;

namespace RFCProxyBuilder
{
	public class SAPServerConfig
	{
		#region Internal Class
		internal class SAPSeverInfo 
		{
			public SAPServerConfig owner;

			public SAPSeverInfo(SAPServerConfig owner) 
			{
				this.owner = owner;
			}

            //[Category("USER")]
            //[Description("USER ID")]
            //public string USER_ID
            //{
            //    get
            //    {
            //        return owner.m_userId;
            //    }
            //    set
            //    {
            //        owner.m_userId = value;
            //    }
            //}


            //[Category("USER")]
            //[Description("PASSWORD")]
            //public string USER_PASSWORD
            //{
            //    get
            //    {
            //        return owner.m_userPassWord;
            //    }
            //    set
            //    {
            //        owner.m_userPassWord = value;
            //    }
            //}

            [Category("SAP Sever Name")]
			[Description("SAP Sever Name")]
			public string NAME
			{
				get 
				{
					return owner.m_strName;
				}
				set 
				{
					owner.m_strName = value;
				}
			}

			[Category("SAP Server Information.")]
			[Description("Host name or IP address of a specific application server (R/3, No Load Balancing)")]
			public string ASHOST
			{
				get 
				{
					return owner.m_strASHOST;
				}
				set 
				{
					owner.m_strASHOST = value;
				}
			}

            [Category("SAP Server Information.")]
			[Description("R/3 system number (R/3, No Load Balancing)")]
			public string SYSNR 
			{
				get 
				{
					return owner.m_strSYSNR;
				}
				set 
				{
					owner.m_strSYSNR = value;
				}
			}

            [Category("SAP Server Information.")]
			[Description("SAP logon client number")]
			public string CLIENT 
			{
				get 
				{
					return owner.m_strCLIENT;
				}
				set 
				{
					owner.m_strCLIENT = value;
				}
			}

            [Category("SAP Server Information.")]
			[Description("SAP logon language (1-byte SAP language or 2-byte ISO language)")]
			public string LANG 
			{
				get 
				{
					return owner.m_strLANG;
				}
				set 
				{
					owner.m_strLANG = value;
				}
			}

		} // class

		#endregion

		private const String XML_FILE_NAME = "SAPINFO.XML";

		private string m_strName   = "";
		private string m_strASHOST = "";
		private string m_strSYSNR    = "";
		private string m_strCLIENT   = "";
		private string m_strLANG   = "EN";

        private string m_userId = "";
        private string m_userPassWord= "";

		public SAPServerConfig()
		{
		}

		public string LoadConfigFile(string strXMLFileName)
		{
			XmlDocument oXMLDoc     = null;
			string         strXML       = null;
		
			try 
			{
				oXMLDoc = new XmlDocument ();
				oXMLDoc.PreserveWhitespace = false;

				oXMLDoc.Load(strXMLFileName);
				strXML = oXMLDoc.InnerXml;

			}
			catch (System.IO.FileNotFoundException)
			{
				strXML = null;
			}
			catch (Exception exp)
			{
				throw exp;
			}

			return strXML;
		}

		public bool SaveConfigFile(string strXMLFileName, string strXML)
		{
			XmlDocument oXMLDoc = null;
			bool        bResult = false;
			
			try
			{
				if (String.IsNullOrEmpty(strXML))
					return bResult;

				oXMLDoc = new XmlDocument();
				oXMLDoc.PreserveWhitespace = false;

				oXMLDoc.LoadXml(strXML);

				oXMLDoc.Save(strXMLFileName);

				bResult = true;
			}
			catch (System.IO.FileNotFoundException)
			{
				bResult = false;
			}
			catch (Exception exp)
			{
				throw exp;
			}

			return bResult;
		}

		public bool AddConfiguration(ref string strXML)
		{
			XmlDocument     oXMLDoc     = null;
			XmlElement      oRootNode   = null;
			XmlElement      oSubNode    = null;
			XmlAttribute    oAttr       = null;
			XmlNodeList     oNodeList   = null;
			bool            bRetVal     = false;

			try 
			{
				oXMLDoc = new XmlDocument ();
				oXMLDoc.PreserveWhitespace = false;

				if (String.IsNullOrEmpty(strXML)) 
				{
					oRootNode = (XmlElement)oXMLDoc.CreateNode(XmlNodeType.Element, "", "SAPServer", "");
				}
				else 
				{
					oXMLDoc.LoadXml(strXML);
					oNodeList = SearchConfiguration(strXML, this.m_strName);
					if (oNodeList.Count >= 1)
						return bRetVal;

					oRootNode = (XmlElement)oXMLDoc.FirstChild;
				}

				oSubNode  = (XmlElement)oXMLDoc.CreateNode(XmlNodeType.Element, "", "ServerInfo", "");

				oAttr = oXMLDoc.CreateAttribute("NAME");
				oAttr.Value = m_strName;
				oSubNode.SetAttributeNode(oAttr);

				oAttr = oXMLDoc.CreateAttribute("ASHOST");
				oAttr.Value = m_strASHOST;
				oSubNode.SetAttributeNode(oAttr);

				oAttr = oXMLDoc.CreateAttribute("SYSNR");
				oAttr.Value = m_strSYSNR;
				oSubNode.SetAttributeNode(oAttr);

				oAttr = oXMLDoc.CreateAttribute("CLIENT");
				oAttr.Value = m_strCLIENT;
				oSubNode.SetAttributeNode(oAttr);

				oAttr = oXMLDoc.CreateAttribute("LANG");
				oAttr.Value = m_strLANG;
				oSubNode.SetAttributeNode(oAttr);

				oRootNode.AppendChild(oSubNode);
				oXMLDoc.AppendChild(oRootNode);

				strXML = oXMLDoc.InnerXml;

				bRetVal = true;
			}
			catch (System.IO.FileNotFoundException)
			{
				bRetVal = false;
			}
			catch (Exception exp)
			{
				throw exp;
			}

			return bRetVal;
		}

		public bool RemoveConfiguration(ref string strXML)
		{
			XmlDocument     oXMLDoc     = null;
			XmlNode         oNode       = null;
			string          strXPath    = null;
			bool            bRetVal     = false;

			try 
			{
				oXMLDoc = new XmlDocument ();
				oXMLDoc.PreserveWhitespace = false;

				oXMLDoc.LoadXml(strXML);
				strXPath = String.Format("//*[@NAME='{0}']", this.m_strName);
				oNode = oXMLDoc.SelectSingleNode(strXPath);
				if (oNode != null)
				{
					oXMLDoc.DocumentElement.RemoveChild(oNode);
					strXML = oXMLDoc.InnerXml;
					bRetVal = true;
				}
			} 
			catch (Exception exp)
			{
				throw exp;
			}
			return bRetVal;
		}

		public XmlNodeList SearchConfiguration(string strXMLDOC, string strSAPName)
		{
			XmlNodeList  oNodeList = null;
			XmlDocument  oXMLDOC   = null;
			string       strXPath  = null;
			try
			{
				oXMLDOC  = new XmlDocument();
				oXMLDOC.PreserveWhitespace = false;
				oXMLDOC.LoadXml(strXMLDOC);

				strXPath = String.Format("//*[@NAME = '{0}']", strSAPName);

				oNodeList  = oXMLDOC.SelectNodes(strXPath);
			}
			catch (Exception exp)
			{
				throw exp;
			}

			return oNodeList;
		}

		public String[] GetServerNames(string strXMLDOC)
		{
			string[] arrNames = null;

			XmlNodeList  oNodeList = null;
			XmlDocument  oXMLDOC   = null;

			if (strXMLDOC == null) 
				return arrNames;

			try
			{
				oXMLDOC = new XmlDocument();
				oXMLDOC.PreserveWhitespace = false;
				oXMLDOC.LoadXml(strXMLDOC);

				oNodeList = oXMLDOC.SelectNodes("//@NAME");
			
				arrNames = new string[oNodeList.Count];
				for (int i = 0; i < oNodeList.Count; ++i)
				{
					arrNames[i] = oNodeList[i].Value;
				}
			}
			catch (Exception exp)
			{
				throw exp;
			}
			return arrNames;
		}
	} // class
}
