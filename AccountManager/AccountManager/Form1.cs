using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Xml;

namespace AccountManager
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        // Form Load Event
        private void Form1_Load(object sender, EventArgs e)
        {
            listView1.View = View.Details;
            listView1.GridLines = true;
            listView1.FullRowSelect = true;

            //Add column header
            listView1.Columns.Add("ACCOUNT NAME", 120);
            listView1.Columns.Add("BAN STATUS", 100);
            listView1.Columns.Add("READY STATUS", 139);

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
                    listView1.Items.Add(itm);
                }

                foreach (var item in listView1.Items.Cast<ListViewItem>())
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

                foreach (var item in listView1.Items.Cast<ListViewItem>())
                    if (item.SubItems[2].Text.Length > 6)
                    {
                        var myDateTime = DateTime.Parse(DateTime.Now.ToShortTimeString());
                        long CurrentTime = Convert.ToInt64(myDateTime.ToString().Replace(".", "").Replace(":", "").Replace(" ", ""));

                        var uCConvertedTime = item.SubItems[2].Text.Replace(".", "").Replace(":", "").Replace(" ", "");
                        long CConvertedTime = Convert.ToInt64(uCConvertedTime);

                        if (CurrentTime >= CConvertedTime)
                        {
                            item.SubItems[2].Text = "READY";
                            item.BackColor = Color.Green;

                            foreach (XmlNode subNode in AccountsContainer)
                                if (subNode.Attributes.GetNamedItem("VALUE").InnerText.Equals(item.SubItems[0].Text))
                                {
                                    subNode.ChildNodes[0].InnerText = "READY";
                                }

                            doc.Save("Accounts.xml");
                        }
                    }
            }
            else
            {
                File.WriteAllText("Accounts.xml", "<Informations>\r\n  <Accounts>\r\n  </Accounts>\r\n</Informations>");
            }
        }

        // Add Account Button
        private void button1_Click(object sender, EventArgs e)
        {
            var AlreadyExists = false;

            if (textBox1.Text.Length == 0)
            {
                MessageBox.Show("Textbox is empty");
                return;
            }

            foreach (var item in listView1.Items.Cast<ListViewItem>())
                if (item.Text.Equals(textBox1.Text))
                {
                    MessageBox.Show("Already Exists");
                    return;
                }

            var arr = new string[4];
            ListViewItem itm;

            arr[0] = textBox1.Text;

            itm = new ListViewItem(arr);
            listView1.Items.Add(itm);

            if (File.Exists("Accounts.xml"))
            {
                var doc = new XmlDocument();
                doc.Load("Accounts.xml");

                var AccountsContainer = doc.DocumentElement.SelectSingleNode("/Informations/Accounts");

                var subNode = AccountsContainer;

                var NAME = doc.CreateElement("Name");
                NAME.SetAttribute("VALUE", textBox1.Text);
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

        // Remove Acount Button
        private void button2_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count == 0)
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
                    if (subNode.Attributes.GetNamedItem("VALUE").InnerText.Equals(listView1.SelectedItems[0].Text))
                    {
                        subNode.ParentNode.RemoveChild(subNode);
                        listView1.SelectedItems[0].Remove();
                    }

                doc.Save("Accounts.xml");
            }
        }

        // Set READY Button
        private void button6_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count == 0)
            {
                MessageBox.Show("Select something first");
                return;
            }

            listView1.SelectedItems[0].SubItems[1].Text = "CLEAN";
            listView1.SelectedItems[0].SubItems[2].Text = "READY";
            listView1.SelectedItems[0].BackColor = Color.Green;

            if (File.Exists("Accounts.xml"))
            {
                var doc = new XmlDocument();
                doc.Load("Accounts.xml");

                var AccountsContainer = doc.DocumentElement.SelectSingleNode("/Informations/Accounts");

                foreach (XmlNode subNode in AccountsContainer)
                {
                    if (subNode.Attributes.GetNamedItem("VALUE").InnerText.Equals(listView1.SelectedItems[0].Text))
                    {
                        subNode.ChildNodes[0].InnerText = "READY";
                        subNode.ChildNodes[1].InnerText = "CLEAN";
                    }

                    doc.Save("Accounts.xml");
                }
            }
        }

        // Set Banned
        private void button3_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count == 0)
            {
                MessageBox.Show("Select something first");
                return;
            }

            listView1.SelectedItems[0].SubItems[1].Text = "BANNED";
            listView1.SelectedItems[0].SubItems[2].Text = "BANNED";
            listView1.SelectedItems[0].BackColor = Color.Red;

            if (File.Exists("Accounts.xml"))
            {
                var doc = new XmlDocument();
                doc.Load("Accounts.xml");

                var AccountsContainer = doc.DocumentElement.SelectSingleNode("/Informations/Accounts");

                foreach (XmlNode subNode in AccountsContainer)
                {
                    if (subNode.Attributes.GetNamedItem("VALUE").InnerText.Equals(listView1.SelectedItems[0].Text))
                    {
                        subNode.ChildNodes[0].InnerText = "BANNED";
                        subNode.ChildNodes[1].InnerText = "BANNED";
                    }

                    doc.Save("Accounts.xml");
                }
            }
        }

        // 24H Cooldown Button
        private void button4_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count == 0)
            {
                MessageBox.Show("Select something first");
                return;
            }

            var myDateTime = DateTime.Parse(DateTime.Now.ToShortTimeString());
            myDateTime = myDateTime.AddHours(24);

            listView1.SelectedItems[0].SubItems[2].Text = myDateTime.ToString();
            listView1.SelectedItems[0].SubItems[1].Text = "CLEAN";
            listView1.SelectedItems[0].BackColor = Color.DeepPink;

            if (File.Exists("Accounts.xml"))
            {
                var doc = new XmlDocument();
                doc.Load("Accounts.xml");

                var AccountsContainer = doc.DocumentElement.SelectSingleNode("/Informations/Accounts");

                foreach (XmlNode subNode in AccountsContainer)
                {
                    if (subNode.Attributes.GetNamedItem("VALUE").InnerText.Equals(listView1.SelectedItems[0].Text))
                    {
                        subNode.ChildNodes[0].InnerText = myDateTime.ToString();
                        subNode.ChildNodes[1].InnerText = "CLEAN";
                    }

                    doc.Save("Accounts.xml");
                }
            }
        }
    }
}