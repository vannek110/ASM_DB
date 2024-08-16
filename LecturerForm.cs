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
    public partial class LecturerForm : Form
    {
        private string username;
        private int selectedScheduleId = 0;
        
        public LecturerForm(string username)
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            this.username = username;
            LoadSchedules();
            LoadLecturerName();
            dgvStudents.CellValueChanged += dgvStudents_CellValueChanged;
            dgvStudents.CellClick += dgvStudents_CellClick;
        }


        public class LecturerInfo
        {
            public string LecturerName { get; set; }
            public int UserID { get; set; }
        }
        private LecturerInfo GetLecturerInfo(string username)
        {
            LecturerInfo info = new LecturerInfo();

            using (SqlConnection connection = new SqlConnection("Data Source=DESKTOP-7STFHO0\\SQLEXPRESS;Initial Catalog=Test3;Integrated Security=True;"))
            {
                string query = @"
            SELECT l.Fullname, u.UserID 
            FROM [User] u 
            JOIN Lecturer l ON u.UserID = l.UserID 
            WHERE u.Username = @Username";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Username", username);
                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        info.LecturerName = reader["Fullname"].ToString();
                        info.UserID = Convert.ToInt32(reader["UserID"]);
                    }
                }
            }

            return info;
        }
        private void LoadLecturerName()
        {
            LecturerInfo info = GetLecturerInfo(username);
            string name = info.LecturerName;
            lecturerNameLabel.Text = "Lecturer Name: " +name;
        }
        
        private void LoadSchedules()
        {
            LecturerInfo info = GetLecturerInfo(username);
            int UserID = info.UserID;

            using (SqlConnection connection = new SqlConnection("Data Source=DESKTOP-7STFHO0\\SQLEXPRESS;Initial Catalog=Test3;Integrated Security=True;"))
            {
                string query = @"
                SELECT ScheID, 
                    CONCAT(CONVERT(VARCHAR, [Date], 103), ' ', CONVERT(VARCHAR, [StartTime], 108), ' - ', CONVERT(VARCHAR, [EndTime], 108)) AS ScheduleName
                FROM Schedule
                WHERE UserID = @UserID";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@UserID", UserID);
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable dataTable = new DataTable();

                try
                {
                    connection.Open();
                    adapter.Fill(dataTable);

                    cmbSchedule.DataSource = dataTable;
                    cmbSchedule.DisplayMember = "ScheduleName";
                    cmbSchedule.ValueMember = "ScheID";

                    // Đặt giá trị mặc định nếu có dữ liệu
                    if (cmbSchedule.Items.Count > 0)
                    {
                        cmbSchedule.SelectedIndex = 0;

                        // Cập nhật biến selectedScheduleId với giá trị ScheID của mục được chọn
                        selectedScheduleId = Convert.ToInt32(cmbSchedule.SelectedValue);

                        // Tải dữ liệu sinh viên cho lịch đã chọn
                        LoadStudentData(selectedScheduleId);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error loading schedules: " + ex.Message);
                }
            }
        }

        private void LoadStudentData(int scheduleId)
        {
            using (SqlConnection connection = new SqlConnection("Data Source=DESKTOP-7STFHO0\\SQLEXPRESS;Initial Catalog=Test3;Integrated Security=True;"))
            {
                string query = @"
        SELECT s.UserID, s.Fullname, 
               ISNULL(a.AttendanceStatus, 'Absent') AS AttendanceStatus
        FROM [Student] s
        LEFT JOIN [Attendance] a ON s.UserID = a.UserID AND a.ScheID = @ScheID";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@ScheID", scheduleId);
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable dataTable = new DataTable();

                try
                {
                    connection.Open();
                    adapter.Fill(dataTable);

                    // Thêm cột Absent và Present vào DataTable nếu chưa có
                    if (!dataTable.Columns.Contains("Absent"))
                        dataTable.Columns.Add("Absent", typeof(bool));
                    if (!dataTable.Columns.Contains("Present"))
                        dataTable.Columns.Add("Present", typeof(bool));

                    // Cập nhật cột Absent và Present dựa trên AttendanceStatus
                    foreach (DataRow row in dataTable.Rows)
                    {
                        string status = row["AttendanceStatus"].ToString();
                        if (status == "Absent")
                        {
                            row["Absent"] = true;
                            row["Present"] = false;
                        }
                        else if (status == "Attended")
                        {
                            row["Absent"] = false;
                            row["Present"] = true;
                        }
                    }

                    dgvStudents.DataSource = dataTable;
                    dgvStudents.Columns["AttendanceStatus"].Visible = false;
                    dgvStudents.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error loading student data: " + ex.Message);
                }
            }
        }

        private void cmbSchedule_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Cập nhật biến selectedScheduleId khi người dùng chọn lịch khác
            if (cmbSchedule.SelectedValue != null)
            {
                selectedScheduleId = Convert.ToInt32(cmbSchedule.SelectedValue);

                // Debug: Kiểm tra giá trị của selectedScheduleId
                Console.WriteLine("Selected Schedule ID: " + selectedScheduleId);
            }
        }



        private void SaveAttendance()
        {
            int scheduleId = Convert.ToInt32(cmbSchedule.SelectedValue);
            if (selectedScheduleId == 0)
            {
                MessageBox.Show("Please select a valid schedule.");
                return;
            }

            using (SqlConnection connection = new SqlConnection("Data Source=DESKTOP-7STFHO0\\SQLEXPRESS;Initial Catalog=Test3;Integrated Security=True;"))
            {
                connection.Open();
                SqlTransaction transaction = connection.BeginTransaction();

                try
                {
                    foreach (DataGridViewRow row in dgvStudents.Rows)
                    {
                        if (row.IsNewRow) continue;

                        // Kiểm tra và lấy giá trị UserID
                        int userId = row.Cells["UserID"].Value != DBNull.Value ? Convert.ToInt32(row.Cells["UserID"].Value) : 0;

                        // Kiểm tra và lấy giá trị Absent
                        bool isAbsent = row.Cells["Absent"].Value != DBNull.Value ? Convert.ToBoolean(row.Cells["Absent"].Value) : false;

                        // Kiểm tra và lấy giá trị Present
                        bool isPresent = row.Cells["Present"].Value != DBNull.Value ? Convert.ToBoolean(row.Cells["Present"].Value) : false;

                        // Xác định trạng thái điểm danh
                        string attendanceStatus = "";
                        if (isAbsent) attendanceStatus = "Absent";
                        if (isPresent) attendanceStatus = "Attended";

                        // Cập nhật hoặc thêm dữ liệu điểm danh vào cơ sở dữ liệu
                        string query = @"
MERGE INTO Attendance AS target
USING (SELECT @UserID AS UserID, @ScheduleID AS ScheduleID, @AttendanceStatus AS AttendanceStatus) AS source
ON (target.UserID = source.UserID AND target.ScheID = source.ScheduleID)
WHEN MATCHED THEN
    UPDATE SET AttendanceStatus = source.AttendanceStatus
WHEN NOT MATCHED THEN
    INSERT (ScheID, UserID, AttendanceStatus)
    VALUES (source.ScheduleID, source.UserID, source.AttendanceStatus);";

                        using (SqlCommand command = new SqlCommand(query, connection, transaction))
                        {
                            command.Parameters.AddWithValue("@UserID", userId);
                            command.Parameters.AddWithValue("@ScheduleID", scheduleId);
                            command.Parameters.AddWithValue("@AttendanceStatus", attendanceStatus);

                            command.ExecuteNonQuery();
                        }
                    }

                    transaction.Commit();
                    MessageBox.Show("Attendance saved successfully.");
                }
                catch (SqlException sqlEx)
                {
                    transaction.Rollback();
                    MessageBox.Show("SQL Error saving attendance: " + sqlEx.Message);
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    MessageBox.Show("Error saving attendance: " + ex.Message);
                }
            }
        }

        private void btn_LoadAttendance_Click(object sender, EventArgs e)
        {
            if (cmbSchedule.SelectedValue != null)
            {
                int scheduleId = Convert.ToInt32(cmbSchedule.SelectedValue);
                LoadStudentData(scheduleId);
            }
            else
            {
                MessageBox.Show("Please select a valid schedule.");
            }

        }

        private void LecturerForm_Load(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            ManagementForm managementForm = new ManagementForm();
            managementForm.Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form1 form = new Form1();
            form.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SaveAttendance();
        }

        

        private void dgvStudents_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                // Kiểm tra nếu thay đổi ở cột Absent
                if (dgvStudents.Columns[e.ColumnIndex].Name == "Absent")
                {
                    DataGridViewCell cell = dgvStudents.Rows[e.RowIndex].Cells["Absent"];
                    if (Convert.ToBoolean(cell.Value))
                    {
                        dgvStudents.Rows[e.RowIndex].Cells["Present"].Value = false;
                    }
                }
                // Kiểm tra nếu thay đổi ở cột Present
                else if (dgvStudents.Columns[e.ColumnIndex].Name == "Present")
                {
                    DataGridViewCell cell = dgvStudents.Rows[e.RowIndex].Cells["Present"];
                    if (Convert.ToBoolean(cell.Value))
                    {
                        dgvStudents.Rows[e.RowIndex].Cells["Absent"].Value = false;
                    }
                }
            }
        }
        private void dgvStudents_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                if (dgvStudents.Columns[e.ColumnIndex].Name == "Absent")
                {
                    // Bỏ chọn Present nếu Absent được chọn
                    dgvStudents.Rows[e.RowIndex].Cells["Present"].Value = false;
                }
                else if (dgvStudents.Columns[e.ColumnIndex].Name == "Present")
                {
                    // Bỏ chọn Absent nếu Present được chọn
                    dgvStudents.Rows[e.RowIndex].Cells["Absent"].Value = false;
                }
            }
        }

        private void dgvStudents_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
