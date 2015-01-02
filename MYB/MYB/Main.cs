using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Security.Permissions;
using System.Web;
using System.Collections;
using MYB.DAO;

namespace MYB
{
    public partial class frmMain : Form
    {
        AutoID ai = null;
        BindingSource bs = null;
        DataTable dt = new DataTable();
        public delegate void delegateGrid(DataTable dt);

        string Pro_ID;


        public void frm_Main_Load(object sender, EventArgs e)
        {

            txt_BEID.Text = username;
        }

        public frmMain(string Pro_ID)
        {
            this.Pro_ID = Pro_ID;
            SqlClientPermission permission = new SqlClientPermission(PermissionState.Unrestricted);
            permission.Demand();
            InitializeComponent();
            ConnectionString cs = new ConnectionString();
            string con_string = cs.GetCS;
            SqlConnection con = new SqlConnection(con_string);
            SqlDataAdapter da = null;
            DataTable dt = new DataTable();
            SqlCommandBuilder builder = null;
            BindingSource bs = null;
            da = new SqlDataAdapter("Select * from Product", con);
            builder = new SqlCommandBuilder(da);
            bs = new BindingSource();
            bs.DataSource = dt;


        }

        #region get user
        string userID = "";
        string username = "";
        #endregion
        
        public frmMain(string uid, string uname)
        {
            InitializeComponent();
            userID = uid;
            username = uname;
        }


        public frmMain()
        {
            InitializeComponent();

        }



        private void btn_PAdd_Click(object sender, EventArgs e)
        {
            ai = new AutoID();
            string id = ai.MakeID("Product", "Pro_ID", "P", 6);
            txt_PID.Text = id;

            ConnectionString cs = new ConnectionString();
            string con_string = cs.GetCS;

            string cmd_string1 = "";
            //cmd_string1 = string.Format(@"insert Product values ('{0}', '{1}', N'{2}', '{3}','{4},'{5}',N'{6}')", txt_PID.Text, cbb_cat.SelectedValue.ToString(), txt_PName.Text, txt_PQuantity.Text, txt_Cost.Text, txt_Price.Text, txt_Pnote.Text);
            cmd_string1 = "insert Product values ('" + txt_PID.Text + "', '" + cbb_cat.SelectedValue.ToString() + "','" + txt_PName.Text + "', '" + txt_PQuantity.Text + "',  '" + txt_Pnote.Text + "', '" + txt_Price.Text + "','" + txt_Cost.Text + "','" + dp_date.Value.ToString("yyyy/MM/dd") + "')";
            //cmd_string1 = "insert Product values ('1','1','1','1','1','1','1')";
            //SqlTransaction tran = null;
            //SqlConnection con = null;
            
            //try
            //{

            //    con.Open();
            //    tran = con.BeginTransaction();

            //    SqlCommand cmd = new SqlCommand(cmd_string1, con, tran);
            //    int res = cmd.ExecuteNonQuery();
            //    if (res > 0)
            //    {
            //        MessageBox.Show("Added");
            //    }
            //    else
            //    {
            //        tran.Rollback();
            //        con.Close();
            //    }
            //    tran.Commit();
            //    con.Close();
            //}
            //catch
            //{
            //    tran.Rollback();
            //    con.Close();
            //}

            using (SqlConnection con = new SqlConnection(con_string))
            {
                con.Open();

                SqlCommand command = con.CreateCommand();
                SqlTransaction tran;

                // Start a local transaction.
                tran = con.BeginTransaction();
                command.Connection = con;
                command.Transaction = tran;


                try
                {

                    //SqlCommand cmd = new SqlCommand(cmd_string1, con, tran);
                    command.CommandText = cmd_string1;
                    int res = command.ExecuteNonQuery();
                    if (res > 0)
                    {
                        MessageBox.Show("Added");
                    }
                    else
                    {
                        tran.Rollback();
                        con.Close();
                    }
                    tran.Commit();
                    con.Close();
                }
                catch
                {
                    tran.Rollback();
                    con.Close();
                }

            }
            ai = new AutoID();
            fillgvproduct();
        }



        private void frmMain_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'mYBDataSet2.Product' table. You can move, or remove it, as needed.
            this.productTableAdapter1.Fill(this.mYBDataSet2.Product);
            // TODO: This line of code loads data into the 'mYBDataSet1.Product' table. You can move, or remove it, as needed.
            this.productTableAdapter.Fill(this.mYBDataSet1.Product);
            ai = new AutoID();
            txt_PID.Text = ai.MakeID("Product", "Pro_ID", "P", 6);
            txt_billid.Text = ai.MakeID("Orders", "Ord_ID", "O", 6);
            txt_CID.Text = ai.MakeID("Customer", "Cus_ID", "C", 6);
           

            // TODO: This line of code loads data into the 'mYBDataSet.Category' table. You can move, or remove it, as needed.
            ConnectionString cs = new ConnectionString();
            string con_string = cs.GetCS;
            SqlConnection con = new SqlConnection(con_string);
            con.Open();
            SqlCommand cmd = new SqlCommand("select Cat_ID, Cat_Name from Category", con);
            SqlDataReader dr;
            DataTable dt = new DataTable();

            dr = cmd.ExecuteReader();

            dt.Columns.Add("Cat_ID", typeof(string));
            dt.Columns.Add("Cat_Name", typeof(string));
            dt.Load(dr);
            cbb_cat.ValueMember = "Cat_ID";
            cbb_cat.DisplayMember = "Cat_Name";
            cbb_cat.DataSource = dt;
            cbb_catselected.ValueMember = "Cat_ID";
            cbb_catselected.DisplayMember = "Cat_Name";
            cbb_catselected.DataSource = dt;

            con.Close();
            fillgvproduct();
            fillgvcustomer();
            fillgvproductbycatID();
            txt_BEID.Text = username;
            
        }

        private void fillgvcustomer()
        {
            ConnectionString cs = new ConnectionString();
            string con_string = cs.GetCS;
            SqlConnection con = new SqlConnection(con_string);
            con.Open();
            SqlCommand gvcmd = new SqlCommand("select * from Customer", con);
            SqlDataAdapter gvda = new SqlDataAdapter(gvcmd);
            DataSet gvds = new DataSet();
            gvda.Fill(gvds, "Cus_ID");
            gv_customer.DataSource = gvds;
            gv_customer.DataMember = "Cus_ID";
            con.Close();
        }
        private void fillgvproduct()
        {
            ConnectionString cs = new ConnectionString();
            string con_string = cs.GetCS;
            SqlConnection con = new SqlConnection(con_string);
            con.Open();
            SqlCommand gvcmd = new SqlCommand("select * from Product", con);
            SqlDataAdapter gvda = new SqlDataAdapter(gvcmd);
            DataSet gvds = new DataSet();
            gvda.Fill(gvds, "PID");
            gv_Product.DataSource = gvds;
            gv_Product.DataMember = "PID";
            con.Close();
        } 

        private void fillgvproductbycatID()
        {
            string catId = cbb_catselected.SelectedValue.ToString();
            ConnectionString cs = new ConnectionString();
            string con_string = cs.GetCS;
            SqlConnection con = new SqlConnection(con_string);
            con.Open();
            string string_cmd = string.Format("Select ProductName, QOH, Price, Description, Pro_ID from Product where Cat_ID like '%{0}%'", catId);
            SqlCommand cmd = new SqlCommand(string_cmd, con);
            SqlDataAdapter gvda = new SqlDataAdapter(cmd);           
            DataSet gvds = new DataSet();
            gvda.Fill(gvds, "PID");
            gv_search.DataSource = gvds;
            gv_search.DataMember = "PID";
            con.Close();
        }
        private void fillgvbillbyCusID()
        {
            string cusID = txt_BCusID.Text;
            ConnectionString cs = new ConnectionString();
            string con_string = cs.GetCS;
            SqlConnection con = new SqlConnection(con_string);
            con.Open();
            string string_cmd = string.Format("Select Ord_Date, Ord_ID, E_ID, Total from Orders where Cus_ID like '%{0}%'", cusID);
            SqlCommand cmd = new SqlCommand(string_cmd, con);
            SqlDataAdapter gvda = new SqlDataAdapter(cmd);
            DataSet gvds = new DataSet();
            gvda.Fill(gvds, "Cus_ID");
            gv_billbycusid.DataSource = gvds;
            gv_billbycusid.DataMember = "Cus_ID";
            con.Close();
        }


        private void btn_search_Click(object sender, EventArgs e)
        {
            string stxt = txt_search.Text;
            ConnectionString cs = new ConnectionString();
            string con_string = cs.GetCS;
            SqlConnection con = new SqlConnection(con_string);
            con.Open();
            //string cmd_string = string.Format("select ProductName, QOH, Price, Description, Pro_ID from Product where Pro_ID like '%{0}%'", stxt);
            string cmd_string = string.Format("select ProductName, QOH, Price, Description, Pro_ID from Product where Pro_ID like '%{0}%'", stxt);
            SqlCommand cmd = new SqlCommand(cmd_string, con);
            SqlDataReader dr = cmd.ExecuteReader();
            if (!dr.HasRows)
            {
                gv_search.DataSource = "";
                MessageBox.Show("Not found");
            }
            else
            {
                bs = new BindingSource();
                bs.DataSource = dr;
                gv_search.DataSource = bs;

            }
            con.Close();
        }

        private void gv_search_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            
            txt_OPName.Text = gv_search.Rows[e.RowIndex].Cells[0].Value.ToString();
            txt_OPrice.Text = gv_search.Rows[e.RowIndex].Cells[2].Value.ToString();
            txt_OProID.Text = gv_search.Rows[e.RowIndex].Cells[4].Value.ToString();
     
        }


        private void btn_AddO_Click(object sender, EventArgs e)
        {
            string col1 = txt_OPName.Text;
            string col2 = txt_OQuantity.Text;
            string col3 = txt_OPrice.Text;
            string col4 = Convert.ToString(Int32.Parse(txt_OPrice.Text) * Int32.Parse(txt_OQuantity.Text));
            //string col4 = "10";
            string col5 = txt_OProID.Text;
            string[] row = { col1, col2, col3, col4, col5 };
            gv_order.Rows.Add(row);
         
            int total = gv_order.Rows.Cast<DataGridViewRow>().Sum(x => Convert.ToInt32(x.Cells[3].Value));
            txt_total.Text = total.ToString();
            txt_total2.Text = total.ToString();
        }

        private void btn_Odel_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow item in this.gv_order.SelectedRows)
            {
                gv_order.Rows.RemoveAt(item.Index);
                int total = gv_order.Rows.Cast<DataGridViewRow>().Sum(x => Convert.ToInt32(x.Cells[3].Value));
                txt_total.Text = total.ToString();
                txt_total2.Text = total.ToString();
                //int total = 0;
                //int rc = gv_order.Rows.Count;
                //for (int i = 0; i < rc - 1; ++i)
                //{
                //    total += Convert.ToInt32(gv_order.CurrentRow.Cells[3].Value);

                //}
                //txt_total.Text = total.ToString();
            }
        }

        public DataTable GetDataTable()
        {
            return gv_order.Rows.Cast<DataRow>().CopyToDataTable();
        }


        private List<OrderDetails> lstOrderDetails = new List<OrderDetails>();
        private void btn_Pay_Click(object sender, EventArgs e)
        {
            txt_billid.Text = ai.MakeID("Orders", "Ord_ID", "O", 6);
            tab_control.SelectedTab = tab_bill;
           

            
            ConnectionString cs = new ConnectionString();
            string con_string = cs.GetCS;
            SqlConnection con = new SqlConnection(con_string);
            con.Open();


            for (int i = 0; i < gv_order.Rows.Count; i++)
            {
                //SqlCommand cmd = new SqlCommand("insert into OrderDetails(Pro_Name, Quantity,Price, SubTotal, Pro_ID, Ord_ID) values(@proName, @quantity, @price, @subTotal, @pro_ID, @ord_ID)", con);
                
                //cmd.Parameters.AddWithValue("@proName", gv_order.Rows[i].Cells[0].Value);
                //cmd.Parameters.AddWithValue("@quantity", gv_order.Rows[i].Cells[1].Value);
                //cmd.Parameters.AddWithValue("@price", gv_order.Rows[i].Cells[2].Value);
                //cmd.Parameters.AddWithValue("@subTotal", gv_order.Rows[i].Cells[3].Value);
                //cmd.Parameters.AddWithValue("@pro_Id", txt_OProID.Text);
                //cmd.Parameters.AddWithValue("@ord_ID", txt_billid.Text);
                //cmd.ExecuteNonQuery();

                OrderDetails odt = new OrderDetails();
                odt.proName = gv_order.Rows[i].Cells[0].Value.ToString();
                odt.quality = int.Parse(gv_order.Rows[i].Cells[1].Value.ToString());
                odt.price = float.Parse(gv_order.Rows[i].Cells[2].Value.ToString());
                odt.subTotal = float.Parse(gv_order.Rows[i].Cells[3].Value.ToString());
                odt.proID = gv_order.Rows[i].Cells[4].Value.ToString();
                lstOrderDetails.Add(odt);

                //SqlCommand updatecmd = new SqlCommand("update Product set QOH = '" + txt_CID.Text + "' where Pro_Name = '" + gv_order.Rows[i].Cells[0].ToString() +"'");

                //MessageBox.Show("Added successfully!");
            }
            con.Close();


        }



        private void btn_Pdel_Click(object sender, EventArgs e)
        {
            if (gv_Product.SelectedRows.Count > 0)
            {

                string rowId = gv_Product[gv_Product.CurrentCell.ColumnIndex, gv_Product.CurrentCell.RowIndex].Value.ToString();

                foreach (DataGridViewRow item in this.gv_Product.SelectedRows)
                {
                    ConnectionString cs = new ConnectionString();
                    string con_string = cs.GetCS;
                    string delpcmd = "delete from Product where Pro_ID = ('" + rowId.ToString() + "')";
                    //cmd_string1 = "insert Product values ('" + txt_PID.Text + "')";
                    gv_Product.Rows.RemoveAt(item.Index);
                    using (SqlConnection con = new SqlConnection(con_string))
                    {
                        con.Open();

                        SqlCommand command = con.CreateCommand();
                        SqlTransaction tran;

                        // Start a local transaction.
                        tran = con.BeginTransaction();
                        command.Connection = con;
                        command.Transaction = tran;


                        try
                        {

                            //SqlCommand cmd = new SqlCommand(cmd_string1, con, tran);
                            command.CommandText = delpcmd;
                            int res = command.ExecuteNonQuery();
                            //if (res > 0)
                            //{
                            //    MessageBox.Show("Added");
                            //}
                            //else
                            //{
                            //    tran.Rollback();
                            //    con.Close();
                            //}
                            tran.Commit();
                            con.Close();
                        }
                        catch
                        {
                            tran.Rollback();
                            con.Close();
                        }
                    }
                }
                fillgvproduct();
            }

        }
        private void toolStripStatusLabel1_Click(object sender, EventArgs e)
        {

        }

        private void btn_addCus_Click(object sender, EventArgs e)
        {
            ai = new AutoID();
            string id = ai.MakeID("Customer", "Cus_ID", "C", 6);
            txt_CID.Text = id;

            ConnectionString cs = new ConnectionString();
            string con_string = cs.GetCS;

            string cmd_string1 = "";
            cmd_string1 = "insert Customer values ('" + txt_CID.Text + "', '" + txt_CName.Text + "','" + txt_CPhone.Text + "', '" + txt_CAdd.Text + "', '" + txt_CNote.Text + "','" + dp_Cbday.Value.ToString("yyyy/MM/dd") + "','" + txt_NaID.Text + "')";

            using (SqlConnection con = new SqlConnection(con_string))
            {
                con.Open();

                SqlCommand command = con.CreateCommand();
                SqlTransaction tran;

                // Start a local transaction.
                tran = con.BeginTransaction();
                command.Connection = con;
                command.Transaction = tran;


                try
                {

                    command.CommandText = cmd_string1;
                    int res = command.ExecuteNonQuery();
                    if (res > 0)
                    {
                        MessageBox.Show("Added");
                    }
                    else
                    {
                        tran.Rollback();
                        con.Close();
                    }
                    tran.Commit();
                    con.Close();
                }
                catch
                {
                    tran.Rollback();
                    con.Close();
                }

            }
            ai = new AutoID();
            fillgvcustomer();
        }

        private void btn_delCus_Click(object sender, EventArgs e)
        {
            if (gv_customer.SelectedRows.Count > 0)
            {

                string rowId = gv_customer[gv_customer.CurrentCell.ColumnIndex, gv_customer.CurrentCell.RowIndex].Value.ToString();

                foreach (DataGridViewRow item in this.gv_customer.SelectedRows)
                {
                    ConnectionString cs = new ConnectionString();
                    string con_string = cs.GetCS;
                    string delccmd = "delete from Customer where Cus_ID = ('" + rowId.ToString() + "')";

                    gv_customer.Rows.RemoveAt(item.Index);
                    using (SqlConnection con = new SqlConnection(con_string))
                    {
                        con.Open();

                        SqlCommand command = con.CreateCommand();
                        SqlTransaction tran;

                        // Start a local transaction.
                        tran = con.BeginTransaction();
                        command.Connection = con;
                        command.Transaction = tran;


                        try
                        {                           
                            command.CommandText = delccmd;
                            int res = command.ExecuteNonQuery();

                            tran.Commit();
                            con.Close();
                        }
                        catch
                        {
                            tran.Rollback();
                            con.Close();
                        }
                    }
                }
                fillgvcustomer();
            }
        }

        private void btn_BloadCus_Click(object sender, EventArgs e)
        {
            fillgvbillbyCusID();
            string scid = txt_BCusID.Text;
            ConnectionString cs = new ConnectionString();
            string con_string = cs.GetCS;
            SqlConnection con = new SqlConnection(con_string);
            con.Open();
            string cmd_string = string.Format("select * from Customer where Cus_ID like '%{0}%'", scid);
            SqlCommand cmd = new SqlCommand(cmd_string, con);
            SqlDataReader dr = cmd.ExecuteReader();
            if (!dr.Read())
            {
                MessageBox.Show("Not found");
            }
            else
            {
                txt_CID.Text = string.Empty;
                txt_CID.Text = dr["Cus_ID"].ToString();
                txt_BCName.Text = dr["Cus_Name"].ToString();
                txt_BCPhone.Text = dr["Cus_Phone"].ToString();
                txt_BCAdd.Text = dr["Cus_Address"].ToString();
                txt_BCNote.Text = dr["Cus_Note"].ToString();
            }
            con.Close();
            int total = gv_billbycusid.Rows.Cast<DataGridViewRow>().Sum(x => Convert.ToInt32(x.Cells[3].Value));
            txt_custotal.Text = total.ToString();
        }

        private void cbb_catselected_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbb_catselected.SelectedIndex > -1)
            {
                string catId = cbb_catselected.SelectedValue.ToString();
                fillgvproductbycatID();             
            
                }
            }

        private void btn_confirm_Click(object sender, EventArgs e)
        {
            ConnectionString cs = new ConnectionString();
            string con_string = cs.GetCS;
            
            using (SqlConnection con = new SqlConnection(con_string))
            {
                con.Open();
                SqlCommand command = con.CreateCommand();
                SqlTransaction tran;

                // Start a local transaction.
                tran = con.BeginTransaction();
                command.Connection = con;
                command.Transaction = tran;


                try
                {

         
                SqlCommand cmd = new SqlCommand("insert into Orders(Ord_ID, Ord_Date, Total, Cus_ID, Phone, Address, E_ID, CName) values(@ordID, @ordDate, @total, @cusID, @phone, @address, @eID, @cName)", con);
                cmd.Parameters.AddWithValue("@ordID", txt_billid.Text);
                cmd.Parameters.AddWithValue("@ordDate", dp_bdate.Value.ToString("yyyy/MM/dd"));
                cmd.Parameters.AddWithValue("@total", txt_total2.Text);
                cmd.Parameters.AddWithValue("@cusID", txt_BCusID.Text);
                cmd.Parameters.AddWithValue("@phone", txt_BCPhone.Text);
                cmd.Parameters.AddWithValue("@address", txt_BCAdd.Text);
                cmd.Parameters.AddWithValue("@eID", "E001");
                cmd.Parameters.AddWithValue("@cName", txt_CName.Text);
     
                cmd.ExecuteNonQuery();
            
                for (int i = 0; i < lstOrderDetails.Count; i++) {
                    SqlCommand cmd_odt = new SqlCommand("insert into OrderDetails(Pro_Name, Quantity,Price, SubTotal, Pro_ID, Ord_ID) values(@proName, @quantity, @price, @subTotal, @pro_ID, @ord_ID)", con);

                    cmd_odt.Parameters.AddWithValue("@proName", lstOrderDetails[i].proName);
                    cmd_odt.Parameters.AddWithValue("@quantity", lstOrderDetails[i].quality);
                    cmd_odt.Parameters.AddWithValue("@price", lstOrderDetails[i].price);
                    cmd_odt.Parameters.AddWithValue("@subTotal", lstOrderDetails[i].subTotal);
                    cmd_odt.Parameters.AddWithValue("@pro_Id", lstOrderDetails[i].proID);
                    cmd_odt.Parameters.AddWithValue("@ord_ID", txt_billid.Text);
                    cmd_odt.ExecuteNonQuery();
                }


                    //SqlCommand updatecmd = new SqlCommand("update Product set QOH = '" + txt_CID.Text + "' where Pro_Name = '" + gv_order.Rows[i].Cells[0].ToString() +"'");

                                     
                    int res = command.ExecuteNonQuery();
                    if (res > 0)
                    {
                        MessageBox.Show("Added successfully!");  
                    }
                    else
                    {
                        tran.Rollback();
                        con.Close();
                    }
                    tran.Commit();
                    con.Close();
                }
                catch
                {
                    tran.Rollback();
                    con.Close();
                }

            }
        }

        private void gv_order_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void tab_Order_Click(object sender, EventArgs e)
        {

        }
    }

}

