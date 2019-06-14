using System;
using System.IO;
using System.Collections;

using System.Xml;
using System.Text;

using System.Resources;
using System.Reflection;

namespace RFCProxyBuilder
{
    /// <summary>
    /// RFCProxyGen에 대한 요약 설명입니다.
    /// </summary>
    public class RFCProxyGen : RFCProxyBuild
    {
        #region Member Constants
        private const String RESOURCE_FILENAME = "RFCProxyGen";
        private const String RES_RFC_TABLE_TEMP = "RFC_TABLE_TEMP";
        private const String RES_RFC_STRUCTURE_TEMP = "RFC_STRUCTURE_TEMP";
        private const String RES_RFC_ELEMENTFIELD_TEMP = "RFC_ELEMENTFIELD_TEMP";
        private const String RES_RFC_ARRAYFIELD_TEMP = "RFC_ARRAYFIELD_TEMP";
        private const String RES_CLIENT_PROXY_TEMP = "SAP_CLIENT_PROXY_TEMP";
        private const String RES_SERVER_PROXY_TEMP = "SAP_SERVER_PROXY_TEMP";

        private const String RES_SIMPLE_PARA_TEMP = "SIMPLE_PARA_TEMP";
        private const String RES_TABLE_PARA_TEMP = "TABLE_PARA_TEMP";
        private const String RES_STRUCT_PARA_TEMP = "STRUCT_PARA_TEMP";

        private const String STR_REMOVE_LENGTH = ", Length=[=INTLENGTH=], Length2=[=INTLENGTH2=]";
        private const String STR_REMOVE_DECIMALS = ", Decimals=[=DECIMALS=]";

        private const String XML_ATTRIBUTE_FIELDNAME = "FIELDNAME";
        private const String XML_ATTRIBUTE_EXID = "EXID";
        private const String XML_ATTRIBUTE_INTLENGTH = "INTLENGTH";
        private const String XML_ATTRIBUTE_OFFSET = "OFFSET";
        private const String XML_ATTRIBUTE_TABNAME = "TABNAME";
        private const String XML_ATTRIBUTE_PARAMCLASS = "PARAMCLASS";
        private const String XML_ATTRIBUTE_PARAMETER = "PARAMETER";
        private const String XML_ATTRIBUTE_OPTIONAL = "OPTIONAL";
        private const String XML_ATTRIBUTE_DECIMALS = "DECIMALS";
        private const String XML_ATTRIBUTE_DTYP = "DTYP";
        private const String XML_ATTRIBUTE_ROLLNAME = "ROLLNAME";
        private const String XML_ATTRIBUTE_DBLENGTH = "DBLENGTH";
        private const String XML_ATTRIBUTE_DEPTH = "DEPTH";
        private const String XML_ATTRIBUTE_TABLEN = "Tablen";

        private const String XML_ATTRIBUTE_LENGTHB1 = "LENGTH_B1";
        private const String XML_ATTRIBUTE_LENGTHB2 = "LENGTH_B2";
        private const String XML_ATTRIBUTE_OFFSETB1 = "OFFSET_B1";
        private const String XML_ATTRIBUTE_OFFSETB2 = "OFFSET_B2";

        private const String XML_VALUE_PARAMCLASS_X = "X";
        private const String XML_VALUE_PARAMCLASS_T = "T";
        private const String XML_VALUE_PARAMCLASS_E = "E";

        private const String XPATH_EXCEPTION_PARAM = "//RFC_FUNINTTable[@PARAMCLASS = 'X']";
        private const String XPATH_NOT_EXCEPTION_PARAM = "//RFC_FUNINTTable[@PARAMCLASS != 'X']";
        private const String XPATH_EXIDLPARAMNODE = "*/*[@EXID = 'L']";
        private const String XPATH_EXIDHSUBPARAMNODE = "child::*/child::*[@EXID = 'h' or EXID = 'v' or EXID = 'u']";

        private const String REPLACE_PARAM_INFO = "// Parameters";
        private const String REPLACE_PARAM_NAME = "[=PARAM_NMAE=]";
        private const String REPLACE_PARAM_ORDER = "[=PARAM_ORDER=]"; 

        private const String REPLACE_RESULT = "// [=PARAM_NMAE=]=([=var_type=])results[[=PARAM_ORDER=]];";
        private const String REPLACE_NAMESPACE = "[=NAMESPACE=]";
        private const String REPLACE_CLASSNAME = "[=CLASSNAME=]";
        private const String REPLACE_TYPENAME = "[=TYPENAME=]";
        private const String REPLACE_FIELDNAME = "[=FIELD_NAME=]";
        private const String REPALCE_FIELDXMLNAME = "[=FIELD_XMLNAME=]";
        private const String REPLACE_RFCTYPE = "[=RFC_TYPE=]";
        private const String REPLACE_INTLENGTH = "[=INTLENGTH=]";
        private const String REPLACE_INTLENGTH2 = "[=INTLENGTH2=]";
        private const String REPLACE_OFFSET = "[=OFFSET=]";
        private const String REPLACE_OFFSET2 = "[=OFFSET2=]";
        private const String REPLACE_DECIMALS = "[=DECIMALS=]";
        private const String REPLACE_VARTYPE = "[=var_type=]";
        private const String REPLACE_ABAPNAME = "[=ABAP_NAME=]";
        private const String REPLACE_XMLPARAMNAME = "[=XML_PARAM_NMAE=]";
        private const String REPLACE_PROXYFUNCNAME = "[=PROXY_FUNC_NAME=]";
        private const String REPLACE_PROXYXMLNAME = "[=PROXY_XML_NAME=]";
        private const String REPLACE_DIRECTION = "[=DIRECTION=]";
        private const String REPLACE_OPTIONAL = "[=OPTIONAL=]";

        private const String REPLACE_EXCEPT_INFO = "// public const string [=EXCEPTION=]=\"[=EXCEPTION=]\";";
        private const String REPLACE_EXCEPT = "[=EXCEPTION=]";

        private const String XPATH_TABLESTRUCT_NODE = "//*[@TABNAME !='' and ((@EXID = '' and @PARAMCLASS != 'X') or @EXID = 'u' or @EXID = 'h' or @EXID = 'v')]";
        private const String XPATH_X031LROOT_NODE = "//ROOT_X031LTable";
        private const String XPATH_X030LTABLE_NODE = ".//X030LTable";
        #endregion

        #region Member VARIABLE
        private ArrayList m_arrSAPStructClass = null;
        private ArrayList m_arrSAPTableClass = null;
        private String m_strFuncProxyClass = null;
        private ArrayList m_arrProcessedTableName = null;
        private ArrayList m_arrProcessedStructName = null;
        private ResourceManager m_oRM = null;
        #endregion

        public RFCProxyGen()
        {
            m_arrSAPStructClass = new ArrayList();
            m_arrSAPTableClass = new ArrayList();
            m_arrProcessedTableName = new ArrayList();
            m_arrProcessedStructName = new ArrayList();

            OpenResource();
        }

        ~RFCProxyGen()
        {
            if (m_arrSAPStructClass != null)
                m_arrSAPStructClass.Clear();

            if (m_arrSAPTableClass != null)
                m_arrSAPTableClass.Clear();

            if (m_arrProcessedTableName != null)
                m_arrProcessedTableName.Clear();

            if (m_arrProcessedStructName != null)
                m_arrProcessedStructName.Clear();

            CloseResource();
        }

        private void OpenResource()
        {
            String strResourceName = null;
            strResourceName = String.Format("{0}.{1}", this.GetType().Namespace, RESOURCE_FILENAME);

            if (m_oRM == null)
                m_oRM = new ResourceManager(strResourceName, this.GetType().Assembly);
        }

        private void CloseResource()
        {
            if (m_oRM != null)
            {
                m_oRM.ReleaseAllResources();
                m_oRM = null;
            }
        }

        #region

        private void ConvEXID2Type(string strEXIDVal, ref String strRFCType, ref String strCSType)
        {
            switch (strEXIDVal)
            {
                case "C":
                    strRFCType = "RFCTYPE_CHAR";
                    strCSType = "String";
                    break;
                case "I":
                    strRFCType = "RFCTYPE_INT";
                    strCSType = "Int32";
                    break;
                case "F":
                    strRFCType = "RFCTYPE_FLOAT";
                    strCSType = "Double";
                    break;
                case "D":
                    strRFCType = "RFCTYPE_DATE";
                    strCSType = "String";
                    break;
                case "T":
                    strRFCType = "RFCTYPE_TIME";
                    strCSType = "String";
                    break;
                case "P":
                    strRFCType = "RFCTYPE_BCD";
                    strCSType = "Decimal";
                    break;
                case "N":
                    strRFCType = "RFCTYPE_NUM";
                    strCSType = "String";
                    break;
                case "X":
                    strRFCType = "RFCTYPE_BYTE";
                    strCSType = "Byte[]";
                    break;
                case "b":
                    strRFCType = "RFCTYPE_INT1";
                    strCSType = "Byte";
                    break;
                case "s":
                    strRFCType = "RFCTYPE_INT2";
                    strCSType = "Int16";
                    break;
                case "g":
                    strRFCType = "RFCTYPE_STRING";
                    strCSType = "String";
                    break;
                case "y":
                    strRFCType = "RFCTYPE_XSTRING";
                    strCSType = "Byte[]";
                    break;
                case "v":
                case "h":
                    strRFCType = "RFCTYPE_XMLDATA";
                    break;
                case "u":
                    strRFCType = "RFCTYPE_STRUCTURE";
                    break;
            } // switch
            return;
        }

        #endregion

        private void GenRFCTableCollectionClass(String strNamespace, String strTableName, String strTypeName, 
                                                int nInitialSize, bool bOutSource, String strOutputPath)
        {
            StreamWriter oSW = null;     
            String strFilePath = null;   
            String strClassSource = null;

            try
            {
                strFilePath = (bOutSource) ? strOutputPath + strTableName.Replace("/", "_") + ".CS" : null;

                strClassSource = m_oRM.GetString(RES_RFC_TABLE_TEMP);

                strClassSource = strClassSource.Replace(REPLACE_NAMESPACE, strNamespace);
                strClassSource = strClassSource.Replace(REPLACE_CLASSNAME, strTableName.Replace("/", "_"));
                strClassSource = strClassSource.Replace(REPLACE_TYPENAME, strTypeName.Replace("/", "_"));
                strClassSource = strClassSource.Replace("new String();", "\"\";");
                strClassSource = strClassSource.Replace("return new Byte[]();", 
                String.Format("return new Byte[{0}];", nInitialSize));

                if (!String.IsNullOrEmpty(strFilePath))
                {
                    oSW = File.CreateText(strFilePath);
                    oSW.Write(strClassSource);
                }

                m_arrSAPTableClass.Add(strClassSource);
            }
            catch (IOException iexp)
            {
                Console.Write(iexp.Message);
            }
            catch (Exception exp)
            {
                throw exp;
            }
            finally
            {
                if (oSW != null) oSW.Close();
            }
            return;
        }

        private int AddFieldInfo2StructClass(XmlNodeList oFieldNodeList, ref String strClassSource)
        {
            XmlNode oFieldNode = null;      
            String strElemFieldTemp = null; 
            String strArrayFieldTemp = null;
            String strFieldSource = null;   

            String strEXIDVal = null;  
            String strAbapName = null; 
            String strRFCType = null;  
            String strCSType = null;   

            String strDecimals = null; 

            String strFieldDepth = null;
            String strDepth = null;
            String strLength = null; int nLength = 0;
            String strOffset = null; int nOffset = 0;
            String strLength2 = null;int nLength2 = 0;
            String strOffset2 = null;int nOffset2 = 0;

            int nStructLength2 = 0;   

            try
            {
                strElemFieldTemp = m_oRM.GetString(RES_RFC_ELEMENTFIELD_TEMP);
                strArrayFieldTemp = m_oRM.GetString(RES_RFC_ARRAYFIELD_TEMP);

                if (oFieldNodeList.Count >= 1 )
                    strFieldDepth = oFieldNodeList[0].Attributes[XML_ATTRIBUTE_DEPTH].Value;

                for (int i = 0; i < oFieldNodeList.Count; ++i)
                {
                    oFieldNode = oFieldNodeList[i];

                    strEXIDVal = oFieldNode.Attributes[XML_ATTRIBUTE_EXID].Value;
                    if (strEXIDVal.Equals("L") || strEXIDVal.Equals("")) continue;

                    strAbapName = oFieldNode.Attributes[XML_ATTRIBUTE_FIELDNAME].Value;
                    strDecimals = oFieldNode.Attributes[XML_ATTRIBUTE_DECIMALS].Value;
                    strDepth = oFieldNode.Attributes[XML_ATTRIBUTE_DEPTH].Value;
                   
                    strLength = oFieldNode.Attributes[XML_ATTRIBUTE_DBLENGTH].Value;
                    strOffset = oFieldNode.Attributes[XML_ATTRIBUTE_OFFSET].Value;

                    if (String.Equals(strFieldDepth, strDepth))
                        nOffset2 += nLength2;

                    nLength = Convert.ToInt32(strLength);
                    nOffset = Convert.ToInt32(strOffset);
                    
                    switch (strEXIDVal)
                    {
                        case "h":
                        case "g":
                        case "u":
                            nLength2 = nLength;
                            break;
                        case "v":
                            break;
                        case "C":
                        case "D":
                        case "T":
                        case "N":
                            nLength2 = nLength * 2;
                            break;
                        default:
                            nLength2 = nLength;
                            break;
                    }
                    strLength2 = nLength2.ToString();
                    nStructLength2 += nLength2;
   
                    if (String.Equals(strFieldDepth, strDepth))
                    {
                        if (nOffset > nOffset2) nOffset2 = nOffset;
                        nStructLength2 += (nOffset2 % 2);
                        nOffset2 += (nOffset2 % 2);
                        strOffset2 = nOffset2.ToString();
                    }
                    else
                    {
                        continue;
                    }

                    strRFCType = strCSType = null;
                    ConvEXID2Type(strEXIDVal, ref strRFCType, ref strCSType);
                    switch (strEXIDVal)
                    {
                        case "h":
                            strFieldSource = strArrayFieldTemp;
                            strCSType = oFieldNode.Attributes[XML_ATTRIBUTE_ROLLNAME].Value;
                            break;
                        case "v":
                        case "u":
                            strFieldSource = strElemFieldTemp;
                            strCSType = oFieldNode.Attributes[XML_ATTRIBUTE_ROLLNAME].Value;
                            break;

                        default:
                            strFieldSource = strElemFieldTemp;
                            break;
                    }

                    ReplaceFieldSource(ref strFieldSource, strAbapName,
                                        strEXIDVal, strRFCType, strCSType, strDecimals,
                                        strLength, strLength2, strOffset, strOffset2);
                    strClassSource = strClassSource.Replace(REPLACE_PARAM_INFO, strFieldSource + "\n" + REPLACE_PARAM_INFO);
                   
               } // for
            }
            catch (Exception exp)
            {
                throw exp;
            }
            return nStructLength2;
        }

        private void ReplaceFieldSource(ref String strFieldSource, String strAbapName,
                                        String strEXIDVal, String strRFCType, String strCSType, String strDecimals,
                                        String strLength, String strLength2, String strOffset, String strOffset2)
        {
            String strXMLName = null;
            String strFieldName = null;
            String strCShapType = null;

            try
            {
                if (strEXIDVal.Equals("u"))
                    strFieldSource = strFieldSource.Replace(STR_REMOVE_LENGTH, "");
                if (strDecimals.Equals("0"))
                    strFieldSource = strFieldSource.Replace(STR_REMOVE_DECIMALS, "");

                strCShapType = (strCSType.Equals("")) ? strAbapName : strCSType;

                strCShapType = strCShapType.Replace("/", "_");
                strCShapType = strCShapType.Replace("%", "_");
                strCShapType = strCShapType.Replace("$", "_");

                strXMLName = strAbapName.Replace("/", "_");
                strXMLName = strXMLName.Replace("#", "_--23");
                strXMLName = strXMLName.Replace("$", "_--24");
                strXMLName = strXMLName.Replace("%", "_--25");
                strXMLName = strXMLName.Replace("&", "_--26");
                if (Char.IsNumber(strXMLName, 0)) strXMLName = "_--3" + strXMLName;

                strFieldName = strAbapName.Replace("#", "__");
                strFieldName = strFieldName.Replace("/", "_").ToLower();
                strFieldName = strFieldName.Replace("$", "_").ToLower();
                strFieldName = strFieldName.Replace("%", "__").ToLower();
                strFieldName = strFieldName.Replace("&", "_").ToLower();
                strFieldName = Format1stUpper(strFieldName);
                if (Char.IsNumber(strFieldName, 0)) strFieldName = "N" + strFieldName;

                strFieldSource = strFieldSource.Replace(REPLACE_INTLENGTH2, strLength2);  
                strFieldSource = strFieldSource.Replace(REPLACE_OFFSET2, strOffset2);     
                strFieldSource = strFieldSource.Replace(REPLACE_ABAPNAME, strAbapName);   
                strFieldSource = strFieldSource.Replace(REPALCE_FIELDXMLNAME, strXMLName);
                strFieldSource = strFieldSource.Replace(REPLACE_FIELDNAME, strFieldName); 
                strFieldSource = strFieldSource.Replace(REPLACE_RFCTYPE, strRFCType);     
                strFieldSource = strFieldSource.Replace(REPLACE_INTLENGTH, strLength);    
                strFieldSource = strFieldSource.Replace(REPLACE_DECIMALS, strDecimals);   
                strFieldSource = strFieldSource.Replace(REPLACE_OFFSET, strOffset);       
                strFieldSource = strFieldSource.Replace(REPLACE_VARTYPE, strCShapType);   
            }
            catch (Exception exp)
            {
                throw exp;
            }
        }

        private String Format1stUpper(String strFieldName)
        {
            int nPos = 0; 
            String strRetVal = null; 
            Char[] chFieldName = null;

            chFieldName = strFieldName.ToCharArray();

            nPos = -1;
            do
            {
                if (chFieldName[nPos + 1] >= 'a')
                {
                    chFieldName[nPos + 1] = (Char)(chFieldName[nPos + 1] - 0x20);
                }

                nPos = strFieldName.IndexOf("_", nPos+1);
            } while (nPos >= 0 && nPos < (chFieldName.Length -1));

            strRetVal = new String(chFieldName);

            return strRetVal;
        }

        private void GenRFCStructClass(XmlNodeList oFieldNodeList, String strNamespace, String strAbapName,
                                       String strLength, bool bOutSource, string strOutPath)
        {
            String strClassSource = null;

            String strClassName = null;  
            String strFilePath = null;   
            StreamWriter oSW = null;     

            int nLength2 = 0;            

            try
            {
                strClassSource = m_oRM.GetString(RES_RFC_STRUCTURE_TEMP);

                strClassName = strAbapName.Replace("/", "_");

                strClassSource = strClassSource.Replace(REPLACE_NAMESPACE, strNamespace);
                strClassSource = strClassSource.Replace(REPLACE_ABAPNAME,  strAbapName); 
                strClassSource = strClassSource.Replace(REPLACE_CLASSNAME, strClassName);
                strClassSource = strClassSource.Replace(REPLACE_INTLENGTH, strLength);   

                nLength2 = AddFieldInfo2StructClass(oFieldNodeList, ref strClassSource);
                if (Convert.ToInt32(strLength) > nLength2) nLength2 = Convert.ToInt32(strLength);
                strClassSource = strClassSource.Replace(REPLACE_INTLENGTH2, nLength2.ToString());

                m_arrSAPStructClass.Add(strClassSource);

                if (bOutSource)
                {
                    strFilePath = strOutPath + strAbapName.Replace("/", "_") + ".CS";
                    oSW = File.CreateText(strFilePath);
                    oSW.Write(strClassSource);
                }
            }
            catch (IOException iexp)
            {
                Console.Write(iexp.Message);
            }
            catch (Exception exp)
            {
                throw exp;
            }
            finally
            {
                if (oSW != null) oSW.Close();
            }
            return;
        }


        private void GenRFCFunctionProxy(XmlElement oXmlElem, String strNamespace, String strClassName, String strAbapName,
                                         bool bClientProxy, bool bOutSource, String strOutPath)
        {
            String strProxyClass = null;  
            XmlNodeList oNodeList = null; 
            String strXmlPath = null;     

            String strProxyFuncName = null; 
            String strProxyXMLName = null;  
            StreamWriter oSW = null;        
           
            String strProxyFileFullName = null; 

            try
            {
                strProxyClass = bClientProxy ? 
                                m_oRM.GetString(RES_CLIENT_PROXY_TEMP) : m_oRM.GetString(RES_SERVER_PROXY_TEMP);
                strProxyClass = strProxyClass.Replace(REPLACE_NAMESPACE, strNamespace);
                strProxyClass = strProxyClass.Replace(REPLACE_CLASSNAME, strClassName);
                strProxyClass = strProxyClass.Replace(REPLACE_ABAPNAME,  strAbapName); 

                strProxyFuncName = (strAbapName[0] >= 0x30 && strAbapName[0] <= 0x39) ? "_" + strAbapName : strAbapName;
                strProxyXMLName = strProxyFuncName.Replace("/", "_-");
                strProxyFuncName = strProxyFuncName.Replace("/", "_");
                strProxyClass = strProxyClass.Replace(REPLACE_PROXYXMLNAME, strProxyXMLName);
                strProxyClass = strProxyClass.Replace(REPLACE_PROXYFUNCNAME, strProxyFuncName);

                if (oXmlElem != null)
                {
                    strXmlPath = XPATH_EXCEPTION_PARAM;
                    oNodeList = oXmlElem.SelectNodes(strXmlPath); 
                    if (oNodeList.Count > 0)
                        AddExceptParam2ProxyClass(oNodeList, ref strProxyClass);

                    strXmlPath = XPATH_NOT_EXCEPTION_PARAM;
                    oNodeList = oXmlElem.SelectNodes(strXmlPath); 
                    if (oNodeList.Count > 0)
                        AddNormalParam2ProxyClass(oNodeList, bClientProxy, ref strProxyClass);
                }

                // 출력 소스에서 Temp Line 제거
                strProxyClass = strProxyClass.Replace(REPLACE_EXCEPT_INFO, "");
                strProxyClass = strProxyClass.Replace(REPLACE_RESULT, "");
                strProxyClass = strProxyClass.Replace(REPLACE_PARAM_INFO, "");
                strProxyClass = strProxyClass.Replace(REPLACE_PARAM_NAME, "");
                
                m_strFuncProxyClass = strProxyClass;
                
                if (bOutSource) 
                {
                    strProxyFileFullName = strOutPath + strProxyFuncName + "Proxy.CS";

                    oSW = File.CreateText(strProxyFileFullName);
                    oSW.Write(strProxyClass);
                }
            }
            catch (IOException iexp)
            {
                Console.Write(iexp.Message);
            }
            catch (Exception exp)
            {
                throw exp;
            }
            finally
            {
                if (oSW != null) oSW.Close();
            }
            return;
        }


        private void AddNormalParam2ProxyClass(XmlNodeList oNodeList, bool bClientProxy, ref String strClassSource)
        {
            String strSimpleParamTemp = null;
            String strTableParamTemp = null; 
            String strStructParamTemp = null;
            
            String strParamInfo = null;  
            String strFuncParams = null; 

            #region 템플릿에서 교체될 문자열을 가진 변수
            String strAbapName = null;    // ABAP 이름, PARAMETER 노드의 값
            String strRFCType = null;     // RFC 형식, EXID 값을 이용하여 구한다.
            String strCSTypeName = null;  // C# 변수 형식 이름, EXID 값으로 기본 형식, XMLDATA, ITAB 형식은 TABNAME 값.
            String strOptional = null;    // OPTIONAL
            String strLength = null;      // INTLENGTH
            String strParamName = null;   // 파리미터 이름, ABAP 이름에서 변환된 형식 이름
            String strXMLParamName = null;// XML 요소에서 사용될 파라미터 이름
            String strParamClass = null;  // PARAMCLASS
            String strEXID = null;        // EXID
            String strResult = null; // Proxy 함수가 반환해야 할 결과 문자열 집합
            #endregion

            int nOutOrder = -1;
            
            XmlNode oFieldNode = null;      
            XmlNode oX031TableNode = null;  
          
            String strX031TabName = null;   
            String strX031RollName = null;  
            bool bSameTabName = false;      
            int nX031NodeCnt = 0;           

            int nParamKind = 0;    
            ArrayList arProcessedParamName= null; 
            bool bProcessed = false;

            String strToken = ",\n"; 

            try
            {
                strSimpleParamTemp = m_oRM.GetString(RES_SIMPLE_PARA_TEMP);
                strTableParamTemp = m_oRM.GetString(RES_TABLE_PARA_TEMP);
                strStructParamTemp = m_oRM.GetString(RES_STRUCT_PARA_TEMP);

                arProcessedParamName = new ArrayList(oNodeList.Count);
                for (int i = 0; i < oNodeList.Count; ++i)
                {
                    strX031RollName = strX031TabName = null;
                    bSameTabName = false;
                    nX031NodeCnt = 0;

                    oFieldNode = oNodeList[i];

                    strParamClass = oFieldNode.Attributes[XML_ATTRIBUTE_PARAMCLASS].Value;
                    strAbapName = oFieldNode.Attributes[XML_ATTRIBUTE_PARAMETER].Value;
                    strCSTypeName = oFieldNode.Attributes[XML_ATTRIBUTE_TABNAME].Value;
                    strEXID = oFieldNode.Attributes[XML_ATTRIBUTE_EXID].Value;
                    strLength = oFieldNode.Attributes[XML_ATTRIBUTE_INTLENGTH].Value;
                    strOptional = oFieldNode.Attributes[XML_ATTRIBUTE_OPTIONAL].Value;
 
                    if (oFieldNode.HasChildNodes && oFieldNode.SelectSingleNode("." + XPATH_X031LROOT_NODE).HasChildNodes)
                    {
                        oX031TableNode = oFieldNode.SelectSingleNode("." + XPATH_X031LROOT_NODE).FirstChild;
                        strX031TabName = oX031TableNode.Attributes[XML_ATTRIBUTE_TABNAME].Value;
                        strX031RollName = oX031TableNode.Attributes[XML_ATTRIBUTE_ROLLNAME].Value;

                        bSameTabName = String.Equals(strCSTypeName, strX031TabName);
                        nX031NodeCnt = oFieldNode.FirstChild.ChildNodes.Count;
                    }

                    strXMLParamName = strParamName = strAbapName.Replace("/", "_");
                    if (Char.IsNumber(strParamName, 0)) 
                    {
                        strParamName = "N" + strParamName;
                        strXMLParamName = "_--3" + strXMLParamName;
                    }
                    ConvEXID2Type(strEXID, ref strRFCType, ref strCSTypeName);

                    for (int j = i + 1; j < oNodeList.Count; ++j)
                    {
                        if (String.Equals(strAbapName,oNodeList[j].Attributes[XML_ATTRIBUTE_PARAMETER].Value))
                        {
                            if (strParamClass.IndexOf(oNodeList[j].Attributes[XML_ATTRIBUTE_PARAMCLASS].Value) < 0)
                                strParamClass += oNodeList[j].Attributes[XML_ATTRIBUTE_PARAMCLASS].Value;
                        }
                    }
                    bProcessed = false;
                    for (int j = 0; j < arProcessedParamName.Count; ++j)
                    {
                        if (!String.Equals(strParamName, (string)arProcessedParamName[j]))
                            continue;
                        
                        bProcessed = true;
                        break;
                    }
                    if (!bProcessed)
                        arProcessedParamName.Add((Object)strParamName);
                    else
                        continue;

                    nParamKind = 0;
                    
                    if (strParamClass.Equals(XML_VALUE_PARAMCLASS_T) || strEXID.Equals("h"))
                        nParamKind = 1; // Table
                    else if (strEXID.Equals("v") || strEXID.Equals("u"))
                        nParamKind = 2; // Structure

                    strToken = (i == 0) ? "\n" : ",\n";

                    switch (nParamKind)
                    {
                        case 0:
                            strParamInfo = strToken + strSimpleParamTemp;
                            break;
                        case 1:
                            if (bSameTabName && (nX031NodeCnt > 1) && !String.Equals(strX031RollName, "")) {
                                    strCSTypeName += "Table";
                            }
                            strParamInfo = strToken + strTableParamTemp;
                            break;
                        case 2:
                            strParamInfo = strToken + strStructParamTemp;
                            break;
                    }
                    strParamInfo += REPLACE_PARAM_INFO;
                    strCSTypeName = strCSTypeName.Replace("/", "_");
                    
                    strParamInfo = this.GenParameterInfo(strParamInfo, strParamClass, strXMLParamName, strParamName, 
                                                         strLength, strOptional, strRFCType, strCSTypeName, (nParamKind != 0));
                    strParamInfo = strParamInfo.Replace(REPLACE_ABAPNAME, strAbapName);
                    strClassSource = strClassSource.Replace(REPLACE_PARAM_INFO, strParamInfo);

                    if (!bClientProxy) continue;

                    if (String.Equals(strParamClass, XML_VALUE_PARAMCLASS_E) 
                        || String.Equals(strParamClass, XML_VALUE_PARAMCLASS_T)
                        || String.Equals(strParamClass, "EI")
                        || String.Equals(strParamClass, "IE"))
                    {   
                        ++nOutOrder;
                        strResult = strResult + REPLACE_RESULT + "\n";
                        strResult = strResult.Replace("//", "");

                        strResult = strResult.Replace(REPLACE_PARAM_NAME, strParamName);

                        strResult = strResult.Replace(REPLACE_VARTYPE, strCSTypeName);
                        strResult = strResult.Replace(REPLACE_PARAM_ORDER, nOutOrder.ToString());
                    }

                    if (!String.Equals(strParamClass, XML_VALUE_PARAMCLASS_E))
                    {
                        strFuncParams = strFuncParams + strParamName + ",\n";
                    }
                } // for
                strClassSource = strClassSource.Replace(REPLACE_RESULT, strResult);
                if (!String.IsNullOrEmpty(strFuncParams) && strFuncParams.EndsWith(strToken))
                    strFuncParams = strFuncParams.Remove(strFuncParams.Length - strToken.Length);
                strClassSource = strClassSource.Replace(REPLACE_PARAM_NAME, strFuncParams);
            }
            catch (Exception exp)
            {
                throw exp;
            }
            return;
        }

        private string GenParameterInfo(string strParamInfo, string strParamClass, string strXMLParamName, string strParamName, 
                                        string strLength, string strOptional, string strRFCTypeName, string strCSTypeName, 
                                        bool bComplex)
        {
            string strDirection = null;
            strParamInfo = strParamInfo.Replace(REPLACE_PARAM_NAME, strParamName);
            strParamInfo = strParamInfo.Replace(REPLACE_XMLPARAMNAME, strXMLParamName); 

            strParamInfo = strParamInfo.Replace(REPLACE_RFCTYPE, strRFCTypeName);
            strParamInfo = strParamInfo.Replace(REPLACE_INTLENGTH, strLength);
            strParamInfo = strParamInfo.Replace(REPLACE_INTLENGTH2, String.Format("{0}",Convert.ToInt32(strLength) * 2));

            strParamInfo = String.Equals(strOptional, "") ? 
            strParamInfo.Replace(REPLACE_OPTIONAL, "false") :
            strParamInfo.Replace(REPLACE_OPTIONAL, "true");

            switch (strParamClass)
            {
                case "I":
                    strDirection = "RFCINOUT.IN";
                    break;
                case "E":
                    strDirection = "RFCINOUT.OUT";
                    strCSTypeName = "out " + strCSTypeName;
                    break;
                case "T":
                case "C":
                case "EI":
                case "IE":
                    strDirection = "RFCINOUT.INOUT";
                    strCSTypeName = "ref " + strCSTypeName;
                    break;
                default:
                    strDirection = "RFCINOUT.NONE";
                    break;
            }
            strParamInfo = strParamInfo.Replace(REPLACE_DIRECTION, strDirection);
            strParamInfo = strParamInfo.Replace(REPLACE_VARTYPE, strCSTypeName);
            return strParamInfo;
        }

        private void AddExceptParam2ProxyClass(XmlNodeList oNodeList, ref String strClassSource)
        {
            String strExceptName = null;
            String strExceptParam = null;

            XmlNode oNode = null;
            int nNodeCnt = 0;    
            try
            {
                nNodeCnt = oNodeList.Count;

                for (int i = 0; i < nNodeCnt; ++i)
                {
                    oNode = oNodeList.Item(i); 

                    strExceptName = oNode.Attributes[XML_ATTRIBUTE_PARAMETER].Value;
                    strExceptName = Format1stUpper(strExceptName.ToLower());

                    strExceptParam = REPLACE_EXCEPT_INFO;
                    strExceptParam = strExceptParam.Replace(REPLACE_EXCEPT, strExceptName);
                    strExceptParam = strExceptParam.Replace("// ", ""); 

                    strClassSource = strClassSource.Replace(REPLACE_EXCEPT_INFO, REPLACE_EXCEPT_INFO + "\n" + strExceptParam);
                } // for
            }
            catch (Exception exp)
            {
                throw exp;
            }
            return;
        }

        private bool gen_RFCProxyClass(string strNamespace, string strClassName, string strFuncName, 
                                       bool bClientProxy, string strInfoXML, bool bOutSource, string strOutPath)
        {
            XmlDocument oXMLDOC = null;   
            XmlNodeList oNodeList = null;
            XmlElement oXMLELEM = null;  

            bool bResult = false;

            try
            {
                m_strFuncProxyClass = "";
                m_arrSAPStructClass.Clear();
                m_arrSAPTableClass.Clear();
                m_arrProcessedTableName.Clear();
                m_arrProcessedStructName.Clear();
                if (!String.IsNullOrEmpty(strInfoXML))
                {
                    oXMLDOC = new XmlDocument();
                    oXMLDOC.PreserveWhitespace = false;
                    oXMLDOC.LoadXml(strInfoXML);

                    oXMLELEM = oXMLDOC.DocumentElement;

                    oNodeList = oXMLDOC.SelectNodes(XPATH_X031LROOT_NODE);
                    GenX031LStructTableProxy(oNodeList, strNamespace, bOutSource, strOutPath);
                }
                GenRFCFunctionProxy(oXMLELEM, strNamespace, strClassName, strFuncName, bClientProxy, bOutSource, strOutPath);

                bResult = true;
            }
            catch (Exception exp)
            {
                throw exp;
            }
            return bResult;
        }

       private void GenX031LStructTableProxy(XmlNodeList oNodeList, String strNamespace, bool bOutSource, String strOutPath)
        {
            XmlNode oTableNode = null;     
            XmlNode oParentNode = null;    
            XmlNodeList oFldHNodeList = null; 

            String strTabName = null;   
            String strStructName = null;
            String strTypeName = null;  
            String strLength = null;    
            String strParamClass = null;
            String strExid = null;      
            String strDtyp = null;      
            String strRollName = null;  
            String strFieldName = null; 

            int nInitialSize = 0; 
            bool bSkipTable = false; 
            bool bSkipStruct = false;

 
            for (int i = 0; i < oNodeList.Count; ++i)
            {
                bSkipTable = false;  
                bSkipStruct = false;
                strParamClass = "";

                oTableNode = oNodeList[i];
                oParentNode = oTableNode.ParentNode;

                if (!oTableNode.HasChildNodes) continue;

                strExid = oParentNode.Attributes[XML_ATTRIBUTE_EXID].Value;
                if (String.Equals(oParentNode.Name, "RFC_FUNINTTable"))
                {
                   strTabName = oParentNode.Attributes[XML_ATTRIBUTE_TABNAME].Value;
                    strParamClass = oParentNode.Attributes[XML_ATTRIBUTE_PARAMCLASS].Value;
                }
                else if (String.Equals(oParentNode.Name, "X031LTable"))
                {
                    strTabName = oParentNode.Attributes[XML_ATTRIBUTE_ROLLNAME].Value;
                }

                strLength = oParentNode.SelectSingleNode(XPATH_X030LTABLE_NODE).Attributes[XML_ATTRIBUTE_TABLEN].Value;

                strTypeName = strStructName = oTableNode.ChildNodes[0].Attributes[XML_ATTRIBUTE_TABNAME].Value;

                if (oTableNode.ChildNodes.Count == 1) 
                {
                    strDtyp = oTableNode.ChildNodes[0].Attributes[XML_ATTRIBUTE_DTYP].Value;
                    nInitialSize = Convert.ToInt32(oTableNode.ChildNodes[0].Attributes[XML_ATTRIBUTE_DBLENGTH].Value);
                    strRollName = oTableNode.ChildNodes[0].Attributes[XML_ATTRIBUTE_ROLLNAME].Value;
                    strFieldName = oTableNode.ChildNodes[0].Attributes[XML_ATTRIBUTE_FIELDNAME].Value;
                    bSkipStruct = String.IsNullOrEmpty(strRollName) && String.IsNullOrEmpty(strFieldName);
                    if (bSkipStruct)
                        strTypeName = ConvDTYP2Type(strDtyp);
                }
                for (int nCnt = 0; nCnt < m_arrProcessedTableName.Count; ++nCnt)
                {
                    bSkipTable = strTabName.Equals(Convert.ToString(m_arrProcessedTableName[nCnt]));
                    if (bSkipTable) break; 
                }
                if (!bSkipTable)
                {
                    if (!strParamClass.Equals("T") && (strExid.Equals("u") || strExid.Equals("v")))
                    {
                        // Nothing to do
                    }
                    else
                    {
                        m_arrProcessedTableName.Add(strTabName);
                        if (String.Equals(strTabName, strTypeName)) strTabName += "Table";
                        GenRFCTableCollectionClass(strNamespace, strTabName, strTypeName, nInitialSize, bOutSource, strOutPath);
                    }
                }
                if (!bSkipStruct)
                {
                    for (int nCnt = 0; nCnt < m_arrProcessedStructName.Count; ++nCnt)
                    {
                        bSkipStruct = strStructName.Equals(Convert.ToString(m_arrProcessedStructName[nCnt]));
                        if (bSkipStruct) break;
                    }
                }
                if (bSkipStruct) continue;
                
                m_arrProcessedStructName.Add(strStructName);

                oFldHNodeList = oTableNode.ChildNodes;
                GenRFCStructClass(oFldHNodeList, strNamespace, strStructName, strLength, bOutSource, strOutPath);
            } // For
        }

        private String ConvDTYP2Type(String strDTYP)
        {
            String strCSType = null;

            switch (strDTYP)
            {
                case "CHAR":
                case "LANG":
                case "NUMC":
                case "STRG":
                case "SSTR":
                    strCSType = "String";
                    break;
                case "INT1":
                    strCSType = "Byte";
                    break;
                case "INT2":
                    strCSType = "short";
                    break;
                case "INT4":
                    strCSType = "int";
                    break;
                case "LRAW":
                case "RAW":
                case "RSTR":
                    strCSType = "Byte[]";
                    break;
                default:
                    break;
            }

            return strCSType;
        }

        public bool gen_RFCProxyDLL(string strNamespace, string strClassName, string strFuncName, bool bClientProxy,
                                    string strInfoXML, bool bOutSource, string strOutPath)
        {
            String[] strArrClass = null; 
            int nIdx = 0;            
            int nClassCnt = 0;       
            int nResCode = 0;        

            String strFileName = null;         
            String strProxyDllFullName = null;

            bool bResult = false;
            try
            {
                gen_RFCProxyClass(strNamespace, strClassName, strFuncName, bClientProxy, strInfoXML, bOutSource, strOutPath);
                strArrClass = new String[m_arrSAPStructClass.Count + m_arrSAPTableClass.Count + 1];
                for (nIdx = 0; nIdx < m_arrSAPStructClass.Count; ++nIdx)
                {
                    strArrClass[nClassCnt] = (string)m_arrSAPStructClass[nIdx];
                    ++nClassCnt;
                }
                for (nIdx = 0; nIdx < m_arrSAPTableClass.Count; ++nIdx)
                {
                    strArrClass[nClassCnt] = (string)m_arrSAPTableClass[nIdx];
                    ++nClassCnt;
                }
                strArrClass[nClassCnt] = m_strFuncProxyClass;

                strFileName = strFuncName.Replace("/", "_"); 
                strProxyDllFullName = strOutPath + strFileName + ".DLL";

                nResCode = BuildRFCProxyDLL(strArrClass, strProxyDllFullName, bClientProxy);

                bResult = true;
            }
            catch (Exception exp)
            {
                throw exp;
            }

            return bResult;
        }
    } // class
} // namaspace