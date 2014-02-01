using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CataclysmModder
{
    public partial class ServerQuery : Form
    {
        private string oldserver;
        public static string server = "";

        public ServerQuery()
        {
            InitializeComponent();

            textBox1.Text = server;
            oldserver = server;
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            DialogResult = System.Windows.Forms.DialogResult.OK;
            Form1.Instance.SaveConfig();
            Close();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            server = textBox1.Text;
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            DialogResult = System.Windows.Forms.DialogResult.Cancel;
            server = oldserver;
            Close();
        }
    }
}
