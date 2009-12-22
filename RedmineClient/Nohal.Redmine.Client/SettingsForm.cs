using System;
using System.Configuration;
using System.Windows.Forms;

namespace Nohal.Redmine.Client
{
    public partial class SettingsForm : Form
    {
        public SettingsForm()
        {
            InitializeComponent();
            LoadConfig();
            EnableDisableAuthenticationFields();
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            Uri uri;
            if (!Uri.TryCreate(RedmineBaseUrlTextBox.Text, UriKind.Absolute, out uri))
            {
                MessageBox.Show("Invalid URL of Redmine installation.", "Error", MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
                this.RedmineBaseUrlTextBox.Focus();
                return;
            }
            SaveConfig();
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void SaveConfig()
        {
            System.Configuration.Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            config.AppSettings.Settings.Clear();
            config.AppSettings.Settings.Add("RedmineURL", RedmineBaseUrlTextBox.Text);
            config.AppSettings.Settings.Add("RedmineUser", RedmineUsernameTextBox.Text);
            config.AppSettings.Settings.Add("RedminePassword", RedminePasswordTextBox.Text);
            config.AppSettings.Settings.Add("RedmineAuthentication", AuthenticationCheckBox.Checked.ToString());
            config.AppSettings.Settings.Add("CheckForUpdates", CheckForUpdatesCheckBox.Checked.ToString());
            config.AppSettings.Settings.Add("CacheLifetime", CacheLifetime.Value.ToString());
            config.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection("appSettings");
        }

        private void LoadConfig()
        {
            RedmineBaseUrlTextBox.Text = ConfigurationManager.AppSettings["RedmineURL"];
            RedmineUsernameTextBox.Text = ConfigurationManager.AppSettings["RedmineUser"];
            RedminePasswordTextBox.Text = ConfigurationManager.AppSettings["RedminePassword"];
            AuthenticationCheckBox.Checked = Convert.ToBoolean(ConfigurationManager.AppSettings["RedmineAuthentication"]);
            CheckForUpdatesCheckBox.Checked = Convert.ToBoolean(ConfigurationManager.AppSettings["CheckForUpdates"]);
            CacheLifetime.Value = Convert.ToInt32(ConfigurationManager.AppSettings["CacheLifetime"]);
        }

        private void AuthenticationCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            EnableDisableAuthenticationFields();
        }

        private void EnableDisableAuthenticationFields()
        {
            if (AuthenticationCheckBox.Checked)
            {
                RedmineUsernameTextBox.Enabled = true;
                RedminePasswordTextBox.Enabled = true;
            }
            else
            {
                RedmineUsernameTextBox.Enabled = false;
                RedminePasswordTextBox.Enabled = false;
            }
        }
    }
}
