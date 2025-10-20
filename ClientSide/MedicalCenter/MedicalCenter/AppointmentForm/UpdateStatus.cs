using System;
using System.Windows.Forms;
using MedicalCenter.Enums;

namespace MedicalCenter.AppointmentForm
{
    public partial class UpdateStatus : Form
    {
        public string SelectedStatus { get; private set; } = string.Empty;

        private ComboBox? comboStatus;
        private Button? btnSave;
        private Button? btnCancel;

        public UpdateStatus(string currentStatus)
        {
            InitializeComponent();
            InitializeUI(currentStatus);
        }

        private void InitializeUI(string currentStatus)
        {
            this.Text = "Update Appointment Status";
            this.Size = new System.Drawing.Size(350, 200);
            this.StartPosition = FormStartPosition.CenterParent;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;

            Label lblStatus = new Label
            {
                Text = "Select New Status:",
                Location = new System.Drawing.Point(30, 40),
                AutoSize = true,
                Font = new System.Drawing.Font("Segoe UI", 10, System.Drawing.FontStyle.Regular)
            };
            this.Controls.Add(lblStatus);

            comboStatus = new ComboBox
            {
                Location = new System.Drawing.Point(160, 38),
                Width = 130,
                DropDownStyle = ComboBoxStyle.DropDownList,
                Font = new System.Drawing.Font("Segoe UI", 10)
            };

            comboStatus.Items.AddRange(Enum.GetNames(typeof(AppointmentStatusEnum)));
            comboStatus.SelectedItem = currentStatus ?? "Pending";
            this.Controls.Add(comboStatus);

            btnSave = new Button
            {
                Text = "Save",
                Location = new System.Drawing.Point(70, 100),
                Width = 90,
                Height = 35,
                BackColor = System.Drawing.Color.MediumSeaGreen,
                ForeColor = System.Drawing.Color.White,
                FlatStyle = FlatStyle.Flat
            };
            btnSave.FlatAppearance.BorderSize = 0;
            btnSave.Click += (s, e) =>
            {
                SelectedStatus = comboStatus.SelectedItem?.ToString() ?? "Pending";
                this.DialogResult = DialogResult.OK;
                this.Close();
            };
            this.Controls.Add(btnSave);

            btnCancel = new Button
            {
                Text = "Cancel",
                Location = new System.Drawing.Point(180, 100),
                Width = 90,
                Height = 35,
                BackColor = System.Drawing.Color.LightGray,
                ForeColor = System.Drawing.Color.Black,
                FlatStyle = FlatStyle.Flat
            };
            btnCancel.FlatAppearance.BorderSize = 0;
            btnCancel.Click += (s, e) =>
            {
                this.DialogResult = DialogResult.Cancel;
                this.Close();
            };
            this.Controls.Add(btnCancel);
        }

        private void UpdateStatus_Load(object sender, EventArgs e)
        {
        }


    }
}
