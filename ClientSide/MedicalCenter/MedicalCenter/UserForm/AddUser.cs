using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Windows.Forms;
using MedicalCenter.Models;
using MedicalCenter.Services;

namespace MedicalCenter.UserForm
{
    public partial class AddUser : Form
    {
        private readonly UserService _userService = new UserService();

        public AddUser()
        {
            InitializeComponent();
        }

        private async void btn_Add_Click(object sender, EventArgs e)
        {
            try
            {
                var newUser = new UserInputModel
                {
                    NationalID = txt_NationalID.Text.Trim(),
                    Gender = txt_Gender.Text.Trim(),
                    DOB = txt_DOB.Value,
                    Email = txt_Email.Text.Trim(),
                    Password = txt_Password.Text,
                    Role = txt_Role.Text.Trim()
                };

                var createdUser = await _userService.AddNewUser(newUser);
                MessageBox.Show($"✅ User Added Successfully!\nEmail: {createdUser.Email}",
                    "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                string message = ex.Message;

                if (message.Contains("errors"))
                {
                    try
                    {
                        int start = message.IndexOf("{");
                        message = message.Substring(start);
                    }
                    catch { /* fallback if parsing fails */ }
                }

                MessageBox.Show($"❌ Failed to add user:\n{message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void AddUser_Load(object sender, EventArgs e)
        {
        }


    }
}
