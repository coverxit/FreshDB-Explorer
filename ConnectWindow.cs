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

namespace DBExplorer
{
    public partial class ConnectWindow : WeifenLuo.WinFormsUI.Docking.DockContent
    {
        private String _serverAddress;
        public String ServerAddress
        {
            get { return _serverAddress; }
            set { _serverAddress = value; }
        }

        private int _serverPort;
        public int ServerPort
        {
            get { return _serverPort; }
            set { _serverPort = value; }
        }

        private String _userName;
        public String UserName
        {
            get { return _userName; }
            set { _userName = value; }
        }

        private String _passWord;
        public String Password
        {
            get { return _passWord; }
            set { _passWord = value; }
        }

        public ConnectWindow()
        {
            InitializeComponent();
        }

        private delegate bool emptyCheckerType(String str, TextBox ctrl, String prompt);
        private delegate bool addressCheckerType(ref String fullAddr, ref String addrOut, ref int portOut);
        private void btnConnect_Click(object sender, EventArgs e)
        {
            String servFullAddr = txtServ.Text.Trim();
            String userName = txtUsername.Text.Trim();
            String passWord = txtPassword.Text.Trim();

            emptyCheckerType emptyChecker = (str, ctrl, prompt) =>
            {
                if (str == String.Empty)
                {
                    errProvider.SetError(ctrl, prompt);
                    ctrl.Focus(); ctrl.SelectAll();
                    return false;
                }
                else
                    return true;
            };

            bool noEmpty = true;
            noEmpty &= emptyChecker(servFullAddr, txtServ, "Server address can't be null");
            noEmpty &= emptyChecker(userName, txtUsername, "Username can't be null");
            noEmpty &= emptyChecker(passWord, txtPassword, "Password can't be null");
            if (!noEmpty) return;
           
            if (servFullAddr.IndexOf(':') != -1)
            {
                String[] param = servFullAddr.Split(new Char[] { ':' });

                try
                {
                    ServerAddress = param[0];
                    ServerPort = int.Parse(param[1]);
                }
                catch (Exception)
                {
                    errProvider.SetError(txtServ, "Invalid server address, the format is `[address]:[port]`");
                    txtServ.Focus(); txtServ.SelectAll();
                    return;
                }
            }
            else
                ServerAddress = servFullAddr;

            Task<bool> resolveTask = new Task<bool>(() =>
            {
                this.Invoke((MethodInvoker)delegate
                {
                    txtServ.Enabled = false;
                    txtUsername.Enabled = false;
                    txtPassword.Enabled = false;

                    btnConnect.Enabled = false;
                    btnCancel.Text = "Cancel";
                    lblInfo.Text = "Resolving...";
                });

                try
                {
                    IPHostEntry ipHost = Dns.GetHostEntry(ServerAddress);
                    return true;
                }
                catch (Exception ex)
                {
                    this.Invoke((MethodInvoker)delegate
                    {
                        txtServ.Enabled = true;
                        txtUsername.Enabled = true;
                        txtPassword.Enabled = true;

                        btnConnect.Enabled = true;
                        btnCancel.Text = "Close";
                        btnConnect.Focus();

                        lblInfo.Text = "Error: " + ex.Message;
                    });
                    return false;
                }
            }, TaskCreationOptions.LongRunning | TaskCreationOptions.PreferFairness);

            resolveTask.ContinueWith( (lastTask) =>
            {
                if (lastTask.Result)
                    this.Invoke((MethodInvoker)delegate
                    {
                        lblInfo.Text = "Connecting to " + servFullAddr + "...";
                        btnConnect.Enabled = false;
                        btnCancel.Text = "Cancel";
                    });
            });

            resolveTask.Start();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (btnCancel.Text == "Close")
                this.Close();
            else
            {
                txtServ.Enabled = true;
                txtUsername.Enabled = true;
                txtPassword.Enabled = true;

                btnConnect.Enabled = true;
                btnCancel.Text = "Close";
                lblInfo.Text = "User cancelled connection.";
                btnConnect.Focus();
            }

            this.Close();
        }
    }
}
