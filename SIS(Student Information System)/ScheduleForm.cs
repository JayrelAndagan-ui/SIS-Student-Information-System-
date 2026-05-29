using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace SIS
{
    

    public class ScheduleForm : Form
    {
        TextBox txtSubject, txtInstructor, txtRoom, txtDayTime;

        Button btnSave, btnUpdate, btnDelete, btnBack;

        ListBox listBox;

        List<Schedule> schedules = new List<Schedule>();

        string path = "schedule.json";

        int selectedIndex = -1;

        public ScheduleForm()
        {
            BuildUI();
            LoadData();
            RefreshList();
        }

        void BuildUI()
        {
            Text = "Schedule Management";
            Size = new Size(900, 650);
            StartPosition = FormStartPosition.CenterScreen;
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            BackColor = Color.FromArgb(120, 81, 169);

            Panel left = new Panel()
            {
                Dock = DockStyle.Left,
                Width = 350,
                BackColor = Color.FromArgb(90, 60, 140)
            };

            Label title = new Label()
            {
                Text = "SCHEDULE\nMANAGEMENT",
                Font = new Font("Segoe UI", 22, FontStyle.Bold),
                ForeColor = Color.White,
                AutoSize = true,
                Location = new Point(45, 140)
            };

            left.Controls.Add(title);

            Panel main = new Panel()
            {
                Size = new Size(450, 540),
                Location = new Point(390, 40),
                BackColor = Color.White
            };

            Label formTitle = new Label()
            {
                Text = "SCHEDULE FORM",
                Font = new Font("Segoe UI", 16, FontStyle.Bold),
                ForeColor = Color.MediumPurple,
                AutoSize = true,
                Location = new Point(110, 20)
            };

            // SUBJECT
            Label lblSubject = new Label() { Text = "Subject", Left = 40, Top = 70, AutoSize = true };
            txtSubject = new TextBox() { Left = 40, Top = 95, Width = 360 };

            // INSTRUCTOR
            Label lblInstructor = new Label() { Text = "Instructor", Left = 40, Top = 145, AutoSize = true };
            txtInstructor = new TextBox() { Left = 40, Top = 170, Width = 360 };

            // ROOM
            Label lblRoom = new Label() { Text = "Room", Left = 40, Top = 220, AutoSize = true };
            txtRoom = new TextBox() { Left = 40, Top = 245, Width = 360 };

            // DAY/TIME
            Label lblDayTime = new Label() { Text = "Day / Time", Left = 40, Top = 295, AutoSize = true };
            txtDayTime = new TextBox() { Left = 40, Top = 320, Width = 360 };

            // BUTTONS
            btnSave = CreateBtn("SAVE", 40, 380, Color.MediumSeaGreen);
            btnUpdate = CreateBtn("UPDATE", 155, 380, Color.SteelBlue);
            btnDelete = CreateBtn("DELETE", 270, 380, Color.IndianRed);

            btnBack = new Button()
            {
                Text = "BACK",
                Width = 360,
                Height = 40,
                Left = 40,
                Top = 430,
                BackColor = Color.DarkSlateGray,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };
            btnBack.FlatAppearance.BorderSize = 0;

            // LISTBOX
            listBox = new ListBox()
            {
                Left = 40,
                Top = 480,
                Width = 360,
                Height = 50
            };

            // EVENTS
            btnSave.Click += BtnSave_Click;
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
                    txtSubject.Text = schedules[selectedIndex].Subject;
                    txtInstructor.Text = schedules[selectedIndex].Instructor;
                    txtRoom.Text = schedules[selectedIndex].Room;
                    txtDayTime.Text = schedules[selectedIndex].DayTime;
                }
            };

            // ADD CONTROLS
            main.Controls.Add(formTitle);

            main.Controls.Add(lblSubject);
            main.Controls.Add(txtSubject);

            main.Controls.Add(lblInstructor);
            main.Controls.Add(txtInstructor);

            main.Controls.Add(lblRoom);
            main.Controls.Add(txtRoom);

            main.Controls.Add(lblDayTime);
            main.Controls.Add(txtDayTime);

            main.Controls.Add(btnSave);
            main.Controls.Add(btnUpdate);
            main.Controls.Add(btnDelete);
            main.Controls.Add(btnBack);
            main.Controls.Add(listBox);

            Controls.Add(left);
            Controls.Add(main);
        }

        Button CreateBtn(string text, int left, int top, Color color)
        {
            Button b = new Button()
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

            b.FlatAppearance.BorderSize = 0;
            return b;
        }

        // ================= SAVE =================
        void BtnSave_Click(object sender, EventArgs e)
        {
            schedules.Add(new Schedule()
            {
                Subject = txtSubject.Text,
                Instructor = txtInstructor.Text,
                Room = txtRoom.Text,
                DayTime = txtDayTime.Text
            });

            SaveData();
            RefreshList();
            ClearFields();
        }

        // ================= UPDATE (NEW) =================
        void BtnUpdate_Click(object sender, EventArgs e)
        {
            if (selectedIndex >= 0)
            {
                schedules[selectedIndex].Subject = txtSubject.Text;
                schedules[selectedIndex].Instructor = txtInstructor.Text;
                schedules[selectedIndex].Room = txtRoom.Text;
                schedules[selectedIndex].DayTime = txtDayTime.Text;

                SaveData();
                RefreshList();
                ClearFields();
            }
            else
            {
                MessageBox.Show("Please select a schedule to update!");
            }
        }

        // ================= DELETE =================
        void BtnDelete_Click(object sender, EventArgs e)
        {
            if (selectedIndex >= 0)
            {
                schedules.RemoveAt(selectedIndex);
                SaveData();
                RefreshList();
                ClearFields();
            }
        }

        void LoadData()
        {
            if (File.Exists(path))
            {
                schedules = JsonConvert.DeserializeObject<List<Schedule>>(
                    File.ReadAllText(path)
                ) ?? new List<Schedule>();
            }
        }

        void SaveData()
        {
            File.WriteAllText(path,
                JsonConvert.SerializeObject(schedules, Formatting.Indented));
        }

        void RefreshList()
        {
            listBox.Items.Clear();

            foreach (var s in schedules)
            {
                listBox.Items.Add($"{s.Subject} | {s.Instructor} | {s.Room} | {s.DayTime}");
            }
        }

        void ClearFields()
        {
            txtSubject.Clear();
            txtInstructor.Clear();
            txtRoom.Clear();
            txtDayTime.Clear();
            selectedIndex = -1;
        }
    }
}