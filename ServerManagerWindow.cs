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

namespace DBExplorer
{
    public partial class ServerManagerWindow : WeifenLuo.WinFormsUI.Docking.DockContent
    {
        private MainWindow mainWnd;
        public ServerManagerWindow(MainWindow _mainWnd)
        {
            InitializeComponent();

            vS2012ToolStripExtender1.DefaultRenderer = new ToolStripProfessionalRenderer();
            vS2012ToolStripExtender1.VS2012Renderer = new VS2012ToolStripRenderer();

            vS2012ToolStripExtender1.SetEnableVS2012Style(this.toolBar, true);

            mainWnd = _mainWnd;
        }

        internal void btnConnect_Click(object sender, EventArgs e)
        {
            ConnectWindow login = new ConnectWindow();
            login.Show(mainWnd.dockPanel);
        }
    }
}
