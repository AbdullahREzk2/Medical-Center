using System;
using System.Threading.Tasks;
using System.Windows.Forms;
using MedicalCenter.Services;

namespace MedicalCenter.MainForm
{
    public static class UsersSection
    {
        private static readonly UserService _userService = new UserService();

        public static async void ShowUsersSection(Panel panelMain)
        {
            panelMain.Controls.Clear();

            // ===== DataGridView =====
            DataGridView dgvUsers = new DataGridView
            {
                Dock = DockStyle.Top,
                Height = 350,
                ReadOnly = true,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                MultiSelect = false
            };
            panelMain.Controls.Add(dgvUsers);

            // ===== Control Panel =====
            FlowLayoutPanel controlPanel = new FlowLayoutPanel
            {
                Dock = DockStyle.Bottom,
                Height = 100,
                FlowDirection = FlowDirection.LeftToRight,
                Padding = new Padding(10)
            };

            // ===== Buttons & Inputs =====
            Button btnDeleteUser = new Button { Text = "Delete User", Width = 120, Height = 40 };
            Button btnRefreshUsers = new Button { Text = "Refresh", Width = 120, Height = 40 };
            Button btnActivateUser = new Button { Text = "Activate User", Width = 130, Height = 40 };
            Button btnDeactivateUser = new Button { Text = "Deactivate User", Width = 140, Height = 40 };

            TextBox txtSearch = new TextBox { Width = 200, PlaceholderText = "Search by Email or National ID" };
            Button btnSearch = new Button { Text = "Search", Width = 100, Height = 40 };

            controlPanel.Controls.AddRange(new Control[]
            {
                btnDeleteUser, btnRefreshUsers, btnActivateUser, btnDeactivateUser,
                txtSearch, btnSearch
            });
            panelMain.Controls.Add(controlPanel);

            // ===== Events =====
            btnDeleteUser.Click += async (s, e) => await DeleteUser(dgvUsers);
            btnRefreshUsers.Click += async (s, e) =>
            {
                txtSearch.Text = string.Empty;
                await LoadUsersAsync(dgvUsers);
            };
            btnSearch.Click += async (s, e) =>
            {
                var users = await _userService.Search(txtSearch.Text);
                dgvUsers.DataSource = users;
            };
            btnActivateUser.Click += async (s, e) => await ActivateUserAsync(dgvUsers);
            btnDeactivateUser.Click += async (s, e) => await DeactivateUserAsync(dgvUsers);

            await LoadUsersAsync(dgvUsers);
        }

        private static async Task LoadUsersAsync(DataGridView dgv)
        {
            try
            {
                var users = await _userService.GetAllUserAysnc();
                dgv.DataSource = users;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading users: {ex.Message}");
            }
        }

        private static async Task DeleteUser(DataGridView dgv)
        {
            if (dgv.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a user to delete.");
                return;
            }

            string nationalID = dgv.SelectedRows[0].Cells["NationalID"].Value.ToString()!;
            var confirm = MessageBox.Show($"Are you sure you want to delete user with National ID {nationalID}?",
                "Confirm Delete", MessageBoxButtons.YesNo);

            if (confirm == DialogResult.Yes)
            {
                bool success = await _userService.DeleteUserByNationalID(nationalID);
                MessageBox.Show(success ? "User deleted successfully!" : "Failed to delete user.");
                await LoadUsersAsync(dgv);
            }
        }

        private static async Task ActivateUserAsync(DataGridView dgv)
        {
            if (dgv.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a user to activate.");
                return;
            }

            string nationalID = dgv.SelectedRows[0].Cells["NationalID"].Value.ToString()!;
            bool success = await _userService.ActivateUser(nationalID);
            MessageBox.Show(success ? "User activated successfully!" : "Failed to activate user.");
            await LoadUsersAsync(dgv);
        }

        private static async Task DeactivateUserAsync(DataGridView dgv)
        {
            if (dgv.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a user to deactivate.");
                return;
            }

            string nationalID = dgv.SelectedRows[0].Cells["NationalID"].Value.ToString()!;
            bool success = await _userService.DeActivateUser(nationalID);
            MessageBox.Show(success ? "User deactivated successfully!" : "Failed to deactivate user.");
            await LoadUsersAsync(dgv);
        }


    }
}
