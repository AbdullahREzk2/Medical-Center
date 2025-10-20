namespace MedicalCenter.UserForm
{
    partial class AddUser
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();

            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            panelHeader = new Panel();
            lblTitle = new Label();
            lblNationalId = new Label();
            lblEmail = new Label();
            lblPassword = new Label();
            lblGender = new Label();
            lblRole = new Label();
            lblDOB = new Label();

            txt_NationalID = new TextBox();
            txt_Email = new TextBox();
            txt_Password = new TextBox();
            txt_Gender = new ComboBox();
            txt_Role = new ComboBox();
            txt_DOB = new DateTimePicker();

            btn_Add = new Button();
            btn_Cancel = new Button();

            panelHeader.SuspendLayout();
            SuspendLayout();

            // 
            // panelHeader
            // 
            panelHeader.BackColor = Color.MediumSlateBlue;
            panelHeader.Dock = DockStyle.Top;
            panelHeader.Height = 80;
            panelHeader.Controls.Add(lblTitle);

            // 
            // lblTitle
            // 
            lblTitle.Text = "➕ Add New User";
            lblTitle.Font = new Font("Segoe UI Semibold", 20F, FontStyle.Bold);
            lblTitle.ForeColor = Color.White;
            lblTitle.Dock = DockStyle.Fill;
            lblTitle.TextAlign = ContentAlignment.MiddleCenter;

            // 
            // Common Label Style
            // 
            Font labelFont = new Font("Segoe UI", 11F, FontStyle.Bold);
            Color labelColor = Color.FromArgb(40, 40, 40);
            int leftLabel = 80;
            int leftInput = 260;
            int topStart = 110;
            int spacingY = 55;

            // Label & Input fields
            lblNationalId.Text = "National ID:";
            lblNationalId.Font = labelFont;
            lblNationalId.ForeColor = labelColor;
            lblNationalId.Location = new Point(leftLabel, topStart);
            lblNationalId.AutoSize = true;

            txt_NationalID.Location = new Point(leftInput, topStart - 3);
            txt_NationalID.Width = 300;
            txt_NationalID.Font = new Font("Segoe UI", 10F);

            lblEmail.Text = "Email:";
            lblEmail.Font = labelFont;
            lblEmail.ForeColor = labelColor;
            lblEmail.Location = new Point(leftLabel, topStart + spacingY);
            lblEmail.AutoSize = true;

            txt_Email.Location = new Point(leftInput, topStart + spacingY - 3);
            txt_Email.Width = 300;
            txt_Email.Font = new Font("Segoe UI", 10F);

            lblPassword.Text = "Password:";
            lblPassword.Font = labelFont;
            lblPassword.ForeColor = labelColor;
            lblPassword.Location = new Point(leftLabel, topStart + spacingY * 2);
            lblPassword.AutoSize = true;

            txt_Password.Location = new Point(leftInput, topStart + spacingY * 2 - 3);
            txt_Password.Width = 300;
            txt_Password.Font = new Font("Segoe UI", 10F);
            txt_Password.PasswordChar = '●';

            lblGender.Text = "Gender:";
            lblGender.Font = labelFont;
            lblGender.ForeColor = labelColor;
            lblGender.Location = new Point(leftLabel, topStart + spacingY * 3);
            lblGender.AutoSize = true;

            txt_Gender.Location = new Point(leftInput, topStart + spacingY * 3 - 3);
            txt_Gender.Width = 180;
            txt_Gender.Font = new Font("Segoe UI", 10F);
            txt_Gender.DropDownStyle = ComboBoxStyle.DropDownList;
            txt_Gender.Items.AddRange(new object[] { "Male", "Female" });

            lblRole.Text = "Role:";
            lblRole.Font = labelFont;
            lblRole.ForeColor = labelColor;
            lblRole.Location = new Point(leftLabel, topStart + spacingY * 4);
            lblRole.AutoSize = true;

            txt_Role.Location = new Point(leftInput, topStart + spacingY * 4 - 3);
            txt_Role.Width = 180;
            txt_Role.Font = new Font("Segoe UI", 10F);
            txt_Role.DropDownStyle = ComboBoxStyle.DropDownList;
            txt_Role.Items.AddRange(new object[] { "Admin", "Doctor", "Patient" });

            lblDOB.Text = "Date of Birth:";
            lblDOB.Font = labelFont;
            lblDOB.ForeColor = labelColor;
            lblDOB.Location = new Point(leftLabel, topStart + spacingY * 5);
            lblDOB.AutoSize = true;

            txt_DOB.Location = new Point(leftInput, topStart + spacingY * 5 - 3);
            txt_DOB.Width = 220;
            txt_DOB.Font = new Font("Segoe UI", 10F);
            txt_DOB.Format = DateTimePickerFormat.Short;

            // 
            // btn_Add
            // 
            btn_Add.Text = "Add User";
            btn_Add.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            btn_Add.BackColor = Color.MediumSeaGreen;
            btn_Add.ForeColor = Color.White;
            btn_Add.FlatStyle = FlatStyle.Flat;
            btn_Add.FlatAppearance.BorderSize = 0;
            btn_Add.Size = new Size(130, 40);
            btn_Add.Location = new Point(260, topStart + spacingY * 6 + 10);
            btn_Add.Cursor = Cursors.Hand;
            btn_Add.Click += btn_Add_Click;

            // 
            // btn_Cancel
            // 
            btn_Cancel.Text = "Cancel";
            btn_Cancel.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            btn_Cancel.BackColor = Color.LightGray;
            btn_Cancel.ForeColor = Color.Black;
            btn_Cancel.FlatStyle = FlatStyle.Flat;
            btn_Cancel.FlatAppearance.BorderSize = 0;
            btn_Cancel.Size = new Size(120, 40);
            btn_Cancel.Location = new Point(440, topStart + spacingY * 6 + 10);
            btn_Cancel.Cursor = Cursors.Hand;
            btn_Cancel.Click += (s, e) => this.Close();

            // 
            // AddUser Form
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.WhiteSmoke;
            ClientSize = new Size(720, 550);
            StartPosition = FormStartPosition.CenterScreen;
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Text = "Add New User";

            Controls.Add(panelHeader);
            Controls.Add(lblNationalId);
            Controls.Add(lblEmail);
            Controls.Add(lblPassword);
            Controls.Add(lblGender);
            Controls.Add(lblRole);
            Controls.Add(lblDOB);
            Controls.Add(txt_NationalID);
            Controls.Add(txt_Email);
            Controls.Add(txt_Password);
            Controls.Add(txt_Gender);
            Controls.Add(txt_Role);
            Controls.Add(txt_DOB);
            Controls.Add(btn_Add);
            Controls.Add(btn_Cancel);

            panelHeader.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Panel panelHeader;
        private Label lblTitle;
        private Label lblNationalId;
        private Label lblEmail;
        private Label lblPassword;
        private Label lblGender;
        private Label lblRole;
        private Label lblDOB;
        private TextBox txt_NationalID;
        private TextBox txt_Email;
        private TextBox txt_Password;
        private ComboBox txt_Gender;
        private ComboBox txt_Role;
        private DateTimePicker txt_DOB;
        private Button btn_Add;
        private Button btn_Cancel;
    }
}
