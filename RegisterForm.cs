using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Security.Cryptography;
using System.Data.SqlClient;


namespace StudentAttendanceSystem
{
    public partial class RegisterForm : Form
    {
        public RegisterForm()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        private void RegisterForm_Load(object sender, EventArgs e)
        {
            // Initialize Day ComboBox
            for (int i = 1; i <= 31; i++)
            {
                cbb_day.Items.Add(i);
            }

            // Initialize Month ComboBox
            cbb_month.Items.AddRange(new string[] { "January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December" });

            // Initialize Year ComboBox
            for (int i = DateTime.Now.Year; i >= 1995; i--)
            {
                cbb_year.Items.Add(i);
            }
        }

        private void btn_register_Click(object sender, EventArgs e)
        {
            if (txt_password.Text != txt_confirmpass.Text)
            {
                MessageBox.Show("Passwords do not match.");
                return;
            }

            if (string.IsNullOrWhiteSpace(txt_firstname.Text) ||
       string.IsNullOrWhiteSpace(txt_lastname.Text) ||
       string.IsNullOrWhiteSpace(txt_mail.Text) ||
       string.IsNullOrWhiteSpace(txt_username.Text) ||
       string.IsNullOrWhiteSpace(txt_password.Text) ||
       !int.TryParse(txt_classid.Text, out int classID) || // Validate ClassID
       (!cb_male.Checked && !cb_female.Checked))
            {
                MessageBox.Show("Please fill in all fields correctly.");
                return;
            }


            string firstName = txt_firstname.Text;
            string lastName = txt_lastname.Text;
            string email = txt_mail.Text;
            string username = txt_username.Text;
            string password = txt_password.Text;
            string classId = txt_classid.Text;
            string day = cbb_day.SelectedItem.ToString();
            string month = cbb_month.SelectedItem.ToString();
            string year = cbb_year.SelectedItem.ToString();
            string gender = cb_male.Checked ? "Male" : "Female";

            if (!ClassIDExists(classID))
            {
                MessageBox.Show("The ClassID does not exist.");
                return;
            }

            if (!DateTime.TryParse($"{year}-{month}-{day}", out DateTime dateOfBirth))
            {
                MessageBox.Show("Invalid date.");
                return;
            }

            // Insert into the database
            InsertStudent(username, password, $"{firstName} {lastName}", dateOfBirth, email, classID, gender);

            // Show success message
            MessageBox.Show("Registration successful!");
        }

        private void InsertStudent(string username, string password, string fullName, DateTime dob, string email, int classID, string gender)
        {
            using (SqlConnection conn = new SqlConnection("Data Source=DESKTOP-7STFHO0\\SQLEXPRESS;Initial Catalog=Test3;Integrated Security=True;"))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("InsertStudent", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@username", username);
                    cmd.Parameters.AddWithValue("@password", password);
                    cmd.Parameters.AddWithValue("@Fullname", fullName);
                    cmd.Parameters.AddWithValue("@DateOfBirth", dob);
                    cmd.Parameters.AddWithValue("@Email", email);
                    cmd.Parameters.AddWithValue("@ClassID", classID);
                    cmd.Parameters.AddWithValue("@Gender", gender);
                    cmd.ExecuteNonQuery();
                }
            }
        }
        private bool ClassIDExists(int classID)
        {
            bool exists = false;

            // Replace "your_connection_string" with your actual database connection string
            using (SqlConnection conn = new SqlConnection("Data Source=DESKTOP-7STFHO0\\SQLEXPRESS;Initial Catalog=Test3;Integrated Security=True;"))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM [Class] WHERE ClassID = @ClassID", conn))
                {
                    cmd.Parameters.AddWithValue("@ClassID", classID);
                    int count = (int)cmd.ExecuteScalar();
                    exists = count > 0;
                }
            }

            return exists;
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
        private void txt_firstname_Enter_1(object sender, EventArgs e)
        {
            TextBox textBox = sender as TextBox;
            if (textBox.Text == "First name")
            {
                textBox.Text = "";
                textBox.ForeColor = Color.Black;
            }
        }

        private void txt_firstname_Leave_1(object sender, EventArgs e)
        {
            TextBox textBox = sender as TextBox;
            if (string.IsNullOrWhiteSpace(textBox.Text))
            {
                textBox.Text = "First name";
                textBox.ForeColor = Color.Gray;
            }
        }

        private void txt_lastname_Enter(object sender, EventArgs e)
        {
            TextBox textBox = sender as TextBox;
            if (textBox.Text == "Last name")
            {
                textBox.Text = "";
                textBox.ForeColor = Color.Black;
            }
        }

        private void txt_lastname_Leave(object sender, EventArgs e)
        {
            TextBox textBox = sender as TextBox;
            if (string.IsNullOrWhiteSpace(textBox.Text))
            {
                textBox.Text = "Last name";
                textBox.ForeColor = Color.Gray;
            }
        }

        private void txt_mail_Enter(object sender, EventArgs e)
        {
            TextBox textBox = sender as TextBox;
            if (textBox.Text == "Email")
            {
                textBox.Text = "";
                textBox.ForeColor = Color.Black;
            }
        }

        private void txt_mail_Leave(object sender, EventArgs e)
        {
            TextBox textBox = sender as TextBox;
            if (string.IsNullOrWhiteSpace(textBox.Text))
            {
                textBox.Text = "Email";
                textBox.ForeColor = Color.Gray;
            }
        }

        private void txt_classid_Enter(object sender, EventArgs e)
        {
            TextBox textBox = sender as TextBox;
            if (textBox.Text == "Class ID")
            {
                textBox.Text = "";
                textBox.ForeColor = Color.Black;
            }
        }

        private void txt_classid_Leave(object sender, EventArgs e)
        {
            TextBox textBox = sender as TextBox;
            if (string.IsNullOrWhiteSpace(textBox.Text))
            {
                textBox.Text = "Class ID";
                textBox.ForeColor = Color.Gray;
            }
        }

        private void txt_username_Enter(object sender, EventArgs e)
        {
            TextBox textBox = sender as TextBox;
            if (textBox.Text == "Username")
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
                textBox.Text = "Username";
                textBox.ForeColor = Color.Gray;
            }
        }

        private void txt_password_Enter(object sender, EventArgs e)
        {
            TextBox textBox = sender as TextBox;
            if (textBox.Text == "Password")
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
                textBox.Text = "Password";
                textBox.ForeColor = Color.Gray;
            }
        }

        private void txt_confirmpass_Enter(object sender, EventArgs e)
        {
            TextBox textBox = sender as TextBox;
            if (textBox.Text == "Confirm password")
            {
                textBox.Text = "";
                textBox.ForeColor = Color.Black;
                textBox.PasswordChar = '*';
            }
        }

        private void txt_confirmpass_Leave(object sender, EventArgs e)
        {
            TextBox textBox = sender as TextBox;
            if (string.IsNullOrWhiteSpace(textBox.Text))
            {
                textBox.PasswordChar = '\0';
                textBox.Text = "Confirm password";
                textBox.ForeColor = Color.Gray;
            }
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (cb_female.Checked)
            {
                cb_female.Checked = false;
            }
        }

        private void cb_male_CheckedChanged(object sender, EventArgs e)
        {
            if(cb_male.Checked) 
            {
                cb_female.Checked = false;
            }
        }

        private void link_Login_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Form1 form1 = new Form1();
            form1.Show();
            this.Hide();
        }
    }
}
