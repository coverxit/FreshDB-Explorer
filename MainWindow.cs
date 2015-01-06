using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Lextm.SharpSnmpLib;
using WeifenLuo.WinFormsUI.Docking;

namespace DBExplorer
{
    public partial class MainWindow : Form
    {
        private ServerManagerWindow servManWnd;
        private TerminalWindow termWnd;
        private LogWindow logWnd;

        public MainWindow()
        {
            InitializeComponent();

            vS2012ToolStripExtender1.DefaultRenderer = new ToolStripProfessionalRenderer();
            vS2012ToolStripExtender1.VS2012Renderer = new VS2012ToolStripRenderer();

            vS2012ToolStripExtender1.SetEnableVS2012Style(this.menuMain, true);

            servManWnd = new ServerManagerWindow(this);
            termWnd = new TerminalWindow();
            logWnd = new LogWindow();

            servManWnd.Show(this.dockPanel, DockState.DockLeft);
            logWnd.Show(this.dockPanel, DockState.DockBottom);
            termWnd.Show(this.dockPanel);
        }

        private void MainWindow_Load(object sender, EventArgs e)
        {
            
        }

        private void exitXToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void connectXToolStripMenuItem_Click(object sender, EventArgs e)
        {
            servManWnd.btnConnect_Click(sender, e);
        }

        private void serverManagerWindowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            servManWnd.Show(this.dockPanel);
        }

        private void logWindowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            logWnd.Show(this.dockPanel);
        }

        private void terminalWindowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            termWnd.Show(this.dockPanel);
        }

    }
}
