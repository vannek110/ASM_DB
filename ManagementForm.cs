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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace StudentAttendanceSystem
{
    public partial class ManagementForm : Form
    {
        private SqlConnection connection;
        private string currentUserType = "Student"; // Mặc định là Student
        public ManagementForm()
        {
            InitializeComponent();
            // Khởi tạo kết nối

            string connectionString = "Data Source=DESKTOP-7STFHO0\\SQLEXPRESS;Initial Catalog=Test3;Integrated Security=True;";
            connection = new SqlConnection(connectionString);
            this.StartPosition = FormStartPosition.CenterScreen;
        }
      
        
        private void ManagementForm_Load(object sender, EventArgs e)
        {
            dataGridViewUsers.SelectionChanged += dataGridViewUsers_SelectionChanged;
            dgvSchedule.SelectionChanged += dgvSchedule_SelectionChanged;
            LoadClasses();
            InitializeComboBoxes();
            LoadComboBoxData();
        }

        private void btnStudent_Click(object sender, EventArgs e)
        {
            LoadStudents();
            currentUserType = "Student";
        }
        private void btnLecturer_Click_1(object sender, EventArgs e)
        {
            currentUserType = "Lecturer";
            LoadLecturers();
        }
        private void btnAddUser_Click_1(object sender, EventArgs e)
        {
            if (currentUserType == "Student")
            {
                AddStudent();
            }
            else 
            {
                AddLecturer();
            }
            
        }

        private void btnEditUser_Click_1(object sender, EventArgs e)
        {          
            if (currentUserType == "Student")
            {
                EditStudent();
            }
            else
            {
                EditLecturer();
            }
        }

        private void btnDeleteUser_Click_1(object sender, EventArgs e)
        {         
            if (currentUserType == "Student")
            {
                DeleteStudent();
            }
            else
            {
                DeleteLecturer();
            }
        }

        private void LoadStudents()
        {
            try
            {
                string query = "SELECT UserID, Fullname, Email, Gender, DateOfBirth, ClassID FROM Student";
                SqlDataAdapter adapter = new SqlDataAdapter(query, connection);

                // Tạo DataTable để lưu dữ liệu
                DataTable dataTable = new DataTable();

                // Mở kết nối trước khi nạp dữ liệu
                connection.Open();
                adapter.Fill(dataTable);
                connection.Close();

                // Gán dữ liệu cho DataGridView
                dataGridViewUsers.DataSource = dataTable;
                ConfigureDataGridView();
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
            }
        }

        private void LoadLecturers()
        {
            try
            {
                string query = "SELECT UserID, Fullname, Email, Gender, DateOfBirth FROM Lecturer";
                SqlDataAdapter adapter = new SqlDataAdapter(query, connection);


                // Tạo DataTable để lưu dữ liệu
                DataTable dataTable = new DataTable();

                // Mở kết nối trước khi nạp dữ liệu
                connection.Open();
                adapter.Fill(dataTable);
                connection.Close();

                // Gán dữ liệu cho DataGridView
                dataGridViewUsers.DataSource = dataTable;
                ConfigureDataGridView();
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
            }
        }
        private void LoadClasses()
        {
            string query = "SELECT ClassID, ClassName FROM Class";
            SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
            DataTable dataTable = new DataTable();

            connection.Open();
            adapter.Fill(dataTable);
            connection.Close();

            cmbClass.DisplayMember = "ClassName";
            cmbClass.ValueMember = "ClassID";
            cmbClass.DataSource = dataTable;
        }
        private void dataGridViewUsers_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridViewUsers.SelectedRows.Count > 0)
            {
                DataGridViewRow row = dataGridViewUsers.SelectedRows[0];

                // Lấy UserID từ dòng đã chọn
                lblUserID.Text = row.Cells["UserID"].Value.ToString();

                // Phân biệt loại người dùng (Student hoặc Lecturer)
                if (currentUserType == "Student")
                {
                    // Cập nhật thông tin cho Student
                    txtFullname.Text = row.Cells["Fullname"].Value.ToString();
                    txtEmail.Text = row.Cells["Email"].Value.ToString();
                    cmbClass.SelectedValue = row.Cells["ClassID"].Value;
                    cmbGender.SelectedItem = row.Cells["Gender"].Value.ToString();

                    // Cập nhật ngày sinh
                    DateTime dateOfBirth;
                    if (DateTime.TryParse(row.Cells["DateOfBirth"].Value.ToString(), out dateOfBirth))
                    {
                        cmbDay.SelectedItem = dateOfBirth.Day;
                        cmbMonth.SelectedItem = dateOfBirth.Month;
                        cmbYear.SelectedItem = dateOfBirth.Year;
                    }
                    else
                    {
                        cmbDay.SelectedItem = null;
                        cmbMonth.SelectedItem = null;
                        cmbYear.SelectedItem = null;
                    }
                }
                else if (currentUserType == "Lecturer")
                {
                    // Cập nhật thông tin cho Lecturer
                    txtFullname.Text = row.Cells["Fullname"].Value.ToString();
                    txtEmail.Text = row.Cells["Email"].Value.ToString();
                    cmbGender.SelectedItem = row.Cells["Gender"].Value.ToString();

                    // Cập nhật ngày sinh
                    DateTime dateOfBirth;
                    if (DateTime.TryParse(row.Cells["DateOfBirth"].Value.ToString(), out dateOfBirth))
                    {
                        cmbDay.SelectedItem = dateOfBirth.Day;
                        cmbMonth.SelectedItem = dateOfBirth.Month;
                        cmbYear.SelectedItem = dateOfBirth.Year;
                    }
                    else
                    {
                        cmbDay.SelectedItem = null;
                        cmbMonth.SelectedItem = null;
                        cmbYear.SelectedItem = null;
                    }
                }
            }
        }
        private void ConfigureDataGridView()
        {
            foreach (DataGridViewColumn column in dataGridViewUsers.Columns)
            {
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }
            
        }


        private void InitializeComboBoxes()
        {
            // Khởi tạo Day ComboBox
            for (int i = 1; i <= 31; i++)
            {
                cmbDay.Items.Add(i);
            }
            cmbDay.SelectedIndex = 0; // Chọn giá trị mặc định

            // Khởi tạo Month ComboBox
            for (int i = 1; i <= 12; i++)
            {
                cmbMonth.Items.Add(i);
            }
            cmbMonth.SelectedIndex = 0; // Chọn giá trị mặc định

            // Khởi tạo Year ComboBox
            for (int i = DateTime.Now.Year; i >= 1900; i--)
            {
                cmbYear.Items.Add(i);
            }
            cmbYear.SelectedIndex = 0; // Chọn giá trị mặc định
        }
        private void cmbDay_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateDateOfBirth();
        }

        private void cmbMonth_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateDateOfBirth();
        }

        private void cmbYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateDateOfBirth();
        }

        private void UpdateDateOfBirth()
        {
            int day = Convert.ToInt32(cmbDay.SelectedItem);
            int month = Convert.ToInt32(cmbMonth.SelectedItem);
            int year = Convert.ToInt32(cmbYear.SelectedItem);

            try
            {
                DateTime dateOfBirth = new DateTime(year, month, day);
              
            }
            catch (Exception ex)
            {
                // Xử lý lỗi nếu ngày không hợp lệ (ví dụ: 30 tháng 2)
                MessageBox.Show("Invalid date selected: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private DateTime GetDateOfBirth()
        {
            int day = int.Parse(cmbDay.SelectedItem.ToString());
            int month = int.Parse(cmbMonth.SelectedItem.ToString());
            int year = int.Parse(cmbYear.SelectedItem.ToString());

            return new DateTime(year, month, day);
        }
        private void AddStudent()
        {
            try
            {
                // Chèn người dùng vào bảng [User]
                string query = "INSERT INTO [User] (username, password, role) VALUES (@username, @password, 'student'); SELECT SCOPE_IDENTITY();";
                int userID;
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@username", txtUsername.Text);
                    command.Parameters.AddWithValue("@password", txtPassword.Text); // Nên mã hóa mật khẩu

                    connection.Open();
                    userID = Convert.ToInt32(command.ExecuteScalar()); // Lấy UserID vừa chèn
                    connection.Close();
                }

                // Thêm vào bảng Student
                query = "INSERT INTO Student (UserID, Fullname, Email, ClassID, Gender, DateOfBirth) VALUES (@UserID, @Fullname, @Email, @ClassID, @Gender, @DateOfBirth)";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@UserID", userID);
                    command.Parameters.AddWithValue("@Fullname", txtFullname.Text);
                    command.Parameters.AddWithValue("@Email", txtEmail.Text);
                    command.Parameters.AddWithValue("@ClassID", cmbClass.SelectedValue);
                    command.Parameters.AddWithValue("@Gender", cmbGender.SelectedItem);
                    command.Parameters.AddWithValue("@DateOfBirth", GetDateOfBirth());

                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();
                }

                MessageBox.Show("User added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadStudents(); // Cập nhật dữ liệu trong DataGridView sau khi thêm mới
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void EditStudent()
        {
            try
            {
                // Lấy UserID từ lblUserID
                int userID;
                if (!int.TryParse(lblUserID.Text, out userID))
                {
                    MessageBox.Show("Invalid UserID.");
                    return;
                }

                // Cập nhật thông tin sinh viên
                string query = "UPDATE Student SET Fullname = @Fullname, Email = @Email, ClassID = @ClassID, Gender = @Gender,DateOfBirth = @DateOfBirth WHERE UserID = @UserID";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Fullname", txtFullname.Text);
                    command.Parameters.AddWithValue("@Email", txtEmail.Text);
                    command.Parameters.AddWithValue("@ClassID", cmbClass.SelectedValue);
                    command.Parameters.AddWithValue("@Gender", cmbGender.SelectedItem);
                    command.Parameters.AddWithValue("@DateOfBirth", GetDateOfBirth());
                    command.Parameters.AddWithValue("@UserID", userID);

                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();
                }
                MessageBox.Show("User Edit successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadStudents();
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
        }
        private void DeleteStudent()
        {
            try
            {
                // Lấy UserID từ lblUserID
                int userID;
                if (!int.TryParse(lblUserID.Text, out userID))
                {
                    MessageBox.Show("Invalid UserID.");
                    return;
                }

                // Xóa từ bảng Student
                string query = "DELETE FROM Student WHERE UserID = @UserID";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@UserID", userID);

                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();
                }

                // Xóa từ bảng User
                query = "DELETE FROM [User] WHERE UserID = @UserID";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@UserID", userID);

                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();
                }
                MessageBox.Show("User Delete successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadStudents();
            }
            catch(Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void AddLecturer()
        {
            try
            {
                // Chèn người dùng vào bảng [User]
                string query = "INSERT INTO [User] (username, password, role) VALUES (@username, @password, 'Lecturer'); SELECT SCOPE_IDENTITY();";
                int userID;
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@username", txtUsername.Text);
                    command.Parameters.AddWithValue("@password", txtPassword.Text); // Nên mã hóa mật khẩu

                    connection.Open();
                    userID = Convert.ToInt32(command.ExecuteScalar()); // Lấy UserID vừa chèn
                    connection.Close();
                }

                // Thêm vào bảng Student
                query = "INSERT INTO Lecturer (UserID, Fullname, DateOfBirth, Email, Gender) VALUES (@UserID, @Fullname, @DateOfBirth, @Email, @Gender)";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@UserID", userID);
                    command.Parameters.AddWithValue("@Fullname", txtFullname.Text);
                    command.Parameters.AddWithValue("@DateOfBirth", GetDateOfBirth());
                    command.Parameters.AddWithValue("@Email", txtEmail.Text);
                    command.Parameters.AddWithValue("@Gender", cmbGender.SelectedItem);
                    

                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();
                }

                MessageBox.Show("Lecturer added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadLecturers(); // Cập nhật dữ liệu trong DataGridView sau khi thêm mới
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void EditLecturer()
        {
            try
            {

                int userID;
                if (!int.TryParse(lblUserID.Text, out userID))
                {
                    MessageBox.Show("Invalid UserID.");
                    return;
                }


                string query = "UPDATE Lecturer SET Fullname = @Fullname,DateOfBirth = @DateOfBirth, Email = @Email , Gender = @Gender WHERE UserID = @UserID";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Fullname", txtFullname.Text);
                    command.Parameters.AddWithValue("@DateOfBirth", GetDateOfBirth());
                    command.Parameters.AddWithValue("@Email", txtEmail.Text);
                    command.Parameters.AddWithValue("@Gender", cmbGender.SelectedItem);
                    command.Parameters.AddWithValue("@UserID", userID);

                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();
                }
                MessageBox.Show("Lecturer Edit successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadLecturers();
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void DeleteLecturer()
        {
            try
            {
                // Lấy UserID từ lblUserID
                int userID;
                if (!int.TryParse(lblUserID.Text, out userID))
                {
                    MessageBox.Show("Invalid UserID.");
                    return;
                }

                // Xóa từ bảng Lecturer
                string query = "DELETE FROM Lecturer WHERE UserID = @UserID";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@UserID", userID);

                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();
                }

                // Xóa từ bảng User
                query = "DELETE FROM [User] WHERE UserID = @UserID";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@UserID", userID);

                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();
                }
                MessageBox.Show("Lecturer Delete successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadLecturers();
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //-----------------------------------------ScheduleManagement--------------------------------------------
        private string GetLecturerName(int userID)
        {
            string lecturerName = "";
            string query = "SELECT Fullname FROM Lecturer WHERE UserID = @UserID";

            using (SqlCommand cmd = new SqlCommand(query, connection))
            {
                cmd.Parameters.AddWithValue("@UserID", userID);
                connection.Open();
                object result = cmd.ExecuteScalar();
                if (result != null)
                {
                    lecturerName = result.ToString();
                }
                connection.Close();
            }

            return lecturerName;
        }

        private string GetClassName(int classID)
        {
            string className = "";
            string query = "SELECT ClassName FROM Class WHERE ClassID = @ClassID";

            using (SqlCommand cmd = new SqlCommand(query, connection))
            {
                cmd.Parameters.AddWithValue("@ClassID", classID);
                connection.Open();
                object result = cmd.ExecuteScalar();
                if (result != null)
                {
                    className = result.ToString();
                }
                connection.Close();
            }

            return className;
        }

        private string GetSubjectName(int subjectID)
        {
            string subjectName = "";
            string query = "SELECT SubjectName FROM Subject WHERE SubID = @SubID";

            using (SqlCommand cmd = new SqlCommand(query, connection))
            {
                cmd.Parameters.AddWithValue("@SubID", subjectID);
                connection.Open();
                object result = cmd.ExecuteScalar();
                if (result != null)
                {
                    subjectName = result.ToString();
                }
                connection.Close();
            }

            return subjectName;
        }



        private DataTable GetScheduleData()
        {
            DataTable scheduleTable = new DataTable();
            string query = "SELECT ScheID, Date, StartTime, EndTime, UserID, ClassID, SubID FROM Schedule";

            try
            {
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);

                    // Mở kết nối
                    connection.Open();
                    adapter.Fill(scheduleTable);
                }
            }
            catch (Exception ex)
            {
                // Xử lý lỗi, ví dụ: hiển thị thông báo lỗi
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                // Đảm bảo kết nối được đóng lại
                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
            }

            return scheduleTable;
        }


        private void LoadScheduleData()
        {
            DataTable dtSchedule = GetScheduleData();
            DataTable dtWithNames = new DataTable();
            dtWithNames.Columns.Add("ScheduleID", typeof(int));
            dtWithNames.Columns.Add("Date", typeof(DateTime));
            dtWithNames.Columns.Add("StartTime", typeof(TimeSpan));
            dtWithNames.Columns.Add("EndTime", typeof(TimeSpan));
            dtWithNames.Columns.Add("LecturerName", typeof(string));
            dtWithNames.Columns.Add("ClassName", typeof(string));
            dtWithNames.Columns.Add("SubjectName", typeof(string));

            foreach (DataRow row in dtSchedule.Rows)
            {
                DataRow newRow = dtWithNames.NewRow();
                newRow["ScheduleID"] = row["ScheID"];
                newRow["Date"] = row["Date"];
                newRow["StartTime"] = row["StartTime"];
                newRow["EndTime"] = row["EndTime"];

                // Thay đổi các phương thức truy vấn của bạn để lấy tên
                string lecturerName = GetLecturerName(Convert.ToInt32(row["UserID"]));
                string className = GetClassName(Convert.ToInt32(row["ClassID"]));
                string subjectName = GetSubjectName(Convert.ToInt32(row["SubID"]));

                newRow["LecturerName"] = lecturerName;
                newRow["ClassName"] = className;
                newRow["SubjectName"] = subjectName;

                dtWithNames.Rows.Add(newRow);
            }

            dgvSchedule.DataSource = dtWithNames;
        }


        private void dgvSchedule_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvSchedule.SelectedRows.Count > 0)
            {
                DataGridViewRow selectedRow = dgvSchedule.SelectedRows[0];

                // Get the values from the selected row
                int scheduleID = Convert.ToInt32(selectedRow.Cells["ScheduleID"].Value);
                string lecturerName = selectedRow.Cells["LecturerName"].Value.ToString();
                string className = selectedRow.Cells["ClassName"].Value.ToString();
                string subjectName = selectedRow.Cells["SubjectName"].Value.ToString();
                DateTime date = Convert.ToDateTime(selectedRow.Cells["Date"].Value);
                TimeSpan startTime = (TimeSpan)selectedRow.Cells["StartTime"].Value;
                TimeSpan endTime = (TimeSpan)selectedRow.Cells["EndTime"].Value;

                // Set the selected values in the TextBoxes and ComboBoxes
                txtDate.Text = date.ToString("yyyy-MM-dd");
                txtStartTime.Text = startTime.ToString(@"hh\:mm");
                txtEndTime.Text = endTime.ToString(@"hh\:mm");

                // Set the ComboBox selections
                cmbLecturer.SelectedIndex = cmbLecturer.Items.Cast<DataRowView>()
                    .Select((item, index) => new { item, index })
                    .FirstOrDefault(x => x.item.Row["Fullname"].ToString() == lecturerName)?.index ?? -1;

                cmb_Class.SelectedIndex = cmb_Class.Items.Cast<DataRowView>()
                    .Select((item, index) => new { item, index })
                    .FirstOrDefault(x => x.item.Row["ClassName"].ToString() == className)?.index ?? -1;

                cmbSubject.SelectedIndex = cmbSubject.Items.Cast<DataRowView>()
                    .Select((item, index) => new { item, index })
                    .FirstOrDefault(x => x.item.Row["SubjectName"].ToString() == subjectName)?.index ?? -1;
            }
        }

        private void AddSchedule()
        {
            try
            {
                string query = "INSERT INTO Schedule (Date, StartTime, EndTime, UserID, ClassID, SubID) VALUES (@Date, @StartTime, @EndTime, @LecturerID, @ClassID, @SubjectID)";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Date", DateTime.Parse(txtDate.Text));
                    command.Parameters.AddWithValue("@StartTime", TimeSpan.Parse(txtStartTime.Text));
                    command.Parameters.AddWithValue("@EndTime", TimeSpan.Parse(txtEndTime.Text));
                    command.Parameters.AddWithValue("@LecturerID", cmbLecturer.SelectedValue);
                    command.Parameters.AddWithValue("@ClassID", cmb_Class.SelectedValue);
                    command.Parameters.AddWithValue("@SubjectID", cmbSubject.SelectedValue);

                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();
                }

                MessageBox.Show("Schedule added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadScheduleData(); // Reload data to reflect changes
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while adding the schedule: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void EditSchedule()
        {
            try
            {
                string query = "UPDATE Schedule SET Date = @Date, StartTime = @StartTime, EndTime = @EndTime, UserID = @LecturerID, ClassID = @ClassID, SubID = @SubjectID WHERE ScheID = @ScheduleID";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Date", DateTime.Parse(txtDate.Text));
                    command.Parameters.AddWithValue("@StartTime", TimeSpan.Parse(txtStartTime.Text));
                    command.Parameters.AddWithValue("@EndTime", TimeSpan.Parse(txtEndTime.Text));
                    command.Parameters.AddWithValue("@LecturerID", cmbLecturer.SelectedValue);
                    command.Parameters.AddWithValue("@ClassID", cmb_Class.SelectedValue);
                    command.Parameters.AddWithValue("@SubjectID", cmbSubject.SelectedValue);
                    command.Parameters.AddWithValue("@ScheduleID", GetSelectedScheduleID());

                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();
                }

                MessageBox.Show("Schedule updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadScheduleData(); // Reload data to reflect changes
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while updating the schedule: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void DeleteSchedule()
        {
            try
            {
                int scheduleID = GetSelectedScheduleID();
                if (scheduleID <= 0)
                {
                    MessageBox.Show("Invalid ScheduleID.");
                    return;
                }

                string query = "DELETE FROM Schedule WHERE UserID = @ScheduleID";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ScheduleID", scheduleID);

                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();
                }

                MessageBox.Show("Schedule deleted successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadScheduleData(); // Reload data to reflect changes
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while deleting the schedule: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void LoadComboBoxData()
        {
            try
            {
                // Load Lecturer data
                string query = "SELECT UserID, Fullname FROM Lecturer";
                SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                DataTable lecturerTable = new DataTable();
                connection.Open();
                adapter.Fill(lecturerTable);
                connection.Close();
                cmbLecturer.DisplayMember = "Fullname";
                cmbLecturer.ValueMember = "UserID";
                cmbLecturer.DataSource = lecturerTable;

                // Load Class data
                query = "SELECT ClassID, ClassName FROM Class";
                adapter = new SqlDataAdapter(query, connection);
                DataTable classTable = new DataTable();
                connection.Open();
                adapter.Fill(classTable);
                connection.Close();
                cmb_Class.DisplayMember = "ClassName";
                cmb_Class.ValueMember = "ClassID";
                cmb_Class.DataSource = classTable;

                // Load Subject data
                query = "SELECT SubID, SubjectName FROM Subject";
                adapter = new SqlDataAdapter(query, connection);
                DataTable subjectTable = new DataTable();
                connection.Open();
                adapter.Fill(subjectTable);
                connection.Close();
                cmbSubject.DisplayMember = "SubjectName";
                cmbSubject.ValueMember = "SubID";
                cmbSubject.DataSource = subjectTable;
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while loading combo box data: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private int GetSelectedScheduleID()
        {
            if (dgvSchedule.SelectedRows.Count > 0)
            {
                DataGridViewRow selectedRow = dgvSchedule.SelectedRows[0];
                return Convert.ToInt32(selectedRow.Cells["ScheduleID"].Value);
            }
            else
            {
                MessageBox.Show("Please select a schedule to edit or delete.", "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return -1; 
            }
        }

        private void btnDeleteSchedule_Click(object sender, EventArgs e)
        {
            DeleteSchedule();
        }

        private void btnAddSchedule_Click(object sender, EventArgs e)
        {
            AddSchedule();
        }

        private void btnEditSchedule_Click(object sender, EventArgs e)
        {
            EditSchedule();
        }

        private void btn_schedule_Click(object sender, EventArgs e)
        {
            LoadScheduleData();
        }
    }
}
