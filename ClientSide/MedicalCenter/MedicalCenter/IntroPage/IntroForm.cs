using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace MedicalCenter
{
    public partial class IntroForm : Form
    {
        private System.Windows.Forms.Timer? animationTimer;
        private float rotationAngle = 0f;
        private float fadeOpacity = 0f;
        private int animationStep = 0;

        public IntroForm()
        {
            InitializeComponent();
            SetupForm();
            StartAnimation();
        }

        private void SetupForm()
        {
            // Form properties
            this.Size = new Size(800, 600);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.None;
            this.BackColor = Color.White;
            this.DoubleBuffered = true;

            // Enable smooth graphics
            this.SetStyle(ControlStyles.AllPaintingInWmPaint |
                         ControlStyles.UserPaint |
                         ControlStyles.OptimizedDoubleBuffer, true);
        }

        private void StartAnimation()
        {
            animationTimer = new System.Windows.Forms.Timer();
            animationTimer.Interval = 20; // 50 FPS
            animationTimer.Tick += AnimationTimer_Tick!;
            animationTimer.Start();

            // Auto close after 3 seconds and show login
            System.Windows.Forms.Timer closeTimer = new System.Windows.Forms.Timer();
            closeTimer.Interval = 3000;
            closeTimer.Tick += (s, e) =>
            {
                closeTimer.Stop();
                animationTimer.Stop();
                OpenLoginForm();
            };
            closeTimer.Start();
        }

        private void AnimationTimer_Tick(object sender, EventArgs e)
        {
            // Rotate the circle
            rotationAngle += 5f;
            if (rotationAngle >= 360f)
                rotationAngle = 0f;

            // Fade in effect
            if (fadeOpacity < 1f)
            {
                fadeOpacity += 0.02f;
                if (fadeOpacity > 1f)
                    fadeOpacity = 1f;
            }

            animationStep++;
            this.Invalidate(); // Trigger repaint
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;

            // Draw gradient background
            using (LinearGradientBrush brush = new LinearGradientBrush(
                this.ClientRectangle,
                Color.FromArgb(41, 128, 185),  // Blue
                Color.FromArgb(109, 213, 250), // Light Blue
                45f))
            {
                g.FillRectangle(brush, this.ClientRectangle);
            }

            int centerX = this.Width / 2;
            int centerY = this.Height / 2 - 50;

            // Draw medical cross logo in center
            DrawMedicalCrossLogo(g, centerX, centerY);

            // Draw rotating circle around logo
            DrawRotatingCircle(g, centerX, centerY);

            // Draw app name with fade effect
            DrawAppName(g);

            // Draw loading text
            DrawLoadingText(g);
        }

        private void DrawMedicalCrossLogo(Graphics g, int centerX, int centerY)
        {
            int logoSize = 80;
            int crossThickness = 25;

            // Clamp alpha values
            int alpha200 = Math.Min(255, Math.Max(0, (int)(fadeOpacity * 200)));

            // Shadow effect
            using (SolidBrush shadowBrush = new SolidBrush(Color.FromArgb(50, 0, 0, 0)))
            {
                g.FillRectangle(shadowBrush,
                    centerX - crossThickness / 2 + 3,
                    centerY - logoSize / 2 + 3,
                    crossThickness,
                    logoSize);
                g.FillRectangle(shadowBrush,
                    centerX - logoSize / 2 + 3,
                    centerY - crossThickness / 2 + 3,
                    logoSize,
                    crossThickness);
            }

            // White medical cross
            using (SolidBrush crossBrush = new SolidBrush(Color.White))
            {
                // Vertical bar
                g.FillRectangle(crossBrush,
                    centerX - crossThickness / 2,
                    centerY - logoSize / 2,
                    crossThickness,
                    logoSize);

                // Horizontal bar
                g.FillRectangle(crossBrush,
                    centerX - logoSize / 2,
                    centerY - crossThickness / 2,
                    logoSize,
                    crossThickness);
            }

            // Add circular background
            using (SolidBrush bgBrush = new SolidBrush(Color.FromArgb(alpha200, 52, 152, 219)))
            {
                g.FillEllipse(bgBrush,
                    centerX - 60,
                    centerY - 60,
                    120,
                    120);
            }

            // Redraw cross on top
            using (SolidBrush crossBrush = new SolidBrush(Color.White))
            {
                g.FillRectangle(crossBrush,
                    centerX - crossThickness / 2,
                    centerY - logoSize / 2,
                    crossThickness,
                    logoSize);
                g.FillRectangle(crossBrush,
                    centerX - logoSize / 2,
                    centerY - crossThickness / 2,
                    logoSize,
                    crossThickness);
            }
        }

        private void DrawRotatingCircle(Graphics g, int centerX, int centerY)
        {
            int radius = 100;

            // Clamp fadeOpacity to ensure alpha is between 0-255
            int alpha = Math.Min(255, Math.Max(0, (int)(fadeOpacity * 255)));

            using (Pen circlePen = new Pen(Color.FromArgb(alpha, 255, 255, 255), 4))
            {
                circlePen.DashStyle = DashStyle.Dash;

                // Save current state
                var state = g.Save();

                // Rotate around center
                g.TranslateTransform(centerX, centerY);
                g.RotateTransform(rotationAngle);
                g.TranslateTransform(-centerX, -centerY);

                // Draw circle
                g.DrawEllipse(circlePen,
                    centerX - radius,
                    centerY - radius,
                    radius * 2,
                    radius * 2);

                // Restore state
                g.Restore(state);
            }

            // Draw small dots on circle path
            for (int i = 0; i < 8; i++)
            {
                float angle = (rotationAngle + i * 45) * (float)Math.PI / 180f;
                float dotX = centerX + (float)Math.Cos(angle) * radius;
                float dotY = centerY + (float)Math.Sin(angle) * radius;

                using (SolidBrush dotBrush = new SolidBrush(Color.FromArgb(alpha, 255, 255, 255)))
                {
                    g.FillEllipse(dotBrush, dotX - 4, dotY - 4, 8, 8);
                }
            }
        }

        private void DrawAppName(Graphics g)
        {
            string appName = "Hope Medical Center";

            using (Font font = new Font("Segoe UI", 36, FontStyle.Bold))
            using (SolidBrush textBrush = new SolidBrush(Color.FromArgb(
                (int)(fadeOpacity * 255), 255, 255, 255)))
            {
                SizeF textSize = g.MeasureString(appName, font);
                float x = (this.Width - textSize.Width) / 2;
                float y = this.Height / 2 + 100;

                // Draw shadow
                using (SolidBrush shadowBrush = new SolidBrush(Color.FromArgb(
                    (int)(fadeOpacity * 100), 0, 0, 0)))
                {
                    g.DrawString(appName, font, shadowBrush, x + 2, y + 2);
                }

                // Draw text
                g.DrawString(appName, font, textBrush, x, y);
            }
        }

        private void DrawLoadingText(Graphics g)
        {
            string loadingText = "Loading";
            int dotCount = (animationStep / 15) % 4; // Animate dots
            loadingText += new string('.', dotCount);

            using (Font font = new Font("Segoe UI", 14, FontStyle.Regular))
            using (SolidBrush textBrush = new SolidBrush(Color.FromArgb(
                (int)(fadeOpacity * 200), 255, 255, 255)))
            {
                SizeF textSize = g.MeasureString(loadingText, font);
                float x = (this.Width - textSize.Width) / 2;
                float y = this.Height - 80;

                g.DrawString(loadingText, font, textBrush, x, y);
            }
        }

        private void OpenLoginForm()
        {
            this.Hide();

            // Open your login form
            var loginForm = new LoginForm.Login(); // Adjust namespace as needed
            loginForm.FormClosed += (s, e) => this.Close();
            loginForm.Show();
        }
    }
}