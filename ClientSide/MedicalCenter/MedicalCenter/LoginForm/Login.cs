using System;
using System.Drawing;
using System.Windows.Forms;
using MedicalCenter.Auth;

namespace MedicalCenter.LoginForm
{
    public partial class Login : Form
    {
        private readonly AuthService _authService = new AuthService();

        public Login()
        {
            InitializeComponent();
        }

        private async void btnLogin_Click(object sender, EventArgs e)
        {
            lblStatus.Text = "";
            btnLogin.Enabled = false;

            try
            {
                var email = txtEmail.Text.Trim();
                var password = txtPassword.Text;

                if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
                {
                    ShowError("Please enter both email and password.");
                    return;
                }

                bool success = await _authService.LoginAsync(email, password);

                if (!success)
                {
                    ShowError("Invalid email or password.");
                    return;
                }

                lblStatus.ForeColor = Color.Green;
                lblStatus.Text = "Login successful!";

                var mainForm = new MainForm.Main();
                mainForm.Show();
                this.Hide();
            }
            catch (Exception ex)
            {
                ShowError($"Error: {ex.Message}");
            }
            finally
            {
                btnLogin.Enabled = true;
            }
        }

        private void chkShowPassword_CheckedChanged(object sender, EventArgs e)
        {
            txtPassword.UseSystemPasswordChar = !chkShowPassword.Checked;
        }

        private void ShowError(string message)
        {
            lblStatus.ForeColor = Color.Red;
            lblStatus.Text = message;
            btnLogin.Enabled = true;
        }

    }
}
