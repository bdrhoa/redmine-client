using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;

namespace Nohal.Redmine.Client
{
    public partial class NewIssueForm : Form
    {
        internal int ProjectId;

        public NewIssueForm()
        {
            InitializeComponent();
        }

        private void BtnSaveButton_Click(object sender, EventArgs e)
        {
            Issue issue = new Issue();
            issue.ProjectId = this.ProjectId;
            issue.AssignedTo = Convert.ToInt32(ComboBoxAssignedTo.SelectedValue);
            issue.Description = TextBoxDescription.Text;
            
            int time;
            issue.EstimatedTime = Int32.TryParse(TextBoxEstimatedTime.Text, out time) ? time : 0;
            issue.PercentDone = Convert.ToInt32(numericUpDown1.Value);
            issue.PriorityId = Convert.ToInt32(ComboBoxPriority.SelectedValue);
            if (DateStart.Enabled)
            {
                issue.Start = DateStart.Value;
            }
            if (DateDue.Enabled)
            {
                issue.DueDate = DateDue.Value;   
            }
            issue.StatusId = Convert.ToInt32(ComboBoxStatus.SelectedValue);
            issue.Subject = TextBoxSubject.Text;
            issue.TargetVersionId = Convert.ToInt32(ComboBoxTargetVersion.SelectedValue);
            issue.TrackerId = Convert.ToInt32(ComboBoxTracker.SelectedValue);
            if (ListBoxWatchers.SelectedItems.Count > 0 && issue.Watchers == null)
            {
                issue.Watchers = new List<User>();
            }
            foreach (var watcher in ListBoxWatchers.SelectedItems)
            {
                issue.Watchers.Add((User)watcher);   
            }
            try
            {
                if (issue.Subject != String.Empty)
                {
                    RedmineClientForm.redmine.CreateIssue(issue);
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    MessageBox.Show("The issue subject is mandatory.",
                                "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(String.Format("Creating the issue failed, the server responded: {0}", ex.Message),
                                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnCancelButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void NewIssueForm_Load(object sender, EventArgs e)
        {
            cbDueDate.Checked = false;
            cbStartDate.Checked = false;
            DateStart.Enabled = false;
            DateDue.Enabled = false;
            if (RedmineClientForm.DataCache == null)
            {
                this.Cursor = Cursors.AppStarting;
                backgroundWorker2.RunWorkerAsync(ProjectId);
                this.BtnSaveButton.Enabled = false;
            }
            else
            {
                FillForm();   
            }
        }

        private void FillForm()
        {
            this.ComboBoxAssignedTo.DataSource = RedmineClientForm.DataCache.Assignees;
            this.ComboBoxAssignedTo.DisplayMember = "Name";
            this.ComboBoxAssignedTo.ValueMember = "Id";
            this.ComboBoxPriority.DataSource = RedmineClientForm.DataCache.Priorities;
            this.ComboBoxPriority.DisplayMember = "Name";
            this.ComboBoxPriority.ValueMember = "Id";
            this.ComboBoxStatus.DataSource = RedmineClientForm.DataCache.Statuses;
            this.ComboBoxStatus.DisplayMember = "Name";
            this.ComboBoxStatus.ValueMember = "Id";
            this.ComboBoxTargetVersion.DataSource = RedmineClientForm.DataCache.Versions;
            this.ComboBoxTargetVersion.DisplayMember = "Name";
            this.ComboBoxTargetVersion.ValueMember = "Id";
            this.ComboBoxTracker.DataSource = RedmineClientForm.DataCache.Trackers;
            this.ComboBoxTracker.DisplayMember = "Name";
            this.ComboBoxTracker.ValueMember = "Id";
            this.ListBoxWatchers.DataSource = RedmineClientForm.DataCache.Watchers;
            this.ListBoxWatchers.DisplayMember = "Name";
            this.ListBoxWatchers.ClearSelected();
        }

        private void backgroundWorker2_DoWork(object sender, DoWorkEventArgs e)
        {
            RedmineClientForm.DataCache = new IssueFormData();
            int projectId = (int)e.Argument;
            RedmineClientForm.DataCache.Priorities = RedmineClientForm.redmine.GetPriorities(projectId);
            RedmineClientForm.DataCache.Statuses = RedmineClientForm.redmine.GetStatuses(projectId);
            RedmineClientForm.DataCache.Trackers = RedmineClientForm.redmine.GetTrackers(projectId);
            RedmineClientForm.DataCache.Watchers = RedmineClientForm.redmine.GetWatchers(projectId);
            RedmineClientForm.DataCache.Assignees = RedmineClientForm.redmine.GetAssignees(projectId);
        }

        private void backgroundWorker2_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            this.Cursor = Cursors.Default;
            FillForm();
            this.BtnSaveButton.Enabled = true;
        }

        private void cbStartDate_CheckedChanged(object sender, EventArgs e)
        {
            DateStart.Enabled = cbStartDate.Checked;
        }

        private void cbDueDate_CheckedChanged(object sender, EventArgs e)
        {
            DateDue.Enabled = cbDueDate.Checked;
        }


    }
}
