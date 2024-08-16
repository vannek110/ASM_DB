namespace StudentAttendanceSystem
{
    partial class RegisterForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.txt_firstname = new System.Windows.Forms.TextBox();
            this.txt_lastname = new System.Windows.Forms.TextBox();
            this.txt_mail = new System.Windows.Forms.TextBox();
            this.txt_classid = new System.Windows.Forms.TextBox();
            this.cbb_day = new System.Windows.Forms.ComboBox();
            this.cbb_month = new System.Windows.Forms.ComboBox();
            this.cbb_year = new System.Windows.Forms.ComboBox();
            this.txt_username = new System.Windows.Forms.TextBox();
            this.txt_password = new System.Windows.Forms.TextBox();
            this.txt_confirmpass = new System.Windows.Forms.TextBox();
            this.cb_male = new System.Windows.Forms.CheckBox();
            this.cb_female = new System.Windows.Forms.CheckBox();
            this.btn_register = new System.Windows.Forms.Button();
            this.link_Login = new System.Windows.Forms.LinkLabel();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // txt_firstname
            // 
            this.txt_firstname.ForeColor = System.Drawing.Color.Gray;
            this.txt_firstname.Location = new System.Drawing.Point(42, 91);
            this.txt_firstname.Multiline = true;
            this.txt_firstname.Name = "txt_firstname";
            this.txt_firstname.Size = new System.Drawing.Size(207, 43);
            this.txt_firstname.TabIndex = 1;
            this.txt_firstname.Text = "First name";
            this.txt_firstname.Enter += new System.EventHandler(this.txt_firstname_Enter_1);
            this.txt_firstname.Leave += new System.EventHandler(this.txt_firstname_Leave_1);
            // 
            // txt_lastname
            // 
            this.txt_lastname.ForeColor = System.Drawing.Color.Gray;
            this.txt_lastname.Location = new System.Drawing.Point(307, 91);
            this.txt_lastname.Multiline = true;
            this.txt_lastname.Name = "txt_lastname";
            this.txt_lastname.Size = new System.Drawing.Size(204, 43);
            this.txt_lastname.TabIndex = 2;
            this.txt_lastname.Text = "Last name";
            this.txt_lastname.Enter += new System.EventHandler(this.txt_lastname_Enter);
            this.txt_lastname.Leave += new System.EventHandler(this.txt_lastname_Leave);
            // 
            // txt_mail
            // 
            this.txt_mail.ForeColor = System.Drawing.Color.Gray;
            this.txt_mail.Location = new System.Drawing.Point(40, 180);
            this.txt_mail.Multiline = true;
            this.txt_mail.Name = "txt_mail";
            this.txt_mail.Size = new System.Drawing.Size(471, 43);
            this.txt_mail.TabIndex = 3;
            this.txt_mail.Text = "Email";
            this.txt_mail.Enter += new System.EventHandler(this.txt_mail_Enter);
            this.txt_mail.Leave += new System.EventHandler(this.txt_mail_Leave);
            // 
            // txt_classid
            // 
            this.txt_classid.ForeColor = System.Drawing.Color.Gray;
            this.txt_classid.Location = new System.Drawing.Point(42, 265);
            this.txt_classid.Multiline = true;
            this.txt_classid.Name = "txt_classid";
            this.txt_classid.Size = new System.Drawing.Size(471, 43);
            this.txt_classid.TabIndex = 4;
            this.txt_classid.Text = "Class ID";
            this.txt_classid.Enter += new System.EventHandler(this.txt_classid_Enter);
            this.txt_classid.Leave += new System.EventHandler(this.txt_classid_Leave);
            // 
            // cbb_day
            // 
            this.cbb_day.ForeColor = System.Drawing.Color.Gray;
            this.cbb_day.FormattingEnabled = true;
            this.cbb_day.Location = new System.Drawing.Point(42, 359);
            this.cbb_day.Name = "cbb_day";
            this.cbb_day.Size = new System.Drawing.Size(121, 24);
            this.cbb_day.TabIndex = 26;
            this.cbb_day.Text = "Day";
            // 
            // cbb_month
            // 
            this.cbb_month.ForeColor = System.Drawing.Color.Gray;
            this.cbb_month.FormattingEnabled = true;
            this.cbb_month.Location = new System.Drawing.Point(218, 359);
            this.cbb_month.Name = "cbb_month";
            this.cbb_month.Size = new System.Drawing.Size(121, 24);
            this.cbb_month.TabIndex = 27;
            this.cbb_month.Text = "Month";
            // 
            // cbb_year
            // 
            this.cbb_year.ForeColor = System.Drawing.Color.Gray;
            this.cbb_year.FormattingEnabled = true;
            this.cbb_year.Location = new System.Drawing.Point(390, 359);
            this.cbb_year.Name = "cbb_year";
            this.cbb_year.Size = new System.Drawing.Size(121, 24);
            this.cbb_year.TabIndex = 28;
            this.cbb_year.Text = "Year";
            // 
            // txt_username
            // 
            this.txt_username.ForeColor = System.Drawing.Color.Gray;
            this.txt_username.Location = new System.Drawing.Point(40, 427);
            this.txt_username.Multiline = true;
            this.txt_username.Name = "txt_username";
            this.txt_username.Size = new System.Drawing.Size(471, 43);
            this.txt_username.TabIndex = 29;
            this.txt_username.Text = "Username";
            this.txt_username.Enter += new System.EventHandler(this.txt_username_Enter);
            this.txt_username.Leave += new System.EventHandler(this.txt_username_Leave);
            // 
            // txt_password
            // 
            this.txt_password.ForeColor = System.Drawing.Color.Gray;
            this.txt_password.Location = new System.Drawing.Point(40, 518);
            this.txt_password.Multiline = true;
            this.txt_password.Name = "txt_password";
            this.txt_password.Size = new System.Drawing.Size(471, 43);
            this.txt_password.TabIndex = 30;
            this.txt_password.Text = "Password";
            this.txt_password.Enter += new System.EventHandler(this.txt_password_Enter);
            this.txt_password.Leave += new System.EventHandler(this.txt_password_Leave);
            // 
            // txt_confirmpass
            // 
            this.txt_confirmpass.ForeColor = System.Drawing.Color.Gray;
            this.txt_confirmpass.Location = new System.Drawing.Point(40, 612);
            this.txt_confirmpass.Multiline = true;
            this.txt_confirmpass.Name = "txt_confirmpass";
            this.txt_confirmpass.Size = new System.Drawing.Size(471, 43);
            this.txt_confirmpass.TabIndex = 31;
            this.txt_confirmpass.Text = "Confirm password";
            this.txt_confirmpass.Enter += new System.EventHandler(this.txt_confirmpass_Enter);
            this.txt_confirmpass.Leave += new System.EventHandler(this.txt_confirmpass_Leave);
            // 
            // cb_male
            // 
            this.cb_male.AutoSize = true;
            this.cb_male.Location = new System.Drawing.Point(188, 676);
            this.cb_male.Name = "cb_male";
            this.cb_male.Size = new System.Drawing.Size(59, 20);
            this.cb_male.TabIndex = 33;
            this.cb_male.Text = "Male";
            this.cb_male.UseVisualStyleBackColor = true;
            this.cb_male.CheckedChanged += new System.EventHandler(this.cb_male_CheckedChanged);
            // 
            // cb_female
            // 
            this.cb_female.AutoSize = true;
            this.cb_female.Location = new System.Drawing.Point(316, 676);
            this.cb_female.Name = "cb_female";
            this.cb_female.Size = new System.Drawing.Size(75, 20);
            this.cb_female.TabIndex = 32;
            this.cb_female.Text = "Female";
            this.cb_female.UseVisualStyleBackColor = true;
            this.cb_female.CheckedChanged += new System.EventHandler(this.checkBox2_CheckedChanged);
            // 
            // btn_register
            // 
            this.btn_register.BackColor = System.Drawing.Color.SpringGreen;
            this.btn_register.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_register.Location = new System.Drawing.Point(203, 724);
            this.btn_register.Name = "btn_register";
            this.btn_register.Size = new System.Drawing.Size(146, 54);
            this.btn_register.TabIndex = 34;
            this.btn_register.Text = "Register";
            this.btn_register.UseVisualStyleBackColor = false;
            this.btn_register.Click += new System.EventHandler(this.btn_register_Click);
            // 
            // link_Login
            // 
            this.link_Login.AutoSize = true;
            this.link_Login.Location = new System.Drawing.Point(390, 781);
            this.link_Login.Name = "link_Login";
            this.link_Login.Size = new System.Drawing.Size(123, 16);
            this.link_Login.TabIndex = 35;
            this.link_Login.TabStop = true;
            this.link_Login.Text = "Comeback to Login";
            this.link_Login.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.link_Login_LinkClicked);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Lucida Fax", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.RoyalBlue;
            this.label1.Location = new System.Drawing.Point(227, 33);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(112, 26);
            this.label1.TabIndex = 36;
            this.label1.Text = "Register";
            // 
            // RegisterForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.PaleTurquoise;
            this.ClientSize = new System.Drawing.Size(567, 820);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.link_Login);
            this.Controls.Add(this.btn_register);
            this.Controls.Add(this.cb_male);
            this.Controls.Add(this.cb_female);
            this.Controls.Add(this.txt_confirmpass);
            this.Controls.Add(this.txt_password);
            this.Controls.Add(this.txt_username);
            this.Controls.Add(this.cbb_year);
            this.Controls.Add(this.cbb_month);
            this.Controls.Add(this.cbb_day);
            this.Controls.Add(this.txt_classid);
            this.Controls.Add(this.txt_mail);
            this.Controls.Add(this.txt_lastname);
            this.Controls.Add(this.txt_firstname);
            this.Name = "RegisterForm";
            this.Text = "RegisterForm";
            this.Load += new System.EventHandler(this.RegisterForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txt_firstname;
        private System.Windows.Forms.TextBox txt_lastname;
        private System.Windows.Forms.TextBox txt_mail;
        private System.Windows.Forms.TextBox txt_classid;
        private System.Windows.Forms.ComboBox cbb_day;
        private System.Windows.Forms.ComboBox cbb_month;
        private System.Windows.Forms.ComboBox cbb_year;
        private System.Windows.Forms.TextBox txt_username;
        private System.Windows.Forms.TextBox txt_password;
        private System.Windows.Forms.TextBox txt_confirmpass;
        private System.Windows.Forms.CheckBox cb_male;
        private System.Windows.Forms.CheckBox cb_female;
        private System.Windows.Forms.Button btn_register;
        private System.Windows.Forms.LinkLabel link_Login;
        private System.Windows.Forms.Label label1;
    }
}