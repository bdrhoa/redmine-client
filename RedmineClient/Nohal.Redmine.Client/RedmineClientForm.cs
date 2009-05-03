using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Drawing;
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
        private static Redmine redmine;
        private bool updating = false;

        private string RedmineURL;
        private bool RedmineAuthentication;
        private string RedmineUser;
        private string RedminePassword;

        public RedmineClientForm()
        {
            InitializeComponent();
            if (!IsRunningOnMono())
            {
                this.Icon = (Icon)Properties.Resources.ResourceManager.GetObject("clock");
                this.notifyIcon1.Icon = (Icon)Properties.Resources.ResourceManager.GetObject("clock");
                this.notifyIcon1.Visible = true;
            }
			else 
			{
				this.DataGridViewIssues.Click += new System.EventHandler(this.DataGridViewIssues_SelectionChanged);
			}
            redmine = new Redmine();
            LoadConfig();
            redmine.RedmineBaseUri = RedmineURL;
            if (RedmineAuthentication)
            {
                redmine.RedmineUser = RedmineUser;
                redmine.RedminePassword = RedminePassword; 
            }
            this.Cursor = Cursors.AppStarting;
            this.BtnCommitButton.Enabled = false;
            this.BtnRefreshButton.Enabled = false;
            this.BtnNewIssueButton.Enabled = false;
            backgroundWorker1.RunWorkerAsync(0);
        }

        private FormData PrepareFormData(int projectId)
        {
            redmine.LogIn();
            List<Project> projects = redmine.GetProjects();
            if (projects.Count > 0)
            {
                if (projectId == 0)
                {
                    projectId = projects[0].Id;
                }
                return new FormData() { Activities = redmine.GetActivities(projectId), Issues = redmine.GetIssues(projectId), Projects = projects };
            }
            throw new Exception("No projects found in Redmine.");
        }

        private void FillForm(FormData data)
        {
            updating = true;
            if (data.Projects.Count == 0 || data.Issues.Count == 0 || data.Activities.Count == 0)
            {
                BtnCommitButton.Enabled = false;
                if (data.Projects.Count > 0)
                {
                    BtnNewIssueButton.Enabled = true;    
                }
                else
                {
                    BtnNewIssueButton.Enabled = false;
                }
                
                BtnRefreshButton.Enabled = true;
            }
            else
            {
                BtnCommitButton.Enabled = true;
                BtnNewIssueButton.Enabled = true;
                BtnRefreshButton.Enabled = true;
            }
            ComboBoxProject.DataSource = data.Projects;
            ComboBoxProject.ValueMember = "Id";
            ComboBoxProject.DisplayMember = "Name";
            
            ComboBoxActivity.DataSource = data.Activities;
            ComboBoxActivity.DisplayMember = "Description";
            ComboBoxActivity.ValueMember = "Id";

            DataGridViewIssues.DataSource = data.Issues;
            foreach (DataGridViewColumn column in DataGridViewIssues.Columns)
            {
                if (column.Name != "Id" && column.Name != "Subject")
                {
                    column.Visible = false;
                }
            }
            DataGridViewIssues.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
            if (DataGridViewIssues.Columns.Count > 0)
            {
                DataGridViewIssues.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;    
            }

            if (ComboBoxProject.Items.Count > 0)
            {
                ComboBoxProject.SelectedIndex = 0;
                if (!Int32.TryParse(ComboBoxProject.SelectedValue.ToString(), out projectId))
                {
                    projectId = 0;
                }   
            }
            if (ComboBoxActivity.Items.Count > 0)
            {
                ComboBoxActivity.SelectedIndex = 0;
            }
            if (DataGridViewIssues.Rows.Count > 0)
            {
                DataGridViewIssues.Rows[0].Selected = true;
                DataGridViewIssues_SelectionChanged(null, null);
            }
            updating = false;
        }

        private void LoadConfig()
        {
            RedmineURL = ConfigurationManager.AppSettings["RedmineURL"];
            RedmineAuthentication = Convert.ToBoolean(ConfigurationManager.AppSettings["RedmineAuthentication"]);
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
                    MessageBox.Show("Work logged successfully ", "Work logged", MessageBoxButtons.OK,
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
            if (!updating)
            {
                int reselect = ComboBoxProject.SelectedIndex;
                this.Cursor = Cursors.AppStarting;
                if (!Int32.TryParse(ComboBoxProject.SelectedValue.ToString(), out projectId))
                {
                    projectId = 0;
                }
                FillForm(PrepareFormData(projectId));
                updating = true;
                ComboBoxProject.SelectedIndex = reselect;
                updating = false;
                this.Cursor = Cursors.Default;
            }
        }

        public static bool IsRunningOnMono()
        {
            return Type.GetType("Mono.Runtime") != null;
        }

        private void BtnRefreshButton_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.AppStarting;
            int selectedProject = 0;
            if (ComboBoxProject.SelectedValue != null)
            {
                selectedProject = ComboBoxProject.SelectedIndex;
                if (!Int32.TryParse(ComboBoxProject.SelectedValue.ToString(), out projectId))
                {
                    projectId = 0;
                }
            }
            else
            {
                projectId = 0;
            }
            try
            {
                FillForm(PrepareFormData(projectId));
                ComboBoxProject.SelectedIndex = selectedProject;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            this.Cursor = Cursors.Default;
        }

        private void BtnSettingsButton_Click(object sender, EventArgs e)
        {
            SettingsForm dlg = new SettingsForm();
            if (dlg.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
            {
                this.Cursor = Cursors.AppStarting;
                LoadConfig();
                redmine.RedmineBaseUri = RedmineURL;
                if (RedmineAuthentication)
                {
                    redmine.RedmineUser = RedmineUser;
                    redmine.RedminePassword = RedminePassword;
                }
                else
                {
                    redmine.RedmineUser = String.Empty;
                    redmine.RedminePassword = String.Empty;
                }
                
                try
                {
                    FillForm(PrepareFormData(0));
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
                this.Cursor = Cursors.Default;
            }
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            FormData data;
            try
            {
                data = PrepareFormData((int)e.Argument);
                e.Result = data;
            }
            catch (Exception ex)
            {
                e.Result = ex;
            }
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Result is Exception)
            {
                MessageBox.Show(((Exception) e.Result).Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            else
            {
				FillForm((FormData)e.Result);
            }
            this.Cursor = Cursors.Default;
        }

        private void BtnNewIssueButton_Click(object sender, EventArgs e)
        {
            NewIssueForm dlg = new NewIssueForm();
            if (dlg.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
            {
                MessageBox.Show("Adding issues to Redmine not yet implemented.");
            }
        }

    }
}
