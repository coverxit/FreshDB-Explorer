using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Reflection;

namespace DBExplorer
{
    public partial class LogWindow : WeifenLuo.WinFormsUI.Docking.DockContent
    {
        public LogWindow()
        {
            InitializeComponent();
        }

        public void DoLog(String str)
        {
            txtLog.Text += "[" + System.DateTime.Now.ToString() + "] " + str;
        }

        private void LogWindow_Load(object sender, EventArgs e)
        {
            txtLog.Text = String.Empty;
            DoLog("FreshDB Explorer started." + Environment.NewLine);
        }
    }
}
