using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using MedicalCenter.AppointmentForm;
using MedicalCenter.Models;
using MedicalCenter.Services;

namespace MedicalCenter.MainForm
{
    public class AppointmentsSection
    {
        private readonly AppointmentService _appointmentService = new AppointmentService();
        private DataGridView? dgvAppointments;
        private Button? btnAddAppointment;
        private Button? btnDeleteAppointment;
        private Button? btnRefresh;
        private Button? btnUpdateStatus;
        private TextBox? txtSearch;
        private Button? btnSearch;

        public async Task ShowAppointmentsSection(Panel panelMain)
        {
            panelMain.Controls.Clear();

            dgvAppointments = new DataGridView
            {
                Dock = DockStyle.Top,
                Height = 350,
                ReadOnly = true,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
            };
            panelMain.Controls.Add(dgvAppointments);

            // Control panel for actions
            FlowLayoutPanel controlPanel = new FlowLayoutPanel
            {
                Dock = DockStyle.Bottom,
                Height = 90,
                FlowDirection = FlowDirection.LeftToRight,
                Padding = new Padding(10)
            };

            btnAddAppointment = new Button { Text = "Add Appointment", Width = 160, Height = 40 };
            btnDeleteAppointment = new Button { Text = "Delete", Width = 120, Height = 40 };
            btnRefresh = new Button { Text = "Refresh", Width = 120, Height = 40 };
            btnUpdateStatus = new Button { Text = "Update Status", Width = 150, Height = 40 };

            txtSearch = new TextBox { Width = 200, PlaceholderText = "Search by Doctor or Patient Name" };
            btnSearch = new Button { Text = "Search", Width = 100, Height = 40 };

            controlPanel.Controls.Add(btnAddAppointment);
            controlPanel.Controls.Add(btnDeleteAppointment);
            controlPanel.Controls.Add(btnUpdateStatus);
            controlPanel.Controls.Add(btnRefresh);
            controlPanel.Controls.Add(txtSearch);
            controlPanel.Controls.Add(btnSearch);

            panelMain.Controls.Add(controlPanel);

            // Wire up button events
            btnAddAppointment.Click += BtnAddAppointment_Click!;
            btnDeleteAppointment.Click += async (s, e) => await DeleteAppointment();
            btnRefresh.Click += async (s, e) => await LoadAppointmentsAsync();
            btnUpdateStatus.Click += async (s, e) => await UpdateAppointmentStatus();
            btnSearch.Click += async (s, e) => await SearchAppointments(txtSearch.Text);

            await LoadAppointmentsAsync();
        }

        private async Task LoadAppointmentsAsync()
        {
            txtSearch!.Text=string.Empty;
            try
            {
                var appointments = await _appointmentService.Getallappointments();
                dgvAppointments!.DataSource = appointments;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading appointments: {ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async Task DeleteAppointment()
        {
            if (dgvAppointments!.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select an appointment to delete.");
                return;
            }

            var selectedRow = dgvAppointments.SelectedRows[0];
            int appointmentId = Convert.ToInt32(selectedRow.Cells["AppointmentID"].Value);

            var confirm = MessageBox.Show($"Are you sure you want to delete appointment ID: {appointmentId}?",
                "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (confirm == DialogResult.Yes)
            {
                bool success = await _appointmentService.deleteAppointment(appointmentId);
                if (success)
                {
                    MessageBox.Show("✅ Appointment deleted successfully!");
                    await LoadAppointmentsAsync();
                }
                else
                {
                    MessageBox.Show("❌ Failed to delete appointment.");
                }
            }
        }

        private async Task UpdateAppointmentStatus()
        {
            if (dgvAppointments!.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select an appointment to update status.");
                return;
            }

            var selectedRow = dgvAppointments.SelectedRows[0];
            int appointmentId = Convert.ToInt32(selectedRow.Cells["AppointmentID"].Value);
            string currentStatus = selectedRow.Cells["Status"].Value?.ToString() ?? "Pending";

            using (var form = new UpdateStatus(currentStatus))
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    string newStatus = form.SelectedStatus;
                    bool success = await _appointmentService.updateStatus(appointmentId, newStatus);

                    if (success)
                    {
                        MessageBox.Show("✅ Status updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        await LoadAppointmentsAsync();
                    }
                    else
                    {
                        MessageBox.Show("❌ Failed to update status. Please ensure the status is valid.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void BtnAddAppointment_Click(object sender, EventArgs e)
        {
            addAppointment frm = new addAppointment();
            frm.ShowDialog();
        }

        private async Task SearchAppointments(string keyword)
        {
            if (string.IsNullOrWhiteSpace(keyword))
            {
                await LoadAppointmentsAsync();
                return;
            }

            try
            {
                var allAppointments = await _appointmentService.Getallappointments();
                var filtered = allAppointments.Where(a =>
                    a.DoctorName.Contains(keyword, StringComparison.OrdinalIgnoreCase) ||
                    a.PateintName.Contains(keyword, StringComparison.OrdinalIgnoreCase));

                dgvAppointments!.DataSource = filtered.ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Search failed: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
