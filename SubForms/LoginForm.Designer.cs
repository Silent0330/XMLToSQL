
namespace XMLToSQL.SubForms
{
    partial class LoginForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }


        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.idTextBox = new System.Windows.Forms.TextBox();
            this.idLabel = new System.Windows.Forms.Label();
            this.passwordLabel = new System.Windows.Forms.Label();
            this.severLabel = new System.Windows.Forms.Label();
            this.passwordTextBox = new System.Windows.Forms.TextBox();
            this.severTextBox = new System.Windows.Forms.TextBox();
            this.databaseLabel = new System.Windows.Forms.Label();
            this.databaseTextBox = new System.Windows.Forms.TextBox();
            this.loginBtn = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.cancelBtn = new System.Windows.Forms.Button();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 24.46043F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 75.53957F));
            this.tableLayoutPanel1.Controls.Add(this.idTextBox, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.idLabel, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.passwordLabel, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.severLabel, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.passwordTextBox, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.severTextBox, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.databaseLabel, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.databaseTextBox, 1, 4);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 6;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(462, 221);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // idTextBox
            // 
            this.idTextBox.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.idTextBox.Location = new System.Drawing.Point(185, 31);
            this.idTextBox.Name = "idTextBox";
            this.idTextBox.Size = new System.Drawing.Size(205, 25);
            this.idTextBox.TabIndex = 1;
            this.idTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.idTextBox_KeyDown);
            // 
            // idLabel
            // 
            this.idLabel.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.idLabel.AutoSize = true;
            this.idLabel.Font = new System.Drawing.Font("新細明體", 12F);
            this.idLabel.Location = new System.Drawing.Point(42, 34);
            this.idLabel.Name = "idLabel";
            this.idLabel.Size = new System.Drawing.Size(29, 20);
            this.idLabel.TabIndex = 2;
            this.idLabel.Text = "ID";
            // 
            // passwordLabel
            // 
            this.passwordLabel.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.passwordLabel.AutoSize = true;
            this.passwordLabel.Font = new System.Drawing.Font("新細明體", 12F);
            this.passwordLabel.Location = new System.Drawing.Point(17, 78);
            this.passwordLabel.Name = "passwordLabel";
            this.passwordLabel.Size = new System.Drawing.Size(79, 20);
            this.passwordLabel.TabIndex = 5;
            this.passwordLabel.Text = "Password";
            // 
            // severLabel
            // 
            this.severLabel.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.severLabel.AutoSize = true;
            this.severLabel.Font = new System.Drawing.Font("新細明體", 12F);
            this.severLabel.Location = new System.Drawing.Point(31, 122);
            this.severLabel.Name = "severLabel";
            this.severLabel.Size = new System.Drawing.Size(50, 20);
            this.severLabel.TabIndex = 6;
            this.severLabel.Text = "Sever";
            // 
            // passwordTextBox
            // 
            this.passwordTextBox.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.passwordTextBox.Location = new System.Drawing.Point(185, 75);
            this.passwordTextBox.Name = "passwordTextBox";
            this.passwordTextBox.PasswordChar = '*';
            this.passwordTextBox.Size = new System.Drawing.Size(205, 25);
            this.passwordTextBox.TabIndex = 7;
            this.passwordTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.passwordTextBox_KeyDown);
            // 
            // severTextBox
            // 
            this.severTextBox.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.severTextBox.Location = new System.Drawing.Point(185, 119);
            this.severTextBox.Name = "severTextBox";
            this.severTextBox.Size = new System.Drawing.Size(205, 25);
            this.severTextBox.TabIndex = 8;
            this.severTextBox.Text = "(local)";
            this.severTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.severTextBox_KeyDown);
            // 
            // databaseLabel
            // 
            this.databaseLabel.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.databaseLabel.AutoSize = true;
            this.databaseLabel.Font = new System.Drawing.Font("新細明體", 12F);
            this.databaseLabel.Location = new System.Drawing.Point(18, 166);
            this.databaseLabel.Name = "databaseLabel";
            this.databaseLabel.Size = new System.Drawing.Size(76, 20);
            this.databaseLabel.TabIndex = 9;
            this.databaseLabel.Text = "Database";
            // 
            // databaseTextBox
            // 
            this.databaseTextBox.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.databaseTextBox.Location = new System.Drawing.Point(185, 163);
            this.databaseTextBox.Name = "databaseTextBox";
            this.databaseTextBox.Size = new System.Drawing.Size(205, 25);
            this.databaseTextBox.TabIndex = 10;
            this.databaseTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dataBaseTextBox_KeyDown);
            // 
            // loginBtn
            // 
            this.loginBtn.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.loginBtn.Font = new System.Drawing.Font("新細明體", 9F);
            this.loginBtn.Location = new System.Drawing.Point(309, 10);
            this.loginBtn.Name = "loginBtn";
            this.loginBtn.Size = new System.Drawing.Size(106, 34);
            this.loginBtn.TabIndex = 0;
            this.loginBtn.Text = "Login";
            this.loginBtn.UseVisualStyleBackColor = true;
            this.loginBtn.Click += new System.EventHandler(this.loginBtn_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.cancelBtn);
            this.panel1.Controls.Add(this.loginBtn);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 217);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(462, 56);
            this.panel1.TabIndex = 1;
            // 
            // cancelBtn
            // 
            this.cancelBtn.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.cancelBtn.Font = new System.Drawing.Font("新細明體", 9F);
            this.cancelBtn.Location = new System.Drawing.Point(46, 10);
            this.cancelBtn.Name = "cancelBtn";
            this.cancelBtn.Size = new System.Drawing.Size(106, 34);
            this.cancelBtn.TabIndex = 1;
            this.cancelBtn.Text = "Cancel";
            this.cancelBtn.UseVisualStyleBackColor = true;
            this.cancelBtn.Click += new System.EventHandler(this.cancelBtn_Click);
            // 
            // LoginForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(462, 273);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "LoginForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "LoginForm";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TextBox idTextBox;
        private System.Windows.Forms.Label idLabel;
        private System.Windows.Forms.Button loginBtn;
        private System.Windows.Forms.Label passwordLabel;
        private System.Windows.Forms.Label severLabel;
        private System.Windows.Forms.TextBox passwordTextBox;
        private System.Windows.Forms.TextBox severTextBox;
        private System.Windows.Forms.Label databaseLabel;
        private System.Windows.Forms.TextBox databaseTextBox;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button cancelBtn;
    }
}