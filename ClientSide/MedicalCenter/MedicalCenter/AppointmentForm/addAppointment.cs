using System;
using System.Windows.Forms;
using MedicalCenter.Enums;
using MedicalCenter.Models;
using MedicalCenter.Services;

namespace MedicalCenter.AppointmentForm

{
    public partial class addAppointment : Form
    {
        private ComboBox? comboPatient;
        private ComboBox? comboDoctor;
        private DateTimePicker? dateTimeAppointment;
        private ComboBox? comboStatus;
        private TextBox? txtNotes;
        private Button? btnSave;
        private Button? btnCancel;
        private Label? lblTitle;

        public addAppointment()
        {
            InitializeComponent();
            InitializeUI();
        }

        private async void AddAppointmentForm_Load(object sender, EventArgs e)
        {
            var doctorService = new DoctorService();
            var patientService = new PatientsService();

            var doctors = await doctorService.GetAllDoctors();
            var patients = await patientService.GetAllPateints();

            // Show "ID - Name" for doctors
            comboDoctor!.DataSource = doctors.Select(d => new
            {
                Display = $"{d.DoctorID} - {d.FirstName + d.LastName}",
                Value = d.DoctorID
            }).ToList();
            comboDoctor.DisplayMember = "Display";
            comboDoctor.ValueMember = "Value";

            // Show "ID - Name" for patients
            comboPatient!.DataSource = patients.Select(p => new
            {
                Display = $"{p.PatientID} - {p.FirstName + p.LastName}",
                Value = p.PatientID
            }).ToList();
            comboPatient.DisplayMember = "Display";
            comboPatient.ValueMember = "Value";

            // Fill Status combo with enum names
            comboStatus!.DataSource = Enum.GetNames(typeof(AppointmentStatusEnum));
        }

        private void InitializeUI()
        {
            this.Text = "Add Appointment";
            this.Size = new System.Drawing.Size(500, 550);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = System.Drawing.Color.WhiteSmoke;

            lblTitle = new Label
            {
                Text = "Add New Appointment",
                Font = new System.Drawing.Font("Segoe UI", 16, System.Drawing.FontStyle.Bold),
                ForeColor = System.Drawing.Color.FromArgb(50, 50, 90),
                AutoSize = true,
                Location = new System.Drawing.Point(120, 30)
            };
            this.Controls.Add(lblTitle);

            Label lblPatient = new Label { Text = "Patient:", Location = new System.Drawing.Point(60, 100), AutoSize = true };
            comboPatient = new ComboBox { Location = new System.Drawing.Point(180, 95), Width = 200 };

            Label lblDoctor = new Label { Text = "Doctor:", Location = new System.Drawing.Point(60, 150), AutoSize = true };
            comboDoctor = new ComboBox { Location = new System.Drawing.Point(180, 145), Width = 200 };

            Label lblDate = new Label { Text = "Appointment Date:", Location = new System.Drawing.Point(60, 200), AutoSize = true };
            dateTimeAppointment = new DateTimePicker
            {
                Location = new System.Drawing.Point(180, 195),
                Width = 200,
                Format = DateTimePickerFormat.Custom,
                CustomFormat = "dd/MM/yyyy hh:mm tt"
            };

            Label lblStatus = new Label { Text = "Status:", Location = new System.Drawing.Point(60, 250), AutoSize = true };
            comboStatus = new ComboBox { Location = new System.Drawing.Point(180, 245), Width = 200 };

            Label lblNotes = new Label { Text = "Notes:", Location = new System.Drawing.Point(60, 300), AutoSize = true };
            txtNotes = new TextBox { Location = new System.Drawing.Point(180, 295), Width = 200, Height = 70, Multiline = true };

            // 🔹 Save Button
            btnSave = new Button
            {
                Text = "Save Appointment",
                Location = new System.Drawing.Point(100, 400),
                Width = 130,
                Height = 40,
                BackColor = System.Drawing.Color.MediumSeaGreen,
                ForeColor = System.Drawing.Color.White,
                FlatStyle = FlatStyle.Flat
            };
            btnSave.FlatAppearance.BorderSize = 0;
            btnSave.Click += btnSave_Click!;

            // 🔹 Cancel Button
            btnCancel = new Button
            {
                Text = "Cancel",
                Location = new System.Drawing.Point(260, 400),
                Width = 130,
                Height = 40,
                BackColor = System.Drawing.Color.LightGray,
                ForeColor = System.Drawing.Color.Black,
                FlatStyle = FlatStyle.Flat
            };
            btnCancel.FlatAppearance.BorderSize = 0;
            btnCancel.Click += (s, e) => this.Close();

            // Add controls to form
            this.Controls.Add(lblPatient);
            this.Controls.Add(comboPatient);
            this.Controls.Add(lblDoctor);
            this.Controls.Add(comboDoctor);
            this.Controls.Add(lblDate);
            this.Controls.Add(dateTimeAppointment);
            this.Controls.Add(lblStatus);
            this.Controls.Add(comboStatus);
            this.Controls.Add(lblNotes);
            this.Controls.Add(txtNotes);
            this.Controls.Add(btnSave);
            this.Controls.Add(btnCancel);

            this.Load += AddAppointmentForm_Load!;
        }

        private async void btnSave_Click(object sender, EventArgs e)
        {
            var appointment = new appointmentInputModel
            {
                DoctorID = (int)comboDoctor!.SelectedValue!,
                PatientID = (int)comboPatient!.SelectedValue!,
                AppointmentDate = dateTimeAppointment!.Value,
                Status = comboStatus!.SelectedItem!.ToString()!,
                Notes = txtNotes!.Text,
                CreatedAt = DateTime.UtcNow
            };

            var service = new AppointmentService();
            var result = await service.addNewAppointment(appointment);

            if (result != null)
            {
                MessageBox.Show("✅ Appointment added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            else
            {
                MessageBox.Show("❌ Failed to add appointment.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


    }
}
