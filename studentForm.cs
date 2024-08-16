using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace StudentAttendanceSystem
{
    public partial class studentForm : Form
    {
        private string username;

        public studentForm(string username)
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            this.username = username;
        }



        private void studentForm_Load(object sender, EventArgs e)
        {
            // Display StuID and ClassName
            DisplayStudentInfo();     

        }

        private void btn_load_Click(object sender, EventArgs e)
        {
            LoadAttendanceData();
        }


        private void DisplayStudentInfo()
        {
            using (SqlConnection conn = new SqlConnection("Data Source=DESKTOP-7STFHO0\\SQLEXPRESS;Initial Catalog=Test3;Integrated Security=True;"))
            {
                conn.Open();
                string query = @"
                SELECT 
                    s.Fullname AS [Student Name], 
                    c.ClassName AS [Class Name]
                FROM 
                    Student s
                    JOIN [Class] c ON s.ClassID = c.ClassID
                    JOIN [User] u ON s.UserID = u.UserID
                WHERE u.username = @username";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@username", username);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            if (reader.HasRows)
                            {
                                // Use column name as a string to avoid exceptions
                                lblStuName.Text = $"Student Name: {reader["Student Name"]}";
                                lblClassName.Text = $"Class Name: {reader["Class Name"]}";
                            }
                            else
                            {
                                // Handle the case when no data is returned
                                lblStuName.Text = "Student ID: Not found";
                                lblClassName.Text = "Class Name: Not found";
                            }
                        }
                        else
                        {
                            // Handle the case when no rows are returned
                            lblStuName.Text = "Student ID: Not found";
                            lblClassName.Text = "Class Name: Not found";
                        }
                    }
                }
            }
        }
        private void LoadAttendanceData()
        {
            using (SqlConnection conn = new SqlConnection("Data Source=DESKTOP-7STFHO0\\SQLEXPRESS;Initial Catalog=Test3;Integrated Security=True;"))
            {
                conn.Open();
                string query = @"
                SELECT 
                    s.Date,
                    s.StartTime,
                    s.EndTime,
                    l.Fullname AS Lecturer,
                    sub.SubjectName AS Subject,
                    a.AttendanceStatus As Status
                FROM 
                    Schedule s
                    JOIN Lecturer l ON s.UserID = l.UserID
                    JOIN Subject sub ON s.SubID = sub.SubID
                    JOIN Attendance a ON s.ScheID = a.ScheID
                    JOIN [Student] st ON a.UserID = st.UserID
                    JOIN [User] u ON st.UserID = u.UserID
                WHERE 
                    u.username = @username";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@username", username);
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    dataGridView1.DataSource = dt;
                }
            }
        }

        private void btn_exit_Click(object sender, EventArgs e)
        {
            Form1 form1 = new Form1();
            form1.Show();
            this.Hide();
        }
    }
}
