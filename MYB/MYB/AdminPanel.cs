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
    public partial class frm_AdminPanel : Form
    {
        AutoID ai = null;
        private TabControl tab;
        private TabPage tab_newemployee;
        private TextBox txt_role;
        private TextBox txt_note;
        private Label lb_note;
        private Button btn_Logout;
        private Button btn_Reset;
        private Button btn_Create;
        private RadioButton rb_female;
        private RadioButton rb_male;
        private DateTimePicker dp_DOB;
        private TextBox txt_naid;
        private TextBox txt_phone;
        private TextBox txt_password;
        private TextBox txt_address;
        private TextBox txt_username;
        private TextBox txt_name;
        private Label lb_NaID;
        private Label lb_Phone;
        private Label lb_Role;
        private Label lb_Address;
        private Label lb_Password;
        private Label lb_DOB;
        private Label lb_Username;
        private Label lb_Name;
        private Label lb_E_ID;
        private TabPage tab_editemployee;
        private ComboBox cbx_Role_edit;
        private Button btn_save;
        private Button btn_load;
        private RadioButton rb_female_edit;
        private RadioButton rb_male_edit;
        private DateTimePicker dp_DOB_edit;
        private TextBox txt_idcardnumber_edit;
        private TextBox txt_phone_edit;
        private TextBox txt_password_edit;
        private TextBox txt_address_edit;
        private TextBox txt_speciality_edit;
        private TextBox txt_username_edit;
        private TextBox txt_name_edit;
        private TextBox txt_EID_edit;
        private Label lb_IDCardNumber_edit;
        private Label lb_Phone_edit;
        private Label lb_Role_edit;
        private Label lb_Address_edit;
        private Label lb_Password_edit;
        private Label lb_DOB_edit;
        private Label lb_Username_edit;
        private Label lb_Spec_edit;
        private Label lb_Name_edit;
        private Label lb_E_ID_edit;
        private TabPage tab_manageemployee;
        private Button btn_search_manage;
        private TextBox txt_search_manage;
        private DataGridView gv_emp_manage;
        private GroupBox gb_operation;
        private Label lb_E_ID_manage;
        private TextBox txt_E_ID_manage;
        private Button btn_delete;
        private Button btn_lock;
        private GroupBox gb_search;
        private RadioButton rbtn_search_E_Name;
        private RadioButton rbtn_search_E_ID;
        private TextBox txt_EID;
        BindingSource bs = null;

        public frm_AdminPanel()
        {
            InitializeComponent();
        }

        private void frm_AdminPanel_Load(object sender, EventArgs e)
        {
            ai = new AutoID();
            txt_EID.Text = ai.MakeID("Employee", "E_ID", "E", 3);          
        }

        private void tab_acc_Click(object sender, EventArgs e)
        {

        }

        private void btn_Create_Click(object sender, EventArgs e)
        {
            string msg = "";

            ConnectionString cs = new ConnectionString();
            string con_string = cs.GetCS;

            SqlConnection con = new SqlConnection(con_string);
            con.Open();

            SqlCommand cmd_uid = new SqlCommand("select UserName from Employee", con);
            SqlDataReader dr_uid = cmd_uid.ExecuteReader();
            dr_uid.Read();

            if (txt_username.Text == dr_uid.GetValue(0).ToString())
            {
                msg += "Duplicated";
            }
            dr_uid.Close();
            con.Close();

            if (msg != "")
            {
                MessageBox.Show(msg);
                return;
            }

            try
            {
                string gender = "";
                if (rb_female.Checked)
                {
                    gender = "Female";
                }
                else
                {
                    gender = "Male";
                }

                ai = new AutoID();
                string id = ai.MakeID("Employee", "E_ID", "E", 3);
                txt_EID.Text = id;

                string cmd_string = string.Format("insert Employee Values('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}')", txt_EID.Text, txt_role.Text, txt_name.Text, txt_username.Text, txt_password.Text, dp_DOB.Value.ToString("yyyy/MM/dd"), txt_phone.Text, gender, txt_address.Text, txt_note.Text, txt_naid.Text);

                SqlTransaction tran = null;

                try
                {
                    con.Open();
                    tran.Connection.BeginTransaction();

                    SqlCommand cmd = new SqlCommand(cmd_string, con, tran);
                    int res = cmd.ExecuteNonQuery();
                    if (res > 0)
                    {
                        MessageBox.Show("Done");
                    }
                    else
                    {
                        MessageBox.Show("Fail");
                        tran.Rollback();
                    }
                    tran.Commit();
                    con.Close();
                    ai = new AutoID();
                    id = ai.MakeID("Employee", "E_ID", "E", 3);
                    txt_EID.Text = id;

                    txt_address.Clear();
                    txt_naid.Clear();
                    txt_name.Clear();
                    txt_note.Clear();
                    txt_password.Clear();
                    txt_phone.Clear();
                    txt_username.Clear();
                }
                catch
                {
                    tran.Rollback();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btn_Reset_Click(object sender, EventArgs e)
        {
            //txt_address.Clear();
            //txt_naid.Clear();
            //txt_name.Clear();
            //txt_password.Clear();
            //txt_phone.Clear();
            //txt_note.Clear();
            //txt_username.Clear();
        }

        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.tab = new System.Windows.Forms.TabControl();
            this.tab_newemployee = new System.Windows.Forms.TabPage();
            this.txt_EID = new System.Windows.Forms.TextBox();
            this.txt_role = new System.Windows.Forms.TextBox();
            this.txt_note = new System.Windows.Forms.TextBox();
            this.lb_note = new System.Windows.Forms.Label();
            this.btn_Logout = new System.Windows.Forms.Button();
            this.btn_Reset = new System.Windows.Forms.Button();
            this.btn_Create = new System.Windows.Forms.Button();
            this.rb_female = new System.Windows.Forms.RadioButton();
            this.rb_male = new System.Windows.Forms.RadioButton();
            this.dp_DOB = new System.Windows.Forms.DateTimePicker();
            this.txt_naid = new System.Windows.Forms.TextBox();
            this.txt_phone = new System.Windows.Forms.TextBox();
            this.txt_password = new System.Windows.Forms.TextBox();
            this.txt_address = new System.Windows.Forms.TextBox();
            this.txt_username = new System.Windows.Forms.TextBox();
            this.txt_name = new System.Windows.Forms.TextBox();
            this.lb_NaID = new System.Windows.Forms.Label();
            this.lb_Phone = new System.Windows.Forms.Label();
            this.lb_Role = new System.Windows.Forms.Label();
            this.lb_Address = new System.Windows.Forms.Label();
            this.lb_Password = new System.Windows.Forms.Label();
            this.lb_DOB = new System.Windows.Forms.Label();
            this.lb_Username = new System.Windows.Forms.Label();
            this.lb_Name = new System.Windows.Forms.Label();
            this.lb_E_ID = new System.Windows.Forms.Label();
            this.tab_editemployee = new System.Windows.Forms.TabPage();
            this.cbx_Role_edit = new System.Windows.Forms.ComboBox();
            this.btn_save = new System.Windows.Forms.Button();
            this.btn_load = new System.Windows.Forms.Button();
            this.rb_female_edit = new System.Windows.Forms.RadioButton();
            this.rb_male_edit = new System.Windows.Forms.RadioButton();
            this.dp_DOB_edit = new System.Windows.Forms.DateTimePicker();
            this.txt_idcardnumber_edit = new System.Windows.Forms.TextBox();
            this.txt_phone_edit = new System.Windows.Forms.TextBox();
            this.txt_password_edit = new System.Windows.Forms.TextBox();
            this.txt_address_edit = new System.Windows.Forms.TextBox();
            this.txt_speciality_edit = new System.Windows.Forms.TextBox();
            this.txt_username_edit = new System.Windows.Forms.TextBox();
            this.txt_name_edit = new System.Windows.Forms.TextBox();
            this.txt_EID_edit = new System.Windows.Forms.TextBox();
            this.lb_IDCardNumber_edit = new System.Windows.Forms.Label();
            this.lb_Phone_edit = new System.Windows.Forms.Label();
            this.lb_Role_edit = new System.Windows.Forms.Label();
            this.lb_Address_edit = new System.Windows.Forms.Label();
            this.lb_Password_edit = new System.Windows.Forms.Label();
            this.lb_DOB_edit = new System.Windows.Forms.Label();
            this.lb_Username_edit = new System.Windows.Forms.Label();
            this.lb_Spec_edit = new System.Windows.Forms.Label();
            this.lb_Name_edit = new System.Windows.Forms.Label();
            this.lb_E_ID_edit = new System.Windows.Forms.Label();
            this.tab_manageemployee = new System.Windows.Forms.TabPage();
            this.btn_search_manage = new System.Windows.Forms.Button();
            this.txt_search_manage = new System.Windows.Forms.TextBox();
            this.gv_emp_manage = new System.Windows.Forms.DataGridView();
            this.gb_operation = new System.Windows.Forms.GroupBox();
            this.lb_E_ID_manage = new System.Windows.Forms.Label();
            this.txt_E_ID_manage = new System.Windows.Forms.TextBox();
            this.btn_delete = new System.Windows.Forms.Button();
            this.btn_lock = new System.Windows.Forms.Button();
            this.gb_search = new System.Windows.Forms.GroupBox();
            this.rbtn_search_E_Name = new System.Windows.Forms.RadioButton();
            this.rbtn_search_E_ID = new System.Windows.Forms.RadioButton();
            this.tab.SuspendLayout();
            this.tab_newemployee.SuspendLayout();
            this.tab_editemployee.SuspendLayout();
            this.tab_manageemployee.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gv_emp_manage)).BeginInit();
            this.gb_operation.SuspendLayout();
            this.gb_search.SuspendLayout();
            this.SuspendLayout();
            // 
            // tab
            // 
            this.tab.Controls.Add(this.tab_newemployee);
            this.tab.Controls.Add(this.tab_editemployee);
            this.tab.Controls.Add(this.tab_manageemployee);
            this.tab.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tab.Location = new System.Drawing.Point(0, 0);
            this.tab.Name = "tab";
            this.tab.SelectedIndex = 0;
            this.tab.Size = new System.Drawing.Size(677, 368);
            this.tab.TabIndex = 2;
            // 
            // tab_newemployee
            // 
            this.tab_newemployee.Controls.Add(this.txt_EID);
            this.tab_newemployee.Controls.Add(this.txt_role);
            this.tab_newemployee.Controls.Add(this.txt_note);
            this.tab_newemployee.Controls.Add(this.lb_note);
            this.tab_newemployee.Controls.Add(this.btn_Logout);
            this.tab_newemployee.Controls.Add(this.btn_Reset);
            this.tab_newemployee.Controls.Add(this.btn_Create);
            this.tab_newemployee.Controls.Add(this.rb_female);
            this.tab_newemployee.Controls.Add(this.rb_male);
            this.tab_newemployee.Controls.Add(this.dp_DOB);
            this.tab_newemployee.Controls.Add(this.txt_naid);
            this.tab_newemployee.Controls.Add(this.txt_phone);
            this.tab_newemployee.Controls.Add(this.txt_password);
            this.tab_newemployee.Controls.Add(this.txt_address);
            this.tab_newemployee.Controls.Add(this.txt_username);
            this.tab_newemployee.Controls.Add(this.txt_name);
            this.tab_newemployee.Controls.Add(this.lb_NaID);
            this.tab_newemployee.Controls.Add(this.lb_Phone);
            this.tab_newemployee.Controls.Add(this.lb_Role);
            this.tab_newemployee.Controls.Add(this.lb_Address);
            this.tab_newemployee.Controls.Add(this.lb_Password);
            this.tab_newemployee.Controls.Add(this.lb_DOB);
            this.tab_newemployee.Controls.Add(this.lb_Username);
            this.tab_newemployee.Controls.Add(this.lb_Name);
            this.tab_newemployee.Controls.Add(this.lb_E_ID);
            this.tab_newemployee.Location = new System.Drawing.Point(4, 22);
            this.tab_newemployee.Name = "tab_newemployee";
            this.tab_newemployee.Padding = new System.Windows.Forms.Padding(3);
            this.tab_newemployee.Size = new System.Drawing.Size(669, 342);
            this.tab_newemployee.TabIndex = 0;
            this.tab_newemployee.Text = "New Employee";
            this.tab_newemployee.UseVisualStyleBackColor = true;
            // 
            // txt_EID
            // 
            this.txt_EID.Enabled = false;
            this.txt_EID.Location = new System.Drawing.Point(165, 15);
            this.txt_EID.Name = "txt_EID";
            this.txt_EID.Size = new System.Drawing.Size(68, 20);
            this.txt_EID.TabIndex = 17;
            this.txt_EID.TextChanged += new System.EventHandler(this.txt_EID_TextChanged);
            // 
            // txt_role
            // 
            this.txt_role.Location = new System.Drawing.Point(165, 165);
            this.txt_role.Name = "txt_role";
            this.txt_role.Size = new System.Drawing.Size(146, 20);
            this.txt_role.TabIndex = 7;
            // 
            // txt_note
            // 
            this.txt_note.Location = new System.Drawing.Point(165, 195);
            this.txt_note.Name = "txt_note";
            this.txt_note.Size = new System.Drawing.Size(146, 20);
            this.txt_note.TabIndex = 9;
            // 
            // lb_note
            // 
            this.lb_note.AutoSize = true;
            this.lb_note.Location = new System.Drawing.Point(58, 198);
            this.lb_note.Name = "lb_note";
            this.lb_note.Size = new System.Drawing.Size(30, 13);
            this.lb_note.TabIndex = 14;
            this.lb_note.Text = "Note";
            // 
            // btn_Logout
            // 
            this.btn_Logout.Location = new System.Drawing.Point(444, 248);
            this.btn_Logout.Name = "btn_Logout";
            this.btn_Logout.Size = new System.Drawing.Size(84, 40);
            this.btn_Logout.TabIndex = 13;
            this.btn_Logout.Text = "Log out";
            this.btn_Logout.UseVisualStyleBackColor = true;
            this.btn_Logout.Click += new System.EventHandler(this.btn_Logout_Click);
            // 
            // btn_Reset
            // 
            this.btn_Reset.Location = new System.Drawing.Point(314, 248);
            this.btn_Reset.Name = "btn_Reset";
            this.btn_Reset.Size = new System.Drawing.Size(83, 40);
            this.btn_Reset.TabIndex = 12;
            this.btn_Reset.Text = "Reset";
            this.btn_Reset.UseVisualStyleBackColor = true;
            this.btn_Reset.Click += new System.EventHandler(this.btn_Reset_Click_1);
            // 
            // btn_Create
            // 
            this.btn_Create.Location = new System.Drawing.Point(184, 248);
            this.btn_Create.Name = "btn_Create";
            this.btn_Create.Size = new System.Drawing.Size(90, 40);
            this.btn_Create.TabIndex = 11;
            this.btn_Create.Text = "Create";
            this.btn_Create.UseVisualStyleBackColor = true;
            this.btn_Create.Click += new System.EventHandler(this.btn_Create_Click_1);
            // 
            // rb_female
            // 
            this.rb_female.AutoSize = true;
            this.rb_female.Location = new System.Drawing.Point(233, 72);
            this.rb_female.Name = "rb_female";
            this.rb_female.Size = new System.Drawing.Size(59, 17);
            this.rb_female.TabIndex = 2;
            this.rb_female.Text = "Female";
            this.rb_female.UseVisualStyleBackColor = true;
            // 
            // rb_male
            // 
            this.rb_male.AutoSize = true;
            this.rb_male.Checked = true;
            this.rb_male.Location = new System.Drawing.Point(165, 72);
            this.rb_male.Name = "rb_male";
            this.rb_male.Size = new System.Drawing.Size(48, 17);
            this.rb_male.TabIndex = 1;
            this.rb_male.TabStop = true;
            this.rb_male.Text = "Male";
            this.rb_male.UseVisualStyleBackColor = true;
            // 
            // dp_DOB
            // 
            this.dp_DOB.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dp_DOB.Location = new System.Drawing.Point(444, 105);
            this.dp_DOB.Name = "dp_DOB";
            this.dp_DOB.Size = new System.Drawing.Size(146, 20);
            this.dp_DOB.TabIndex = 5;
            // 
            // txt_naid
            // 
            this.txt_naid.Location = new System.Drawing.Point(444, 195);
            this.txt_naid.Name = "txt_naid";
            this.txt_naid.Size = new System.Drawing.Size(146, 20);
            this.txt_naid.TabIndex = 10;
            // 
            // txt_phone
            // 
            this.txt_phone.Location = new System.Drawing.Point(444, 165);
            this.txt_phone.Name = "txt_phone";
            this.txt_phone.Size = new System.Drawing.Size(146, 20);
            this.txt_phone.TabIndex = 8;
            // 
            // txt_password
            // 
            this.txt_password.Location = new System.Drawing.Point(165, 134);
            this.txt_password.Name = "txt_password";
            this.txt_password.PasswordChar = '*';
            this.txt_password.Size = new System.Drawing.Size(146, 20);
            this.txt_password.TabIndex = 4;
            // 
            // txt_address
            // 
            this.txt_address.Location = new System.Drawing.Point(444, 135);
            this.txt_address.Name = "txt_address";
            this.txt_address.Size = new System.Drawing.Size(146, 20);
            this.txt_address.TabIndex = 6;
            // 
            // txt_username
            // 
            this.txt_username.Location = new System.Drawing.Point(165, 104);
            this.txt_username.Name = "txt_username";
            this.txt_username.Size = new System.Drawing.Size(146, 20);
            this.txt_username.TabIndex = 3;
            // 
            // txt_name
            // 
            this.txt_name.Location = new System.Drawing.Point(165, 45);
            this.txt_name.Name = "txt_name";
            this.txt_name.Size = new System.Drawing.Size(146, 20);
            this.txt_name.TabIndex = 0;
            // 
            // lb_NaID
            // 
            this.lb_NaID.AutoSize = true;
            this.lb_NaID.Location = new System.Drawing.Point(346, 198);
            this.lb_NaID.Name = "lb_NaID";
            this.lb_NaID.Size = new System.Drawing.Size(83, 13);
            this.lb_NaID.TabIndex = 0;
            this.lb_NaID.Text = "ID Card Number";
            // 
            // lb_Phone
            // 
            this.lb_Phone.AutoSize = true;
            this.lb_Phone.Location = new System.Drawing.Point(346, 168);
            this.lb_Phone.Name = "lb_Phone";
            this.lb_Phone.Size = new System.Drawing.Size(38, 13);
            this.lb_Phone.TabIndex = 0;
            this.lb_Phone.Text = "Phone";
            // 
            // lb_Role
            // 
            this.lb_Role.AutoSize = true;
            this.lb_Role.Location = new System.Drawing.Point(58, 168);
            this.lb_Role.Name = "lb_Role";
            this.lb_Role.Size = new System.Drawing.Size(29, 13);
            this.lb_Role.TabIndex = 0;
            this.lb_Role.Text = "Role";
            // 
            // lb_Address
            // 
            this.lb_Address.AutoSize = true;
            this.lb_Address.Location = new System.Drawing.Point(346, 138);
            this.lb_Address.Name = "lb_Address";
            this.lb_Address.Size = new System.Drawing.Size(45, 13);
            this.lb_Address.TabIndex = 0;
            this.lb_Address.Text = "Address";
            // 
            // lb_Password
            // 
            this.lb_Password.AutoSize = true;
            this.lb_Password.Location = new System.Drawing.Point(58, 137);
            this.lb_Password.Name = "lb_Password";
            this.lb_Password.Size = new System.Drawing.Size(53, 13);
            this.lb_Password.TabIndex = 0;
            this.lb_Password.Text = "Password";
            // 
            // lb_DOB
            // 
            this.lb_DOB.AutoSize = true;
            this.lb_DOB.Location = new System.Drawing.Point(346, 110);
            this.lb_DOB.Name = "lb_DOB";
            this.lb_DOB.Size = new System.Drawing.Size(30, 13);
            this.lb_DOB.TabIndex = 0;
            this.lb_DOB.Text = "DOB";
            // 
            // lb_Username
            // 
            this.lb_Username.AutoSize = true;
            this.lb_Username.Location = new System.Drawing.Point(58, 107);
            this.lb_Username.Name = "lb_Username";
            this.lb_Username.Size = new System.Drawing.Size(55, 13);
            this.lb_Username.TabIndex = 0;
            this.lb_Username.Text = "Username";
            // 
            // lb_Name
            // 
            this.lb_Name.AutoSize = true;
            this.lb_Name.Location = new System.Drawing.Point(58, 48);
            this.lb_Name.Name = "lb_Name";
            this.lb_Name.Size = new System.Drawing.Size(35, 13);
            this.lb_Name.TabIndex = 0;
            this.lb_Name.Text = "Name";
            // 
            // lb_E_ID
            // 
            this.lb_E_ID.AutoSize = true;
            this.lb_E_ID.Location = new System.Drawing.Point(58, 18);
            this.lb_E_ID.Name = "lb_E_ID";
            this.lb_E_ID.Size = new System.Drawing.Size(28, 13);
            this.lb_E_ID.TabIndex = 0;
            this.lb_E_ID.Text = "E-ID";
            // 
            // tab_editemployee
            // 
            this.tab_editemployee.Controls.Add(this.cbx_Role_edit);
            this.tab_editemployee.Controls.Add(this.btn_save);
            this.tab_editemployee.Controls.Add(this.btn_load);
            this.tab_editemployee.Controls.Add(this.rb_female_edit);
            this.tab_editemployee.Controls.Add(this.rb_male_edit);
            this.tab_editemployee.Controls.Add(this.dp_DOB_edit);
            this.tab_editemployee.Controls.Add(this.txt_idcardnumber_edit);
            this.tab_editemployee.Controls.Add(this.txt_phone_edit);
            this.tab_editemployee.Controls.Add(this.txt_password_edit);
            this.tab_editemployee.Controls.Add(this.txt_address_edit);
            this.tab_editemployee.Controls.Add(this.txt_speciality_edit);
            this.tab_editemployee.Controls.Add(this.txt_username_edit);
            this.tab_editemployee.Controls.Add(this.txt_name_edit);
            this.tab_editemployee.Controls.Add(this.txt_EID_edit);
            this.tab_editemployee.Controls.Add(this.lb_IDCardNumber_edit);
            this.tab_editemployee.Controls.Add(this.lb_Phone_edit);
            this.tab_editemployee.Controls.Add(this.lb_Role_edit);
            this.tab_editemployee.Controls.Add(this.lb_Address_edit);
            this.tab_editemployee.Controls.Add(this.lb_Password_edit);
            this.tab_editemployee.Controls.Add(this.lb_DOB_edit);
            this.tab_editemployee.Controls.Add(this.lb_Username_edit);
            this.tab_editemployee.Controls.Add(this.lb_Spec_edit);
            this.tab_editemployee.Controls.Add(this.lb_Name_edit);
            this.tab_editemployee.Controls.Add(this.lb_E_ID_edit);
            this.tab_editemployee.Location = new System.Drawing.Point(4, 22);
            this.tab_editemployee.Name = "tab_editemployee";
            this.tab_editemployee.Padding = new System.Windows.Forms.Padding(3);
            this.tab_editemployee.Size = new System.Drawing.Size(669, 342);
            this.tab_editemployee.TabIndex = 2;
            this.tab_editemployee.Text = "Edit Employee";
            this.tab_editemployee.UseVisualStyleBackColor = true;
            // 
            // cbx_Role_edit
            // 
            this.cbx_Role_edit.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbx_Role_edit.FormattingEnabled = true;
            this.cbx_Role_edit.Location = new System.Drawing.Point(167, 217);
            this.cbx_Role_edit.Name = "cbx_Role_edit";
            this.cbx_Role_edit.Size = new System.Drawing.Size(146, 21);
            this.cbx_Role_edit.TabIndex = 6;
            // 
            // btn_save
            // 
            this.btn_save.Enabled = false;
            this.btn_save.Location = new System.Drawing.Point(371, 270);
            this.btn_save.Name = "btn_save";
            this.btn_save.Size = new System.Drawing.Size(93, 41);
            this.btn_save.TabIndex = 11;
            this.btn_save.Text = "Save";
            this.btn_save.UseVisualStyleBackColor = true;
            // 
            // btn_load
            // 
            this.btn_load.Location = new System.Drawing.Point(222, 270);
            this.btn_load.Name = "btn_load";
            this.btn_load.Size = new System.Drawing.Size(91, 41);
            this.btn_load.TabIndex = 13;
            this.btn_load.Text = "Load";
            this.btn_load.UseVisualStyleBackColor = true;
            // 
            // rb_female_edit
            // 
            this.rb_female_edit.AutoSize = true;
            this.rb_female_edit.Location = new System.Drawing.Point(235, 94);
            this.rb_female_edit.Name = "rb_female_edit";
            this.rb_female_edit.Size = new System.Drawing.Size(59, 17);
            this.rb_female_edit.TabIndex = 2;
            this.rb_female_edit.Text = "Female";
            this.rb_female_edit.UseVisualStyleBackColor = true;
            // 
            // rb_male_edit
            // 
            this.rb_male_edit.AutoSize = true;
            this.rb_male_edit.Checked = true;
            this.rb_male_edit.Location = new System.Drawing.Point(167, 94);
            this.rb_male_edit.Name = "rb_male_edit";
            this.rb_male_edit.Size = new System.Drawing.Size(48, 17);
            this.rb_male_edit.TabIndex = 1;
            this.rb_male_edit.TabStop = true;
            this.rb_male_edit.Text = "Male";
            this.rb_male_edit.UseVisualStyleBackColor = true;
            // 
            // dp_DOB_edit
            // 
            this.dp_DOB_edit.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dp_DOB_edit.Location = new System.Drawing.Point(446, 127);
            this.dp_DOB_edit.Name = "dp_DOB_edit";
            this.dp_DOB_edit.Size = new System.Drawing.Size(146, 20);
            this.dp_DOB_edit.TabIndex = 7;
            // 
            // txt_idcardnumber_edit
            // 
            this.txt_idcardnumber_edit.Location = new System.Drawing.Point(446, 217);
            this.txt_idcardnumber_edit.Name = "txt_idcardnumber_edit";
            this.txt_idcardnumber_edit.Size = new System.Drawing.Size(146, 20);
            this.txt_idcardnumber_edit.TabIndex = 10;
            // 
            // txt_phone_edit
            // 
            this.txt_phone_edit.Location = new System.Drawing.Point(446, 187);
            this.txt_phone_edit.Name = "txt_phone_edit";
            this.txt_phone_edit.Size = new System.Drawing.Size(146, 20);
            this.txt_phone_edit.TabIndex = 9;
            // 
            // txt_password_edit
            // 
            this.txt_password_edit.Location = new System.Drawing.Point(167, 187);
            this.txt_password_edit.Name = "txt_password_edit";
            this.txt_password_edit.Size = new System.Drawing.Size(146, 20);
            this.txt_password_edit.TabIndex = 5;
            // 
            // txt_address_edit
            // 
            this.txt_address_edit.Location = new System.Drawing.Point(446, 157);
            this.txt_address_edit.Name = "txt_address_edit";
            this.txt_address_edit.Size = new System.Drawing.Size(146, 20);
            this.txt_address_edit.TabIndex = 8;
            // 
            // txt_speciality_edit
            // 
            this.txt_speciality_edit.Location = new System.Drawing.Point(167, 129);
            this.txt_speciality_edit.Name = "txt_speciality_edit";
            this.txt_speciality_edit.Size = new System.Drawing.Size(146, 20);
            this.txt_speciality_edit.TabIndex = 3;
            // 
            // txt_username_edit
            // 
            this.txt_username_edit.Location = new System.Drawing.Point(167, 157);
            this.txt_username_edit.Name = "txt_username_edit";
            this.txt_username_edit.Size = new System.Drawing.Size(146, 20);
            this.txt_username_edit.TabIndex = 4;
            // 
            // txt_name_edit
            // 
            this.txt_name_edit.Location = new System.Drawing.Point(167, 67);
            this.txt_name_edit.Name = "txt_name_edit";
            this.txt_name_edit.Size = new System.Drawing.Size(146, 20);
            this.txt_name_edit.TabIndex = 0;
            // 
            // txt_EID_edit
            // 
            this.txt_EID_edit.Location = new System.Drawing.Point(167, 37);
            this.txt_EID_edit.Name = "txt_EID_edit";
            this.txt_EID_edit.Size = new System.Drawing.Size(68, 20);
            this.txt_EID_edit.TabIndex = 12;
            // 
            // lb_IDCardNumber_edit
            // 
            this.lb_IDCardNumber_edit.AutoSize = true;
            this.lb_IDCardNumber_edit.Location = new System.Drawing.Point(348, 220);
            this.lb_IDCardNumber_edit.Name = "lb_IDCardNumber_edit";
            this.lb_IDCardNumber_edit.Size = new System.Drawing.Size(83, 13);
            this.lb_IDCardNumber_edit.TabIndex = 0;
            this.lb_IDCardNumber_edit.Text = "ID Card Number";
            // 
            // lb_Phone_edit
            // 
            this.lb_Phone_edit.AutoSize = true;
            this.lb_Phone_edit.Location = new System.Drawing.Point(348, 190);
            this.lb_Phone_edit.Name = "lb_Phone_edit";
            this.lb_Phone_edit.Size = new System.Drawing.Size(38, 13);
            this.lb_Phone_edit.TabIndex = 0;
            this.lb_Phone_edit.Text = "Phone";
            // 
            // lb_Role_edit
            // 
            this.lb_Role_edit.AutoSize = true;
            this.lb_Role_edit.Location = new System.Drawing.Point(60, 220);
            this.lb_Role_edit.Name = "lb_Role_edit";
            this.lb_Role_edit.Size = new System.Drawing.Size(29, 13);
            this.lb_Role_edit.TabIndex = 0;
            this.lb_Role_edit.Text = "Role";
            // 
            // lb_Address_edit
            // 
            this.lb_Address_edit.AutoSize = true;
            this.lb_Address_edit.Location = new System.Drawing.Point(348, 160);
            this.lb_Address_edit.Name = "lb_Address_edit";
            this.lb_Address_edit.Size = new System.Drawing.Size(45, 13);
            this.lb_Address_edit.TabIndex = 0;
            this.lb_Address_edit.Text = "Address";
            // 
            // lb_Password_edit
            // 
            this.lb_Password_edit.AutoSize = true;
            this.lb_Password_edit.Location = new System.Drawing.Point(60, 190);
            this.lb_Password_edit.Name = "lb_Password_edit";
            this.lb_Password_edit.Size = new System.Drawing.Size(53, 13);
            this.lb_Password_edit.TabIndex = 0;
            this.lb_Password_edit.Text = "Password";
            // 
            // lb_DOB_edit
            // 
            this.lb_DOB_edit.AutoSize = true;
            this.lb_DOB_edit.Location = new System.Drawing.Point(348, 132);
            this.lb_DOB_edit.Name = "lb_DOB_edit";
            this.lb_DOB_edit.Size = new System.Drawing.Size(30, 13);
            this.lb_DOB_edit.TabIndex = 0;
            this.lb_DOB_edit.Text = "DOB";
            // 
            // lb_Username_edit
            // 
            this.lb_Username_edit.AutoSize = true;
            this.lb_Username_edit.Location = new System.Drawing.Point(60, 160);
            this.lb_Username_edit.Name = "lb_Username_edit";
            this.lb_Username_edit.Size = new System.Drawing.Size(55, 13);
            this.lb_Username_edit.TabIndex = 0;
            this.lb_Username_edit.Text = "Username";
            // 
            // lb_Spec_edit
            // 
            this.lb_Spec_edit.AutoSize = true;
            this.lb_Spec_edit.Location = new System.Drawing.Point(60, 132);
            this.lb_Spec_edit.Name = "lb_Spec_edit";
            this.lb_Spec_edit.Size = new System.Drawing.Size(52, 13);
            this.lb_Spec_edit.TabIndex = 0;
            this.lb_Spec_edit.Text = "Speciality";
            // 
            // lb_Name_edit
            // 
            this.lb_Name_edit.AutoSize = true;
            this.lb_Name_edit.Location = new System.Drawing.Point(60, 70);
            this.lb_Name_edit.Name = "lb_Name_edit";
            this.lb_Name_edit.Size = new System.Drawing.Size(35, 13);
            this.lb_Name_edit.TabIndex = 0;
            this.lb_Name_edit.Text = "Name";
            // 
            // lb_E_ID_edit
            // 
            this.lb_E_ID_edit.AutoSize = true;
            this.lb_E_ID_edit.Location = new System.Drawing.Point(60, 40);
            this.lb_E_ID_edit.Name = "lb_E_ID_edit";
            this.lb_E_ID_edit.Size = new System.Drawing.Size(28, 13);
            this.lb_E_ID_edit.TabIndex = 0;
            this.lb_E_ID_edit.Text = "E-ID";
            // 
            // tab_manageemployee
            // 
            this.tab_manageemployee.Controls.Add(this.btn_search_manage);
            this.tab_manageemployee.Controls.Add(this.txt_search_manage);
            this.tab_manageemployee.Controls.Add(this.gv_emp_manage);
            this.tab_manageemployee.Controls.Add(this.gb_operation);
            this.tab_manageemployee.Controls.Add(this.gb_search);
            this.tab_manageemployee.Location = new System.Drawing.Point(4, 22);
            this.tab_manageemployee.Name = "tab_manageemployee";
            this.tab_manageemployee.Padding = new System.Windows.Forms.Padding(3);
            this.tab_manageemployee.Size = new System.Drawing.Size(669, 342);
            this.tab_manageemployee.TabIndex = 3;
            this.tab_manageemployee.Text = "Manage Employee";
            this.tab_manageemployee.UseVisualStyleBackColor = true;
            // 
            // btn_search_manage
            // 
            this.btn_search_manage.Location = new System.Drawing.Point(174, 60);
            this.btn_search_manage.Name = "btn_search_manage";
            this.btn_search_manage.Size = new System.Drawing.Size(100, 23);
            this.btn_search_manage.TabIndex = 3;
            this.btn_search_manage.Text = "Search";
            this.btn_search_manage.UseVisualStyleBackColor = true;
            // 
            // txt_search_manage
            // 
            this.txt_search_manage.Location = new System.Drawing.Point(174, 33);
            this.txt_search_manage.Name = "txt_search_manage";
            this.txt_search_manage.Size = new System.Drawing.Size(100, 20);
            this.txt_search_manage.TabIndex = 2;
            // 
            // gv_emp_manage
            // 
            this.gv_emp_manage.AllowUserToAddRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.gv_emp_manage.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.gv_emp_manage.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.gv_emp_manage.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gv_emp_manage.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.gv_emp_manage.Location = new System.Drawing.Point(3, 111);
            this.gv_emp_manage.Name = "gv_emp_manage";
            this.gv_emp_manage.RowHeadersVisible = false;
            this.gv_emp_manage.Size = new System.Drawing.Size(663, 228);
            this.gv_emp_manage.TabIndex = 1;
            // 
            // gb_operation
            // 
            this.gb_operation.Controls.Add(this.lb_E_ID_manage);
            this.gb_operation.Controls.Add(this.txt_E_ID_manage);
            this.gb_operation.Controls.Add(this.btn_delete);
            this.gb_operation.Controls.Add(this.btn_lock);
            this.gb_operation.Location = new System.Drawing.Point(405, 15);
            this.gb_operation.Name = "gb_operation";
            this.gb_operation.Size = new System.Drawing.Size(230, 83);
            this.gb_operation.TabIndex = 0;
            this.gb_operation.TabStop = false;
            this.gb_operation.Text = "Operation";
            // 
            // lb_E_ID_manage
            // 
            this.lb_E_ID_manage.AutoSize = true;
            this.lb_E_ID_manage.Location = new System.Drawing.Point(17, 22);
            this.lb_E_ID_manage.Name = "lb_E_ID_manage";
            this.lb_E_ID_manage.Size = new System.Drawing.Size(67, 13);
            this.lb_E_ID_manage.TabIndex = 7;
            this.lb_E_ID_manage.Text = "Employee ID";
            // 
            // txt_E_ID_manage
            // 
            this.txt_E_ID_manage.Location = new System.Drawing.Point(20, 47);
            this.txt_E_ID_manage.Name = "txt_E_ID_manage";
            this.txt_E_ID_manage.Size = new System.Drawing.Size(74, 20);
            this.txt_E_ID_manage.TabIndex = 4;
            // 
            // btn_delete
            // 
            this.btn_delete.Location = new System.Drawing.Point(128, 47);
            this.btn_delete.Name = "btn_delete";
            this.btn_delete.Size = new System.Drawing.Size(84, 27);
            this.btn_delete.TabIndex = 6;
            this.btn_delete.Text = "Delete";
            this.btn_delete.UseVisualStyleBackColor = true;
            // 
            // btn_lock
            // 
            this.btn_lock.Location = new System.Drawing.Point(128, 15);
            this.btn_lock.Name = "btn_lock";
            this.btn_lock.Size = new System.Drawing.Size(84, 27);
            this.btn_lock.TabIndex = 5;
            this.btn_lock.Text = "Lock";
            this.btn_lock.UseVisualStyleBackColor = true;
            // 
            // gb_search
            // 
            this.gb_search.Controls.Add(this.rbtn_search_E_Name);
            this.gb_search.Controls.Add(this.rbtn_search_E_ID);
            this.gb_search.Location = new System.Drawing.Point(29, 15);
            this.gb_search.Name = "gb_search";
            this.gb_search.Size = new System.Drawing.Size(127, 83);
            this.gb_search.TabIndex = 0;
            this.gb_search.TabStop = false;
            this.gb_search.Text = "Search By";
            // 
            // rbtn_search_E_Name
            // 
            this.rbtn_search_E_Name.AutoSize = true;
            this.rbtn_search_E_Name.Location = new System.Drawing.Point(7, 45);
            this.rbtn_search_E_Name.Name = "rbtn_search_E_Name";
            this.rbtn_search_E_Name.Size = new System.Drawing.Size(102, 17);
            this.rbtn_search_E_Name.TabIndex = 1;
            this.rbtn_search_E_Name.Text = "Employee Name";
            this.rbtn_search_E_Name.UseVisualStyleBackColor = true;
            // 
            // rbtn_search_E_ID
            // 
            this.rbtn_search_E_ID.AutoSize = true;
            this.rbtn_search_E_ID.Checked = true;
            this.rbtn_search_E_ID.Location = new System.Drawing.Point(7, 20);
            this.rbtn_search_E_ID.Name = "rbtn_search_E_ID";
            this.rbtn_search_E_ID.Size = new System.Drawing.Size(85, 17);
            this.rbtn_search_E_ID.TabIndex = 0;
            this.rbtn_search_E_ID.TabStop = true;
            this.rbtn_search_E_ID.Text = "Employee ID";
            this.rbtn_search_E_ID.UseVisualStyleBackColor = true;
            // 
            // frm_AdminPanel
            // 
            this.ClientSize = new System.Drawing.Size(677, 368);
            this.Controls.Add(this.tab);
            this.Name = "frm_AdminPanel";
            this.tab.ResumeLayout(false);
            this.tab_newemployee.ResumeLayout(false);
            this.tab_newemployee.PerformLayout();
            this.tab_editemployee.ResumeLayout(false);
            this.tab_editemployee.PerformLayout();
            this.tab_manageemployee.ResumeLayout(false);
            this.tab_manageemployee.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gv_emp_manage)).EndInit();
            this.gb_operation.ResumeLayout(false);
            this.gb_operation.PerformLayout();
            this.gb_search.ResumeLayout(false);
            this.gb_search.PerformLayout();
            this.ResumeLayout(false);

        }
        private bool IsNumber(string s)
        {
            foreach (char c in s)
            {
                if (!char.IsDigit(c))
                {
                    return false;
                }
            }
            return true;
        }
        private void btn_Create_Click_1(object sender, EventArgs e)
        {
            string msg = "";

            ConnectionString cs = new ConnectionString();
            string con_string = cs.GetCS;

            SqlConnection con = new SqlConnection(con_string);
            con.Open();

            SqlCommand cmd_uid = new SqlCommand("select UserName from Employee", con);
            SqlDataReader dr_uid = cmd_uid.ExecuteReader();
            dr_uid.Read();

            if (txt_username.Text == dr_uid.GetValue(0).ToString())
            {
                msg += "Duplicated";
            }
            dr_uid.Close();
            con.Close();
            if (txt_name.Text.Length < 2)
            {
                msg += "Name must be geater than 2 character\n\n";
            }
            if (txt_note.Text == "")
            {
                msg += "Speciality not allow left null\n\n";
            }
            if (txt_username.Text.Length < 2)
            {
                msg += "User Name must be geater than 2 character\n\n";
            }
            if (txt_password.Text.Length < 2)
            {
                msg += "Password must be geater than 2 character\n\n";
            }
            if (DateTime.Today.Year - dp_DOB.Value.Year < 18)
            {
                msg += "Employee Age Must Be Greater Than 18\n\n";
            }
            if (txt_address.Text == "")
            {
                msg += "Address not allow left null\n\n";
            }
            if (!IsNumber(txt_phone.Text) || txt_phone.Text.Length < 8)
            {
                msg += "Phone must be in number format and greater than 8 number\n\n";
            }
            if (!IsNumber(txt_naid.Text) || txt_naid.Text.Length != 9)
            {
                msg += "ID Card Number must be in number format and equal to 9 number\n\n";
            }
            if (msg != "")
            {
                MessageBox.Show(msg);
                return;
            }

            try
            {
                string gender = "";
                if (rb_female.Checked)
                {
                    gender = "Female";
                }
                else
                {
                    gender = "Male";
                }
                ai = new AutoID();
                string id = ai.MakeID("Employee", "E_ID", "E", 3);
                txt_EID.Text = id;

                string cmd_string = string.Format("insert Employee Values('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}')", txt_EID.Text, txt_role.Text, txt_name.Text, txt_username.Text, txt_password.Text, dp_DOB.Value.ToString("yyyy/MM/dd"), txt_phone.Text, gender, txt_address.Text, txt_note.Text, txt_naid.Text);

                SqlTransaction tran = null;
                try
                {
                    con.Open();
                    tran.Connection.BeginTransaction();

                    SqlCommand cmd = new SqlCommand(cmd_string, con, tran);
                    int res = cmd.ExecuteNonQuery();
                    if (res > 0)
                    {
                        MessageBox.Show("Done");
                    }
                    else
                    {
                        MessageBox.Show("Fail");
                        tran.Rollback();
                    }
                    tran.Commit();
                    con.Close();
                    ai = new AutoID();
                    id = ai.MakeID("Employee", "E_ID", "E", 3);
                    txt_EID.Text = id;

                    txt_address.Clear();
                    txt_naid.Clear();
                    txt_name.Clear();
                    txt_note.Clear();
                    txt_password.Clear();
                    txt_phone.Clear();
                    txt_username.Clear();
                }
                catch
                {
                    tran.Rollback();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btn_Reset_Click_1(object sender, EventArgs e)
        {
            txt_address.Clear();
            txt_naid.Clear();
            txt_name.Clear();
            txt_password.Clear();
            txt_phone.Clear();
            txt_note.Clear();
            txt_username.Clear();
            txt_role.Clear();
        }

        private void btn_Logout_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void txt_EID_TextChanged(object sender, EventArgs e)
        {

        }
    }
}

