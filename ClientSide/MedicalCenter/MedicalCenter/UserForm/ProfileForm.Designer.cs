namespace MedicalCenter
{
    partial class ProfileForm
    {
        private System.ComponentModel.IContainer components = null;
        private Label lblTitle;
        private Label lblEmailTitle;
        private Label lblRoleTitle;
        private Label lblNationalIdTitle;
        private Label lblStatusTitle;
        private Label lblEmail;
        private Label lblRole;
        private Label lblNationalId;
        private Label lblStatus;
        private Button btnClose;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.lblTitle = new Label();
            this.lblEmailTitle = new Label();
            this.lblRoleTitle = new Label();
            this.lblNationalIdTitle = new Label();
            this.lblStatusTitle = new Label();
            this.lblEmail = new Label();
            this.lblRole = new Label();
            this.lblNationalId = new Label();
            this.lblStatus = new Label();
            this.btnClose = new Button();
            this.SuspendLayout();

            // 
            // lblTitle
            // 
            this.lblTitle.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            this.lblTitle.ForeColor = Color.FromArgb(41, 128, 185);
            this.lblTitle.Location = new Point(0, 30);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new Size(500, 40);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "User Profile";
            this.lblTitle.TextAlign = ContentAlignment.MiddleCenter;

            // 
            // Static Labels
            // 
            int leftTitle = 80, leftValue = 220, top = 100, gap = 40;

            this.lblEmailTitle.Text = "Email:";
            this.lblRoleTitle.Text = "Role:";
            this.lblNationalIdTitle.Text = "National ID:";
            this.lblStatusTitle.Text = "Status:";

            Label[] titles = { lblEmailTitle, lblRoleTitle, lblNationalIdTitle, lblStatusTitle };
            foreach (var label in titles)
            {
                label.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
                label.ForeColor = Color.FromArgb(44, 62, 80);
                label.AutoSize = true;
                label.Location = new Point(leftTitle, top);
                top += gap;
            }

            // Value Labels
            top = 100;
            Label[] values = { lblEmail, lblRole, lblNationalId, lblStatus };
            foreach (var label in values)
            {
                label.Font = new Font("Segoe UI", 10F);
                label.ForeColor = Color.FromArgb(52, 73, 94);
                label.AutoSize = true;
                label.Location = new Point(leftValue, top);
                top += gap;
            }

            // 
            // btnClose
            // 
            this.btnClose.Text = "Close";
            this.btnClose.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            this.btnClose.BackColor = Color.FromArgb(231, 76, 60);
            this.btnClose.ForeColor = Color.White;
            this.btnClose.FlatStyle = FlatStyle.Flat;
            this.btnClose.FlatAppearance.BorderSize = 0;
            this.btnClose.Location = new Point(200, 320);
            this.btnClose.Size = new Size(120, 35);
            this.btnClose.Click += (s, e) => this.Close();

            // 
            // ProfileForm
            // 
            this.AutoScaleDimensions = new SizeF(7F, 15F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new Size(500, 400);
            this.BackColor = Color.White;
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.lblEmailTitle);
            this.Controls.Add(this.lblRoleTitle);
            this.Controls.Add(this.lblNationalIdTitle);
            this.Controls.Add(this.lblStatusTitle);
            this.Controls.Add(this.lblEmail);
            this.Controls.Add(this.lblRole);
            this.Controls.Add(this.lblNationalId);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.btnClose);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Name = "ProfileForm";
            this.Text = "Profile";
            this.Load += new EventHandler(this.ProfileForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
