using System;
using System.Drawing;
using System.Windows.Forms;
using MedicalCenter.Enums;
using MedicalCenter.Models;
using MedicalCenter.Services;

namespace MedicalCenter.DoctorForm
{
    public partial class AddDoctor : Form
    {
        private readonly DoctorService _doctorService = new DoctorService();

        // Define controls here to make them accessible in methods
        private TextBox? txtFName, txtLName, txtLicense, txtEmail, txtPassword, txtNationalID, txtPhone;
        private ComboBox? cbGender, cbSpecialization;
        private NumericUpDown? numShiftHours, numSalary;
        private DateTimePicker? dtpDOB;

        public AddDoctor()
        {
            InitializeComponent();
            SetupUI();
        }

        private void SetupUI()
        {
            this.Text = "Add New Doctor";
            this.BackColor = Color.WhiteSmoke;
            this.Size = new Size(850, 720);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Controls.Clear();

            // ==== Title ====
            Label lblTitle = new Label
            {
                Text = "🩺 Add New Doctor",
                Font = new Font("Segoe UI", 18, FontStyle.Bold),
                ForeColor = Color.FromArgb(30, 60, 114),
                AutoSize = true,
                Location = new Point(280, 20)
            };
            this.Controls.Add(lblTitle);

            // ==== Doctor Info ====
            GroupBox grpDoctorInfo = new GroupBox
            {
                Text = "Doctor Information",
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                ForeColor = Color.FromArgb(30, 60, 114),
                Location = new Point(40, 80),
                Size = new Size(750, 200),
                BackColor = Color.White
            };
            this.Controls.Add(grpDoctorInfo);

            // Specialization
            Label lblSpecialization = new Label { Text = "Specialization:", Location = new Point(30, 40), AutoSize = true };
            cbSpecialization = new ComboBox
            {
                Location = new Point(160, 35),
                Width = 200,
                DropDownStyle = ComboBoxStyle.DropDownList
            };
            cbSpecialization.Items.AddRange(Enum.GetNames(typeof(SpecializationEnum)));
            grpDoctorInfo.Controls.Add(lblSpecialization);
            grpDoctorInfo.Controls.Add(cbSpecialization);

            // Shift Hours
            Label lblShiftHours = new Label { Text = "Shift Hours:", Location = new Point(400, 40), AutoSize = true };
            numShiftHours = new NumericUpDown
            {
                Location = new Point(520, 35),
                Width = 150,
                Minimum = 1,
                Maximum = 24
            };
            grpDoctorInfo.Controls.Add(lblShiftHours);
            grpDoctorInfo.Controls.Add(numShiftHours);

            // Salary
            Label lblSalary = new Label { Text = "Salary:", Location = new Point(30, 90), AutoSize = true };
            numSalary = new NumericUpDown
            {
                Location = new Point(160, 85),
                Width = 200,
                Minimum = 1000,
                Maximum = 100000
            };
            grpDoctorInfo.Controls.Add(lblSalary);
            grpDoctorInfo.Controls.Add(numSalary);

            // License
            Label lblLicense = new Label { Text = "License Number:", Location = new Point(400, 90), AutoSize = true };
            txtLicense = new TextBox { Location = new Point(520, 85), Width = 150 };
            grpDoctorInfo.Controls.Add(lblLicense);
            grpDoctorInfo.Controls.Add(txtLicense);

            // ==== User Info ====
            GroupBox grpUserInfo = new GroupBox
            {
                Text = "User Information",
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                ForeColor = Color.FromArgb(30, 60, 114),
                Location = new Point(40, 300),
                Size = new Size(750, 320),
                BackColor = Color.White
            };
            this.Controls.Add(grpUserInfo);

            // First Name
            Label lblFName = new Label { Text = "First Name:", Location = new Point(30, 40), AutoSize = true };
            txtFName = new TextBox { Location = new Point(160, 35), Width = 200 };
            grpUserInfo.Controls.Add(lblFName);
            grpUserInfo.Controls.Add(txtFName);

            // Last Name
            Label lblLName = new Label { Text = "Last Name:", Location = new Point(400, 40), AutoSize = true };
            txtLName = new TextBox { Location = new Point(520, 35), Width = 150 };
            grpUserInfo.Controls.Add(lblLName);
            grpUserInfo.Controls.Add(txtLName);

            // Gender
            Label lblGender = new Label { Text = "Gender:", Location = new Point(30, 90), AutoSize = true };
            cbGender = new ComboBox
            {
                Location = new Point(160, 85),
                Width = 200,
                DropDownStyle = ComboBoxStyle.DropDownList
            };
            cbGender.Items.AddRange(new string[] { "Male", "Female" });
            grpUserInfo.Controls.Add(lblGender);
            grpUserInfo.Controls.Add(cbGender);

            // DOB
            Label lblDOB = new Label { Text = "Date of Birth:", Location = new Point(400, 90), AutoSize = true };
            dtpDOB = new DateTimePicker
            {
                Location = new Point(520, 85),
                Width = 150,
                Format = DateTimePickerFormat.Short
            };
            grpUserInfo.Controls.Add(lblDOB);
            grpUserInfo.Controls.Add(dtpDOB);

            // Email
            Label lblEmail = new Label { Text = "Email:", Location = new Point(30, 140), AutoSize = true };
            txtEmail = new TextBox { Location = new Point(160, 135), Width = 510 };
            grpUserInfo.Controls.Add(lblEmail);
            grpUserInfo.Controls.Add(txtEmail);

            // ✅ Phone Number
            Label lblPhone = new Label { Text = "Phone Number:", Location = new Point(30, 190), AutoSize = true };
            txtPhone = new TextBox { Location = new Point(160, 185), Width = 510 };
            grpUserInfo.Controls.Add(lblPhone);
            grpUserInfo.Controls.Add(txtPhone);

            // National ID
            Label lblNationalID = new Label { Text = "National ID:", Location = new Point(30, 240), AutoSize = true };
            txtNationalID = new TextBox { Location = new Point(160, 235), Width = 200 };
            grpUserInfo.Controls.Add(lblNationalID);
            grpUserInfo.Controls.Add(txtNationalID);

            // Password
            Label lblPassword = new Label { Text = "Password:", Location = new Point(400, 240), AutoSize = true };
            txtPassword = new TextBox { Location = new Point(520, 235), Width = 150, UseSystemPasswordChar = true };
            grpUserInfo.Controls.Add(lblPassword);
            grpUserInfo.Controls.Add(txtPassword);

            // ==== Button ====
            Button btnAdd = new Button
            {
                Text = "Add Doctor",
                BackColor = Color.FromArgb(30, 60, 114),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 11, FontStyle.Bold),
                FlatStyle = FlatStyle.Flat,
                Size = new Size(200, 50),
                Location = new Point(320, 640),
                Cursor = Cursors.Hand
            };
            btnAdd.FlatAppearance.BorderSize = 0;
            btnAdd.Click += async (s, e) => await HandleAddDoctorAsync();
            this.Controls.Add(btnAdd);
        }

        private async Task HandleAddDoctorAsync()
        {
            try
            {
                var newDoctor = new DoctorInputModel
                {
                    FirstName = txtFName!.Text.Trim(),
                    LastName = txtLName!.Text.Trim(),
                    Phone = txtPhone!.Text.Trim(), 
                    Specialization = cbSpecialization!.SelectedItem?.ToString() ?? "",
                    LicenceNumber = txtLicense!.Text.Trim(),
                    ShiftHours = (int)numShiftHours!.Value,
                    Salary = numSalary!.Value,
                    User = new UserInputModel
                    {
                        NationalID = txtNationalID!.Text.Trim(),
                        Gender = cbGender!.SelectedItem?.ToString() ?? "",
                        DOB = dtpDOB!.Value,
                        Email = txtEmail!.Text.Trim(),
                        Password = txtPassword!.Text.Trim(),
                        Role = "Doctor"
                    }
                };


                var createdDoctor = await _doctorService.AddNewDoctor(newDoctor);
                MessageBox.Show($"✅ Doctor {createdDoctor.FirstName} {createdDoctor.LastName} added successfully!");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"❌ Failed to add doctor: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void AddDoctor_Load(object sender, EventArgs e)
        {
           
        }

    }
}
