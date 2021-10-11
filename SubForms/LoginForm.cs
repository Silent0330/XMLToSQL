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
    public partial class LoginForm : Form
    {
        private MainForm mainForm;

        public LoginForm(MainForm mainForm)
        {
            InitializeComponent();
            this.mainForm = mainForm;
        }

        private bool login()
        {
            if(idTextBox.Text == "")
            {
                MessageBox.Show("Warning:\nID is empty", "Login");
                return false;
            }
            else if (severTextBox.Text == "")
            {
                MessageBox.Show("Warning:\nSever is empty", "Login");
                return false;
            }
            else if (databaseTextBox.Text == "")
            {
                MessageBox.Show("Warning:\nDatabase is empty", "Login");
                return false;
            }

            int result = mainForm.Login(idTextBox.Text, passwordTextBox.Text, severTextBox.Text, databaseTextBox.Text);
            switch (result)
            {
                case 0:
                    return true;
                case -2:
                    MessageBox.Show("Login failed:\nNot found sever", "Login");
                    break;
                case -3:
                    MessageBox.Show("Login failed:\nID or password error", "Login");
                    break;

            }
            return false;

        }

        private void loginBtn_Click(object sender, EventArgs e)
        {
            if (login())
                this.Close();
        }

        private void idTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (login())
                    this.Close();
            }
        }

        private void passwordTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                if (login())
                    this.Close();
            }
        }

        private void severTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (login())
                    this.Close();
            }
        }

        private void dataBaseTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (login())
                    this.Close();
            }
        }

        private void cancelBtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
