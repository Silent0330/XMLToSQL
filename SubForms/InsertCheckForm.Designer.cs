
namespace XMLToSQL.SubForms
{
    partial class InsertCheckForm
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
            this.serverLabel = new System.Windows.Forms.Label();
            this.serverTextBox = new System.Windows.Forms.TextBox();
            this.databaseTextBox = new System.Windows.Forms.TextBox();
            this.databaseLabel = new System.Windows.Forms.Label();
            this.cancelBtn = new System.Windows.Forms.Button();
            this.confirmBtn = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30.51948F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 69.48052F));
            this.tableLayoutPanel1.Controls.Add(this.serverLabel, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.serverTextBox, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.databaseTextBox, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.databaseLabel, 0, 2);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 4;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 5F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 45F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 45F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 5F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(462, 212);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // serverLabel
            // 
            this.serverLabel.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.serverLabel.AutoSize = true;
            this.serverLabel.Font = new System.Drawing.Font("新細明體", 14F);
            this.serverLabel.Location = new System.Drawing.Point(35, 45);
            this.serverLabel.Name = "serverLabel";
            this.serverLabel.Size = new System.Drawing.Size(70, 24);
            this.serverLabel.TabIndex = 0;
            this.serverLabel.Text = "Server";
            // 
            // serverTextBox
            // 
            this.serverTextBox.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.serverTextBox.Location = new System.Drawing.Point(176, 45);
            this.serverTextBox.Name = "serverTextBox";
            this.serverTextBox.Size = new System.Drawing.Size(250, 25);
            this.serverTextBox.TabIndex = 1;
            // 
            // databaseTextBox
            // 
            this.databaseTextBox.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.databaseTextBox.Location = new System.Drawing.Point(176, 140);
            this.databaseTextBox.Name = "databaseTextBox";
            this.databaseTextBox.Size = new System.Drawing.Size(250, 25);
            this.databaseTextBox.TabIndex = 6;
            // 
            // databaseLabel
            // 
            this.databaseLabel.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.databaseLabel.AutoSize = true;
            this.databaseLabel.Font = new System.Drawing.Font("新細明體", 14F);
            this.databaseLabel.Location = new System.Drawing.Point(24, 140);
            this.databaseLabel.Name = "databaseLabel";
            this.databaseLabel.Size = new System.Drawing.Size(92, 24);
            this.databaseLabel.TabIndex = 4;
            this.databaseLabel.Text = "Database";
            // 
            // cancelBtn
            // 
            this.cancelBtn.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.cancelBtn.Location = new System.Drawing.Point(26, 19);
            this.cancelBtn.Name = "cancelBtn";
            this.cancelBtn.Size = new System.Drawing.Size(90, 30);
            this.cancelBtn.TabIndex = 2;
            this.cancelBtn.Text = "Cancel";
            this.cancelBtn.UseVisualStyleBackColor = true;
            this.cancelBtn.Click += new System.EventHandler(this.cancelBtn_Click);
            // 
            // confirmBtn
            // 
            this.confirmBtn.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.confirmBtn.Location = new System.Drawing.Point(333, 19);
            this.confirmBtn.Name = "confirmBtn";
            this.confirmBtn.Size = new System.Drawing.Size(90, 30);
            this.confirmBtn.TabIndex = 3;
            this.confirmBtn.Text = "Confirm";
            this.confirmBtn.UseVisualStyleBackColor = true;
            this.confirmBtn.Click += new System.EventHandler(this.confirmBtn_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.cancelBtn);
            this.panel1.Controls.Add(this.confirmBtn);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 212);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(462, 61);
            this.panel1.TabIndex = 1;
            // 
            // InsertCheckForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(462, 273);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "InsertCheckForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "InputDataBaseForm";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.InsertCheckForm_FormClosed);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label serverLabel;
        private System.Windows.Forms.TextBox serverTextBox;
        private System.Windows.Forms.Button cancelBtn;
        private System.Windows.Forms.Button confirmBtn;
        private System.Windows.Forms.Label databaseLabel;
        private System.Windows.Forms.TextBox databaseTextBox;
        private System.Windows.Forms.Panel panel1;
    }
}