
namespace XMLToSQL
{
    partial class MainForm
    {
        /// <summary>
        /// 設計工具所需的變數。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清除任何使用中的資源。
        /// </summary>
        /// <param name="disposing">如果應該處置受控資源則為 true，否則為 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 設計工具產生的程式碼

        /// <summary>
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器修改
        /// 這個方法的內容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.dataGridView = new System.Windows.Forms.DataGridView();
            this.readBtn = new System.Windows.Forms.Button();
            this.folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.srcTextBox = new System.Windows.Forms.TextBox();
            this.srcBrowseBtn = new System.Windows.Forms.Button();
            this.fullRunBtn = new System.Windows.Forms.Button();
            this.LoginBtn = new System.Windows.Forms.Button();
            this.stopBtn = new System.Windows.Forms.Button();
            this.stateTitleLabel = new System.Windows.Forms.Label();
            this.stateLabel = new System.Windows.Forms.Label();
            this.timer = new System.Windows.Forms.Timer(this.components);
            this.dstTextBox = new System.Windows.Forms.TextBox();
            this.dstBrowseBtn = new System.Windows.Forms.Button();
            this.srcLabel = new System.Windows.Forms.Label();
            this.dstLabel = new System.Windows.Forms.Label();
            this.insertBrn = new System.Windows.Forms.Button();
            this.clearBtn = new System.Windows.Forms.Button();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.processLabel = new System.Windows.Forms.Label();
            this.UserTitleLabel = new System.Windows.Forms.Label();
            this.UserLabel = new System.Windows.Forms.Label();
            this.switchTableBtn = new System.Windows.Forms.Button();
            this.tableTitleLabel = new System.Windows.Forms.Label();
            this.tableLabel = new System.Windows.Forms.Label();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView
            // 
            this.dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView.Location = new System.Drawing.Point(0, 120);
            this.dataGridView.Name = "dataGridView";
            this.dataGridView.RowHeadersWidth = 51;
            this.dataGridView.RowTemplate.Height = 27;
            this.dataGridView.Size = new System.Drawing.Size(1265, 555);
            this.dataGridView.TabIndex = 0;
            // 
            // readBtn
            // 
            this.readBtn.Font = new System.Drawing.Font("微軟正黑體", 12F);
            this.readBtn.Location = new System.Drawing.Point(10, 10);
            this.readBtn.Name = "readBtn";
            this.readBtn.Size = new System.Drawing.Size(100, 40);
            this.readBtn.TabIndex = 1;
            this.readBtn.Text = "Read";
            this.readBtn.UseVisualStyleBackColor = true;
            this.readBtn.Click += new System.EventHandler(this.readXMLBtn_Click);
            // 
            // folderBrowserDialog
            // 
            this.folderBrowserDialog.ShowNewFolderButton = false;
            // 
            // srcTextBox
            // 
            this.srcTextBox.Font = new System.Drawing.Font("新細明體", 12F);
            this.srcTextBox.Location = new System.Drawing.Point(834, 15);
            this.srcTextBox.Name = "srcTextBox";
            this.srcTextBox.Size = new System.Drawing.Size(310, 31);
            this.srcTextBox.TabIndex = 2;
            // 
            // srcBrowseBtn
            // 
            this.srcBrowseBtn.Font = new System.Drawing.Font("微軟正黑體", 12F);
            this.srcBrowseBtn.Location = new System.Drawing.Point(1150, 10);
            this.srcBrowseBtn.Name = "srcBrowseBtn";
            this.srcBrowseBtn.Size = new System.Drawing.Size(100, 40);
            this.srcBrowseBtn.TabIndex = 3;
            this.srcBrowseBtn.Text = "Browse";
            this.srcBrowseBtn.UseVisualStyleBackColor = true;
            this.srcBrowseBtn.Click += new System.EventHandler(this.srcBrowseBtn_Click);
            // 
            // fullRunBtn
            // 
            this.fullRunBtn.Font = new System.Drawing.Font("微軟正黑體", 12F);
            this.fullRunBtn.Location = new System.Drawing.Point(230, 10);
            this.fullRunBtn.Name = "fullRunBtn";
            this.fullRunBtn.Size = new System.Drawing.Size(100, 40);
            this.fullRunBtn.TabIndex = 4;
            this.fullRunBtn.Text = "FullRun";
            this.fullRunBtn.UseVisualStyleBackColor = true;
            this.fullRunBtn.Click += new System.EventHandler(this.fullRunBtn_Click);
            // 
            // LoginBtn
            // 
            this.LoginBtn.Font = new System.Drawing.Font("微軟正黑體", 12F);
            this.LoginBtn.Location = new System.Drawing.Point(660, 70);
            this.LoginBtn.Name = "LoginBtn";
            this.LoginBtn.Size = new System.Drawing.Size(100, 40);
            this.LoginBtn.TabIndex = 5;
            this.LoginBtn.Text = "Login";
            this.LoginBtn.UseVisualStyleBackColor = true;
            this.LoginBtn.Click += new System.EventHandler(this.LoginBtn_Click);
            // 
            // stopBtn
            // 
            this.stopBtn.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.stopBtn.Location = new System.Drawing.Point(340, 10);
            this.stopBtn.Name = "stopBtn";
            this.stopBtn.Size = new System.Drawing.Size(100, 40);
            this.stopBtn.TabIndex = 8;
            this.stopBtn.Text = "Stop";
            this.stopBtn.UseVisualStyleBackColor = true;
            this.stopBtn.Click += new System.EventHandler(this.stopBtn_Click);
            // 
            // stateTitleLabel
            // 
            this.stateTitleLabel.AutoSize = true;
            this.stateTitleLabel.Font = new System.Drawing.Font("微軟正黑體", 12F);
            this.stateTitleLabel.Location = new System.Drawing.Point(10, 80);
            this.stateTitleLabel.Name = "stateTitleLabel";
            this.stateTitleLabel.Size = new System.Drawing.Size(64, 25);
            this.stateTitleLabel.TabIndex = 9;
            this.stateTitleLabel.Text = "State:";
            // 
            // stateLabel
            // 
            this.stateLabel.AutoSize = true;
            this.stateLabel.Font = new System.Drawing.Font("微軟正黑體", 12F);
            this.stateLabel.Location = new System.Drawing.Point(80, 80);
            this.stateLabel.Name = "stateLabel";
            this.stateLabel.Size = new System.Drawing.Size(64, 25);
            this.stateLabel.TabIndex = 10;
            this.stateLabel.Text = "None";
            // 
            // timer
            // 
            this.timer.Enabled = true;
            this.timer.Interval = 30;
            this.timer.Tick += new System.EventHandler(this.timer_Tick);
            // 
            // dstTextBox
            // 
            this.dstTextBox.Font = new System.Drawing.Font("新細明體", 12F);
            this.dstTextBox.Location = new System.Drawing.Point(834, 75);
            this.dstTextBox.Name = "dstTextBox";
            this.dstTextBox.Size = new System.Drawing.Size(310, 31);
            this.dstTextBox.TabIndex = 11;
            // 
            // dstBrowseBtn
            // 
            this.dstBrowseBtn.Font = new System.Drawing.Font("微軟正黑體", 12F);
            this.dstBrowseBtn.Location = new System.Drawing.Point(1150, 70);
            this.dstBrowseBtn.Name = "dstBrowseBtn";
            this.dstBrowseBtn.Size = new System.Drawing.Size(100, 40);
            this.dstBrowseBtn.TabIndex = 12;
            this.dstBrowseBtn.Text = "Browse";
            this.dstBrowseBtn.UseVisualStyleBackColor = true;
            this.dstBrowseBtn.Click += new System.EventHandler(this.dstBrowseBtn_Click);
            // 
            // srcLabel
            // 
            this.srcLabel.AutoSize = true;
            this.srcLabel.Font = new System.Drawing.Font("新細明體", 14F);
            this.srcLabel.Location = new System.Drawing.Point(776, 20);
            this.srcLabel.Name = "srcLabel";
            this.srcLabel.Size = new System.Drawing.Size(47, 24);
            this.srcLabel.TabIndex = 13;
            this.srcLabel.Text = "Src:";
            // 
            // dstLabel
            // 
            this.dstLabel.AutoSize = true;
            this.dstLabel.Font = new System.Drawing.Font("新細明體", 14F);
            this.dstLabel.Location = new System.Drawing.Point(776, 80);
            this.dstLabel.Name = "dstLabel";
            this.dstLabel.Size = new System.Drawing.Size(47, 24);
            this.dstLabel.TabIndex = 14;
            this.dstLabel.Text = "Dst:";
            // 
            // insertBrn
            // 
            this.insertBrn.Font = new System.Drawing.Font("微軟正黑體", 12F);
            this.insertBrn.Location = new System.Drawing.Point(120, 10);
            this.insertBrn.Name = "insertBrn";
            this.insertBrn.Size = new System.Drawing.Size(100, 40);
            this.insertBrn.TabIndex = 15;
            this.insertBrn.Text = "Insert";
            this.insertBrn.UseVisualStyleBackColor = true;
            this.insertBrn.Click += new System.EventHandler(this.insertBtn_Click);
            // 
            // clearBtn
            // 
            this.clearBtn.Font = new System.Drawing.Font("微軟正黑體", 12F);
            this.clearBtn.Location = new System.Drawing.Point(450, 10);
            this.clearBtn.Name = "clearBtn";
            this.clearBtn.Size = new System.Drawing.Size(100, 40);
            this.clearBtn.TabIndex = 16;
            this.clearBtn.Text = "Clear";
            this.clearBtn.UseVisualStyleBackColor = true;
            this.clearBtn.Click += new System.EventHandler(this.clearBtn_Click);
            // 
            // progressBar
            // 
            this.progressBar.Location = new System.Drawing.Point(195, 89);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(200, 25);
            this.progressBar.TabIndex = 17;
            // 
            // processLabel
            // 
            this.processLabel.AutoSize = true;
            this.processLabel.BackColor = System.Drawing.Color.Transparent;
            this.processLabel.Font = new System.Drawing.Font("微軟正黑體", 12F);
            this.processLabel.Location = new System.Drawing.Point(271, 61);
            this.processLabel.Name = "processLabel";
            this.processLabel.Size = new System.Drawing.Size(44, 25);
            this.processLabel.TabIndex = 18;
            this.processLabel.Text = "0/0";
            // 
            // UserTitleLabel
            // 
            this.UserTitleLabel.AutoSize = true;
            this.UserTitleLabel.Font = new System.Drawing.Font("微軟正黑體", 12F);
            this.UserTitleLabel.Location = new System.Drawing.Point(560, 20);
            this.UserTitleLabel.Name = "UserTitleLabel";
            this.UserTitleLabel.Size = new System.Drawing.Size(59, 25);
            this.UserTitleLabel.TabIndex = 19;
            this.UserTitleLabel.Text = "User:";
            // 
            // UserLabel
            // 
            this.UserLabel.AutoSize = true;
            this.UserLabel.Font = new System.Drawing.Font("微軟正黑體", 12F);
            this.UserLabel.Location = new System.Drawing.Point(620, 20);
            this.UserLabel.Name = "UserLabel";
            this.UserLabel.Size = new System.Drawing.Size(64, 25);
            this.UserLabel.TabIndex = 20;
            this.UserLabel.Text = "None";
            // 
            // switchTableBtn
            // 
            this.switchTableBtn.Font = new System.Drawing.Font("微軟正黑體", 12F);
            this.switchTableBtn.Location = new System.Drawing.Point(550, 70);
            this.switchTableBtn.Name = "switchTableBtn";
            this.switchTableBtn.Size = new System.Drawing.Size(100, 40);
            this.switchTableBtn.TabIndex = 21;
            this.switchTableBtn.Text = "Switch";
            this.switchTableBtn.UseVisualStyleBackColor = true;
            this.switchTableBtn.Click += new System.EventHandler(this.switchTableBtn_Click);
            // 
            // tableTitleLabel
            // 
            this.tableTitleLabel.AutoSize = true;
            this.tableTitleLabel.Font = new System.Drawing.Font("微軟正黑體", 12F);
            this.tableTitleLabel.Location = new System.Drawing.Point(401, 78);
            this.tableTitleLabel.Name = "tableTitleLabel";
            this.tableTitleLabel.Size = new System.Drawing.Size(68, 25);
            this.tableTitleLabel.TabIndex = 22;
            this.tableTitleLabel.Text = "Table:";
            // 
            // tableLabel
            // 
            this.tableLabel.AutoSize = true;
            this.tableLabel.Font = new System.Drawing.Font("微軟正黑體", 12F);
            this.tableLabel.Location = new System.Drawing.Point(467, 78);
            this.tableLabel.Name = "tableLabel";
            this.tableLabel.Size = new System.Drawing.Size(83, 25);
            this.tableLabel.TabIndex = 23;
            this.tableLabel.Text = "Normal";
            // 
            // toolTip
            // 
            this.toolTip.IsBalloon = true;
            this.toolTip.SetToolTip(this.readBtn, "讀取來源XML到Datatable");
            this.toolTip.SetToolTip(this.insertBrn, "將Datatable寫入到Database");
            this.toolTip.SetToolTip(this.fullRunBtn, "讀取來源XML並寫入到Database");
            this.toolTip.SetToolTip(this.stopBtn, "停止現在執行的程序");
            this.toolTip.SetToolTip(this.LoginBtn, "登入到Database");
            this.toolTip.SetToolTip(this.srcBrowseBtn, "選擇來源的資料夾");
            this.toolTip.SetToolTip(this.dstBrowseBtn, "選擇完成檔案的資料夾");
            this.toolTip.SetToolTip(this.switchTableBtn, "切換normal table/error table");
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1262, 673);
            this.Controls.Add(this.tableLabel);
            this.Controls.Add(this.tableTitleLabel);
            this.Controls.Add(this.switchTableBtn);
            this.Controls.Add(this.UserLabel);
            this.Controls.Add(this.UserTitleLabel);
            this.Controls.Add(this.processLabel);
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.clearBtn);
            this.Controls.Add(this.insertBrn);
            this.Controls.Add(this.dstLabel);
            this.Controls.Add(this.srcLabel);
            this.Controls.Add(this.dstBrowseBtn);
            this.Controls.Add(this.dstTextBox);
            this.Controls.Add(this.stateLabel);
            this.Controls.Add(this.stateTitleLabel);
            this.Controls.Add(this.stopBtn);
            this.Controls.Add(this.LoginBtn);
            this.Controls.Add(this.fullRunBtn);
            this.Controls.Add(this.srcBrowseBtn);
            this.Controls.Add(this.srcTextBox);
            this.Controls.Add(this.readBtn);
            this.Controls.Add(this.dataGridView);
            this.MinimumSize = new System.Drawing.Size(1280, 720);
            this.Name = "MainForm";
            this.Text = "XMLToSQL";
            this.SizeChanged += new System.EventHandler(this.MainForm_SizeChanged);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView;
        private System.Windows.Forms.Button readBtn;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog;
        private System.Windows.Forms.TextBox srcTextBox;
        private System.Windows.Forms.Button srcBrowseBtn;
        private System.Windows.Forms.Button fullRunBtn;
        private System.Windows.Forms.Button LoginBtn;
        private System.Windows.Forms.Button stopBtn;
        private System.Windows.Forms.Label stateTitleLabel;
        private System.Windows.Forms.Label stateLabel;
        private System.Windows.Forms.Timer timer;
        private System.Windows.Forms.TextBox dstTextBox;
        private System.Windows.Forms.Button dstBrowseBtn;
        private System.Windows.Forms.Label srcLabel;
        private System.Windows.Forms.Label dstLabel;
        private System.Windows.Forms.Button insertBrn;
        private System.Windows.Forms.Button clearBtn;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.Label processLabel;
        private System.Windows.Forms.Label UserTitleLabel;
        private System.Windows.Forms.Label UserLabel;
        private System.Windows.Forms.Button switchTableBtn;
        private System.Windows.Forms.Label tableTitleLabel;
        private System.Windows.Forms.Label tableLabel;
        private System.Windows.Forms.ToolTip toolTip;
    }
}

