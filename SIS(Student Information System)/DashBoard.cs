using System;
using System.Drawing;
using System.Windows.Forms;

namespace SIS
{
    public class DashboardForm : Form
    {
        Button btnStudents, btnView, btnSchedule, btnBack;

        public DashboardForm()
        {
            BuildUI();
        }

        void BuildUI()
        {
            // FORM STYLE
            Text = "SIS DASHBOARD";
            Size = new Size(900, 550);
            StartPosition = FormStartPosition.CenterScreen;
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            BackColor = Color.FromArgb(120, 81, 169);

            // LEFT PANEL
            Panel left = new Panel()
            {
                Dock = DockStyle.Left,
                Width = 400,
                BackColor = Color.FromArgb(90, 60, 140)
            };

            Label title = new Label()
            {
                Text = "WELCOME\nADMIN",
                Font = new Font("Segoe UI", 26, FontStyle.Bold),
                ForeColor = Color.White,
                AutoSize = true,
                Location = new Point(60, 150)
            };

            Label desc = new Label()
            {
                Text = "Choose a module to manage your system",
                Font = new Font("Segoe UI", 10),
                ForeColor = Color.WhiteSmoke,
                AutoSize = true,
                Location = new Point(65, 320)
            };

            left.Controls.Add(title);
            left.Controls.Add(desc);

            // MAIN PANEL
            Panel main = new Panel()
            {
                Size = new Size(350, 420),
                Location = new Point(470, 60),
                BackColor = Color.White
            };

            Label lblDash = new Label()
            {
                Text = "DASHBOARD",
                Font = new Font("Segoe UI", 18, FontStyle.Bold),
                ForeColor = Color.MediumPurple,
                AutoSize = true,
                Location = new Point(95, 20)
            };

            // BUTTONS (FIXED FEATURE SET)
            btnStudents = CreateBtn("MANAGE STUDENTS", 90);
            btnView = CreateBtn("VIEW STUDENTS", 160);
            btnSchedule = CreateBtn("VIEW SCHEDULE", 230);

            btnBack = new Button()
            {
                Text = "BACK TO LOGIN",
                Top = 310,
                Left = 50,
                Width = 250,
                Height = 40,
                BackColor = Color.IndianRed,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };

            btnBack.FlatAppearance.BorderSize = 0;

            // EVENTS
            btnStudents.Click += (s, e) =>
            {
                new Manageform().Show();
                Hide();
            };

            btnView.Click += (s, e) =>
            {
                new ViewForm().Show();
                Hide();
            };

            btnSchedule.Click += (s, e) =>
            {
                new ScheduleForm().Show();
                Hide();
            };

            btnBack.Click += (s, e) =>
            {
                new LoginForm().Show();
                Hide();
            };

            // ADD CONTROLS
            main.Controls.Add(lblDash);
            main.Controls.Add(btnStudents);
            main.Controls.Add(btnView);
            main.Controls.Add(btnSchedule);
            main.Controls.Add(btnBack);

            Controls.Add(left);
            Controls.Add(main);
        }

        Button CreateBtn(string text, int top)
        {
            Button b = new Button()
            {
                Text = text,
                Top = top,
                Left = 50,
                Width = 250,
                Height = 40,
                BackColor = Color.MediumPurple,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };

            b.FlatAppearance.BorderSize = 0;
            return b;
        }
    }
}