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
    public partial class TerminalWindow : WeifenLuo.WinFormsUI.Docking.DockContent
    {
        public TerminalWindow()
        {
            InitializeComponent();
        }

        private void TerminalWindow_Load(object sender, EventArgs e)
        {
            richTxtTerm.Text = "FreshDB Explorer version " + Assembly.GetExecutingAssembly().GetName().Version + Environment.NewLine;
            richTxtTerm.Text += "Enter `help` for instructions" + Environment.NewLine;
            richTxtTerm.Text += "Enter FQL Statments after connected" + Environment.NewLine + Environment.NewLine;
        }
    }
}
