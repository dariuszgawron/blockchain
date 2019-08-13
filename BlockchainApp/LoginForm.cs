using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PeerToPeerChat
{
    public partial class LoginForm : Form
    {
        string userName = "";

        public string UserName
        {
            get { return userName; }
        }

        public LoginForm()
        {
            InitializeComponent();

            this.FormClosing += new FormClosingEventHandler(LoginForm_FormClosing);
            btnOK.Click += new EventHandler(btnOK_Click);
        }

        void btnOK_Click(object sender, EventArgs e)
        {
            userName = tbUserName.Text.Trim();

            if (string.IsNullOrEmpty(userName))
            {
                MessageBox.Show("Please select a user name up to 32 characters.");
                return;
            }

            this.FormClosing -= LoginForm_FormClosing;
            this.Close();
        }

        void LoginForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            userName = "";
        }
    }
}
