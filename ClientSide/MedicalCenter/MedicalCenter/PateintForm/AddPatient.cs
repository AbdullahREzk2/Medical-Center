using System;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using MedicalCenter.Models;
using MedicalCenter.Services;

namespace MedicalCenter.PatientForm
{
    public partial class AddPatient : Form
    {
        private readonly PatientsService _patientService = new PatientsService();

        // Define controls
        private TextBox? txtFName, txtLName, txtEmail, txtPassword, txtNationalID, txtPhone, txtMedicalHistory, txtAllergies;
        private ComboBox? cbGender;
        private DateTimePicker? dtpDOB;
        private Label? lblStatus;
        private Button? btn_Add;

        public AddPatient()
        {
            InitializeComponent();
            SetupUI();
        }

        private void SetupUI()
        {
            this.Text = "Add New Patient";
            this.BackColor = Color.WhiteSmoke;
            this.Size = new Size(850, 720);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Controls.Clear();

            // ==== Title ====
            Label lblTitle = new Label
            {
                Text = "🏥 Add New Patient",
                Font = new Font("Segoe UI", 18, FontStyle.Bold),
                ForeColor = Color.FromArgb(30, 60, 114),
                AutoSize = true,
                Location = new Point(290, 20)
            };
            this.Controls.Add(lblTitle);

            // ==== Patient Info ====
            GroupBox grpPatientInfo = new GroupBox
            {
                Text = "Patient Information",
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                ForeColor = Color.FromArgb(30, 60, 114),
                Location = new Point(40, 80),
                Size = new Size(750, 200),
                BackColor = Color.White
            };
            this.Controls.Add(grpPatientInfo);

            // Phone
            Label lblPhone = new Label { Text = "Phone:", Location = new Point(30, 40), AutoSize = true };
            txtPhone = new TextBox { Location = new Point(160, 35), Width = 200 };
            grpPatientInfo.Controls.Add(lblPhone);
            grpPatientInfo.Controls.Add(txtPhone);

            // Medical History
            Label lblHistory = new Label { Text = "Medical History:", Location = new Point(400, 40), AutoSize = true };
            txtMedicalHistory = new TextBox { Location = new Point(520, 35), Width = 180 };
            grpPatientInfo.Controls.Add(lblHistory);
            grpPatientInfo.Controls.Add(txtMedicalHistory);

            // Allergies
            Label lblAllergies = new Label { Text = "Allergies:", Location = new Point(30, 90), AutoSize = true };
            txtAllergies = new TextBox { Location = new Point(160, 85), Width = 540 };
            grpPatientInfo.Controls.Add(lblAllergies);
            grpPatientInfo.Controls.Add(txtAllergies);

            // ==== User Info ====
            GroupBox grpUserInfo = new GroupBox
            {
                Text = "User Information",
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                ForeColor = Color.FromArgb(30, 60, 114),
                Location = new Point(40, 300),
                Size = new Size(750, 280),
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

            // National ID
            Label lblNationalID = new Label { Text = "National ID:", Location = new Point(30, 190), AutoSize = true };
            txtNationalID = new TextBox { Location = new Point(160, 185), Width = 200 };
            grpUserInfo.Controls.Add(lblNationalID);
            grpUserInfo.Controls.Add(txtNationalID);

            // Password
            Label lblPassword = new Label { Text = "Password:", Location = new Point(400, 190), AutoSize = true };
            txtPassword = new TextBox { Location = new Point(520, 185), Width = 150, UseSystemPasswordChar = true };
            grpUserInfo.Controls.Add(lblPassword);
            grpUserInfo.Controls.Add(txtPassword);

            // ==== Button & Status ====
            btn_Add = new Button
            {
                Text = "Add Patient",
                BackColor = Color.FromArgb(30, 60, 114),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 11, FontStyle.Bold),
                FlatStyle = FlatStyle.Flat,
                Size = new Size(200, 50),
                Location = new Point(320, 600),
                Cursor = Cursors.Hand
            };
            btn_Add.FlatAppearance.BorderSize = 0;
            btn_Add.Click += async (s, e) => await HandleAddPatientAsync();
            this.Controls.Add(btn_Add);

            lblStatus = new Label
            {
                Text = "Adding patient...",
                ForeColor = Color.Gray,
                Location = new Point(320, 660),
                AutoSize = true,
                Visible = false
            };
            this.Controls.Add(lblStatus);
        }

        private async Task HandleAddPatientAsync()
        {
            try
            {
                var patientInput = new PatientInputModel
                {
                    FirstName = txtFName!.Text.Trim(),
                    LastName = txtLName!.Text.Trim(),
                    Phone = txtPhone!.Text.Trim(),
                    MedicalHistory = txtMedicalHistory!.Text.Trim(),
                    Allergies = txtAllergies!.Text.Trim(),
                    User = new UserInputModel
                    {
                        NationalID = txtNationalID!.Text.Trim(),
                        Gender = cbGender!.SelectedItem?.ToString() ?? "",
                        DOB = dtpDOB!.Value,
                        Email = txtEmail!.Text.Trim(),
                        Password = txtPassword!.Text.Trim(),
                        Role = "Patient"
                    }
                };

                btn_Add!.Enabled = false;
                lblStatus!.Visible = true;

                var response = await _patientService.AddNewPateint(patientInput);

                if (response != null)
                {
                    MessageBox.Show($"✅ Patient {response.FirstName} {response.LastName} added successfully!",
                        "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
                else
                {
                    MessageBox.Show("❌ Failed to add patient.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"❌ Failed to add patient: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                btn_Add!.Enabled = true;
                lblStatus!.Visible = false;
            }
        }
    }
}
