using System;
using System.Windows.Forms;
using System.Diagnostics;

namespace Behaviac.Design
{
    public partial class ConnectDialog : Form
    {
        private uint m_Port;

        public ConnectDialog(bool useLocalIp, string ip, int portNr)
        {
            InitializeComponent();

            LoadCurrentUnityInstances();

            listView1.Select();

            if (listView1.Items.Count > 0)
            {
                listView1.Items[0].Focused = true;
                listView1.Items[0].Selected = true;
            }
            else
            {
                btnOk.Enabled = false;
            }

//            localIPCheckBox.Checked = useLocalIp;
//            tbServer.Text = !useLocalIp && Utilities.IPOnlyNumbersAndDots(ip) ? ip : Utilities.GetLocalIP();
//            tbServer.Enabled = !useLocalIp;
//            tbPort.Text = portNr.ToString();
        }

        private void LoadCurrentUnityInstances()
        {
            var processes = Process.GetProcessesByName("unity");
            foreach (var p in processes)
            {
                var item = GetItemFromProcess(p);
                listView1.Items.Add(item);
            }
        }

        ListViewItem GetItemFromProcess(Process p)
        {
            var item = new ListViewItem(p.MainWindowTitle);
            var port = 57000 + p.Id % 1000;
            item.SubItems.Add(port.ToString());

            return item;
        }

        public bool UseLocalIP()
        {
            return true;
        }

        public String GetServer()
        {
//            return Utilities.IPOnlyNumbersAndDots(tbServer.Text) ? tbServer.Text : Utilities.GetIP(tbServer.Text);
            return "127.0.0.1";
        }

        public int GetPort()
        {
            if (listView1.SelectedItems.Count > 0)
                return int.Parse(listView1.SelectedItems[0].SubItems[1].Text);
            return Convert.ToInt32(m_Port);
        }

//        private void tbPort_KeyPress(object sender, KeyPressEventArgs e)
//        {
//            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
//            {
//                e.Handled = true;
//            }
//        }
//
//        private void localIPCheckBox_CheckedChanged(object sender, EventArgs e)
//        {
//            if (UseLocalIP())
//            {
////                tbServer.Text = Utilities.GetLocalIP();
////                tbServer.Enabled = false;
//            }
//        }

        private void OnItemSelected(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            if (listView1.SelectedItems.Count <= 0)
            {
                btnOk.Enabled = false;
            }
            else
            {
                btnOk.Enabled = true;
            }
        }
    }
}