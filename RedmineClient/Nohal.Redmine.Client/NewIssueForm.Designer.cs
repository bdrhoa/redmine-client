﻿namespace Nohal.Redmine.Client
{
    partial class NewIssueForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.BtnSaveButton = new System.Windows.Forms.Button();
            this.BtnCancelButton = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.ComboBoxTracker = new System.Windows.Forms.ComboBox();
            this.DateStart = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.TextBoxSubject = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.TextBoxDescription = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.ComboBoxStatus = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.ComboBoxPriority = new System.Windows.Forms.ComboBox();
            this.DateDue = new System.Windows.Forms.DateTimePicker();
            this.label8 = new System.Windows.Forms.Label();
            this.TextBoxEstimatedTime = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.ComboBoxAssignedTo = new System.Windows.Forms.ComboBox();
            this.label10 = new System.Windows.Forms.Label();
            this.ComboBoxTargetVersion = new System.Windows.Forms.ComboBox();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.label11 = new System.Windows.Forms.Label();
            this.ListBoxWatchers = new System.Windows.Forms.ListBox();
            this.label12 = new System.Windows.Forms.Label();
            this.backgroundWorker2 = new System.ComponentModel.BackgroundWorker();
            this.cbStartDate = new System.Windows.Forms.CheckBox();
            this.cbDueDate = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            this.SuspendLayout();
            // 
            // BtnSaveButton
            // 
            this.BtnSaveButton.Location = new System.Drawing.Point(410, 311);
            this.BtnSaveButton.Name = "BtnSaveButton";
            this.BtnSaveButton.Size = new System.Drawing.Size(75, 23);
            this.BtnSaveButton.TabIndex = 4;
            this.BtnSaveButton.Text = "Save";
            this.BtnSaveButton.UseVisualStyleBackColor = true;
            this.BtnSaveButton.Click += new System.EventHandler(this.BtnSaveButton_Click);
            // 
            // BtnCancelButton
            // 
            this.BtnCancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.BtnCancelButton.Location = new System.Drawing.Point(491, 311);
            this.BtnCancelButton.Name = "BtnCancelButton";
            this.BtnCancelButton.Size = new System.Drawing.Size(75, 23);
            this.BtnCancelButton.TabIndex = 3;
            this.BtnCancelButton.Text = "Cancel";
            this.BtnCancelButton.UseVisualStyleBackColor = true;
            this.BtnCancelButton.Click += new System.EventHandler(this.BtnCancelButton_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(57, 17);
            this.label2.TabIndex = 12;
            this.label2.Text = "Tracker";
            // 
            // ComboBoxTracker
            // 
            this.ComboBoxTracker.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ComboBoxTracker.FormattingEnabled = true;
            this.ComboBoxTracker.Location = new System.Drawing.Point(12, 28);
            this.ComboBoxTracker.Name = "ComboBoxTracker";
            this.ComboBoxTracker.Size = new System.Drawing.Size(180, 24);
            this.ComboBoxTracker.TabIndex = 11;
            // 
            // DateStart
            // 
            this.DateStart.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.DateStart.Location = new System.Drawing.Point(15, 220);
            this.DateStart.Name = "DateStart";
            this.DateStart.Size = new System.Drawing.Size(114, 22);
            this.DateStart.TabIndex = 13;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 55);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 17);
            this.label1.TabIndex = 15;
            this.label1.Text = "Subject";
            // 
            // TextBoxSubject
            // 
            this.TextBoxSubject.Location = new System.Drawing.Point(15, 75);
            this.TextBoxSubject.Name = "TextBoxSubject";
            this.TextBoxSubject.Size = new System.Drawing.Size(551, 22);
            this.TextBoxSubject.TabIndex = 14;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 100);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(79, 17);
            this.label3.TabIndex = 17;
            this.label3.Text = "Description";
            // 
            // TextBoxDescription
            // 
            this.TextBoxDescription.AcceptsReturn = true;
            this.TextBoxDescription.AcceptsTab = true;
            this.TextBoxDescription.Location = new System.Drawing.Point(15, 120);
            this.TextBoxDescription.Multiline = true;
            this.TextBoxDescription.Name = "TextBoxDescription";
            this.TextBoxDescription.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.TextBoxDescription.Size = new System.Drawing.Size(551, 77);
            this.TextBoxDescription.TabIndex = 16;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(198, 9);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(48, 17);
            this.label4.TabIndex = 19;
            this.label4.Text = "Status";
            // 
            // ComboBoxStatus
            // 
            this.ComboBoxStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ComboBoxStatus.FormattingEnabled = true;
            this.ComboBoxStatus.Location = new System.Drawing.Point(198, 28);
            this.ComboBoxStatus.Name = "ComboBoxStatus";
            this.ComboBoxStatus.Size = new System.Drawing.Size(180, 24);
            this.ComboBoxStatus.TabIndex = 18;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(383, 8);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(52, 17);
            this.label5.TabIndex = 21;
            this.label5.Text = "Priority";
            // 
            // ComboBoxPriority
            // 
            this.ComboBoxPriority.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ComboBoxPriority.FormattingEnabled = true;
            this.ComboBoxPriority.Location = new System.Drawing.Point(386, 28);
            this.ComboBoxPriority.Name = "ComboBoxPriority";
            this.ComboBoxPriority.Size = new System.Drawing.Size(180, 24);
            this.ComboBoxPriority.TabIndex = 20;
            // 
            // DateDue
            // 
            this.DateDue.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.DateDue.Location = new System.Drawing.Point(135, 220);
            this.DateDue.Name = "DateDue";
            this.DateDue.Size = new System.Drawing.Size(114, 22);
            this.DateDue.TabIndex = 23;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(255, 200);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(100, 17);
            this.label8.TabIndex = 26;
            this.label8.Text = "Estimated time";
            // 
            // TextBoxEstimatedTime
            // 
            this.TextBoxEstimatedTime.Location = new System.Drawing.Point(258, 220);
            this.TextBoxEstimatedTime.Name = "TextBoxEstimatedTime";
            this.TextBoxEstimatedTime.Size = new System.Drawing.Size(117, 22);
            this.TextBoxEstimatedTime.TabIndex = 25;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(383, 200);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(82, 17);
            this.label9.TabIndex = 28;
            this.label9.Text = "Assigned to";
            // 
            // ComboBoxAssignedTo
            // 
            this.ComboBoxAssignedTo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ComboBoxAssignedTo.FormattingEnabled = true;
            this.ComboBoxAssignedTo.Location = new System.Drawing.Point(386, 220);
            this.ComboBoxAssignedTo.Name = "ComboBoxAssignedTo";
            this.ComboBoxAssignedTo.Size = new System.Drawing.Size(180, 24);
            this.ComboBoxAssignedTo.TabIndex = 27;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(383, 247);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(100, 17);
            this.label10.TabIndex = 30;
            this.label10.Text = "Target version";
            // 
            // ComboBoxTargetVersion
            // 
            this.ComboBoxTargetVersion.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ComboBoxTargetVersion.FormattingEnabled = true;
            this.ComboBoxTargetVersion.Location = new System.Drawing.Point(386, 267);
            this.ComboBoxTargetVersion.Name = "ComboBoxTargetVersion";
            this.ComboBoxTargetVersion.Size = new System.Drawing.Size(180, 24);
            this.ComboBoxTargetVersion.TabIndex = 29;
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.Increment = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numericUpDown1.Location = new System.Drawing.Point(258, 268);
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(120, 22);
            this.numericUpDown1.TabIndex = 31;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(255, 247);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(56, 17);
            this.label11.TabIndex = 32;
            this.label11.Text = "% done";
            // 
            // ListBoxWatchers
            // 
            this.ListBoxWatchers.FormattingEnabled = true;
            this.ListBoxWatchers.ItemHeight = 16;
            this.ListBoxWatchers.Location = new System.Drawing.Point(15, 268);
            this.ListBoxWatchers.Name = "ListBoxWatchers";
            this.ListBoxWatchers.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.ListBoxWatchers.Size = new System.Drawing.Size(234, 68);
            this.ListBoxWatchers.TabIndex = 33;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(15, 247);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(68, 17);
            this.label12.TabIndex = 34;
            this.label12.Text = "Watchers";
            // 
            // backgroundWorker2
            // 
            this.backgroundWorker2.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker2_DoWork);
            this.backgroundWorker2.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker2_RunWorkerCompleted);
            // 
            // cbStartDate
            // 
            this.cbStartDate.AutoSize = true;
            this.cbStartDate.Location = new System.Drawing.Point(15, 200);
            this.cbStartDate.Name = "cbStartDate";
            this.cbStartDate.Size = new System.Drawing.Size(89, 21);
            this.cbStartDate.TabIndex = 35;
            this.cbStartDate.Text = "Start date";
            this.cbStartDate.UseVisualStyleBackColor = true;
            this.cbStartDate.CheckedChanged += new System.EventHandler(this.cbStartDate_CheckedChanged);
            // 
            // cbDueDate
            // 
            this.cbDueDate.AutoSize = true;
            this.cbDueDate.Location = new System.Drawing.Point(135, 200);
            this.cbDueDate.Name = "cbDueDate";
            this.cbDueDate.Size = new System.Drawing.Size(85, 21);
            this.cbDueDate.TabIndex = 36;
            this.cbDueDate.Text = "Due date";
            this.cbDueDate.UseVisualStyleBackColor = true;
            this.cbDueDate.CheckedChanged += new System.EventHandler(this.cbDueDate_CheckedChanged);
            // 
            // NewIssueForm
            // 
            this.AcceptButton = this.BtnSaveButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.BtnCancelButton;
            this.ClientSize = new System.Drawing.Size(578, 346);
            this.Controls.Add(this.DateDue);
            this.Controls.Add(this.DateStart);
            this.Controls.Add(this.cbDueDate);
            this.Controls.Add(this.cbStartDate);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.ListBoxWatchers);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.numericUpDown1);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.ComboBoxTargetVersion);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.ComboBoxAssignedTo);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.TextBoxEstimatedTime);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.ComboBoxPriority);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.ComboBoxStatus);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.TextBoxDescription);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.TextBoxSubject);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.ComboBoxTracker);
            this.Controls.Add(this.BtnSaveButton);
            this.Controls.Add(this.BtnCancelButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "NewIssueForm";
            this.Text = "Create new issue";
            this.Load += new System.EventHandler(this.NewIssueForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button BtnSaveButton;
        private System.Windows.Forms.Button BtnCancelButton;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox ComboBoxTracker;
        private System.Windows.Forms.DateTimePicker DateStart;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox TextBoxSubject;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox TextBoxDescription;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox ComboBoxStatus;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox ComboBoxPriority;
        private System.Windows.Forms.DateTimePicker DateDue;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox TextBoxEstimatedTime;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox ComboBoxAssignedTo;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.ComboBox ComboBoxTargetVersion;
        private System.Windows.Forms.NumericUpDown numericUpDown1;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.ListBox ListBoxWatchers;
        private System.Windows.Forms.Label label12;
        private System.ComponentModel.BackgroundWorker backgroundWorker2;
        private System.Windows.Forms.CheckBox cbStartDate;
        private System.Windows.Forms.CheckBox cbDueDate;
    }
}