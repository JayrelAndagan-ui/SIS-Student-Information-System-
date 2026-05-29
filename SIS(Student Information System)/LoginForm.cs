using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace SIS
{
    public class LoginForm : Form
    {
        TextBox txtUser, txtPass;
        Button btnLogin;
        CheckBox chkShow;

        List<User> users = new List<User>();
        string path = "users.json";

        public LoginForm()
        {
            BuildUI();
            LoadUsers();
        }

        void BuildUI()
        {
            // FORM
            Text = "SIS Login";
            Size = new Size(900, 550);
            StartPosition = FormStartPosition.CenterScreen;
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            BackColor = Color.FromArgb(120, 81, 169);

            // LEFT PANEL (BRANDING)
            Panel left = new Panel()
            {
                Dock = DockStyle.Left,
                Width = 400,
                BackColor = Color.FromArgb(90, 60, 140)
            };

            Label title = new Label()
            {
                Text = "STUDENT\nINFORMATION\nSYSTEM",
                Font = new Font("Segoe UI", 24, FontStyle.Bold),
                ForeColor = Color.White,
                AutoSize = true,
                Location = new Point(50, 150)
            };

            Label desc = new Label()
            {
                Text = "Manage students easily and securely",
                Font = new Font("Segoe UI", 10),
                ForeColor = Color.WhiteSmoke,
                AutoSize = true,
                Location = new Point(55, 300)
            };

            left.Controls.Add(title);
            left.Controls.Add(desc);

            // LOGIN PANEL
            Panel box = new Panel()
            {
                Size = new Size(350, 350),
                Location = new Point(470, 90),
                BackColor = Color.White
            };

            Label lblLogin = new Label()
            {
                Text = "LOGIN",
                Font = new Font("Segoe UI", 18, FontStyle.Bold),
                ForeColor = Color.FromArgb(120, 81, 169),
                AutoSize = true,
                Location = new Point(130, 20)
            };

            Label lblUser = new Label()
            {
                Text = "Username",
                Location = new Point(50, 80)
            };

            txtUser = new TextBox()
            {
                Top = 105,
                Left = 50,
                Width = 250
            };

            Label lblPass = new Label()
            {
                Text = "Password",
                Location = new Point(50, 150)
            };

            txtPass = new TextBox()
            {
                Top = 175,
                Left = 50,
                Width = 250,
                UseSystemPasswordChar = true
            };

            chkShow = new CheckBox()
            {
                Text = "Show Password",
                Top = 210,
                Left = 50
            };

            chkShow.CheckedChanged += (s, e) =>
            {
                txtPass.UseSystemPasswordChar = !chkShow.Checked;
            };

            btnLogin = new Button()
            {
                Text = "LOGIN",
                Top = 250,
                Left = 50,
                Width = 250,
                Height = 40,
                BackColor = Color.MediumPurple,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };

            btnLogin.FlatAppearance.BorderSize = 0;

            btnLogin.Click += BtnLogin_Click;

            // ADD CONTROLS
            box.Controls.Add(lblLogin);
            box.Controls.Add(lblUser);
            box.Controls.Add(txtUser);
            box.Controls.Add(lblPass);
            box.Controls.Add(txtPass);
            box.Controls.Add(chkShow);
            box.Controls.Add(btnLogin);

            Controls.Add(left);
            Controls.Add(box);
        }

        void BtnLogin_Click(object sender, EventArgs e)
        {
            var user = users.FirstOrDefault(x =>
                x.Username == txtUser.Text &&
                x.Password == txtPass.Text);

            if (user != null)
            {
                MessageBox.Show("Login Successful!", "Success",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                new DashboardForm().Show();
                Hide();
            }
            else
            {
                MessageBox.Show("Invalid credentials!", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        void LoadUsers()
        {
            if (!File.Exists(path))
            {
                users = new List<User>()
                {
                    new User { Username = "admin", Password = "admin" }
                };

                File.WriteAllText(path,
                    JsonConvert.SerializeObject(users, Formatting.Indented));
            }
            else
            {
                users = JsonConvert.DeserializeObject<List<User>>(
                    File.ReadAllText(path)
                ) ?? new List<User>();
            }
        }
    }
}