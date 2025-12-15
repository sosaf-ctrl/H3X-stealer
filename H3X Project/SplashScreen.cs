using System;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace H3XStealer
{
    public class SplashScreen : Form
    {
        private Timer vanishTimer;
        private Label titleLabel;
        private Label asciiLabel;
        private Label subtitleLabel;

        public SplashScreen()
        {
            this.InitializeSplashScreen();
        }

        private void InitializeSplashScreen()
        {
            this.Text = "H3X Stealer";
            this.Size = new Size(500, 400);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.None;
            this.BackColor = Color.FromArgb(25, 0, 50);
            this.TopMost = true;
            this.ShowInTaskbar = false;

            this.titleLabel = this.CreateLabel("H3X by 3x3", new Font("Segoe UI", 24f, FontStyle.Bold), 
                Color.FromArgb(138, 43, 226), new Point(150, 20));

            this.asciiLabel = this.CreateLabel(
                "⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⢀⣀⣀⣀⣀⣀⡀⠀⠀⠀⠀⠀⠀⠀⠀⠀\n" +
                "⠀⠀⠀⠀⠀⣀⣠⣤⣴⣶⣶⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣷⣶⣤⡀⠀⠀⠀⠀\n" +
                "⠀⠀⣿⣷⠀⣿⣿⣿⣿⣿⣿⣿⡿⠟⠛⠛⠉⠉⠉⠉⠉⠉⠙⠛⠻⢿⣷⡀⠀⠀\n" +
                "⠀⠀⢿⣿⠀⢹⣿⣿⡿⠛⠉⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠈⠙⠄⠀\n" +
                "⠀⠀⠸⣿⡇⠈⠟⠁⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀\n" +
                "⠀⠀⠀⢻⣿⡄⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀\n" +
                "⠀⠀⠀⠀⢻⣿⡄⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀\n" +
                "⠀⠀⣀⣠⣴⡿⣿⣦⡀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀\n" +
                "⠀⠈⠛⠛⠉⠀⠈⠛⢿⣦⣄⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀\n" +
                "⠀⠀⠀⠀⠀⠀⠀⠀⠀⠙⠻⣿⣶⣤⡀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀\n" +
                "⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠙⠻⣿⣷⣦⣀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀\n" +
                "⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠈⠙⢿⣿⣷⣄⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀\n" +
                "⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠉⠻⣿⣷⣄⠀⠀⠀⠀⠀⠀⠀⠀\n" +
                "⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠙⢿⣿⣷⡀⠀⠀⠀⠀⠀⠀\n" +
                "⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠈⠉⠉",
                new Font("Consolas", 8f), Color.FromArgb(138, 43, 226), new Point(50, 80));

            this.subtitleLabel = this.CreateLabel("Loading...", new Font("Segoe UI", 12f, FontStyle.Italic), 
                Color.FromArgb(138, 43, 226), new Point(200, 350));

            this.Controls.Add(this.titleLabel);
            this.Controls.Add(this.asciiLabel);
            this.Controls.Add(this.subtitleLabel);

            this.vanishTimer = new Timer();
            this.vanishTimer.Interval = 2000;
            this.vanishTimer.Tick += this.VanishTimer_Tick;
            this.vanishTimer.Start();

            this.Load += (sender, e) => this.FadeIn();
        }

        private Label CreateLabel(string text, Font font, Color foreColor, Point location)
        {
            Label label = new Label();
            label.Text = text;
            label.Font = font;
            label.ForeColor = foreColor;
            label.Location = location;
            label.AutoSize = true;
            label.BackColor = Color.Transparent;
            return label;
        }

        private async void FadeIn()
        {
            this.Opacity = 0.0;
            while (this.Opacity < 1.0)
            {
                this.Opacity += 0.05;
                await Task.Delay(20);
            }
        }

        private async void VanishTimer_Tick(object sender, EventArgs e)
        {
            this.vanishTimer.Stop();
            await this.VanishEffect();
            this.Hide();
            this.Close();
        }

        private async Task VanishEffect()
        {
            while (this.Opacity > 0.0)
            {
                this.Opacity -= 0.1;
                await Task.Delay(30);
            }
        }
    }
}
