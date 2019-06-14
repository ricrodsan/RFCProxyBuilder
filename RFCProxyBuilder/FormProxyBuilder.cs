using System;
//using System.Collections.Generic;
//using System.ComponentModel;
using System.Data;
//using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using SAP.Connector;
using SAP.Connector.Rfc;

namespace RFCProxyBuilder
{
    public partial class FormProxyBuilder : Form
    {
        #region Member VARIABLE
        private const String SAP_CONFIG_FILE = "SAPCONFIG.XML";

        private string m_strConfigXML = null;
        #endregion

        public FormProxyBuilder()
        {
            InitializeComponent();
        }

        #region MainForm Event-Handler
        private void FormProxyBuilder_Load(object sender, System.EventArgs e)
        {
            this.InitializeFormControl();
        }

        private void tabControl1_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            TabControl tab = null;

            tab = (TabControl)sender;

            switch (tab.SelectedIndex)
            {

                case 0:
                    btnPrevious.Visible = false;
                    btnNext.Enabled = true;
                    btnCancelEnd.Text = "Cancel";
                    break;

                case 1:
                    btnPrevious.Visible = true;
                    btnNext.Enabled = true;
                    btnCancelEnd.Text = "Cancel";
                    break;

                case 2:
                    btnPrevious.Visible = true;
                    btnNext.Enabled = false;
                    btnCancelEnd.Text = "Terminate";
                    break;

                default:
                    break;
            } // switch
        }

        private void btnPrevious_Click(object sender, System.EventArgs e)
        {
            tabControl1.SelectedIndex--;
        }

        private void btnNext_Click(object sender, System.EventArgs e)
        {
            tabControl1.SelectedIndex++;
        }

        private void btnCancelEnd_Click(object sender, System.EventArgs e)
        {
            this.Close();
        }

        private void FormProxyBuilder_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            DialogResult nRes = 0;
            if (tabControl1.SelectedIndex < (tabControl1.TabCount - 1))
            {
                nRes = MessageBox.Show(this, "Terminate the RFCProxyBuilder...", this.Text, MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (nRes == DialogResult.OK)
                    e.Cancel = false;
                else
                    e.Cancel = true;
            }
        }

        private void InitializeFormControl()
        {
            string strFilePath = null;
            int nPos = 0;
            string[] arrNames = null;

            SAPServerConfig oSCfg = new SAPServerConfig();
            SAPServerConfig.SAPSeverInfo oInfo = new SAPServerConfig.SAPSeverInfo(oSCfg);

            RFCFilter oFilter = new RFCFilter();

            strFilePath = Application.ExecutablePath;
            nPos = strFilePath.LastIndexOf("\\");
            strFilePath = strFilePath.Substring(0, ++nPos);
            strFilePath += SAP_CONFIG_FILE;
            ReadConfigurationFile(strFilePath);

            lbSAPServer.Sorted = true;
            lbSAPServer.SelectionMode = SelectionMode.One;

            arrNames = oSCfg.GetServerNames(m_strConfigXML);
            lbSAPServer.Items.Clear();
            if (arrNames != null)
            {
                for (int i = 0; i < arrNames.Length; ++i)
                {
                    lbSAPServer.Items.Add(arrNames[i]);
                }
            }

            btnPrevious.Visible = false;
            pgServerConfig.SelectedObject = oInfo;
            pgRFCFilter.SelectedObject = oFilter;

            txtNamespace.Text = "ProxyBuilder";

            radioClient.Checked = true;
            radioServer.Checked = false;

            pgServerConfig.HelpVisible = true;
            pgRFCFilter.HelpVisible = true;
        }

        private void ReadConfigurationFile(string strConfigFile)
        {
            SAPServerConfig oCfg = null;
            try
            {
                oCfg = new SAPServerConfig();
                m_strConfigXML = oCfg.LoadConfigFile(strConfigFile);
            }
            catch (Exception exp)
            {
                throw exp;
            }
        }

        #endregion

        #region tabPage1 Event-Handler

        private void btnAddServer_Click(object sender, System.EventArgs e)
        {
            string strMsg = "";

            SAPServerConfig.SAPSeverInfo oInfo = (SAPServerConfig.SAPSeverInfo)pgServerConfig.SelectedObject;
            SAPServerConfig oSCfg = oInfo.owner;

            if (oInfo.NAME.Trim().Length == 0)
                return;

            if (!oSCfg.AddConfiguration(ref m_strConfigXML))
            {
                strMsg = "Specified SAP server name is already in use.\nPlease select an alternate name.";
                MessageBox.Show(this, strMsg, this.Name, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            lbSAPServer.Items.Add(oInfo.NAME);
        }

        private void btnRemoveServer_Click(object sender, System.EventArgs e)
        {
            string strMsg = "";

            SAPServerConfig.SAPSeverInfo oInfo = (SAPServerConfig.SAPSeverInfo)pgServerConfig.SelectedObject;
            SAPServerConfig oSCfg = oInfo.owner;

            if ((oInfo.NAME.Trim().Length == 0)
                || !String.Equals((string)lbSAPServer.SelectedItem, oInfo.NAME)
                || !oSCfg.RemoveConfiguration(ref m_strConfigXML))
            {
                strMsg = "Please select a server name.";
                MessageBox.Show(this, strMsg, this.Name, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            lbSAPServer.Items.Remove(lbSAPServer.SelectedItem);
        }

        private void btnTestConn_Click(object sender, System.EventArgs e)
        {
            SAPServerConfig.SAPSeverInfo oInfo = (SAPServerConfig.SAPSeverInfo)pgServerConfig.SelectedObject;
            SAPServerConfig oSCfg = oInfo.owner;

            CallSAPProxy oProxy = new CallSAPProxy();

            Cursor.Current = Cursors.WaitCursor;

            try
            {
                if (!ValidateSAPServerInfo())
                {
                    MessageBox.Show(this, "Please confirm the connection state of SAP server.", this.Name, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    Cursor.Current = Cursors.Arrow;
                    return;
                }

                oProxy.SetConnectionInfo("3", oInfo.ASHOST, Convert.ToInt16(oInfo.SYSNR), Convert.ToInt16(oInfo.CLIENT),
                                        oInfo.LANG, txtUserID.Text, txtPassword.Text);
                if (oProxy.ConnectSAPServer())
                    MessageBox.Show(this, "The connection attempt succeeded", this.Name, MessageBoxButtons.OK, MessageBoxIcon.Information);
                else
                    MessageBox.Show(this, "The connection attempt Failed!", this.Name, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception exp)
            {
                MessageBox.Show(this, exp.Message, this.Name, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            finally
            {
                if (oProxy != null) 
                    oProxy.DisconnectSAPServer();
            }
            Cursor.Current = Cursors.Arrow;
        }

        private void btnSaveConfig_Click(object sender, System.EventArgs e)
        {
            string strFilePath = null;
            int nPos = 0;

            SAPServerConfig.SAPSeverInfo oInfo = (SAPServerConfig.SAPSeverInfo)pgServerConfig.SelectedObject;
            SAPServerConfig oSCfg = oInfo.owner;

            strFilePath = Application.ExecutablePath;
            nPos = strFilePath.LastIndexOf("\\");
            strFilePath = strFilePath.Substring(0, ++nPos);
            strFilePath += SAP_CONFIG_FILE;

            oSCfg.SaveConfigFile(strFilePath, m_strConfigXML);
        }

        private void lbSAPServer_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            string strSAPName = null;
            XmlNode oNode = null;

            strSAPName = (String)lbSAPServer.SelectedItem;

            if (strSAPName == null || m_strConfigXML == null)
                return;

            SAPServerConfig.SAPSeverInfo oInfo = (SAPServerConfig.SAPSeverInfo)pgServerConfig.SelectedObject;
            SAPServerConfig oSCfg = oInfo.owner;

            oNode = oSCfg.SearchConfiguration(m_strConfigXML, strSAPName)[0];

            oInfo.NAME = oNode.Attributes["NAME"].Value;
            oInfo.LANG = oNode.Attributes["LANG"].Value;
            oInfo.ASHOST = oNode.Attributes["ASHOST"].Value;
            oInfo.CLIENT = oNode.Attributes["CLIENT"].Value;
            oInfo.SYSNR = oNode.Attributes["SYSNR"].Value;


        //    oInfo.USER_ID = oNode.Attributes["USER_ID"].Value;
         //   oInfo.USER_PASSWORD = oNode.Attributes["USER_PASSWORD"].Value;

            pgServerConfig.Refresh();
        }

        #region private procedure
        private bool ValidateSAPServerInfo()
        {
            bool bValidateRes = true;
            SAPServerConfig.SAPSeverInfo oInfo = (SAPServerConfig.SAPSeverInfo)pgServerConfig.SelectedObject;
            
            if (String.IsNullOrEmpty(oInfo.ASHOST) 
                || String.IsNullOrEmpty(oInfo.CLIENT)
                || String.IsNullOrEmpty(oInfo.LANG)
                || String.IsNullOrEmpty(txtUserID.Text) 
                || String.IsNullOrEmpty(txtPassword.Text))
            {
                bValidateRes = false;
            }

            return bValidateRes;
        }
        #endregion private procedure

        #endregion

        #region tabPage2 Event-Handler
        private void btnGetRFCFunctions_Click(object sender, System.EventArgs e)
        {
            string strFuncNameFilter = "*";
            string strGroupNameFilter = "";
            string strLanguageFilter = "EN";

            string strErrMsg = "";
            DataTable oDT = null;   // 데이터 테이블 개체
            CallSAPProxy oProxy = null;

            SAPServerConfig.SAPSeverInfo oInfo = (SAPServerConfig.SAPSeverInfo)pgServerConfig.SelectedObject;
            SAPServerConfig oSCfg = oInfo.owner;

            RFCFilter oFilter = (RFCFilter)pgRFCFilter.SelectedObject;

            Cursor.Current = Cursors.WaitCursor;

            try
            {
                // 목록 초기화
                lbRFCFunctions.Items.Clear();
                txtSelectedFuncName.Text = "";

                if (!ValidateSAPServerInfo())
                {
                    MessageBox.Show(this, "Please confirm the connection state of SAP server", this.Name, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    Cursor.Current = Cursors.Arrow;
                    return;
                }

                strFuncNameFilter = String.IsNullOrEmpty(oFilter.NameFilter) ? "*" : oFilter.NameFilter;
                strGroupNameFilter = oFilter.GroupFilter;
                strLanguageFilter = String.IsNullOrEmpty(oFilter.Language) ? "EN" : oFilter.Language;

                oProxy = new CallSAPProxy(); // Proxy 개체를 생성한다.
                oProxy.SetConnectionInfo("3", oInfo.ASHOST, Convert.ToInt16(oInfo.SYSNR), Convert.ToInt16(oInfo.CLIENT),
                                        oInfo.LANG, txtUserID.Text, txtPassword.Text);

                if (!oProxy.ConnectSAPServer())
                {
                    MessageBox.Show(this, "Cannot connect the SAP server", this.Name, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    Cursor.Current = Cursors.Arrow;
                    return;
                }
               
                oDT = oProxy.get_RFCFunctionNames(strFuncNameFilter, strGroupNameFilter, strLanguageFilter);

                // Add to Function Names
                for (int nCnt = 0; nCnt < oDT.Rows.Count; ++nCnt)
                {
                    lbRFCFunctions.Items.Add(oDT.Rows[nCnt].ItemArray[0].ToString());
                }
            }
            catch (Exception exp)
            {
                strErrMsg = exp.Message;

                if (strErrMsg.Length == 0)
                    strErrMsg = "Cannot find the function specified.\nPlease reset the filters.";

                MessageBox.Show(this, strErrMsg, this.Name, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            finally
            {
                if (oProxy != null)
                    oProxy.DisconnectSAPServer();
            }

            Cursor.Current = Cursors.Arrow;

            return;
        }

        private void lbRFCFunctions_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            txtSelectedFuncName.Text = (string)lbRFCFunctions.SelectedItem;
        }

        #endregion

        #region tabPage3 Event_Handler
        private void btnBrowsePath_Click(object sender, System.EventArgs e)
        {
            DialogResult nResult = 0;
            folderBrowserDialog1.ShowNewFolderButton = true;
            folderBrowserDialog1.Description = "Please select the output directory.";

            txtOutputPath.Text = "";

            nResult = folderBrowserDialog1.ShowDialog();
            if (nResult == DialogResult.OK)
            {
                if (!folderBrowserDialog1.SelectedPath.EndsWith("\\"))
                    txtOutputPath.Text = folderBrowserDialog1.SelectedPath + "\\";
                else
                    txtOutputPath.Text = folderBrowserDialog1.SelectedPath;
            }
        }

        private void btnGenDLL_Click(object sender, System.EventArgs e)
        {
            string strFunctionName = null; // RFC 함수 이름
            string strLanguage = null; // 언어
            string strNamespace = null; // 네임스페이스
            string strClassName = null; // 클래스 이름
            string strOutPath = null; // 출력 디렉터리
            bool bClientProxy = true; // true - Client Proxy, false - Server Proxy
            bool bOutputCS = false; // true - 소스 출력, false - 소스 출력 하지 않음

            Cursor.Current = Cursors.WaitCursor;
            try
            {
                if (!ValidateSAPServerInfo())
                {
                    MessageBox.Show(this, "Please confirm the connection state of SAP server.", this.Name, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    Cursor.Current = Cursors.Arrow;
                    return;
                }

                if (!IsSelectFunctionName())
                {
                    MessageBox.Show(this, "Please select a BAPI/RFC Function.", this.Name, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    Cursor.Current = Cursors.Arrow;
                    return;
                }

                if (String.IsNullOrEmpty(txtOutputPath.Text))
                {
                    MessageBox.Show(this, "Please confirm the output directory.", this.Name, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    Cursor.Current = Cursors.Arrow;
                    return;
                }

                strFunctionName = txtSelectedFuncName.Text;
                strLanguage = ((RFCFilter)pgRFCFilter.SelectedObject).Language;
                strNamespace = txtNamespace.Text;
                strClassName = txtClass.Text;
                strOutPath = txtOutputPath.Text;
                bClientProxy = radioClient.Checked;
                bOutputCS = chkOutputCS.Checked;

                txtProxyResult.Text = "";
                txtProxyResult.Refresh();

                if (StartBuild(strFunctionName, strLanguage, strNamespace, strClassName, strOutPath, bClientProxy, bOutputCS))
                {
                    string strProxyDllName = strOutPath + strFunctionName + ".DLL";
                    txtProxyResult.Text = String.Format("Build the Proxy Module.\r\n - {0}", strProxyDllName);
                }
                else
                {
                    txtProxyResult.Text = String.Format("Cannot load the schema of RFC/BAPI function.\r\nCannot build the proxy module.\r\n");
                }
            }
            catch (Exception exp)
            {
                txtProxyResult.Text = String.IsNullOrEmpty(exp.Message) ? 
                                      String.Format("Unknown expection occurs.\r\nCannot build the proxy module of {0}.\n", strFunctionName) :
                                      exp.Message + String.Format("\r\nCannot build the proxy module of {0}.\n", strFunctionName);
                txtProxyResult.Text += exp.StackTrace;
            }
            Cursor.Current = Cursors.Arrow;
        }

        private bool StartBuild(string strFunctionName, string strLanguage, string strNamespace, string strClassName,
                                string strOutPath, bool bClientProxy, bool bOutputCS)
        {
            CallSAPProxy oProxy = null; // Proxy 연결 및 메타 정보 클래스 개체
            RFCProxyGen oProxyGen = null; // Proxy 생성 클래스 개체
            SAPServerConfig.SAPSeverInfo oInfo = (SAPServerConfig.SAPSeverInfo)pgServerConfig.SelectedObject;
            string strXML = null;
            try
            {
                oProxyGen = new RFCProxyGen();
                oProxy = new CallSAPProxy(); // Proxy 개체를 생성한다.

                oProxy.SetConnectionInfo("3", oInfo.ASHOST, Convert.ToInt16(oInfo.SYSNR), Convert.ToInt16(oInfo.CLIENT),
                                        oInfo.LANG, txtUserID.Text, txtPassword.Text);
                
                if (!oProxy.ConnectSAPServer())
                {
                    MessageBox.Show(this, "Cannot connect the SAP server.", this.Name, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    Cursor.Current = Cursors.Arrow;
                    return false;
                }

                strXML = oProxy.get_RFCProxyInfo(strFunctionName, strLanguage);
                // strXML null 값이면, 인자 정보가 없다.
                return oProxyGen.gen_RFCProxyDLL(strNamespace, strClassName, strFunctionName, bClientProxy, strXML, bOutputCS, strOutPath);

            }
            catch (Exception exp)
            {
                throw exp;
            }
            finally
            {
                if (oProxy != null)
                    oProxy.DisconnectSAPServer();
            }
        }

        private bool IsSelectFunctionName()
        {
            return (lbRFCFunctions.SelectedIndex >= 0);
        }

        private void txtNamespace_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            string strErrorMsg = null;
            if (!ValidNamespaceClass(txtNamespace.Text, 1, out strErrorMsg))
            {
                // Cancel the event and select the text to be corrected by the user.
                e.Cancel = true;
                txtNamespace.Select(0, txtNamespace.Text.Length);

                // Set the ErrorProvider error with the text to display. 
                this.errorProvider1.SetError(txtNamespace, strErrorMsg);
            }
        }

        private void txtNamespace_Validated(object sender, EventArgs e)
        {
            // 상태가 양호하면 에러 로그를 지운다.
            errorProvider1.SetError(txtNamespace, "");
        }

        private void txtClass_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            string strErrorMsg = null;
            if (!ValidNamespaceClass(txtClass.Text, 0, out strErrorMsg))
            {
                // Cancel the event and select the text to be corrected by the user.
                e.Cancel = true;
                txtNamespace.Select(0, txtClass.Text.Length);

                // Set the ErrorProvider error with the text to display. 
                this.errorProvider1.SetError(txtClass, strErrorMsg);
            }
        }

        private void txtClass_Validated(object sender, EventArgs e)
        {
            // 상태가 양호하면 에러 로그를 지운다.
            errorProvider1.SetError(txtClass, "");
        }

        private bool ValidNamespaceClass(string strInputText, int nMode, out string strErrorMsg)
        {
            String strSender = null;

            strErrorMsg = null;
            strSender = (nMode == 1) ? "Namespace" : "Class";

            if (strInputText.Length == 0)
            {
                strErrorMsg = String.Format("{0} :Please input the value.", strSender);
                return false;
            }

            if ((strInputText[0] == 0x5F)
                || (strInputText[0] >= 0x41 && strInputText[0] <= 0x5A)
                || (strInputText[0] >= 0x61 && strInputText[0] <= 0x7A)
               )
            {
                // Do noting
            }
            else
            {
                strErrorMsg = String.Format("{0} :This value cannot start with Non-Alphabet symbol.", strSender);
                return false;
            }

            for (int i = 1; i < strInputText.Length; ++i)
            {
                if ((strInputText[i] == 0x5F)
                    || (strInputText[i] >= 0x41 && strInputText[i] <= 0x5A)
                    || (strInputText[i] >= 0x61 && strInputText[i] <= 0x7A)
                    || (strInputText[i] >= 0x30 && strInputText[i] <= 0x39)
                   )
                    continue;
                else
                {
                    strErrorMsg = String.Format("{0} :This value cannot have any Non-Alphabet symbol.", strSender);
                    return false;
                }
            }

            return true;
        }

        #endregion

        private void button1_Click(object sender, EventArgs e)
        {
            LicenseForm lcform = new LicenseForm();

            lcform.Show();
        }

    }
}