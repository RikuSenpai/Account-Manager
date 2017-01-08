using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace AcountManagerRework
{
    public partial class Form1 : MetroFramework.Forms.MetroForm
    {
        public Form1()
        {
            InitializeComponent();
        }

        // ON LOAD FUNCTION
        private void Form1_Load(object sender, EventArgs e)
        {
            metroListView1.View = View.Details;
            metroListView1.GridLines = true;
            metroListView1.FullRowSelect = true;

            //Add column header
            metroListView1.Columns.Add("ACCOUNT NAME", 120);
            metroListView1.Columns.Add("BAN STATUS", 100);
            metroListView1.Columns.Add("COOLDOWN STATUS", 138);

            if (File.Exists("Accounts.xml"))
            {
                var doc = new XmlDocument();
                doc.Load("Accounts.xml");

                var AccountsContainer = doc.DocumentElement.SelectSingleNode("/Informations/Accounts");

                var arr = new string[4];
                ListViewItem itm;

                foreach (XmlNode subNode in AccountsContainer)
                {
                    arr[0] = subNode.Attributes.GetNamedItem("VALUE").InnerText;
                    arr[1] = subNode.ChildNodes[1].InnerText;
                    arr[2] = subNode.ChildNodes[0].InnerText;

                    itm = new ListViewItem(arr);
                    metroListView1.Items.Add(itm);
                }

                foreach (var item in metroListView1.Items.Cast<ListViewItem>())
                    if (item.SubItems[2].Text.Equals("READY") && item.SubItems[1].Text.Equals("CLEAN"))
                    {
                        item.BackColor = Color.Green;
                    }
                    else if (item.SubItems[2].Text.Equals("BANNED") || item.SubItems[1].Text.Equals("BANNED"))
                    {
                        item.SubItems[1].Text = "BANNED";
                        item.SubItems[2].Text = "BANNED";
                        item.BackColor = Color.Red;
                    }
                    else
                    {
                        item.BackColor = Color.DeepPink;
                    }

                foreach (var item in metroListView1.Items.Cast<ListViewItem>())
                    if (item.SubItems[2].Text.Length > 6)
                    {
                        var CurrentTime = DateTime.Now;
                        var ConfigTime = DateTime.Parse(item.SubItems[2].Text);

                        if (CurrentTime >= ConfigTime)
                        {
                            item.SubItems[2].Text = "READY";
                            item.BackColor = Color.Green;

                            foreach (XmlNode subNode in AccountsContainer)
                                if (subNode.Attributes.GetNamedItem("VALUE").InnerText.Equals(item.SubItems[0].Text))
                                    subNode.ChildNodes[0].InnerText = "READY";

                            doc.Save("Accounts.xml");
                        }
                    }
            }
            else
            {
                File.WriteAllText("Accounts.xml", "<Informations>\r\n  <Accounts>\r\n  </Accounts>\r\n</Informations>");
            }
        }

        // ADD ACCOUNT BUTTON
        private void metroButton1_Click(object sender, EventArgs e)
        {
            var AlreadyExists = false;

            if (metroTextBox1.Text.Length == 0)
            {
                MessageBox.Show("Textbox is empty");
                return;
            }

            foreach (var item in metroListView1.Items.Cast<ListViewItem>())
                if (item.Text.Equals(metroTextBox1.Text))
                {
                    MessageBox.Show("Already Exists");
                    return;
                }

            var arr = new string[4];
            ListViewItem itm;

            arr[0] = metroTextBox1.Text;
            arr[1] = "CLEAN";
            arr[2] = "READY";

            itm = new ListViewItem(arr);
            metroListView1.Items.Add(itm);

            foreach (var item in metroListView1.Items.Cast<ListViewItem>())
                if (item.Text.Equals(metroTextBox1.Text))
                    item.BackColor = Color.Green;

            if (File.Exists("Accounts.xml"))
            {
                var doc = new XmlDocument();
                doc.Load("Accounts.xml");

                var AccountsContainer = doc.DocumentElement.SelectSingleNode("/Informations/Accounts");

                var subNode = AccountsContainer;

                var NAME = doc.CreateElement("Name");
                NAME.SetAttribute("VALUE", metroTextBox1.Text);
                var COOLDOWNSTATUS = doc.CreateElement("COOLDOWNSTATUS");
                COOLDOWNSTATUS.InnerText = "READY";
                var BANSTATUS = doc.CreateElement("BANSTATUS");
                BANSTATUS.InnerText = "CLEAN";

                NAME.AppendChild(COOLDOWNSTATUS);
                NAME.AppendChild(BANSTATUS);
                subNode.AppendChild(NAME);

                doc.Save("Accounts.xml");
            }
        }

        // REMOVE ACOUNT BUTTON
        private void metroButton2_Click(object sender, EventArgs e)
        {
            if (metroListView1.SelectedItems.Count == 0)
            {
                MessageBox.Show("Select something first");
                return;
            }

            if (File.Exists("Accounts.xml"))
            {
                var doc = new XmlDocument();
                doc.Load("Accounts.xml");

                var AccountsContainer = doc.DocumentElement.SelectSingleNode("/Informations/Accounts");

                foreach (XmlNode subNode in AccountsContainer)
                    if (subNode.Attributes.GetNamedItem("VALUE").InnerText.Equals(metroListView1.SelectedItems[0].Text))
                    {
                        subNode.ParentNode.RemoveChild(subNode);
                        metroListView1.SelectedItems[0].Remove();
                    }

                doc.Save("Accounts.xml");
            }
        }

        // SET READY BUTTON
        private void metroButton3_Click(object sender, EventArgs e)
        {
            if (metroListView1.SelectedItems.Count == 0)
            {
                MessageBox.Show("Select something first");
                return;
            }

            metroListView1.SelectedItems[0].SubItems[1].Text = "CLEAN";
            metroListView1.SelectedItems[0].SubItems[2].Text = "READY";
            metroListView1.SelectedItems[0].BackColor = Color.Green;

            if (File.Exists("Accounts.xml"))
            {
                var doc = new XmlDocument();
                doc.Load("Accounts.xml");

                var AccountsContainer = doc.DocumentElement.SelectSingleNode("/Informations/Accounts");

                foreach (XmlNode subNode in AccountsContainer)
                {
                    if (subNode.Attributes.GetNamedItem("VALUE").InnerText.Equals(metroListView1.SelectedItems[0].Text))
                    {
                        subNode.ChildNodes[0].InnerText = "READY";
                        subNode.ChildNodes[1].InnerText = "CLEAN";
                    }

                    doc.Save("Accounts.xml");
                }
            }
        }

        // SET BANNED BUTTON
        private void metroButton4_Click(object sender, EventArgs e)
        {
            if (metroListView1.SelectedItems.Count == 0)
            {
                MessageBox.Show("Select something first");
                return;
            }

            metroListView1.SelectedItems[0].SubItems[1].Text = "BANNED";
            metroListView1.SelectedItems[0].SubItems[2].Text = "BANNED";
            metroListView1.SelectedItems[0].BackColor = Color.Red;

            if (File.Exists("Accounts.xml"))
            {
                var doc = new XmlDocument();
                doc.Load("Accounts.xml");

                var AccountsContainer = doc.DocumentElement.SelectSingleNode("/Informations/Accounts");

                foreach (XmlNode subNode in AccountsContainer)
                {
                    if (subNode.Attributes.GetNamedItem("VALUE").InnerText.Equals(metroListView1.SelectedItems[0].Text))
                    {
                        subNode.ChildNodes[0].InnerText = "BANNED";
                        subNode.ChildNodes[1].InnerText = "BANNED";
                    }

                    doc.Save("Accounts.xml");
                }
            }
        }

        // SET COOLDOWN BUTTON
        private void metroButton6_Click(object sender, EventArgs e)
        {
            if (metroListView1.SelectedItems.Count == 0)
            {
                MessageBox.Show("Select something first");
                return;
            }

            var myDateTime = DateTime.Parse(DateTime.Now.ToShortTimeString());
            myDateTime = myDateTime.AddHours(metroTrackBar1.Value);

            metroListView1.SelectedItems[0].SubItems[2].Text = myDateTime.ToString();
            metroListView1.SelectedItems[0].SubItems[1].Text = "CLEAN";
            metroListView1.SelectedItems[0].BackColor = Color.DeepPink;

            if (File.Exists("Accounts.xml"))
            {
                var doc = new XmlDocument();
                doc.Load("Accounts.xml");

                var AccountsContainer = doc.DocumentElement.SelectSingleNode("/Informations/Accounts");

                foreach (XmlNode subNode in AccountsContainer)
                {
                    if (subNode.Attributes.GetNamedItem("VALUE").InnerText.Equals(metroListView1.SelectedItems[0].Text))
                    {
                        subNode.ChildNodes[0].InnerText = myDateTime.ToString();
                        subNode.ChildNodes[1].InnerText = "CLEAN";
                    }

                    doc.Save("Accounts.xml");
                }
            }
        }

        // TRACKBAR VALUE CHANGED
        private void metroTrackBar1_ValueChanged(object sender, EventArgs e)
        {
            metroLabel3.Text = metroTrackBar1.Value.ToString();
        }
    }
}
