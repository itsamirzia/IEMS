using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IEMS
{
    public partial class Main : Form
    {
        string username = string.Empty;
        public Main()
        {
            
            InitializeComponent();
        }

        private void Main_Load(object sender, EventArgs e)
        {
            menuStrip1.Enabled = false;
            this.Text = "IEMS V - " + version();
            

        }
        public static string version()
        {
            System.Reflection.Assembly assembly = System.Reflection.Assembly.GetExecutingAssembly();
            FileVersionInfo fvi = FileVersionInfo.GetVersionInfo(assembly.Location);
            return fvi.FileVersion;
        }

        private void releasePurchaseOrderToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void invoiceToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {
        }

        private void recievedOrderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddOrder objAddOrder = new AddOrder();
            objAddOrder.MdiParent = this;
            objAddOrder.Show();
        }
        

        private void btnLogin_Click(object sender, EventArgs e)
        {
            byte[] cryptoKey = Encoding.ASCII.GetBytes("M0OH2D3A4M5I6R7Z8I9Y0A1H2A3S4A5P");
            byte[] authKey = Encoding.ASCII.GetBytes("Z9I8Y7A6I5S4A3L2W1A0Y9S8A7G6R5E4");
            //string psw = Cypto.SimpleEncrypt(txtPassword.Text,cryptoKey,authKey);
            string sql = @"Select * from user where Username like '" + txtUsername.Text + "'";
            DataTable dt = new DataTable();
            db.SQLQuery(ref dt, sql);
            
            if (dt.Rows.Count > 0)
            {
                string dpsw = Cypto.SimpleDecrypt(dt.Rows[0]["password"].ToString(), cryptoKey, authKey);
                if (dpsw == txtPassword.Text)
                {
                    User.userID = dt.Rows[0][0].ToString();
                    User.username = dt.Rows[0]["username"].ToString();
                    User.role = dt.Rows[0][3].ToString();
                    menuStrip1.Enabled = btnLogout.Visible = true;
                    panel2.Hide();
                    panel1.Hide();
                    if (User.role.ToUpper() != "ADMIN")
                        menuStrip1.Items[4].Visible = false;
                    txtUsername.Text = txtPassword.Text = "";
                }
                else
                {
                    MessageBox.Show("Access Denied! Would you like to close this window?", "Error!", MessageBoxButtons.YesNo, MessageBoxIcon.Stop);
                }
            }
            else
            {
                var choice = MessageBox.Show("Access Denied! Would you like to close this window?", "Error!", MessageBoxButtons.YesNo, MessageBoxIcon.Stop);
                if (choice.ToString().ToUpper() == "OK")
                    this.Close();
            }
        }

        private void lblLogout_Click(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            for (int i = Application.OpenForms.Count - 1; i >= 0; i--)
            {
                if (Application.OpenForms[i].Name != "Main")
                    Application.OpenForms[i].Close();
            }
            menuStrip1.Enabled = btnLogout.Visible = false;
            panel2.Show();
            panel1.Show();
        }

        private void dashboardToolStripMenuItem_Click(object sender, EventArgs e)
        {
            for (int i = Application.OpenForms.Count - 1; i >= 0; i--)
            {
                if (Application.OpenForms[i].Name != "Main")
                    Application.OpenForms[i].Close();
            }
        }
    }
}
