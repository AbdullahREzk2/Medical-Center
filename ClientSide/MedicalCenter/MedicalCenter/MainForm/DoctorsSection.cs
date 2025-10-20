using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MedicalCenter.Services;

namespace MedicalCenter.MainForm
{
    public class DoctorsSection
    {
        private static readonly DoctorService _doctorService = new DoctorService();

        private static async Task LoadDoctorsAsync(DataGridView dgv)
        {
          
            try
            {
                var doctors = await _doctorService.GetAllDoctors();
                dgv.DataSource = doctors;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading doctors: {ex.Message}");
            }
        }

        public static async void ShowDoctorsSection(Panel panelMain)
        {
            panelMain.Controls.Clear();

            DataGridView dgvDoctors = new DataGridView
            {
                Dock = DockStyle.Top,
                Height = 350,
                ReadOnly = true,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
            };
            panelMain.Controls.Add(dgvDoctors);

            FlowLayoutPanel controlPanel = new FlowLayoutPanel
            {
                Dock = DockStyle.Bottom,
                Height = 80,
                FlowDirection = FlowDirection.LeftToRight,
                Padding = new Padding(10)
            };

            Button btnAdd = new Button { Text = "Add Doctor", Width = 120, Height = 40 };
            Button btnDelete = new Button { Text = "Delete Doctor", Width = 120, Height = 40 };
            Button btnRefresh = new Button { Text = "Refresh", Width = 120, Height = 40 };
            TextBox txtSearch = new TextBox { Width = 200, PlaceholderText = "Search by Name or Phone" };
            Button btnSearch = new Button { Text = "Search", Width = 100, Height = 40 };

            controlPanel.Controls.AddRange(new Control[] { btnAdd, btnDelete, btnRefresh, txtSearch, btnSearch });
            panelMain.Controls.Add(controlPanel);

            btnAdd.Click += (s, e) => new DoctorForm.AddDoctor().ShowDialog();
            btnDelete.Click += async (s, e) => await DeleteDoctor(dgvDoctors);
            btnRefresh.Click += async (s, e) =>
            {
                txtSearch.Text = string.Empty;
                await LoadDoctorsAsync(dgvDoctors);
            };
            btnSearch.Click += async (s, e) =>
            {
                var patients = await _doctorService.Search(txtSearch.Text);
                dgvDoctors.DataSource = patients;
            };

            await LoadDoctorsAsync(dgvDoctors);
        }


        private static async Task DeleteDoctor(DataGridView dgv)
        {
            if (dgv.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a doctor to delete.");
                return;
            }

            int id = Convert.ToInt32(dgv.SelectedRows[0].Cells["DoctorID"].Value);
            var confirm = MessageBox.Show($"Are you sure you want to delete Doctor with ID {id}?",
                "Confirm Delete", MessageBoxButtons.YesNo);

            if (confirm == DialogResult.Yes)
            {
                bool success = await _doctorService.DeleteDoctor(id);
                MessageBox.Show(success ? "Doctor deleted successfully!" : "Failed to delete doctor.");
                await LoadDoctorsAsync(dgv);
            }
        }
    }
}
