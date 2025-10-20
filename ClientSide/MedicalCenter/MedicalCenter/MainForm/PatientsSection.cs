using System;
using System.Threading.Tasks;
using System.Windows.Forms;
using MedicalCenter.DoctorForm;
using MedicalCenter.Services;

namespace MedicalCenter.MainForm
{
    public class PatientsSection
    {
        private static readonly PatientsService _patientService = new PatientsService();

        public static async void ShowPatientsSection(Panel panelMain)
        {
            panelMain.Controls.Clear();

            DataGridView dgvPatients = new DataGridView
            {
                Dock = DockStyle.Top,
                Height = 350,
                ReadOnly = true,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
            };
            panelMain.Controls.Add(dgvPatients);

            FlowLayoutPanel controlPanel = new FlowLayoutPanel
            {
                Dock = DockStyle.Bottom,
                Height = 80,
                FlowDirection = FlowDirection.LeftToRight,
                Padding = new Padding(10)
            };

            Button btnAdd = new Button { Text = "Add Patient", Width = 120, Height = 40 };
            Button btnDelete = new Button { Text = "Delete Patient", Width = 120, Height = 40 };
            Button btnRefresh = new Button { Text = "Refresh", Width = 120, Height = 40 };
            TextBox txtSearch = new TextBox { Width = 200, PlaceholderText = "Search by Name or Phone" };
            Button btnSearch = new Button { Text = "Search", Width = 100, Height = 40 };

            controlPanel.Controls.AddRange(new Control[] { btnAdd, btnDelete, btnRefresh, txtSearch, btnSearch });
            panelMain.Controls.Add(controlPanel);

            btnAdd.Click += (s, e) => new MedicalCenter.PatientForm.AddPatient().ShowDialog();
            btnDelete.Click += async (s, e) => await DeletePatient(dgvPatients);
            btnRefresh.Click += async (s, e) =>
            {
                txtSearch.Text = string.Empty;
                await LoadPatientsAsync(dgvPatients);
            };
            btnSearch.Click += async (s, e) =>
            {
                var patients = await _patientService.Search(txtSearch.Text);
                dgvPatients.DataSource = patients;
            };

            await LoadPatientsAsync(dgvPatients);
        }

        private static async Task LoadPatientsAsync(DataGridView dgv)
        {
            try
            {
                var patients = await _patientService.GetAllPateints();
                dgv.DataSource = patients;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading patients: {ex.Message}");
            }
        }

        private static async Task DeletePatient(DataGridView dgv)
        {
            if (dgv.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a patient to delete.");
                return;
            }

            int id = Convert.ToInt32(dgv.SelectedRows[0].Cells["PatientID"].Value);
            var confirm = MessageBox.Show($"Are you sure you want to delete patient with ID {id}?",
                "Confirm Delete", MessageBoxButtons.YesNo);

            if (confirm == DialogResult.Yes)
            {
                bool success = await _patientService.DeletePateintById(id);
                MessageBox.Show(success ? "Patient deleted successfully!" : "Failed to delete patient.");
                await LoadPatientsAsync(dgv);
            }
        }


    }
}
