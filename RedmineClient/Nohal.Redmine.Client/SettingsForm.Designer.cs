namespace Nohal.Redmine.Client
{
    partial class SettingsForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.BtnCancelButton = new System.Windows.Forms.Button();
            this.BtnSaveButton = new System.Windows.Forms.Button();
            this.AuthenticationCheckBox = new System.Windows.Forms.CheckBox();
            this.RedmineBaseUrlTextBox = new System.Windows.Forms.TextBox();
            this.RedmineUsernameTextBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.RedminePasswordTextBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(96, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "Redmine URL";
            // 
            // BtnCancelButton
            // 
            this.BtnCancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.BtnCancelButton.Location = new System.Drawing.Point(493, 313);
            this.BtnCancelButton.Name = "BtnCancelButton";
            this.BtnCancelButton.Size = new System.Drawing.Size(75, 23);
            this.BtnCancelButton.TabIndex = 1;
            this.BtnCancelButton.Text = "Cancel";
            this.BtnCancelButton.UseVisualStyleBackColor = true;
            this.BtnCancelButton.Click += new System.EventHandler(this.CancelButton_Click);
            // 
            // BtnSaveButton
            // 
            this.BtnSaveButton.Location = new System.Drawing.Point(412, 313);
            this.BtnSaveButton.Name = "BtnSaveButton";
            this.BtnSaveButton.Size = new System.Drawing.Size(75, 23);
            this.BtnSaveButton.TabIndex = 2;
            this.BtnSaveButton.Text = "Save";
            this.BtnSaveButton.UseVisualStyleBackColor = true;
            this.BtnSaveButton.Click += new System.EventHandler(this.SaveButton_Click);
            // 
            // AuthenticationCheckBox
            // 
            this.AuthenticationCheckBox.AutoSize = true;
            this.AuthenticationCheckBox.Location = new System.Drawing.Point(15, 57);
            this.AuthenticationCheckBox.Name = "AuthenticationCheckBox";
            this.AuthenticationCheckBox.Size = new System.Drawing.Size(177, 21);
            this.AuthenticationCheckBox.TabIndex = 3;
            this.AuthenticationCheckBox.Text = "Requires authentication";
            this.AuthenticationCheckBox.UseVisualStyleBackColor = true;
            this.AuthenticationCheckBox.CheckedChanged += new System.EventHandler(this.AuthenticationCheckBox_CheckedChanged);
            // 
            // RedmineBaseUrlTextBox
            // 
            this.RedmineBaseUrlTextBox.Location = new System.Drawing.Point(15, 29);
            this.RedmineBaseUrlTextBox.Name = "RedmineBaseUrlTextBox";
            this.RedmineBaseUrlTextBox.Size = new System.Drawing.Size(553, 22);
            this.RedmineBaseUrlTextBox.TabIndex = 4;
            // 
            // RedmineUsernameTextBox
            // 
            this.RedmineUsernameTextBox.Location = new System.Drawing.Point(15, 101);
            this.RedmineUsernameTextBox.Name = "RedmineUsernameTextBox";
            this.RedmineUsernameTextBox.Size = new System.Drawing.Size(270, 22);
            this.RedmineUsernameTextBox.TabIndex = 6;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 81);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(131, 17);
            this.label2.TabIndex = 5;
            this.label2.Text = "Redmine username";
            // 
            // RedminePasswordTextBox
            // 
            this.RedminePasswordTextBox.Location = new System.Drawing.Point(298, 101);
            this.RedminePasswordTextBox.Name = "RedminePasswordTextBox";
            this.RedminePasswordTextBox.PasswordChar = '*';
            this.RedminePasswordTextBox.Size = new System.Drawing.Size(270, 22);
            this.RedminePasswordTextBox.TabIndex = 8;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(295, 81);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(128, 17);
            this.label3.TabIndex = 7;
            this.label3.Text = "Redmine password";
            // 
            // SettingsForm
            // 
            this.AcceptButton = this.BtnSaveButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.BtnCancelButton;
            this.ClientSize = new System.Drawing.Size(580, 348);
            this.Controls.Add(this.RedminePasswordTextBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.RedmineUsernameTextBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.RedmineBaseUrlTextBox);
            this.Controls.Add(this.AuthenticationCheckBox);
            this.Controls.Add(this.BtnSaveButton);
            this.Controls.Add(this.BtnCancelButton);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "SettingsForm";
            this.Text = "Settings";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button BtnCancelButton;
        private System.Windows.Forms.Button BtnSaveButton;
        private System.Windows.Forms.CheckBox AuthenticationCheckBox;
        private System.Windows.Forms.TextBox RedmineBaseUrlTextBox;
        private System.Windows.Forms.TextBox RedmineUsernameTextBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox RedminePasswordTextBox;
        private System.Windows.Forms.Label label3;
    }
}