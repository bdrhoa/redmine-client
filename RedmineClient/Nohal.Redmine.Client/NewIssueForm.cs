using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Nohal.Redmine.Client
{
    public partial class NewIssueForm : Form
    {
        public NewIssueForm()
        {
            InitializeComponent();
        }

        private void BtnSaveButton_Click(object sender, EventArgs e)
        {
            Issue issue = new Issue();
            issue.AssignedTo = Convert.ToInt32(ComboBoxAssignedTo.SelectedValue);
            issue.Description = TextBoxDescription.Text;
            issue.DueDate = DateDue.Value;
            int time;
            issue.EstimatedTime = Int32.TryParse(TextBoxEstimatedTime.Text, out time) ? time : 0;
            issue.PercentDone = Convert.ToInt32(numericUpDown1.Value);
            issue.PriorityId = Convert.ToInt32(ComboBoxPriority.SelectedValue);
            issue.Start = DateStart.Value;
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
            //TODO: save the issue
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void BtnCancelButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void NewIssueForm_Load(object sender, EventArgs e)
        {
            while (RedmineClientForm.DataCache == null)
            {
                System.Threading.Thread.Sleep(10);
            }
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

    }
}
