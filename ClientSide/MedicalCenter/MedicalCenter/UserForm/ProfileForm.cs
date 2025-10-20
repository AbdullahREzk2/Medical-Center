using System;
using System.Windows.Forms;
using MedicalCenter.Models;
using MedicalCenter.Services;

namespace MedicalCenter
{
    public partial class ProfileForm : Form
    {
        private readonly UserService _userService = new UserService();

        public ProfileForm()
        {
            InitializeComponent();
        }

        private async void ProfileForm_Load(object sender, EventArgs e)
        {
            try
            {
                var userId = SessionManager.UserId;
                if (userId == 0)
                {
                    MessageBox.Show("No user found in session. Please log in again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                var user = await _userService.getUserById(userId);
                if (user == null)
                {
                    MessageBox.Show("Failed to load profile data.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                lblEmail.Text = user.Email;
                lblNationalId.Text = user.NationalID;
                lblRole.Text = user.Role;
                lblStatus.Text = user.IsActive ? "Active" : "Inactive";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading profile: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


    }
}
