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
    public partial class AddOrder : Form
    {


        ListViewItem lst;
        frmLogin login = new frmLogin();

        public AddOrder()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
          
        } 
        public void getManufacturer()
        {
            cboManufac.Items.Clear();
            try
            {
                string sql2 = @"Select concat(name,'(',ID,')') manu from Manufacturer";
                DataTable dt = new DataTable();
                db.SQLQuery(ref dt, sql2);
                foreach (DataRow dr in dt.Rows)
                {
                    cboManufac.Items.Add(dr[0].ToString());
      
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
    
        }

        private void Form11_Load(object sender, EventArgs e)
        {
            cmbProduct.Items.Clear();
            cmbClient.Items.Clear();
            cboManufac.Items.Clear();

            FillOrderGrid();

            txtOrderID.Text = GenerateID.Get(Enums.IDType.PUOR);

            cmbProduct.DataSource = GetData.SelectAll(Enums.IDType.PROD);
            cmbClient.DataSource = GetData.SelectAll(Enums.IDType.CLNT);
            cboManufac.DataSource = GetData.SelectAll(Enums.IDType.MANF);
          
        }
      
        private void btnUpdateI_Click(object sender, EventArgs e)
        {

        }
        public void CLear() 
        {
          //  txtIDCode.Text = "";
            //txtName.Text = "";
            //txtPrice.Text = "";
            //cboGender.Text = "-SELECT-";
            //cboSize.Text = "-SELECT-";
            //txtBrand.Text = "";
            //txtStock.Text = "";
            
        
        }

        private void txtPrice_TextChanged(object sender, EventArgs e)
        {
           

            if (txtPrice.Text == "")
            {


            }
            else 
            {

                try
                {


                }
                catch (FormatException)
                {
                    MessageBox.Show("Enter Numbers Only.", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtPrice.Text = "0.00";


                }
            
            }

          

        }

        private void txtIDCode_TextChanged(object sender, EventArgs e)
        {
           
        }

        private void cboGender_SelectedIndexChanged(object sender, EventArgs e)
        {
          
        }

        private void txtStock_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void txtPrice_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblTempID.Text = listView1.FocusedItem.Text;
            lblTempName.Text = listView1.FocusedItem.SubItems[2].Text;            
        }


        private void txtDeliveryDate_TextChanged(object sender, EventArgs e)
        {
            if (txtDeliveryDate.Text == "")
            {

                lblTempDate.Text = "";

            }
            else 
            {
                int arrive = Convert.ToInt32(txtDeliveryDate.Text);
                DateTime date = DateTime.Now;
                date = date.AddDays(arrive);             
                lblTempDate.Text = date.ToString();
                txtCritLimit.Enabled = true;   
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {

        }


        public void FillOrderGrid()
        {
            //displaying data from Database to lstview
            try
            {
                listView1.Items.Clear();
                listView1.Columns.Clear();
                listView1.Columns.Add("Order ID", 90);
                listView1.Columns.Add("Product Name", 190);
                listView1.Columns.Add("Manufacturer Price", 90);
                listView1.Columns.Add("Price", 100);
                listView1.Columns.Add("Material", 80);
                listView1.Columns.Add("Size", 80);
                listView1.Columns.Add("Client", 90);
                listView1.Columns.Add("Quantiy", 190);
                listView1.Columns.Add("Manufacturer", 190);
                listView1.Columns.Add("Date Orderded", 190);
                listView1.Columns.Add("Date of Delivery", 190);
                listView1.Columns.Add("CritLimit", 90);

                string sql2 = @"SELECT OrderID, ProductName, `Offered Price`,Price, Material,Size,ClientCode, Quantity,Manufacturer, TotalDays, DODelivery, CriticalDate FROM `product` p inner join `orderreceived` o on p.id = substring_index(substring_index(o.ProductName,'(',-1),')',1);";
                DataTable dtOrders = new DataTable();
                db.SQLQuery(ref dtOrders, sql2);
                
                foreach(DataRow dr in dtOrders.Rows)
                {
                    lst = listView1.Items.Add(dr[0].ToString());
                    lst.SubItems.Add(dr[1].ToString());
                    lst.SubItems.Add(dr[2].ToString());
                    lst.SubItems.Add(dr[3].ToString());
                    lst.SubItems.Add(dr[4].ToString());
                    lst.SubItems.Add(dr[5].ToString());
                    lst.SubItems.Add(dr[6].ToString());
                    lst.SubItems.Add(dr[7].ToString());
                    lst.SubItems.Add(dr[8].ToString());
                    lst.SubItems.Add(dr[9].ToString());
                    lst.SubItems.Add(dr[10].ToString());
                    lst.SubItems.Add(dr[11].ToString());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnAddOrder(object sender, EventArgs e)
        {
            if (txtPrice.Text == "" || txtNetPrice.Text == "" || txtQuantity.Text == "" || cboManufac.Text == "-Select-" || txtDeliveryDate.Text == "" || txtCritLimit.Text == "")
            {
                MessageBox.Show("Fill textboxes to proceed.", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if(Convert.ToDouble(txtPrice.Text) > Convert.ToDouble(txtNetPrice.Text))
            {
                MessageBox.Show("The Price Should not be more than that to It's NetPrice","Message",MessageBoxButtons.OK,MessageBoxIcon.Information);
                return;
            }

            else 
            {
                try
                {
                    string sql = @"INSERT INTO orderreceived VALUES
                    ('" + txtOrderID.Text + "','"+cmbProduct.SelectedItem.ToString()+ "','" + cmbClient.SelectedItem.ToString() + "','"+txtQuantity.Text+"','"+txtDeliveryDate.Text+"','"+lblTempDate.Text+"','"+ Convert.ToInt32(txtQuantity.Text) * Convert.ToInt32(txtPrice.Text)+"','" + lblCriticalDate.Text + "')";
                    if (db.ExecuteSQLQuery(sql))
                    {
                        MessageBox.Show("Record successfully saved!", "OK!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        InsertTrail();
                        this.Clear();
                        //cboGender.SelectedIndex = 0;
                        //cboSize.SelectedIndex = 0;
                        cboManufac.SelectedIndex = 0;
                        txtCritLimit.Text = "";
                        listView1.Refresh();
                        //Form11_Load(IEMS.AddOrder, e);
                        
                    }
                    else
                    {
                        MessageBox.Show("Not able to save records!", "OK!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception l)
                {
                    MessageBox.Show("Re-input again. ID may already be taken!");
                    MessageBox.Show(l.Message);
                }
            
            }
    
        }
        public void InsertTrail()
        {
            try
            {
                string sql = @"INSERT INTO tblAuditTrail VALUES(@Dater,@Transactype,@Description,@Authority)";
                //cm = new SqlCommand(sql, cn);
                //cm.Parameters.AddWithValue("@Dater", lblDate.Text);
                //cm.Parameters.AddWithValue("@Transactype", "Insertion");
                //cm.Parameters.AddWithValue("@Description", "Order:" + txtOrderID.Text + " has been sent to orders!");
                //cm.Parameters.AddWithValue("@Authority", "Admin");


                //cm.ExecuteNonQuery();
                //   MessageBox.Show("Record successfully saved!", "OK!", MessageBoxButtons.OK, MessageBoxIcon.Information);


            }
            catch (SqlException l)
            {
                MessageBox.Show("Re-input again. your username may already be taken!");
                MessageBox.Show(l.Message);
            }
        }
        public void DeleteTrail() 
        {
            try
            {
                string sql = @"INSERT INTO tblAuditTrail VALUES(@Dater,@Transactype,@Description,@Authority)";
                //cm = new SqlCommand(sql, cn);
                //cm.Parameters.AddWithValue("@Dater", lblDate.Text);
                //cm.Parameters.AddWithValue("@Transactype", "Deletion");
                //cm.Parameters.AddWithValue("@Description", "Item: " + lblTempName.Text + " has been removed from orders!");
                //cm.Parameters.AddWithValue("@Authority", "Admin");


                //cm.ExecuteNonQuery();
                //   MessageBox.Show("Record successfully saved!", "OK!", MessageBoxButtons.OK, MessageBoxIcon.Information);


            }
            catch (SqlException l)
            {
                MessageBox.Show("Re-input again. your username may already be taken!");
                MessageBox.Show(l.Message);
            }


        }
        public void AllDelTrail()
        {

            try
            {
                string sql = @"INSERT INTO tblAuditTrail VALUES(@Dater,@Transactype,@Description,@Authority)";
                //cm = new SqlCommand(sql, cn);
                //cm.Parameters.AddWithValue("@Dater", lblDate.Text);
                //cm.Parameters.AddWithValue("@Transactype", "Deletion");
                //cm.Parameters.AddWithValue("@Description", "All Items from orders were REMOVED!");
                //cm.Parameters.AddWithValue("@Authority", "Admin");

                //cm.ExecuteNonQuery();
               
            }
            catch (SqlException l)
            {
                MessageBox.Show("Re-input again. your username may already be taken!");
                MessageBox.Show(l.Message);
            }

        }
        public void Clear() 
        {

           // txtOrderID.Text = "";
           // txtIDCode.Text = "";
            //txtName.Text = "";
            txtPrice.Text = "";
            txtNetPrice.Text = "";
            txtQuantity.Text = "";
            txtDeliveryDate.Text = "";
            //txtBrand.Text = "";
         
            lblTempID.Text = "";
        
        }

        private void txtDeliveryDate_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void txtNetPrice_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }
        private void button2_Click(object sender, EventArgs e)
        {
           this.Dispose();
            //frmAddManufac frmManufac = new frmAddManufac();
            //frmManufac.ShowDialog();
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            getManufacturer();
        }
        private void btnRemove_Click(object sender, EventArgs e)
        {
            if (listView1.Items.Count == 0 || lblTempID.Text == "")
            {
                MessageBox.Show("Nothing to Delete!. Please Select an item.", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else 
            {
                if (MessageBox.Show("Do you really want to delete this Order?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                {
                    DeleteTrail();
                    deleteRecords();                  
                }
            }       
        }
        public void deleteRecords()
        {
            try
            {
             
             //   listView1.FocusedItem.Remove();
                string del = "DELETE from orderreceived where orderID='" + lblTempID.Text + "'";
                if (db.ExecuteSQLQuery(del))
                {
                    //cm = new SqlCommand(del, cn); cm.ExecuteNonQuery();
                    MessageBox.Show("Successfully Deleted!");
                    Clear();
                    FillOrderGrid();
                }
                else
                {
                    MessageBox.Show("Not able to delete!");
                }

            }
            catch (Exception)
            {
                MessageBox.Show("No Item to Remove", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }

        }

        private void btnRemoveAll_Click(object sender, EventArgs e)
        {
            if(listView1.Items.Count == 0)
            {

                return;
            }

            if (MessageBox.Show("Do you really want to delete ALL Order?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
            {
                AllDelTrail();
                DeleteAll();
               
            }
        }
        public void DeleteAll()
        {

            try
            {

                // listView1.FocusedItem.Remove();
                string del = "DELETE * from tblNewOrder ";
                //cm = new SqlCommand(del, cn); cm.ExecuteNonQuery();

                MessageBox.Show("Successfully Deleted!");
                FillOrderGrid();
                Clear();
                
            }
            catch (Exception)
            {
                MessageBox.Show("No Item to Remove", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
        }

        private void cboManufac_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void txtCritLimit_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void txtCritLimit_TextChanged(object sender, EventArgs e)
        {
            //DateTime date = Convert.ToDateTime(lblTempDate.Text);
            //date = date.AddDays(txtCritLimit.Text);
            if (txtCritLimit.Text == "")
            {

                lblCriticalDate.Text = "";

            }
            else
            {
                int arrive = Convert.ToInt32(txtCritLimit.Text);
                DateTime date = Convert.ToDateTime(lblTempDate.Text);
                date = date.AddDays(arrive);
                lblCriticalDate.Text = date.ToString();
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            //addProduct objAddProduct = new addProduct();
            //objAddProduct.Child
            //objAddProduct.Show();
        }

        private void cmbProduct_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable dtProduct = new DataTable();
            dtProduct = GetData.Select(Enums.IDType.PROD, cmbProduct.SelectedItem.ToString().Split(new[] { "(",")"},StringSplitOptions.RemoveEmptyEntries)[1]);
            txtNetPrice.Text = dtProduct.Rows[0]["Price"].ToString();
            txtPrice.Text = dtProduct.Rows[0]["Offered Price"].ToString();
            txtMaterial.Text = dtProduct.Rows[0]["Material"].ToString();
            txtSize.Text = dtProduct.Rows[0]["Size"].ToString();

        }
    }
}
