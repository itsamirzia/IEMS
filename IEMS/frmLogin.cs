using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;


namespace IEMS
{
    
    public partial class frmLogin : Form
    {

        public frmLogin()
        {
             
            InitializeComponent();
        }
     

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Dispose();

        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string sql = @"Select * from user where Username like '" + txtUsername.Text + "' and Password like '" + txtPassword.Text + "'";
            DataTable dt = new DataTable();
            db.SQLQuery(ref dt, sql);

            if (dt.Rows.Count>0)
            {
                User.userID = dt.Rows[0][0].ToString();
                User.username = dt.Rows[0]["username"].ToString();
                User.role = dt.Rows[0][3].ToString();
                this.Close();      
            }
            else
            {
                MessageBox.Show("Access Denied! ", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Hide();

            //FrmRegister frm4 = new FrmRegister();
            //frm4.ShowDialog();

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Hide();

            //FrmAdminLogin frm5 = new FrmAdminLogin();
            //frm5.ShowDialog();
        }

        private void txtPassword_TextChanged(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void lblTime_Click(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            DateTime time = DateTime.Now;
           // string format = "MM-dd-yyy HH:mm:ss";
            lblTime.Text = time.ToString();
        }

        private void lblDate_Click(object sender, EventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
