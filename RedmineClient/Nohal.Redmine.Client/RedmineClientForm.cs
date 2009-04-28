using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Nohal.Redmine.Client
{
    public partial class RedmineClientForm : Form
    {
        private int ticks = 0;
        private bool ticking = false;
        private int issueId = 0;
        private int projectId = 0;
        private int activityId = 0;
        private Redmine redmine;
        private bool updating = false;

        private string RedmineURL;
        private string RedmineUser;
        private string RedminePassword;

        public RedmineClientForm()
        {
            InitializeComponent();
            this.Cursor = Cursors.AppStarting;
            redmine = new Redmine();
            // TODO: takes some time, maybe we could move it to worker thread
            LoadConfig();
            redmine.RedmineBaseUri = RedmineURL;
            redmine.LogIn(RedmineUser, RedminePassword);
            List<Project> projects = redmine.GetProjects();
            ComboBoxProject.DataSource = projects;
            ComboBoxProject.ValueMember = "Id";
            ComboBoxProject.DisplayMember = "Name";
            this.Cursor = Cursors.Default;
            ComboBoxProject_SelectedIndexChanged(this, new EventArgs());
        }

        private void LoadConfig()
        {
            RedmineURL = ConfigurationManager.AppSettings["RedmineURL"];
            RedmineUser = ConfigurationManager.AppSettings["RedmineUser"];
            RedminePassword = ConfigurationManager.AppSettings["RedminePassword"];
        }

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            HideRestore();
        }

        private void HideRestore()
        {
            if (WindowState == FormWindowState.Normal)
            {
                WindowState = FormWindowState.Minimized;
                Hide();
                RestoreToolStripMenuItem.Text = "Restore";
            }
            else
            {
                WindowState = FormWindowState.Normal;
                Show();
                RestoreToolStripMenuItem.Text = "Hide";
            }
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Minimized)
                HideRestore();
        }

        private void RestoreToolStripMenuItem_Click(object sender, EventArgs e)
        {
            HideRestore();
        }

        private void BtnExitButton_Click(object sender, EventArgs e)
        {
            notifyIcon1.Dispose();
            Application.Exit();
        }

        private void ExitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            notifyIcon1.Dispose();
            Application.Exit();
        }

        private void BtnPauseButton_Click(object sender, EventArgs e)
        {
            timer1.Interval = 1000;
            if (ticking)
            {
                timer1.Stop();
                BtnPauseButton.Text = "Start";
            }
            else
            {
                timer1.Start();
                BtnPauseButton.Text = "Pause";
            }
            ticking = !ticking;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            this.ticks++;
            this.UpdateTime();
        }

        private void ResetForm()
        {
            this.ticks = 0;
            this.UpdateTime();
            this.dateTimePicker1.Value = DateTime.Now;
            this.TextBoxComment.Text = String.Empty;
        }

        private void BtnResetButton_Click(object sender, EventArgs e)
        {
            ResetForm();
        }

        private void UpdateTime()
        {
            this.updating = true;
            this.TextBoxHours.Text = (ticks / 3600 % 60).ToString();
            this.TextBoxMinutes.Text = (ticks / 60 % 60).ToString();
            this.TextBoxSeconds.Text = (ticks % 60).ToString();
            this.updating = false;
        }

        private void UpdateTicks()
        {
            ticks = Convert.ToInt32(TextBoxHours.Text)*3600 + Convert.ToInt32(TextBoxMinutes.Text)*60 +
                    Convert.ToInt32(TextBoxSeconds.Text);
            UpdateTime();
        }

        private void BtnAboutButton_Click(object sender, EventArgs e)
        {
            AboutBox aboutBox = new AboutBox();
            aboutBox.ShowDialog(this);
        }

        private void TextBoxSeconds_TextChanged(object sender, EventArgs e)
        {
            if (!CheckNumericValue(TextBoxSeconds.Text, 0, 60))
            {
                MessageBox.Show("Value out of range", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                UpdateTime();
            }
            else
            {
                if (!updating)
                {
                    UpdateTicks();   
                }
            }
        }

        private void TextBoxMinutes_TextChanged(object sender, EventArgs e)
        {
            if (!CheckNumericValue(TextBoxMinutes.Text, 0, 60))
            {
                MessageBox.Show("Value out of range", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                UpdateTime();
            }
            else
            {
                if (!updating)
                {
                    UpdateTicks();
                }
            }
        }

        private void TextBoxHours_TextChanged(object sender, EventArgs e)
        {
            if (!CheckNumericValue(TextBoxHours.Text, 0, 999))
            {
                MessageBox.Show("Value out of range", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                UpdateTime();
            }
            else
            {
                if (!updating)
                {
                    UpdateTicks();
                }
            }
        }

        private static bool CheckNumericValue(string val, int min, int max)
        {
            int myval;
            bool succ = Int32.TryParse(val, out myval);
            if (!succ || myval > max || myval < min)
            {
                return false;
            }
            return true;
        }

        private void DataGridViewIssues_SelectionChanged(object sender, EventArgs e)
        {
            if (DataGridViewIssues.SelectedCells.Count == 0 || !Int32.TryParse(DataGridViewIssues.SelectedCells[0].Value.ToString(), out issueId))
            {
                issueId = 0;
            }
        }

        private void BtnCommitButton_Click(object sender, EventArgs e)
        {
            bool shouldIRestart = ticking;
            if (projectId != 0 && issueId != 0 && activityId != 0 && ticks != 0 )
            {
                ticking = false;
                timer1.Stop();
                BtnPauseButton.Text = "Start";
                if (MessageBox.Show(String.Format("Do you really want to commit the following entry: {6} Project: {0}, Activity: {1}, Issue: {2}, Date: {3}, Comment: {4}, Time: {5}", 
                    projectId, activityId, issueId, dateTimePicker1.Value.ToString("yyyy-MM-dd"), TextBoxComment.Text, String.Format("{0:0.##}", (double)ticks / 3600), Environment.NewLine), 
                    "Ready to commit?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                {
                    redmine.LogTimeForIssue(projectId, issueId, TextBoxComment.Text, (double)ticks / 3600, dateTimePicker1.Value, activityId);
                    ResetForm();
                    MessageBox.Show("Work loggedsuccessfully ", "Work logged", MessageBoxButtons.OK,
                                    MessageBoxIcon.Information);
                }
                else if (shouldIRestart)
                {
                    ticking = true;
                    timer1.Start();
                    BtnPauseButton.Text = "Pause";
                }
            }
            else
            {
                if (ticks == 0)
                {
                    MessageBox.Show("There is no time to log...", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);   
                }
                else
                {
                    MessageBox.Show("Some mandatory information is missing...", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);   
                }
            }
        }

        private void ComboBoxActivity_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!Int32.TryParse(ComboBoxActivity.SelectedValue.ToString(), out activityId))
            {
                activityId = 0;
            }
        }

        private void ComboBoxProject_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!Int32.TryParse(ComboBoxProject.SelectedValue.ToString(), out projectId))
            {
                projectId = 0;
            }
            else
            {
                // TODO: takes some time, maybe we could move it to worker thread
                this.Cursor = Cursors.AppStarting;
                BindingSource bnd = new BindingSource();
                List<Activity> activities = redmine.GetActivities(projectId);
                ComboBoxActivity.DataSource = activities;
                ComboBoxActivity.DisplayMember = "Description";
                ComboBoxActivity.ValueMember = "Id";
                List<Issue> issues = redmine.GetIssues(projectId);
                bnd.DataSource = issues;
                DataGridViewIssues.AutoGenerateColumns = true;
                DataGridViewIssues.DataSource = bnd;
                DataGridViewIssues.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
                DataGridViewIssues.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                this.Cursor = Cursors.Default;
            }
        }
    }
}
