namespace RFCProxyBuilder
{
    partial class FormProxyBuilder
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다.
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마십시오.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.btnSaveConfig = new System.Windows.Forms.Button();
            this.lblSAPNameList = new System.Windows.Forms.Label();
            this.lbSAPServer = new System.Windows.Forms.ListBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnTestConn = new System.Windows.Forms.Button();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.txtUserID = new System.Windows.Forms.TextBox();
            this.lblPassword = new System.Windows.Forms.Label();
            this.lblUserID = new System.Windows.Forms.Label();
            this.btnRemoveServer = new System.Windows.Forms.Button();
            this.btnAddServer = new System.Windows.Forms.Button();
            this.pgServerConfig = new System.Windows.Forms.PropertyGrid();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.lblFiler = new System.Windows.Forms.Label();
            this.pgRFCFilter = new System.Windows.Forms.PropertyGrid();
            this.txtSelectedFuncName = new System.Windows.Forms.TextBox();
            this.lbRFCFunctions = new System.Windows.Forms.ListBox();
            this.btnGetRFCFunctions = new System.Windows.Forms.Button();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.txtClass = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtProxyResult = new System.Windows.Forms.TextBox();
            this.chkOutputCS = new System.Windows.Forms.CheckBox();
            this.btnBrowsePath = new System.Windows.Forms.Button();
            this.txtOutputPath = new System.Windows.Forms.Label();
            this.lblOuputPath = new System.Windows.Forms.Label();
            this.txtNamespace = new System.Windows.Forms.TextBox();
            this.lblNamespace = new System.Windows.Forms.Label();
            this.radioClient = new System.Windows.Forms.RadioButton();
            this.radioServer = new System.Windows.Forms.RadioButton();
            this.lblProxyType = new System.Windows.Forms.Label();
            this.btnGenDLL = new System.Windows.Forms.Button();
            this.btnNext = new System.Windows.Forms.Button();
            this.btnPrevious = new System.Windows.Forms.Button();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.btnCancelEnd = new System.Windows.Forms.Button();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.button1 = new System.Windows.Forms.Button();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Appearance = System.Windows.Forms.TabAppearance.FlatButtons;
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.tabControl1.HotTrack = true;
            this.tabControl1.Location = new System.Drawing.Point(8, 8);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.ShowToolTips = true;
            this.tabControl1.Size = new System.Drawing.Size(520, 344);
            this.tabControl1.SizeMode = System.Windows.Forms.TabSizeMode.FillToRight;
            this.tabControl1.TabIndex = 1;
            this.tabControl1.SelectedIndexChanged += new System.EventHandler(this.tabControl1_SelectedIndexChanged);
            // 
            // tabPage1
            // 
            this.tabPage1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tabPage1.Controls.Add(this.btnSaveConfig);
            this.tabPage1.Controls.Add(this.lblSAPNameList);
            this.tabPage1.Controls.Add(this.lbSAPServer);
            this.tabPage1.Controls.Add(this.groupBox1);
            this.tabPage1.Controls.Add(this.btnRemoveServer);
            this.tabPage1.Controls.Add(this.btnAddServer);
            this.tabPage1.Controls.Add(this.pgServerConfig);
            this.tabPage1.Location = new System.Drawing.Point(4, 27);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Size = new System.Drawing.Size(512, 313);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "1. SAP Server";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // btnSaveConfig
            // 
            this.btnSaveConfig.BackColor = System.Drawing.SystemColors.Control;
            this.btnSaveConfig.Location = new System.Drawing.Point(408, 208);
            this.btnSaveConfig.Name = "btnSaveConfig";
            this.btnSaveConfig.Size = new System.Drawing.Size(96, 32);
            this.btnSaveConfig.TabIndex = 17;
            this.btnSaveConfig.Text = "Save SAP Info.";
            this.btnSaveConfig.UseVisualStyleBackColor = false;
            this.btnSaveConfig.Click += new System.EventHandler(this.btnSaveConfig_Click);
            // 
            // lblSAPNameList
            // 
            this.lblSAPNameList.BackColor = System.Drawing.SystemColors.Control;
            this.lblSAPNameList.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblSAPNameList.Location = new System.Drawing.Point(8, 8);
            this.lblSAPNameList.Name = "lblSAPNameList";
            this.lblSAPNameList.Size = new System.Drawing.Size(176, 24);
            this.lblSAPNameList.TabIndex = 16;
            this.lblSAPNameList.Text = "SAP Server List";
            this.lblSAPNameList.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbSAPServer
            // 
            this.lbSAPServer.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lbSAPServer.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lbSAPServer.ForeColor = System.Drawing.Color.Blue;
            this.lbSAPServer.HorizontalScrollbar = true;
            this.lbSAPServer.ItemHeight = 15;
            this.lbSAPServer.Location = new System.Drawing.Point(8, 40);
            this.lbSAPServer.Name = "lbSAPServer";
            this.lbSAPServer.Size = new System.Drawing.Size(176, 255);
            this.lbSAPServer.Sorted = true;
            this.lbSAPServer.TabIndex = 5;
            this.lbSAPServer.SelectedIndexChanged += new System.EventHandler(this.lbSAPServer_SelectedIndexChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnTestConn);
            this.groupBox1.Controls.Add(this.txtPassword);
            this.groupBox1.Controls.Add(this.txtUserID);
            this.groupBox1.Controls.Add(this.lblPassword);
            this.groupBox1.Controls.Add(this.lblUserID);
            this.groupBox1.Location = new System.Drawing.Point(192, 240);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(312, 64);
            this.groupBox1.TabIndex = 9;
            this.groupBox1.TabStop = false;
            // 
            // btnTestConn
            // 
            this.btnTestConn.BackColor = System.Drawing.SystemColors.Control;
            this.btnTestConn.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnTestConn.Location = new System.Drawing.Point(224, 16);
            this.btnTestConn.Name = "btnTestConn";
            this.btnTestConn.Size = new System.Drawing.Size(80, 40);
            this.btnTestConn.TabIndex = 14;
            this.btnTestConn.Text = "Connection Test";
            this.btnTestConn.UseVisualStyleBackColor = false;
            this.btnTestConn.Click += new System.EventHandler(this.btnTestConn_Click);
            // 
            // txtPassword
            // 
            this.txtPassword.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtPassword.ForeColor = System.Drawing.Color.Blue;
            this.txtPassword.Location = new System.Drawing.Point(80, 40);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '*';
            this.txtPassword.Size = new System.Drawing.Size(136, 14);
            this.txtPassword.TabIndex = 13;
            // 
            // txtUserID
            // 
            this.txtUserID.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtUserID.ForeColor = System.Drawing.Color.Blue;
            this.txtUserID.Location = new System.Drawing.Point(80, 16);
            this.txtUserID.Name = "txtUserID";
            this.txtUserID.Size = new System.Drawing.Size(136, 14);
            this.txtUserID.TabIndex = 11;
            // 
            // lblPassword
            // 
            this.lblPassword.Location = new System.Drawing.Point(8, 40);
            this.lblPassword.Name = "lblPassword";
            this.lblPassword.Size = new System.Drawing.Size(64, 16);
            this.lblPassword.TabIndex = 12;
            this.lblPassword.Text = "Password";
            // 
            // lblUserID
            // 
            this.lblUserID.Location = new System.Drawing.Point(8, 16);
            this.lblUserID.Name = "lblUserID";
            this.lblUserID.Size = new System.Drawing.Size(64, 16);
            this.lblUserID.TabIndex = 10;
            this.lblUserID.Text = "User ID";
            // 
            // btnRemoveServer
            // 
            this.btnRemoveServer.BackColor = System.Drawing.SystemColors.Control;
            this.btnRemoveServer.Location = new System.Drawing.Point(300, 208);
            this.btnRemoveServer.Name = "btnRemoveServer";
            this.btnRemoveServer.Size = new System.Drawing.Size(96, 32);
            this.btnRemoveServer.TabIndex = 8;
            this.btnRemoveServer.Text = "Remove Server";
            this.btnRemoveServer.UseVisualStyleBackColor = false;
            this.btnRemoveServer.Click += new System.EventHandler(this.btnRemoveServer_Click);
            // 
            // btnAddServer
            // 
            this.btnAddServer.BackColor = System.Drawing.SystemColors.Control;
            this.btnAddServer.Location = new System.Drawing.Point(192, 208);
            this.btnAddServer.Name = "btnAddServer";
            this.btnAddServer.Size = new System.Drawing.Size(96, 32);
            this.btnAddServer.TabIndex = 7;
            this.btnAddServer.Text = "Add Server";
            this.btnAddServer.UseVisualStyleBackColor = false;
            this.btnAddServer.Click += new System.EventHandler(this.btnAddServer_Click);
            // 
            // pgServerConfig
            // 
            this.pgServerConfig.HelpVisible = false;
            this.pgServerConfig.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.pgServerConfig.LineColor = System.Drawing.SystemColors.ScrollBar;
            this.pgServerConfig.Location = new System.Drawing.Point(192, 8);
            this.pgServerConfig.Name = "pgServerConfig";
            this.pgServerConfig.Size = new System.Drawing.Size(312, 192);
            this.pgServerConfig.TabIndex = 6;
            this.pgServerConfig.ToolbarVisible = false;
            // 
            // tabPage2
            // 
            this.tabPage2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tabPage2.Controls.Add(this.lblFiler);
            this.tabPage2.Controls.Add(this.pgRFCFilter);
            this.tabPage2.Controls.Add(this.txtSelectedFuncName);
            this.tabPage2.Controls.Add(this.lbRFCFunctions);
            this.tabPage2.Controls.Add(this.btnGetRFCFunctions);
            this.tabPage2.Location = new System.Drawing.Point(4, 27);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Size = new System.Drawing.Size(515, 313);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "2. BAPI / RFC";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // lblFiler
            // 
            this.lblFiler.BackColor = System.Drawing.SystemColors.Control;
            this.lblFiler.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblFiler.Location = new System.Drawing.Point(8, 8);
            this.lblFiler.Name = "lblFiler";
            this.lblFiler.Size = new System.Drawing.Size(192, 32);
            this.lblFiler.TabIndex = 20;
            this.lblFiler.Text = "RFC Filters";
            this.lblFiler.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pgRFCFilter
            // 
            this.pgRFCFilter.HelpVisible = false;
            this.pgRFCFilter.LineColor = System.Drawing.SystemColors.ScrollBar;
            this.pgRFCFilter.Location = new System.Drawing.Point(8, 48);
            this.pgRFCFilter.Name = "pgRFCFilter";
            this.pgRFCFilter.Size = new System.Drawing.Size(192, 256);
            this.pgRFCFilter.TabIndex = 16;
            this.pgRFCFilter.ToolbarVisible = false;
            // 
            // txtSelectedFuncName
            // 
            this.txtSelectedFuncName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSelectedFuncName.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.txtSelectedFuncName.ForeColor = System.Drawing.Color.Blue;
            this.txtSelectedFuncName.Location = new System.Drawing.Point(208, 283);
            this.txtSelectedFuncName.Name = "txtSelectedFuncName";
            this.txtSelectedFuncName.ReadOnly = true;
            this.txtSelectedFuncName.Size = new System.Drawing.Size(296, 21);
            this.txtSelectedFuncName.TabIndex = 19;
            this.txtSelectedFuncName.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // lbRFCFunctions
            // 
            this.lbRFCFunctions.ItemHeight = 15;
            this.lbRFCFunctions.Location = new System.Drawing.Point(208, 48);
            this.lbRFCFunctions.Name = "lbRFCFunctions";
            this.lbRFCFunctions.Size = new System.Drawing.Size(296, 229);
            this.lbRFCFunctions.Sorted = true;
            this.lbRFCFunctions.TabIndex = 18;
            this.lbRFCFunctions.SelectedIndexChanged += new System.EventHandler(this.lbRFCFunctions_SelectedIndexChanged);
            // 
            // btnGetRFCFunctions
            // 
            this.btnGetRFCFunctions.BackColor = System.Drawing.SystemColors.Control;
            this.btnGetRFCFunctions.Location = new System.Drawing.Point(208, 8);
            this.btnGetRFCFunctions.Name = "btnGetRFCFunctions";
            this.btnGetRFCFunctions.Size = new System.Drawing.Size(296, 32);
            this.btnGetRFCFunctions.TabIndex = 17;
            this.btnGetRFCFunctions.Text = "BAPI / RFC Functions";
            this.btnGetRFCFunctions.UseVisualStyleBackColor = false;
            this.btnGetRFCFunctions.Click += new System.EventHandler(this.btnGetRFCFunctions_Click);
            // 
            // tabPage3
            // 
            this.tabPage3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tabPage3.Controls.Add(this.txtClass);
            this.tabPage3.Controls.Add(this.label1);
            this.tabPage3.Controls.Add(this.txtProxyResult);
            this.tabPage3.Controls.Add(this.chkOutputCS);
            this.tabPage3.Controls.Add(this.btnBrowsePath);
            this.tabPage3.Controls.Add(this.txtOutputPath);
            this.tabPage3.Controls.Add(this.lblOuputPath);
            this.tabPage3.Controls.Add(this.txtNamespace);
            this.tabPage3.Controls.Add(this.lblNamespace);
            this.tabPage3.Controls.Add(this.radioClient);
            this.tabPage3.Controls.Add(this.radioServer);
            this.tabPage3.Controls.Add(this.lblProxyType);
            this.tabPage3.Controls.Add(this.btnGenDLL);
            this.tabPage3.Location = new System.Drawing.Point(4, 27);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(515, 313);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "3. Proxy Module";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // txtClass
            // 
            this.txtClass.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtClass.ForeColor = System.Drawing.Color.Blue;
            this.txtClass.Location = new System.Drawing.Point(118, 73);
            this.txtClass.MaxLength = 128;
            this.txtClass.Name = "txtClass";
            this.txtClass.Size = new System.Drawing.Size(136, 21);
            this.txtClass.TabIndex = 26;
            this.txtClass.Text = "SAPProxy";
            this.txtClass.Validating += new System.ComponentModel.CancelEventHandler(this.txtClass_Validating);
            this.txtClass.Validated += new System.EventHandler(this.txtClass_Validated);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(14, 72);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(96, 24);
            this.label1.TabIndex = 25;
            this.label1.Text = "Class Name";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtProxyResult
            // 
            this.txtProxyResult.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtProxyResult.ForeColor = System.Drawing.Color.Blue;
            this.txtProxyResult.Location = new System.Drawing.Point(16, 208);
            this.txtProxyResult.MaxLength = 128;
            this.txtProxyResult.Multiline = true;
            this.txtProxyResult.Name = "txtProxyResult";
            this.txtProxyResult.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtProxyResult.Size = new System.Drawing.Size(473, 96);
            this.txtProxyResult.TabIndex = 33;
            // 
            // chkOutputCS
            // 
            this.chkOutputCS.Location = new System.Drawing.Point(313, 48);
            this.chkOutputCS.Name = "chkOutputCS";
            this.chkOutputCS.Size = new System.Drawing.Size(176, 29);
            this.chkOutputCS.TabIndex = 30;
            this.chkOutputCS.Text = "C# Class Source Files";
            // 
            // btnBrowsePath
            // 
            this.btnBrowsePath.BackColor = System.Drawing.SystemColors.Control;
            this.btnBrowsePath.Location = new System.Drawing.Point(118, 101);
            this.btnBrowsePath.Name = "btnBrowsePath";
            this.btnBrowsePath.Size = new System.Drawing.Size(136, 21);
            this.btnBrowsePath.TabIndex = 28;
            this.btnBrowsePath.Text = "...";
            this.btnBrowsePath.UseVisualStyleBackColor = false;
            this.btnBrowsePath.Click += new System.EventHandler(this.btnBrowsePath_Click);
            // 
            // txtOutputPath
            // 
            this.txtOutputPath.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtOutputPath.ForeColor = System.Drawing.Color.Blue;
            this.txtOutputPath.Location = new System.Drawing.Point(16, 125);
            this.txtOutputPath.Name = "txtOutputPath";
            this.txtOutputPath.Size = new System.Drawing.Size(473, 24);
            this.txtOutputPath.TabIndex = 29;
            this.txtOutputPath.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblOuputPath
            // 
            this.lblOuputPath.Location = new System.Drawing.Point(16, 101);
            this.lblOuputPath.Name = "lblOuputPath";
            this.lblOuputPath.Size = new System.Drawing.Size(96, 24);
            this.lblOuputPath.TabIndex = 27;
            this.lblOuputPath.Text = "Output Path";
            this.lblOuputPath.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtNamespace
            // 
            this.txtNamespace.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtNamespace.ForeColor = System.Drawing.Color.Blue;
            this.txtNamespace.Location = new System.Drawing.Point(118, 46);
            this.txtNamespace.MaxLength = 128;
            this.txtNamespace.Name = "txtNamespace";
            this.txtNamespace.Size = new System.Drawing.Size(136, 21);
            this.txtNamespace.TabIndex = 24;
            this.txtNamespace.Text = "ProxyBuilder";
            this.txtNamespace.Validating += new System.ComponentModel.CancelEventHandler(this.txtNamespace_Validating);
            this.txtNamespace.Validated += new System.EventHandler(this.txtNamespace_Validated);
            // 
            // lblNamespace
            // 
            this.lblNamespace.Location = new System.Drawing.Point(16, 48);
            this.lblNamespace.Name = "lblNamespace";
            this.lblNamespace.Size = new System.Drawing.Size(96, 24);
            this.lblNamespace.TabIndex = 23;
            this.lblNamespace.Text = "Namespace";
            this.lblNamespace.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // radioClient
            // 
            this.radioClient.Checked = true;
            this.radioClient.Location = new System.Drawing.Point(120, 16);
            this.radioClient.Name = "radioClient";
            this.radioClient.Size = new System.Drawing.Size(96, 24);
            this.radioClient.TabIndex = 21;
            this.radioClient.TabStop = true;
            this.radioClient.Text = "Client Proxy";
            // 
            // radioServer
            // 
            this.radioServer.Location = new System.Drawing.Point(232, 16);
            this.radioServer.Name = "radioServer";
            this.radioServer.Size = new System.Drawing.Size(96, 24);
            this.radioServer.TabIndex = 22;
            this.radioServer.Text = "Server Stub";
            // 
            // lblProxyType
            // 
            this.lblProxyType.Location = new System.Drawing.Point(16, 16);
            this.lblProxyType.Name = "lblProxyType";
            this.lblProxyType.Size = new System.Drawing.Size(96, 24);
            this.lblProxyType.TabIndex = 20;
            this.lblProxyType.Text = "Proxy Type";
            this.lblProxyType.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btnGenDLL
            // 
            this.btnGenDLL.BackColor = System.Drawing.SystemColors.Control;
            this.btnGenDLL.Location = new System.Drawing.Point(16, 154);
            this.btnGenDLL.Name = "btnGenDLL";
            this.btnGenDLL.Size = new System.Drawing.Size(473, 48);
            this.btnGenDLL.TabIndex = 31;
            this.btnGenDLL.Text = "Build Proxy Module";
            this.btnGenDLL.UseVisualStyleBackColor = false;
            this.btnGenDLL.Click += new System.EventHandler(this.btnGenDLL_Click);
            // 
            // btnNext
            // 
            this.btnNext.BackColor = System.Drawing.SystemColors.Control;
            this.btnNext.Location = new System.Drawing.Point(327, 352);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(96, 32);
            this.btnNext.TabIndex = 3;
            this.btnNext.Text = "Next >";
            this.btnNext.UseVisualStyleBackColor = false;
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // btnPrevious
            // 
            this.btnPrevious.BackColor = System.Drawing.SystemColors.Control;
            this.btnPrevious.Location = new System.Drawing.Point(226, 352);
            this.btnPrevious.Name = "btnPrevious";
            this.btnPrevious.Size = new System.Drawing.Size(96, 32);
            this.btnPrevious.TabIndex = 2;
            this.btnPrevious.Text = "< Previous";
            this.btnPrevious.UseVisualStyleBackColor = false;
            this.btnPrevious.Click += new System.EventHandler(this.btnPrevious_Click);
            // 
            // btnCancelEnd
            // 
            this.btnCancelEnd.BackColor = System.Drawing.SystemColors.Control;
            this.btnCancelEnd.Location = new System.Drawing.Point(428, 352);
            this.btnCancelEnd.Name = "btnCancelEnd";
            this.btnCancelEnd.Size = new System.Drawing.Size(96, 32);
            this.btnCancelEnd.TabIndex = 4;
            this.btnCancelEnd.Text = "Cancel";
            this.btnCancelEnd.UseVisualStyleBackColor = false;
            this.btnCancelEnd.Click += new System.EventHandler(this.btnCancelEnd_Click);
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(12, 352);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(208, 31);
            this.button1.TabIndex = 5;
            this.button1.Text = "License(Koean)";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // FormProxyBuilder
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
            this.ClientSize = new System.Drawing.Size(737, 468);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btnPrevious);
            this.Controls.Add(this.btnNext);
            this.Controls.Add(this.btnCancelEnd);
            this.Controls.Add(this.tabControl1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "FormProxyBuilder";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SAP RFC Proxy Builder";
            this.Closing += new System.ComponentModel.CancelEventHandler(this.FormProxyBuilder_Closing);
            this.Load += new System.EventHandler(this.FormProxyBuilder_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.ListBox lbRFCFunctions;
        private System.Windows.Forms.Button btnGetRFCFunctions;
        private System.Windows.Forms.Button btnGenDLL;
        private System.Windows.Forms.RadioButton radioClient;
        private System.Windows.Forms.RadioButton radioServer;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.ListBox lbSAPServer;
        private System.Windows.Forms.Button btnTestConn;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.TextBox txtUserID;
        private System.Windows.Forms.Label lblPassword;
        private System.Windows.Forms.Label lblUserID;
        private System.Windows.Forms.Button btnRemoveServer;
        private System.Windows.Forms.Button btnAddServer;
        private System.Windows.Forms.PropertyGrid pgServerConfig;
        private System.Windows.Forms.Button btnNext;
        private System.Windows.Forms.Button btnPrevious;
        private System.Windows.Forms.Button btnCancelEnd;
        private System.Windows.Forms.Label lblSAPNameList;
        private System.Windows.Forms.Button btnSaveConfig;
        private System.Windows.Forms.PropertyGrid pgRFCFilter;
        private System.Windows.Forms.Label lblFiler;
        private System.Windows.Forms.TextBox txtProxyResult;
        private System.Windows.Forms.CheckBox chkOutputCS;
        private System.Windows.Forms.Button btnBrowsePath;
        private System.Windows.Forms.Label txtOutputPath;
        private System.Windows.Forms.Label lblOuputPath;
        private System.Windows.Forms.TextBox txtNamespace;
        private System.Windows.Forms.Label lblNamespace;
        private System.Windows.Forms.Label lblProxyType;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtClass;
        private System.Windows.Forms.TextBox txtSelectedFuncName;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.Button button1;
    }
}

