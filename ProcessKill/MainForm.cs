using System;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;

namespace ProcessKill
{
    public partial class MainForm : Form
    {
        private Process[] processes = null;

        public MainForm()
        {
            InitializeComponent();
        }

        private void FetchAllProcesses()
        {
            processes = Process.GetProcesses();
        }

        private void KillProcess()
        {
            if (ListBox_Processes.Items.Count == 0 || Process.GetCurrentProcess().ProcessName == ListBox_Processes.SelectedItem.ToString())
            {
                return;
            }
            try
            {
                processes.ToList().ForEach(p =>
                {
                    if (p.ProcessName == ListBox_Processes.SelectedItem.ToString())
                    {
                        p.Kill();
                    }
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void AddContentToListBox()
        {
            var process = from p in processes
                          select p.ProcessName;
            var list = process.Distinct().ToList();
            list.Sort();
            ListBox_Processes.DataSource = list;
        }

        private void AutoFilter()
        {
            if (TextBox_Search.Text.Length == 0)
            {
                AddContentToListBox();
                return;
            }

            var process = from p in processes
                          where p.ProcessName.ToUpper().Contains(TextBox_Search.Text.ToUpper())
                          select p.ProcessName;
            var list = process.Distinct().ToList();
            list.Sort();
            ListBox_Processes.DataSource = list;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            FetchAllProcesses();
            AddContentToListBox();
        }

        private void Button_Refrsh_Click(object sender, EventArgs e)
        {
            FetchAllProcesses();
            AddContentToListBox();
            TextBox_Search.Clear();
        }

        private void Button_Kill_Click(object sender, EventArgs e)
        {
            KillProcess();
            FetchAllProcesses();
            AddContentToListBox();
            TextBox_Search.Clear();
        }

        private void TextBox_Search_TextChanged(object sender, EventArgs e)
        {
            AutoFilter();
        }

        private void Button_Suspend_Click(object sender, EventArgs e)
        {
            Hide();
            new Form_SuspendedWindow().ShowDialog();
            Show();
        }
    }
}
