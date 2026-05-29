using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace SIS
{
  
    public class Manageform : Form
    {
        TextBox txtId, txtName, txtCourse;

        Button btnAdd, btnDelete, btnUpdate, btnBack;

        ListBox listBox;

        List<Student> students = new List<Student>();

        string path = "students.json";

        int selectedIndex = -1;

        public Manageform()
        {
            BuildUI();
            LoadData();
            RefreshList();
        }

        void BuildUI()
        {
            Text = "Manage Students";
            Size = new Size(900, 550);
            StartPosition = FormStartPosition.CenterScreen;
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            BackColor = Color.FromArgb(120, 81, 169);

            // ================= LEFT PANEL =================
            Panel left = new Panel()
            {
                Dock = DockStyle.Left,
                Width = 350,
                BackColor = Color.FromArgb(90, 60, 140)
            };

            // ❌ REMOVED "MANAGE STUDENTS" LABEL AS REQUESTED

            // ================= MAIN PANEL =================
            Panel main = new Panel()
            {
                Size = new Size(450, 440),
                Location = new Point(390, 45),
                BackColor = Color.White
            };

            Label formTitle = new Label()
            {
                Text = "STUDENT FORM",
                Font = new Font("Segoe UI", 16, FontStyle.Bold),
                ForeColor = Color.MediumPurple,
                AutoSize = true,
                Location = new Point(120, 20)
            };

            // ================= TEXTBOXES =================
            Label lblId = new Label() { Text = "Student ID", Left = 40, Top = 70, AutoSize = true };
            txtId = new TextBox() { Left = 40, Top = 95, Width = 360 };

            Label lblName = new Label() { Text = "Student Name", Left = 40, Top = 145, AutoSize = true };
            txtName = new TextBox() { Left = 40, Top = 170, Width = 360 };

            Label lblCourse = new Label() { Text = "Course", Left = 40, Top = 220, AutoSize = true };
            txtCourse = new TextBox() { Left = 40, Top = 245, Width = 360 };

            // ================= BUTTONS =================
            btnAdd = CreateBtn("ADD", 40, 310, Color.MediumSeaGreen);
            btnUpdate = CreateBtn("UPDATE", 160, 310, Color.SteelBlue);
            btnDelete = CreateBtn("DELETE", 280, 310, Color.IndianRed);

            btnBack = new Button()
            {
                Text = "BACK",
                Top = 365,
                Left = 40,
                Width = 360,
                Height = 40,
                BackColor = Color.DarkSlateGray,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };
            btnBack.FlatAppearance.BorderSize = 0;

            // ================= LISTBOX (MOVED TO LEFT PANEL) =================
            listBox = new ListBox()
            {
                Top = 80,
                Left = 20,
                Width = 300,
                Height = 350,
                Font = new Font("Segoe UI", 10)
            };

            // ================= EVENTS =================
            btnAdd.Click += BtnAdd_Click;
            btnUpdate.Click += BtnUpdate_Click;
            btnDelete.Click += BtnDelete_Click;

            btnBack.Click += (s, e) =>
            {
                new DashboardForm().Show();
                Hide();
            };

            listBox.SelectedIndexChanged += (s, e) =>
            {
                selectedIndex = listBox.SelectedIndex;

                if (selectedIndex >= 0)
                {
                    txtId.Text = students[selectedIndex].Id;
                    txtName.Text = students[selectedIndex].Name;
                    txtCourse.Text = students[selectedIndex].Course;
                }
            };

            // ================= ADD CONTROLS =================
            main.Controls.Add(formTitle);

            main.Controls.Add(lblId);
            main.Controls.Add(txtId);

            main.Controls.Add(lblName);
            main.Controls.Add(txtName);

            main.Controls.Add(lblCourse);
            main.Controls.Add(txtCourse);

            main.Controls.Add(btnAdd);
            main.Controls.Add(btnUpdate);
            main.Controls.Add(btnDelete);
            main.Controls.Add(btnBack);

            left.Controls.Add(listBox);

            Controls.Add(left);
            Controls.Add(main);
        }

        Button CreateBtn(string text, int left, int top, Color color)
        {
            Button btn = new Button()
            {
                Text = text,
                Left = left,
                Top = top,
                Width = 100,
                Height = 40,
                BackColor = color,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };

            btn.FlatAppearance.BorderSize = 0;
            return btn;
        }

        void BtnAdd_Click(object sender, EventArgs e)
        {
            students.Add(new Student()
            {
                Id = txtId.Text,
                Name = txtName.Text,
                Course = txtCourse.Text
            });

            SaveData();
            RefreshList();
            ClearFields();
        }

        void BtnUpdate_Click(object sender, EventArgs e)
        {
            if (selectedIndex >= 0)
            {
                students[selectedIndex].Id = txtId.Text;
                students[selectedIndex].Name = txtName.Text;
                students[selectedIndex].Course = txtCourse.Text;

                SaveData();
                RefreshList();
                ClearFields();
            }
        }

        void BtnDelete_Click(object sender, EventArgs e)
        {
            if (selectedIndex >= 0)
            {
                students.RemoveAt(selectedIndex);

                SaveData();
                RefreshList();
                ClearFields();
            }
        }

        void LoadData()
        {
            if (File.Exists(path))
            {
                students = JsonConvert.DeserializeObject<List<Student>>(
                    File.ReadAllText(path)
                ) ?? new List<Student>();
            }
        }

        void SaveData()
        {
            File.WriteAllText(
                path,
                JsonConvert.SerializeObject(students, Formatting.Indented)
            );
        }

        void RefreshList()
        {
            listBox.Items.Clear();

            foreach (var s in students)
            {
                listBox.Items.Add($"{s.Id} | {s.Name} | {s.Course}");
            }
        }

        void ClearFields()
        {
            txtId.Clear();
            txtName.Clear();
            txtCourse.Clear();
            selectedIndex = -1;
        }
    }
}