namespace MedicalCenter.MainForm
{
    partial class Main
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            panelHeader = new Panel();
            btnLogout = new Button();
            lblUsername = new Label();
            lblTitle = new Label();
            panelNav = new Panel();
            btnProfile = new Button();
            btnAppointment = new Button();
            btnDoctors = new Button();
            btnPatients = new Button();
            btnUsers = new Button();
            panelMain = new Panel();
            panelHeader.SuspendLayout();
            panelNav.SuspendLayout();
            SuspendLayout();
            // 
            // panelHeader
            // 
            panelHeader.BackColor = Color.FromArgb(44, 62, 80);
            panelHeader.Controls.Add(btnLogout);
            panelHeader.Controls.Add(lblUsername);
            panelHeader.Controls.Add(lblTitle);
            panelHeader.Dock = DockStyle.Top;
            panelHeader.Location = new Point(0, 0);
            panelHeader.Name = "panelHeader";
            panelHeader.Size = new Size(1200, 80);
            panelHeader.TabIndex = 0;
            // 
            // btnLogout
            // 
            btnLogout.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnLogout.BackColor = Color.FromArgb(231, 76, 60);
            btnLogout.Cursor = Cursors.Hand;
            btnLogout.FlatAppearance.BorderSize = 0;
            btnLogout.FlatStyle = FlatStyle.Flat;
            btnLogout.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnLogout.ForeColor = Color.White;
            btnLogout.Location = new Point(1060, 20);
            btnLogout.Name = "btnLogout";
            btnLogout.Size = new Size(120, 40);
            btnLogout.TabIndex = 2;
            btnLogout.Text = "Logout";
            btnLogout.UseVisualStyleBackColor = false;
            btnLogout.Click += btnLogout_Click;
            btnLogout.MouseEnter += BtnLogout_MouseEnter;
            btnLogout.MouseLeave += BtnLogout_MouseLeave;
            // 
            // lblUsername
            // 
            lblUsername.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lblUsername.AutoSize = true;
            lblUsername.Font = new Font("Segoe UI", 9.75F);
            lblUsername.ForeColor = Color.FromArgb(189, 195, 199);
            lblUsername.Location = new Point(920, 30);
            lblUsername.Name = "lblUsername";
            lblUsername.Size = new Size(95, 17);
            lblUsername.TabIndex = 1;
            lblUsername.Text = "Welcome, User";
            lblUsername.TextAlign = ContentAlignment.MiddleRight;
            // 
            // lblTitle
            // 
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Segoe UI", 22F, FontStyle.Bold);
            lblTitle.ForeColor = Color.White;
            lblTitle.Location = new Point(20, 20);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(392, 41);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "Medical Center Dashboard";
            // 
            // panelNav
            // 
            panelNav.BackColor = Color.FromArgb(52, 73, 94);
            panelNav.Controls.Add(btnProfile);
            panelNav.Controls.Add(btnAppointment);
            panelNav.Controls.Add(btnDoctors);
            panelNav.Controls.Add(btnPatients);
            panelNav.Controls.Add(btnUsers);
            panelNav.Dock = DockStyle.Left;
            panelNav.Location = new Point(0, 80);
            panelNav.Name = "panelNav";
            panelNav.Padding = new Padding(10, 20, 10, 20);
            panelNav.Size = new Size(220, 620);
            panelNav.TabIndex = 1;
            // 
            // btnProfile
            // 
            btnProfile.Cursor = Cursors.Hand;
            btnProfile.Dock = DockStyle.Bottom;
            btnProfile.FlatAppearance.BorderColor = Color.FromArgb(52, 152, 219);
            btnProfile.FlatAppearance.BorderSize = 0;
            btnProfile.FlatAppearance.MouseOverBackColor = Color.FromArgb(41, 128, 185);
            btnProfile.FlatStyle = FlatStyle.Flat;
            btnProfile.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            btnProfile.ForeColor = Color.White;
            btnProfile.ImageAlign = ContentAlignment.MiddleLeft;
            btnProfile.Location = new Point(10, 550);
            btnProfile.Margin = new Padding(0, 0, 0, 10);
            btnProfile.Name = "btnProfile";
            btnProfile.Padding = new Padding(15, 0, 0, 0);
            btnProfile.Size = new Size(200, 50);
            btnProfile.TabIndex = 6;
            btnProfile.Text = "👤 Profile";
            btnProfile.TextAlign = ContentAlignment.MiddleLeft;
            btnProfile.UseVisualStyleBackColor = true;
            btnProfile.Click += btnProfile_Click;
            btnProfile.MouseEnter += NavButton_MouseEnter;
            btnProfile.MouseLeave += NavButton_MouseLeave;
            // 
            // btnAppointment
            // 
            btnAppointment.Cursor = Cursors.Hand;
            btnAppointment.FlatAppearance.BorderSize = 0;
            btnAppointment.FlatAppearance.MouseOverBackColor = Color.FromArgb(41, 128, 185);
            btnAppointment.FlatStyle = FlatStyle.Flat;
            btnAppointment.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            btnAppointment.ForeColor = Color.White;
            btnAppointment.Location = new Point(10, 200);
            btnAppointment.Margin = new Padding(0, 0, 0, 10);
            btnAppointment.Name = "btnAppointment";
            btnAppointment.Padding = new Padding(15, 0, 0, 0);
            btnAppointment.Size = new Size(200, 50);
            btnAppointment.TabIndex = 4;
            btnAppointment.Text = "📅 Appointments";
            btnAppointment.TextAlign = ContentAlignment.MiddleLeft;
            btnAppointment.UseVisualStyleBackColor = true;
            btnAppointment.Click += btnAppointment_Click;
            btnAppointment.MouseEnter += NavButton_MouseEnter;
            btnAppointment.MouseLeave += NavButton_MouseLeave;
            // 
            // btnDoctors
            // 
            btnDoctors.Cursor = Cursors.Hand;
            btnDoctors.FlatAppearance.BorderSize = 0;
            btnDoctors.FlatAppearance.MouseOverBackColor = Color.FromArgb(41, 128, 185);
            btnDoctors.FlatStyle = FlatStyle.Flat;
            btnDoctors.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            btnDoctors.ForeColor = Color.White;
            btnDoctors.Location = new Point(10, 140);
            btnDoctors.Margin = new Padding(0, 0, 0, 10);
            btnDoctors.Name = "btnDoctors";
            btnDoctors.Padding = new Padding(15, 0, 0, 0);
            btnDoctors.Size = new Size(200, 50);
            btnDoctors.TabIndex = 3;
            btnDoctors.Text = "\U0001fa7a Doctors";
            btnDoctors.TextAlign = ContentAlignment.MiddleLeft;
            btnDoctors.UseVisualStyleBackColor = true;
            btnDoctors.Click += button2_Click;
            btnDoctors.MouseEnter += NavButton_MouseEnter;
            btnDoctors.MouseLeave += NavButton_MouseLeave;
            // 
            // btnPatients
            // 
            btnPatients.Cursor = Cursors.Hand;
            btnPatients.FlatAppearance.BorderSize = 0;
            btnPatients.FlatAppearance.MouseOverBackColor = Color.FromArgb(41, 128, 185);
            btnPatients.FlatStyle = FlatStyle.Flat;
            btnPatients.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            btnPatients.ForeColor = Color.White;
            btnPatients.Location = new Point(10, 80);
            btnPatients.Margin = new Padding(0, 0, 0, 10);
            btnPatients.Name = "btnPatients";
            btnPatients.Padding = new Padding(15, 0, 0, 0);
            btnPatients.Size = new Size(200, 50);
            btnPatients.TabIndex = 2;
            btnPatients.Text = "\U0001f9d1 Patients";
            btnPatients.TextAlign = ContentAlignment.MiddleLeft;
            btnPatients.UseVisualStyleBackColor = true;
            btnPatients.Click += button1_Click;
            btnPatients.MouseEnter += NavButton_MouseEnter;
            btnPatients.MouseLeave += NavButton_MouseLeave;
            // 
            // btnUsers
            // 
            btnUsers.Cursor = Cursors.Hand;
            btnUsers.FlatAppearance.BorderSize = 0;
            btnUsers.FlatAppearance.MouseOverBackColor = Color.FromArgb(41, 128, 185);
            btnUsers.FlatStyle = FlatStyle.Flat;
            btnUsers.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            btnUsers.ForeColor = Color.White;
            btnUsers.Location = new Point(10, 20);
            btnUsers.Margin = new Padding(0, 0, 0, 10);
            btnUsers.Name = "btnUsers";
            btnUsers.Padding = new Padding(15, 0, 0, 0);
            btnUsers.Size = new Size(200, 50);
            btnUsers.TabIndex = 1;
            btnUsers.Text = "👥 Users";
            btnUsers.TextAlign = ContentAlignment.MiddleLeft;
            btnUsers.UseVisualStyleBackColor = true;
            btnUsers.Click += btnUsers_Click;
            btnUsers.MouseEnter += NavButton_MouseEnter;
            btnUsers.MouseLeave += NavButton_MouseLeave;
            // 
            // panelMain
            // 
            panelMain.BackColor = Color.FromArgb(236, 240, 241);
            panelMain.Dock = DockStyle.Fill;
            panelMain.Location = new Point(220, 80);
            panelMain.Name = "panelMain";
            panelMain.Padding = new Padding(20);
            panelMain.Size = new Size(980, 620);
            panelMain.TabIndex = 2;
            panelMain.Paint += panelMain_Paint_1;
            // 
            // Main
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(1200, 700);
            Controls.Add(panelMain);
            Controls.Add(panelNav);
            Controls.Add(panelHeader);
            MinimumSize = new Size(1000, 600);
            Name = "Main";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Medical Center Dashboard";
            Load += Main_Load;
            panelHeader.ResumeLayout(false);
            panelHeader.PerformLayout();
            panelNav.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Panel panelHeader;
        private Button btnLogout;
        private Label lblUsername;
        private Label lblTitle;
        private Panel panelNav;
        private Button btnProfile;
        private Button btnAppointment;
        private Button btnDoctors;
        private Button btnPatients;
        private Button btnUsers;
        private Panel panelMain;

        // Event handlers for hover effects
        private void NavButton_MouseEnter(object sender, EventArgs e)
        {
            if (sender is Button btn)
            {
                btn.BackColor = Color.FromArgb(41, 128, 185);
            }
        }

        private void NavButton_MouseLeave(object sender, EventArgs e)
        {
            if (sender is Button btn && btn.BackColor != Color.FromArgb(52, 152, 219))
            {
                btn.BackColor = Color.Transparent;
            }
        }

        private void BtnLogout_MouseEnter(object sender, EventArgs e)
        {
            btnLogout.BackColor = Color.FromArgb(192, 57, 43);
        }

        private void BtnLogout_MouseLeave(object sender, EventArgs e)
        {
            btnLogout.BackColor = Color.FromArgb(231, 76, 60);
        }


    }
}