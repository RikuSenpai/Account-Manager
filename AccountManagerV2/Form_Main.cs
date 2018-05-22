using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Xml;

namespace AccountManagerV2
{
    public partial class Form_Main : Form
    {
        public Form_Main()
        {
            InitializeComponent();
        }

        private void Form_Main_Load(object sender, EventArgs e)
        {
            this.RefreshExpireLabel();

            ListView_Accounts.View = View.Details;
            ListView_Accounts.GridLines = true;
            ListView_Accounts.FullRowSelect = true;

            if (!File.Exists("accounts.xml"))
            {
                File.WriteAllText("accounts.xml", "<AccountManagerV2>\r\n  <Accounts>\r\n  </Accounts>\r\n</AccountManagerV2>");
                return;
            }

            var xml_doc = new XmlDocument();

            xml_doc.Load("accounts.xml");

            var accounts_container = xml_doc.DocumentElement?.SelectSingleNode("/AccountManagerV2/Accounts");
            if (accounts_container == null)
                return;

            foreach (XmlNode account_node in accounts_container)
            {
                try
                {
                    var node_attributes = account_node.Attributes;
                    if (node_attributes == null)
                        continue;

                    var account_name = node_attributes.GetNamedItem("VALUE").InnerText;
                    var account_password = account_node.ChildNodes[0].InnerText;
                    var account_status = account_node.ChildNodes[1].InnerText;
                    var account_cooldown = account_node.ChildNodes[2].InnerText;

                    if (!account_cooldown.Equals(string.Empty))
                    {
                        if (DateTime.TryParse(account_cooldown, out var parsed_time))
                        {
                            if (DateTime.Now > parsed_time)
                            {
                                account_status = "Ready";
                                account_cooldown = string.Empty;
                            }
                        }
                        else
                        {
                            account_status = "Unknown";
                            account_cooldown = string.Empty;
                        }
                    }
                    
                    this.ListView_Accounts.Items.Add(new ListViewItem(new[]
                    {
                        account_name,
                        account_password,
                        account_status,
                        account_cooldown
                    }));

                    this.SetItemColor(account_name);
                }
                catch (Exception exception)
                {
                    MessageBox.Show(this, $"Failed to Load Account Data. {exception}", @"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void ListView_Accounts_SelectedIndexChanged(object sender, EventArgs e)
        {
            var items = this.ListView_Accounts.SelectedItems;
            if (items.Count < 1)
                return;

            try
            {
                this.TextBox_AccountName.Text = items[0].Text;
                this.TextBox_AccountPassword.Text = items[0].SubItems[1].Text;
                this.ComboBox_Status.Text = items[0].SubItems[2].Text;

                if (items[0].SubItems[3].Text == string.Empty)
                    return;

                var expire_date = DateTime.Parse(items[0].SubItems[3].Text);

                this.TrackBar_DayTime.Value = expire_date.Hour;
                this.MonthCalendar_CooldownExpireDate.SelectionStart = expire_date.Date;
            }
            catch (Exception exception)
            {
                MessageBox.Show(this, $"Failed to Update Data. {exception}", @"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void TrackBar_DayTime_Scroll(object sender, EventArgs e)
        {
            this.RefreshExpireLabel();
        }

        private void MonthCalendar_CooldownExpireDate_DateChanged(object sender, DateRangeEventArgs e)
        {
            this.RefreshExpireLabel();
        }

        private void Button_AddAccount_Click(object sender, EventArgs e)
        {
            var username = this.TextBox_AccountName.Text;
            var password = this.TextBox_AccountPassword.Text;
            var status = this.ComboBox_Status.Text;
            var expire_date = this.MonthCalendar_CooldownExpireDate.SelectionStart.AddHours(this.TrackBar_DayTime.Value);
            var matching_item = this.ListView_Accounts.Items.Cast<ListViewItem>().FirstOrDefault(item => item.Text.Equals(username));
            var valid_status = false;

            foreach (var status_item in this.ComboBox_Status.Items)
                if (this.ComboBox_Status.GetItemText(status_item).Equals(status))
                {
                    valid_status = true;
                    break;
                }

            if (!valid_status)
            {
                MessageBox.Show(this, "Invalid Status Selected", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                if (matching_item != null)
                {
                    if (status.Equals("Cooldown") && DateTime.Now >= expire_date)
                    {
                        MessageBox.Show(this, "Invalid Expire Date", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    matching_item.SubItems[1].Text = password;
                    matching_item.SubItems[2].Text = status;
                    matching_item.SubItems[3].Text = status.Equals("Cooldown") ? expire_date.ToString() : "" ;
                }
                else
                {
                    if (!username.Any())
                    {
                        MessageBox.Show(this, "Username TextBox is empty", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    if (status.Equals("Cooldown") && DateTime.Now >= expire_date)
                    {
                        MessageBox.Show(this, "Invalid Expire Date", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    this.ListView_Accounts.Items.Add(new ListViewItem(new[] { username, password, status, status.Equals("Cooldown") ? expire_date.ToString() : string.Empty }));
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(this, $"Failed to Add/Update Data. {exception}", @"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            this.UpdateConfig();
            this.SetItemColor(username);
        }

        private void Button_RemoveAccount_Click(object sender, EventArgs e)
        {
            var items = this.ListView_Accounts.SelectedItems;
            var items_count = items.Count;

            if (items_count < 1)
                return;

            for (var i = 0; i < items_count; i++)
                this.ListView_Accounts.Items.Remove(items[i]);

            this.UpdateConfig();
        }

        private void Button_ClearData_Click(object sender, EventArgs e)
        {
            this.TextBox_AccountName.Clear();
            this.TextBox_AccountPassword.Clear();
            this.ComboBox_Status.Text = "Status";
            this.MonthCalendar_CooldownExpireDate.SelectionStart = DateTime.Today.Date;
        }

        private void RefreshExpireLabel()
        {
            this.Label_DayTime.Text = $"Expire Date: {this.MonthCalendar_CooldownExpireDate.SelectionStart.AddHours(this.TrackBar_DayTime.Value)}";
        }

        private void SetItemColor(string username)
        {
            foreach (ListViewItem item in this.ListView_Accounts.Items)
            {
                if (!item.Text.Equals(username))
                    continue;

                switch (item.SubItems[2].Text)
                {
                    case "Ready":
                        item.BackColor = Color.LimeGreen;
                        return;
                    case "Cooldown":
                        item.BackColor = Color.DeepSkyBlue;
                        return;
                    case "Banned":
                        item.BackColor = Color.MediumVioletRed;
                        return;
                    case "Unknown":
                        item.BackColor = Color.White;
                        return;
                    default:
                        break;
                }
            }
        }

        private void UpdateConfig()
        {
            var xml_doc = new XmlDocument();

            xml_doc.Load("accounts.xml");

            var accounts_container = xml_doc.DocumentElement?.SelectSingleNode("/AccountManagerV2/Accounts");
            if (accounts_container == null)
                return;

            accounts_container.RemoveAll();

            foreach (ListViewItem item in this.ListView_Accounts.Items)
            {
                try
                {
                    var name_node = xml_doc.CreateElement("Name");
                    name_node.SetAttribute("VALUE", item.Text);

                    var password_child = xml_doc.CreateElement("Password");
                    password_child.InnerText = item.SubItems[1].Text;

                    var status_child = xml_doc.CreateElement("Status");
                    status_child.InnerText = item.SubItems[2].Text;

                    var cooldown_child = xml_doc.CreateElement("Cooldown");
                    cooldown_child.InnerText = item.SubItems[3].Text;

                    name_node.AppendChild(password_child);
                    name_node.AppendChild(status_child);
                    name_node.AppendChild(cooldown_child);

                    accounts_container.AppendChild(name_node);
                }
                catch (Exception exception)
                {
                    MessageBox.Show(this, $@"Failed to Save Account Data. {exception}", @"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            xml_doc.Save("accounts.xml");
        }
    }
}
