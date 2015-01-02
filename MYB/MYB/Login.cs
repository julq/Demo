using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace MYB
{
    public partial class frm_login : Form
    {
        public frm_login()
        {
            InitializeComponent();
        }

        private void btn_login_Click(object sender, EventArgs e)
        {
            try
            {
                ConnectionString cs = new ConnectionString();
                string con_string = cs.GetCS;
                SqlConnection con = new SqlConnection(con_string);
                con.Open();

                if (txt_user.Text == "")
                {
                    MessageBox.Show("User name must be filled");
                }
                if (txt_pwd.Text == "")
                {
                    MessageBox.Show("Password must be filled");
                }

                SqlCommand cmd_uid = new SqlCommand(string.Format("select UserName from Employee"), con);
                SqlDataReader dr_uid = cmd_uid.ExecuteReader();

                int check = 0;
                while (dr_uid.Read())
                {
                    if (txt_user.Text == dr_uid.GetValue(0).ToString())
                    {
                        check = 1;
                        break;
                    }
                    else

                        check = 0;
                }
                if (check == 0)
                {
                    MessageBox.Show("Your ID is not available");
                    dr_uid.Close();//=====
                    con.Close();//=====
                    return;
                }
                dr_uid.Close();
                
                string cmd_string = "";
                //cmd_string = string.Format("select Password, Role, E_ID from Employee where UserName = '{0}'", txt_user.Text);
                cmd_string = @"select Password, Role, E_ID from Employee where UserName = '" + txt_user.Text + "'";
                
                SqlCommand cmd = new SqlCommand(cmd_string, con);
                //cmd.Parameters.AddWithValue("@username", txt_user.Text);
                SqlDataReader dr = cmd.ExecuteReader();
                dr.Read();

                if (txt_pwd.Text == dr.GetValue(0).ToString())
                {
                    this.Hide();
                    if (dr.GetValue(1).ToString() == "0")
                    {
                        MessageBox.Show("Welcome Administrator");
                        frm_AdminPanel ap = new frm_AdminPanel();
                        ap.ShowDialog();
                        this.Show();
                        txt_pwd.Clear();
                        txt_user.Clear();
                        txt_user.Focus();
                    }
                    if (dr.GetValue(1).ToString() == "1")
                    {
                        //dr.Close();
                        //con.Close();
                        MessageBox.Show("Welcome User");
                        frmMain frm = new frmMain(dr.GetValue(2).ToString(), txt_user.Text);
                        frm.ShowDialog();
                        txt_pwd.Clear();
                        txt_user.Clear();
                        txt_user.Focus();
                    }
                    if (dr.GetValue(1).ToString() == "2")
                    {
                        MessageBox.Show("YOUR ACCOUNT HAS BEEN LOCKED");
                        this.ShowDialog();
                    }
                }
                else
                {
                    MessageBox.Show("Login Fail. Wrong Password");
                }
                
                dr.Close();
                con.Close();
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }



        private void btn_exit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
