using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace XMLToSQL.SubForms
{
    public partial class InsertCheckForm : Form
    {
        private MainForm mainForm;

        private bool confirm;

        public InsertCheckForm(MainForm mainForm)
        {
            InitializeComponent();

            this.mainForm = mainForm;
            serverTextBox.Text = mainForm.ServerName;
            databaseTextBox.Text = mainForm.DatabaseName;

            confirm = false;
        }

        private void cancelBtn_Click(object sender, EventArgs e)
        {
            confirm = false;
            this.Close();
        }

        private void confirmBtn_Click(object sender, EventArgs e)
        {
            if (serverTextBox.Text == "")
            {
                MessageBox.Show("Warning:\nSever is empty", "Insert");
                return;
            }
            else if (databaseTextBox.Text == "")
            {
                MessageBox.Show("Warning:\nDatabase is empty", "Insert");
                return;
            }

            mainForm.ServerName = serverTextBox.Text;
            mainForm.DatabaseName = databaseTextBox.Text;
            confirm = true;
            this.Close();
        }

        private void InsertCheckForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (confirm)
            {
                this.DialogResult = DialogResult.OK;
            }
            else
            {
                this.DialogResult = DialogResult.No;
            }
        }
    }
}
