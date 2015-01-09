using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;

using Lextm.SharpSnmpLib;
using WeifenLuo.WinFormsUI.Docking;

namespace DBExplorer
{
    public delegate void SuccessCallback(String response);
    public delegate void ErrorCallback(bool sendError, String response);

    public partial class MainWindow : Form
    {
        private ServerManagerWindow servManWnd;
        private TerminalWindow termWnd;
        private LogWindow logWnd;

        private TcpClient cliSock = new TcpClient();
        public TcpClient ClientSocket
        {
            get { return cliSock; }
            set { cliSock = value; }
        }

        public MainWindow()
        {
            InitializeComponent();

            vS2012ToolStripExtender.DefaultRenderer = new ToolStripProfessionalRenderer();
            vS2012ToolStripExtender.VS2012Renderer = new VS2012ToolStripRenderer();

            vS2012ToolStripExtender.SetEnableVS2012Style(this.menuMain, true);
            vS2012ToolStripExtender.SetEnableVS2012Style(this.toolBar, true);

            servManWnd = new ServerManagerWindow(this);
            termWnd = new TerminalWindow(this);
            logWnd = new LogWindow();

            servManWnd.Show(this.dockPanel, DockState.DockLeft);
            logWnd.Show(this.dockPanel, DockState.DockBottom);
            termWnd.Show(this.dockPanel);
        }

        private void MainWindow_Load(object sender, EventArgs e)
        {
            
        }

        internal void exitXToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void ShowWindow(DockContent content)
        {
            IDockContent wnd = FindWindow(content.Text);
            if (wnd == null)
                content.Show(this.dockPanel);
            else
            {
                wnd.DockHandler.Activate();
                wnd.DockHandler.Form.Focus();
            }
        }

        private void serverManagerWindowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowWindow(servManWnd);
        }

        private void logWindowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowWindow(logWnd);
        }

        private void terminalWindowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowWindow(termWnd);
        }

        internal void newInstanceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MainWindow newWnd = new MainWindow();
            newWnd.Show();
        }

        private void btnNewInstance_Click(object sender, EventArgs e)
        {
            newInstanceToolStripMenuItem_Click(sender, e);
        }

        private void btnServManWnd_Click(object sender, EventArgs e)
        {
            serverManagerWindowToolStripMenuItem_Click(sender, e);
        }

        private void btnLogWnd_Click(object sender, EventArgs e)
        {
            logWindowToolStripMenuItem_Click(sender, e);
        }

        private void btnTermWnd_Click(object sender, EventArgs e)
        {
            terminalWindowToolStripMenuItem_Click(sender, e);
        }

        public void SetStatusBar(String str)
        {
            this.Invoke((MethodInvoker)delegate
            {
                this.lblInfo.Text = str;
                DoLog(str + Environment.NewLine);
            });
        }

        public void DoLog(String str)
        {
            this.Invoke((MethodInvoker)delegate
            {
                logWnd.DoLog(str);
            });
        }

        public IDockContent FindWindow(String title)
        {
           foreach (IDockContent content in dockPanel.Contents)
                    if (content.DockHandler.TabText == title)
                        return content;

           return null;
        }

        public void ClickConnectBtn(object sender, EventArgs e)
        {
            this.Invoke((MethodInvoker)delegate
            {
                servManWnd.btnConnect_Click(sender, e);
            });
        }

        public void SetConnectStatus(bool connected)
        {
            this.Invoke((MethodInvoker)delegate
            {
                if (connected)
                {
                    servManWnd.btnConnect.Text = "Disconnect";
                    servManWnd.btnConnect.Image = global::DBExplorer.Properties.Resources.Disconnect;
                    servManWnd.btnRefresh.Enabled = true;
                    servManWnd.btnAdd.Enabled = true;
                    servManWnd.treeServer.TopNode.SelectedImageKey = "ServerConnect.ico";
                    servManWnd.treeServer.TopNode.ImageKey = "ServerConnect.ico";
                }
                else
                {
                    servManWnd.btnAdd.Enabled = false;
                    servManWnd.btnEdit.Enabled = false;
                    servManWnd.btnDelete.Enabled = false;
                    servManWnd.btnRefresh.Enabled = false;
                    servManWnd.btnConnect.Text = "Connect";
                    servManWnd.btnConnect.Image = global::DBExplorer.Properties.Resources.Connect;
                    servManWnd.treeServer.TopNode.Text = "(Not connected)";
                    servManWnd.treeServer.TopNode.ImageKey = "ServerNotConnect.ico";
                    servManWnd.treeServer.TopNode.SelectedImageKey = "ServerNotConnect.ico";
                }
            });
        }

        public bool IsConnected()
        {
            return ClientSocket.Client.Connected;
        }

        public String SendRequest(String str)
        {
            lock (ClientSocket)
            {
                try
                {
                    NetworkStream stream = ClientSocket.GetStream();
                    stream.ReadTimeout = 5000;
                    UnicodeEncoding encoder = new UnicodeEncoding();

                    byte[] sendBytes = encoder.GetBytes(str);
                    stream.Write(sendBytes, 0, sendBytes.Length);
                    stream.Flush();

                    byte[] buffer = new byte[1048576];
                    int bytesReaded = stream.Read(buffer, 0, buffer.Length);
                    String data = encoder.GetString(buffer, 0, bytesReaded);
                    return data;
                }
                catch (Exception ex)
                {
                    SetStatusBar("Error: " + ex.Message);

                    if (!IsConnected())
                        SetConnectStatus(false);
                    return String.Empty;
                }
            }
        }

        public void AfterConnectedWorks(String ServerAddress)
        {
            Task afterWorks = new Task(() =>
            {
                String response = SendRequest("version");
                if (response != String.Empty)
                {
                    this.Invoke((MethodInvoker)delegate
                    {
                        servManWnd.treeServer.TopNode.Text = ServerAddress + " (FreshDB " + response.Substring(1) + ")";
                    });
                    DoLog("Remote database version: " + response.Substring(1) + Environment.NewLine);
                }

                this.Invoke((MethodInvoker)delegate
                {
                    servManWnd.btnRefresh_Click(null, null);
                });
            });
            afterWorks.Start();
        }

        public void ExecuteCommand(String command, SuccessCallback sc, ErrorCallback ec)
        {
            Task task = new Task(() =>
            {
                String response = SendRequest(command);
                if (response != String.Empty)
                {
                    if (response[0] == '0')
                        ec(false, response.Substring(1));
                    else
                        sc(response.Substring(1));
                }
                else
                    ec(true, String.Empty);
            });
            task.Start();
        }

        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
