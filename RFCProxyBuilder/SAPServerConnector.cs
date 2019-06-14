using System;

namespace RFCProxyBuilder
{
	/// <summary>
	/// SAP 서버에 연결한다.
	/// </summary>
	public class SAPServerConnector : SAPProxy
	{
		private SAP.Connector.Destination m_oDest = null;

		public SAPServerConnector()
		{
			m_oDest = new SAP.Connector.Destination();
		}

		~SAPServerConnector()
		{
		    this.DisconnectSAPServer();

			m_oDest.Dispose();
		}

		public SAPServerConnector(string strTYPE, string strASHOST, short nSYSNR, short nCLIENT, string strLANG, string strUSER, string strPASSWD)
		{
			m_oDest = new SAP.Connector.Destination();

			SetConnectionInfo(strTYPE, strASHOST, nSYSNR, nCLIENT, strLANG, strUSER, strPASSWD);
		}

		public void SetConnectionInfo(string strTYPE, string strASHOST, short nSYSNR, short nCLIENT, string strLANG, string strUSER, string strPASSWD)
		{
			m_oDest.Type = strTYPE;
			m_oDest.AppServerHost = strASHOST;
			m_oDest.SystemNumber = nSYSNR;	
			m_oDest.Client = nCLIENT;
			m_oDest.Language = strLANG;
			m_oDest.Username = strUSER;
			m_oDest.Password = strPASSWD;
		}

		public void SetConnectionInfo(string strASHOST, short nSYSNR, short nCLIENT, string strLANG, string strUSER, string strPASSWD)
		{
			m_oDest.Type = "3";
			m_oDest.AppServerHost = strASHOST;
			m_oDest.SystemNumber = nSYSNR;	
			m_oDest.Client = nCLIENT;
			m_oDest.Language = strLANG;
			m_oDest.Username = strUSER;
			m_oDest.Password = strPASSWD;
		}

		public virtual bool ConnectSAPServer()
		{
			bool bRetVal = false;

			try
			{
				this.ConnectionString = m_oDest.ConnectionString;
				//Return 0 if failed, != 0 else.
				if ( 0 != this.Connection.Open())
					bRetVal = true;
			} 
			catch(Exception exp)
			{
				this.Connection.Close(); 
                throw exp;
			}

			return bRetVal;
		}

		public virtual void DisconnectSAPServer()
		{
            if (this.Connection != null)
            {
                if (this.Connection.IsOpen == true)
                    this.Connection.Close();

            }
        }

	}
}