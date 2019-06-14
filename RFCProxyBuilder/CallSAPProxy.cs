using System;
using System.Data;
using SAP.Connector;
using SAP.Connector.Rfc;
using System.Collections;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.XPath;
using System.Xml.Serialization;

namespace RFCProxyBuilder
{
    public class CallSAPProxy : SAPServerConnector
    {
        #region Member VARIABLE
        private const string COLUMN_PARAMETER = "PARAMETER";
        private const string COLUMN_PARAMCLASS = "PARAMCLASS";
        private const string COLUMN_FIELDNAME = "FIELDNAME";
        private const string COLUMN_EXID = "EXID";
        private const string COLUMN_TABNAME = "TABNAME";
        private const string COLUMN_TABTYPE = "ROLLNAME";
        private const string XPATH_HNODE = "//*[(@EXID = 'h' or @EXID = 'v' or @EXID = 'u') and (count(.//child::*) <= 0)]";
        #endregion

      public CallSAPProxy()
        {
        }

        ~CallSAPProxy()
        {
            base.DisconnectSAPServer();
        }
        public override bool ConnectSAPServer()
        {
            return base.ConnectSAPServer();
        }
        private bool ConnectSAPServer(string strTYPE, string strASHOST, short nSYSNR, short nCLIENT, string strLANG, string strUSER, string strPASSWD)
        {
            bool bRetVal = false;

            base.SetConnectionInfo(strTYPE, strASHOST, nSYSNR, nCLIENT, strLANG, strUSER, strPASSWD); 
            bRetVal = base.ConnectSAPServer(); 

            return bRetVal;
        }

        public override void DisconnectSAPServer()
        {
            base.DisconnectSAPServer();
        }


        private DataTable RFC_Function_Search(string strFuncName, string strGroupName, string strLang)
        {
            RFCFUNCTable oRfcTbl = null;

            if (this.Connection.IsOpen == false)
                return null;

            try
            {
                oRfcTbl = new RFCFUNCTable();

                RFC_FUNCTION_SEARCH(strFuncName, strGroupName, strLang, ref oRfcTbl);
            }
            catch (Exception exp)
            {
                throw exp;
            }

            return oRfcTbl.ToADODataTable(); // Converts the SAP table to an ADO.NET DataTable.  
        }

        private DataTable RFC_Get_Function_Interface(string strFuncName, string strLang)
        {
            RFC_FUNINTTable oRFCFT = null;

            if (this.Connection.IsOpen == false)
                return null;

            try
            {
                oRFCFT = new RFC_FUNINTTable();
                RFC_GET_FUNCTION_INTERFACE(strFuncName, strLang, "X", ref oRFCFT);
            }
            catch (Exception exp)
            {
                throw exp;
            }

            return oRFCFT.ToADODataTable(); // Converts the SAP table to an ADO.NET DataTable.  
        }

        private DataTable RFC_Get_Structure_Definition(string strTabName, out int nTabLen)
        {
            RFC_FIELDSTable oRFCFT = null;
            nTabLen = -1;

            if (this.Connection.IsOpen == false)
                return null;

            try
            {
                oRFCFT = new RFC_FIELDSTable();

                RFC_GET_STRUCTURE_DEFINITION(strTabName, null, out nTabLen, ref oRFCFT);
            }
            catch (Exception exp)
            {
                throw exp;
            }

            return oRFCFT.ToADODataTable(); // Converts the SAP table to an ADO.NET DataTable.  
        }

        private DataTable RFC_Get_Unicode_Structure(string strTabName, out int nLen1, out int nLen2)
        {
            int nB1TabLen = 0;
            int nB2TabLen = 0;
            int nB4TabLen = 0;
            int nCharLen = 0;
            byte[] btUUID = null;

            RFC_FLDS_UTable oFields = null;

            nLen1 = nLen2 = -1;

            if (this.Connection.IsOpen == false)
                return null;

            try
            {
                oFields = new RFC_FLDS_UTable();

                RFC_GET_UNICODE_STRUCTURE("", "", "", strTabName, out nB1TabLen, out nB2TabLen, out nB4TabLen, out nCharLen, out btUUID, ref oFields);

                nLen1 = nB1TabLen;
                nLen2 = nB2TabLen;
            }
            catch (Exception exp)
            {
                throw exp;
            }

            return oFields.ToADODataTable(); // Converts the SAP table to an ADO.NET DataTable.  
        }

        private DataTable RFC_Get_TabName(String strTabName, out X030L oHeader)
        {
			X031LTable oNameTab  = null;
            oHeader = null;

            if (this.Connection.IsOpen == false)
                return null;

            try
            {
                oNameTab = new X031LTable();
                RFC_GET_NAMETAB(strTabName, out oHeader, ref oNameTab);
            }
            catch (Exception exp)
            {
                throw exp;
            }

            return oNameTab.ToADODataTable(); // Converts the SAP table to an ADO.NET DataTable.  
        }

        private String ConvertDataTable2XML(DataTable oDT)
        {
            String strXML = null;       
            DataSet oDS = null;         
            String strNamespace = null; 
            int nColIdx = 0; // Column Index
            int nRowIdx = 0; // Row Index

            DataTable oDTClone = null; // Clone DataTable 
            DataRow oRow = null; // // Clone Record
            try
            {
                for (nColIdx = 0; nColIdx < oDT.Columns.Count; ++nColIdx)
                    oDT.Columns[nColIdx].ColumnMapping = MappingType.Attribute;

                if (String.Equals(oDT.TableName, "X031LTable") || String.Equals(oDT.TableName, "X030LTable"))
                {
                    oDTClone = oDT.Clone();
                    for (nColIdx = 0; nColIdx < oDTClone.Columns.Count; ++nColIdx)
                    {
                        if (oDTClone.Columns[nColIdx].DataType.Equals(Type.GetType("System.Byte[]")))
                            oDTClone.Columns[nColIdx].DataType = Type.GetType("System.Int32");
                    } // for

                    for (nRowIdx = 0; nRowIdx < oDT.Rows.Count; ++nRowIdx)
                    {
                        oRow = oDTClone.NewRow();

                        for (nColIdx = 0; nColIdx < oDT.Columns.Count; ++nColIdx)
                        {
                            if (oDT.Columns[nColIdx].DataType.Equals(Type.GetType("System.Byte[]")))
                            {
                                oRow[nColIdx] = ConvertByte2Int((Byte[])oDT.Rows[nRowIdx][nColIdx]);
                            }
                            else
                            {
                                oRow[nColIdx] = oDT.Rows[nRowIdx][nColIdx];
                            }
                        }
                        oDTClone.Rows.Add(oRow);
                    } //outer for 
                    oDTClone.AcceptChanges();
                } // if

                oDS = new DataSet();
                if (oDTClone != null)
                    oDS.Tables.Add(oDTClone); 
                else
                    oDS.Tables.Add(oDT);
                oDS.DataSetName = "ROOT_" + oDT.TableName;

                XmlDataDocument oXMLDOC = new XmlDataDocument(oDS);
                strXML = oXMLDOC.InnerXml;

                strNamespace = String.Format("xmlns=\"{0}\"", this.GetType().Namespace);

                strXML = strXML.Replace(strNamespace, "");
            }
            catch (Exception exp)
            {
                throw exp;
            }

            return strXML;
        }

        private int ConvertByte2Int(Byte[] bt256)
        {
            Double dblRetVal = 0;
            int nSize = bt256.Length - 1;

            for (int i = nSize; i >= 0; --i)
            {
                dblRetVal += ((Double)bt256[i] * Math.Pow(0x100, nSize - i));
            }

            return (int)dblRetVal;
        }

        public DataTable get_RFCFunctionNames(string strFilter, string strGroupName, string strLang)
        {
            DataTable oDT = null;	
            try
            {
                oDT = RFC_Function_Search(strFilter, strGroupName, strLang); 
            }
            catch (Exception exp)
            {
                throw exp;
            }

            return oDT;
        }

        public String get_RFCProxyInfo(string strFuncName, string strLang)
        {
            DataTable oDTParam = null;  

            XmlDocument oXmlDoc = null; 
            XmlNode oNode = null;       
            String strXML = null;       

            try
            {
                oDTParam = RFC_Get_Function_Interface(strFuncName, strLang);
                oDTParam.Namespace = "";        

                strXML = ConvertDataTable2XML(oDTParam);
                if (String.IsNullOrEmpty(strXML))
                    return strXML;

                oXmlDoc = new XmlDocument();
                oXmlDoc.PreserveWhitespace = false;
                oXmlDoc.LoadXml(strXML);
                oNode = oXmlDoc.DocumentElement;

                GetRFCStructDef(ref oNode);

                strXML = oXmlDoc.InnerXml;
            }
            catch (Exception exp)
            {
                throw exp;
            }

            return strXML;
        }

        private DataRow GetDataRowFromDT(DataTable oDT, String strFieldName)
        {
            DataRow[] oDRow = null;
            String strQuery = null;

            try
            {
                strQuery = String.Format("{0} = '{1}'", COLUMN_FIELDNAME, strFieldName);
                oDRow = oDT.Select(strQuery);
            }
            catch (Exception exp)
            {
                throw exp;
            }

            return oDRow[0];
        }
  
        private void GetRFCStructDef(ref XmlNode oParentNode)
        {
            XmlNodeList oNodeList = null;
            DataTable oDTStruct = null;  

            String strParamClass = null;
            String strExid = null;      
            String strTabName = null;   

            String strDTXML = null;     
            XmlDocument oXmlDoc = null; 
            XmlNode oDTNode = null;
            XmlNode oImportedNode = null;

            X030L oX030LHeader = null;  
            try
            {
                oXmlDoc = new XmlDocument();
                oXmlDoc.PreserveWhitespace = false;

                while (true)
                {
                    oNodeList = oParentNode.SelectNodes(XPATH_HNODE);
                    if (oNodeList == null || oNodeList.Count == 0)
                        break;

                    for (int i = 0; i < oNodeList.Count; ++i)
                    {
                        strParamClass = strTabName = null;
                        strExid = oNodeList[i].Attributes[COLUMN_EXID].Value;
                        if (String.Equals(oNodeList[i].Name, "RFC_FUNINTTable"))
                        {
                            strParamClass = oNodeList[i].Attributes[COLUMN_PARAMCLASS].Value;
                            if (strParamClass.Equals("T")
                                || (!strParamClass.Equals("X") && strExid.Length.Equals(0))
                                || (strExid.Equals("u") || strExid.Equals("h") || strExid.Equals("v"))
                                )
                                strTabName = oNodeList[i].Attributes[COLUMN_TABNAME].Value;
                          }
                        else if (String.Equals(oNodeList[i].Name, "X031LTable"))
                        {
                            if (strExid.Equals("u") || strExid.Equals("h") || strExid.Equals("v"))
                                strTabName = oNodeList[i].Attributes[COLUMN_TABTYPE].Value;
                        }

                        if (String.IsNullOrEmpty(strTabName)) continue;

                        oDTStruct = RFC_Get_TabName(strTabName, out oX030LHeader);
                        if (oDTStruct == null || oDTStruct.Rows.Count <= 1)
                        {
                            if (!String.IsNullOrEmpty(oX030LHeader.Refname))
                            {
                                strTabName = oX030LHeader.Refname;
                                oDTStruct = RFC_Get_TabName(strTabName, out oX030LHeader);
                            }
                        }
                        oDTStruct.Namespace = "";
                        if (oX030LHeader != null) {

                            strDTXML = ConvertHeadere2XML(oX030LHeader);
                            oXmlDoc.LoadXml(strDTXML);
                            oDTNode = oXmlDoc.DocumentElement;

                            oImportedNode = oNodeList[i].OwnerDocument.ImportNode(oDTNode, true);
                            oNodeList[i].AppendChild(oImportedNode);
                        }
                        strDTXML = ConvertDataTable2XML(oDTStruct);
                        oXmlDoc.LoadXml(strDTXML);
                        oDTNode = oXmlDoc.DocumentElement;

                        oImportedNode = oNodeList[i].OwnerDocument.ImportNode(oDTNode, true);
                        oNodeList[i].AppendChild(oImportedNode);
                    } // for
                } // while (true)
            }
            catch (Exception exp)
            {
                throw exp;
            }
            return;
        }

        private String ConvertHeadere2XML(X030L oX030L)
        {
            X030LTable oTable = null;

            oTable = new X030LTable();
            oTable.Add(oX030L);

            return ConvertDataTable2XML(oTable.ToADODataTable());
        }
    } // class
}
