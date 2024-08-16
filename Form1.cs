using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace StudentAttendanceSystem
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
        }
        private void Form1_Load(object sender, EventArgs e)
        {

        }


        private void btn_login_Click(object sender, EventArgs e)
        {
            string username = txt_username.Text;
            string password = txt_password.Text;
            bool isStudent = cbx_student.Checked;
            bool isLecturer = cbx_lecturer.Checked;

            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Username and Password are required.");
                return;
            }

            if (!isStudent && !isLecturer)
            {
                MessageBox.Show("Please select a role.");
                return;
            }

            string role = isStudent ? "student" : "lecturer";

            if (AuthenticateUser(username, password, role, out int userId))
            {
                MessageBox.Show("Login successful!");
                
                if (isStudent)
                {
                    new studentForm(username).Show();


                }
                else if (isLecturer)
                {
                    new LecturerForm(username).Show();
                }
                this.Hide(); 
            }
            else
            {
                MessageBox.Show("Invalid username or password.");
            }

        }

        private bool AuthenticateUser(string username, string password, string role, out int userId)
        {
            userId = -1;
            bool isAuthenticated = false;


            using (SqlConnection conn = new SqlConnection("Data Source=DESKTOP-7STFHO0\\SQLEXPRESS;Initial Catalog=Test3;Integrated Security=True;"))
            {
                conn.Open();
                string query = "SELECT UserID FROM [User] WHERE username = @username AND password = @password AND role = @role";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@username", username);
                    cmd.Parameters.AddWithValue("@password", password); 
                    cmd.Parameters.AddWithValue("@role", role);
                    object result = cmd.ExecuteScalar();
                    if (result != null)
                    {
                        userId = Convert.ToInt32(result);
                        isAuthenticated = true;
                    }
                }
            }

            return isAuthenticated;
        }

        private void cbx_student_CheckedChanged(object sender, EventArgs e)
        {
            if (cbx_student.Checked)
            {
                cbx_lecturer.Checked = false;
            }
        }

        private void cbx_lecturer_CheckedChanged(object sender, EventArgs e)
        {
            if (cbx_lecturer.Checked) 
            {
                cbx_student.Checked = false;
            }
        }

        private void btn_register_Click_1(object sender, EventArgs e)
        {
            RegisterForm registerForm = new RegisterForm();
            registerForm.Show();
            this.Hide();
        }

        


        private void txt_username_Enter(object sender, EventArgs e)
        {
            TextBox textBox = sender as TextBox;
            if (textBox.Text == "Enter username")
            {
                textBox.Text = "";
                textBox.ForeColor = Color.Black;
            }
        }

        private void txt_username_Leave(object sender, EventArgs e)
        {
            TextBox textBox = sender as TextBox;
            if (string.IsNullOrWhiteSpace(textBox.Text))
            {
                textBox.Text = "Enter username";
                textBox.ForeColor = Color.Gray;
            }
        }

        private void txt_password_Enter(object sender, EventArgs e)
        {
            TextBox textBox = sender as TextBox;
            if (textBox.Text == "Enter password")
            {
                textBox.Text = "";
                textBox.ForeColor = Color.Black;
                textBox.PasswordChar = '*';
            }
        }

        private void txt_password_Leave(object sender, EventArgs e)
        {
            TextBox textBox = sender as TextBox;
            if (string.IsNullOrWhiteSpace(textBox.Text))
            {
                textBox.PasswordChar = '\0';
                textBox.Text = "Enter password";
                textBox.ForeColor = Color.Gray;
            }
        }

        public static string HashPassword(string password)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                // Convert the input string to a byte array and compute the hash.
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(password));

                // Convert the byte array to a string.
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }

        private void txt_username_Enter_1(object sender, EventArgs e)
        {
            TextBox textBox = sender as TextBox;
            if (textBox.Text == "Enter password")
            {
                textBox.Text = "";
                textBox.ForeColor = Color.Black;
            }
        }
    }
}
