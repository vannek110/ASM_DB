namespace StudentAttendanceSystem
{
    partial class Form1
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
            this.txt_username = new System.Windows.Forms.TextBox();
            this.txt_password = new System.Windows.Forms.TextBox();
            this.btn_register = new System.Windows.Forms.Button();
            this.btn_login = new System.Windows.Forms.Button();
            this.cbx_student = new System.Windows.Forms.CheckBox();
            this.cbx_lecturer = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // txt_username
            // 
            this.txt_username.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_username.ForeColor = System.Drawing.Color.Black;
            this.txt_username.Location = new System.Drawing.Point(172, 235);
            this.txt_username.Multiline = true;
            this.txt_username.Name = "txt_username";
            this.txt_username.Size = new System.Drawing.Size(285, 57);
            this.txt_username.TabIndex = 0;
            this.txt_username.Text = "Enter username";
            this.txt_username.Enter += new System.EventHandler(this.txt_username_Enter_1);
            // 
            // txt_password
            // 
            this.txt_password.Font = new System.Drawing.Font("Microsoft YaHei", 9F);
            this.txt_password.ForeColor = System.Drawing.Color.Gray;
            this.txt_password.Location = new System.Drawing.Point(172, 331);
            this.txt_password.Multiline = true;
            this.txt_password.Name = "txt_password";
            this.txt_password.Size = new System.Drawing.Size(285, 57);
            this.txt_password.TabIndex = 2;
            this.txt_password.Text = "Enter password";
            this.txt_password.Enter += new System.EventHandler(this.txt_password_Enter);
            this.txt_password.Leave += new System.EventHandler(this.txt_password_Leave);
            // 
            // btn_register
            // 
            this.btn_register.Font = new System.Drawing.Font("MS Reference Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_register.ForeColor = System.Drawing.Color.SteelBlue;
            this.btn_register.Location = new System.Drawing.Point(114, 521);
            this.btn_register.Name = "btn_register";
            this.btn_register.Size = new System.Drawing.Size(145, 61);
            this.btn_register.TabIndex = 3;
            this.btn_register.Text = "Register";
            this.btn_register.UseVisualStyleBackColor = true;
            this.btn_register.Click += new System.EventHandler(this.btn_register_Click_1);
            // 
            // btn_login
            // 
            this.btn_login.Font = new System.Drawing.Font("MS Reference Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.btn_login.ForeColor = System.Drawing.Color.SteelBlue;
            this.btn_login.Location = new System.Drawing.Point(369, 521);
            this.btn_login.Name = "btn_login";
            this.btn_login.Size = new System.Drawing.Size(145, 61);
            this.btn_login.TabIndex = 4;
            this.btn_login.Text = "Login";
            this.btn_login.UseVisualStyleBackColor = true;
            this.btn_login.Click += new System.EventHandler(this.btn_login_Click);
            // 
            // cbx_student
            // 
            this.cbx_student.AutoSize = true;
            this.cbx_student.Font = new System.Drawing.Font("MS Reference Sans Serif", 7.8F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbx_student.ForeColor = System.Drawing.Color.Aqua;
            this.cbx_student.Location = new System.Drawing.Point(202, 432);
            this.cbx_student.Name = "cbx_student";
            this.cbx_student.Size = new System.Drawing.Size(89, 20);
            this.cbx_student.TabIndex = 5;
            this.cbx_student.Text = "Student";
            this.cbx_student.UseVisualStyleBackColor = true;
            this.cbx_student.CheckedChanged += new System.EventHandler(this.cbx_student_CheckedChanged);
            // 
            // cbx_lecturer
            // 
            this.cbx_lecturer.AutoSize = true;
            this.cbx_lecturer.Font = new System.Drawing.Font("MS Reference Sans Serif", 7.8F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbx_lecturer.ForeColor = System.Drawing.Color.Aqua;
            this.cbx_lecturer.Location = new System.Drawing.Point(326, 432);
            this.cbx_lecturer.Name = "cbx_lecturer";
            this.cbx_lecturer.Size = new System.Drawing.Size(92, 20);
            this.cbx_lecturer.TabIndex = 6;
            this.cbx_lecturer.Text = "Lecturer";
            this.cbx_lecturer.UseVisualStyleBackColor = true;
            this.cbx_lecturer.CheckedChanged += new System.EventHandler(this.cbx_lecturer_CheckedChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Sitka Display", 36F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(210, 74);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(208, 87);
            this.label1.TabIndex = 7;
            this.label1.Text = "LOGIN";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.ClientSize = new System.Drawing.Size(654, 655);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cbx_lecturer);
            this.Controls.Add(this.cbx_student);
            this.Controls.Add(this.btn_login);
            this.Controls.Add(this.btn_register);
            this.Controls.Add(this.txt_password);
            this.Controls.Add(this.txt_username);
            this.ForeColor = System.Drawing.Color.Aqua;
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txt_username;
        private System.Windows.Forms.TextBox txt_password;
        private System.Windows.Forms.Button btn_register;
        private System.Windows.Forms.Button btn_login;
        private System.Windows.Forms.CheckBox cbx_student;
        private System.Windows.Forms.CheckBox cbx_lecturer;
        private System.Windows.Forms.Label label1;
    }
}

