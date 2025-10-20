using System;
using System.Threading.Tasks;
using System.Windows.Forms;
using MedicalCenter.LoginForm;
using MedicalCenter.MainForm;
using MedicalCenter.UserForm;

namespace MedicalCenter.MainForm
{
    public partial class Main : Form
    {
        private readonly AppointmentsSection _appointmentsSection;

        public Main()
        {
            InitializeComponent();
            _appointmentsSection = new AppointmentsSection();
        }

        private void Main_Load(object sender, EventArgs e)
        {
            lblUsername.Text = $"Welcome, {SessionManager.Role ?? "ahmed"}";
        }

        private void panelHeader_Paint(object sender, PaintEventArgs e)
        {
            panelHeader.Height = 80;
        }

        private void panelNav_Paint(object sender, PaintEventArgs e)
        {
            panelNav.Width = 200;
        }

        // =======================
        //  Navigation Handlers
        // =======================
        private void btnUsers_Click(object sender, EventArgs e)
        {
            try
            {
                UsersSection.ShowUsersSection(panelMain);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading Users section: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                PatientsSection.ShowPatientsSection(panelMain);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading Patients section: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                DoctorsSection.ShowDoctorsSection(panelMain);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading Doctors section: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void btnAppointment_Click(object sender, EventArgs e)
        {
            try
            {
                await _appointmentsSection.ShowAppointmentsSection(panelMain);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading Appointments section: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // =======================
        //  Header Button Events
        // =======================
        private void btnLogout_Click(object sender, EventArgs e)
        {
            var confirm = MessageBox.Show(
                "Are you sure you want to log out?",
                "Confirm Logout",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (confirm == DialogResult.Yes)
            {
                // Use the centralized Clear method
                SessionManager.Clear();

                // Optional: Force garbage collection
                GC.Collect();
                GC.WaitForPendingFinalizers();

                // Create new login form
                var loginForm = new LoginForm.Login();
                loginForm.Show();

                this.Close();  
            }
        }

        private void BtnEditProfile_Click(object sender, EventArgs e)
        {
           
        }

        private void panelMain_Paint(object sender, PaintEventArgs e)
        {
            // Optional custom drawing or background
        }

        private void panelMain_Paint_1(object sender, PaintEventArgs e)
        {

        }

        private void btnProfile_Click(object sender, EventArgs e)
        {
            try
            {
                var profileForm = new ProfileForm();
                profileForm.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error opening Profile: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }


    }
}
