using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace SIS
{
    public class ViewForm : Form
    {
        ListBox listBox;

        Button btnBack;

        List<Student> students = new List<Student>();

        string path = "students.json";

        public ViewForm()
        {
            BuildUI();

            LoadData();
        }

        void BuildUI()
        {
            // form
            Text = "View Students";

            Size = new Size(900, 550);

            StartPosition = FormStartPosition.CenterScreen;

            FormBorderStyle = FormBorderStyle.FixedSingle;

            MaximizeBox = false;

            BackColor = Color.FromArgb(120, 81, 169);

            // left panel
            Panel left = new Panel()
            {
                Dock = DockStyle.Left,

                Width = 350,

                BackColor = Color.FromArgb(90, 60, 140)
            };

            Label title = new Label()
            {
                Text = "VIEW\nSTUDENTS",

                Font = new Font("Segoe UI", 24, FontStyle.Bold),

                ForeColor = Color.White,

                AutoSize = true,

                Location = new Point(70, 160)
            };

            Label desc = new Label()
            {
                Text = "View all registered students.",

                Font = new Font("Segoe UI", 10),

                ForeColor = Color.WhiteSmoke,

                AutoSize = true,

                Location = new Point(75, 300)
            };

            left.Controls.Add(title);

            left.Controls.Add(desc);

            // main panel
            Panel main = new Panel()
            {
                Size = new Size(450, 440),

                Location = new Point(390, 45),

                BackColor = Color.White
            };

            Label formTitle = new Label()
            {
                Text = "STUDENT LIST",

                Font = new Font("Segoe UI", 16, FontStyle.Bold),

                ForeColor = Color.MediumPurple,

                AutoSize = true,

                Location = new Point(125, 20)
            };

            // listbox
            listBox = new ListBox()
            {
                Left = 30,

                Top = 80,

                Width = 380,

                Height = 240,

                Font = new Font("Segoe UI", 10)
            };

            // back button
            btnBack = new Button()
            {
                Text = "BACK",

                Width = 380,

                Height = 40,

                Left = 30,

                Top = 340,

                BackColor = Color.DarkSlateGray,

                ForeColor = Color.White,

                FlatStyle = FlatStyle.Flat,

                Font = new Font("Segoe UI", 10, FontStyle.Bold)
            };

            btnBack.FlatAppearance.BorderSize = 0;

            btnBack.Click += (s, e) =>
            {
                new DashboardForm().Show();

                Hide();
            };

            // add control
            main.Controls.Add(formTitle);

            main.Controls.Add(listBox);

            main.Controls.Add(btnBack);

            Controls.Add(left);

            Controls.Add(main);
        }

        void LoadData()
        {
            if (File.Exists(path))
            {
                students = JsonConvert.DeserializeObject<List<Student>>(
                    File.ReadAllText(path)
                ) ?? new List<Student>();
            }

            listBox.Items.Clear();

            foreach (var s in students)
            {
                listBox.Items.Add(
                    $"{s.Id} | {s.Name} | {s.Course}"
                );
            }
        }
    }
}