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
using System.Threading;
using System.Security.Cryptography;

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

        private MainWindow mainWnd;
        public ConnectWindow(MainWindow _mainWnd)
        {
            mainWnd = _mainWnd;
            InitializeComponent();
        }

        private void ResetWindow()
        {
            this.Invoke((MethodInvoker)delegate
            {
                txtServ.Enabled = true;
                txtUsername.Enabled = true;
                txtPassword.Enabled = true;

                btnConnect.Text = "Connect";
                btnConnect.Focus();
            });
        }

        private String GetMD5(String str)
        {
            ASCIIEncoding encoder = new ASCIIEncoding();
            MD5 md5 = new MD5CryptoServiceProvider();

            byte[] hash = md5.ComputeHash(encoder.GetBytes(str));
            StringBuilder sb = new StringBuilder();
            foreach (byte b in hash)
                sb.Append(b.ToString("x2"));

            return sb.ToString();
        }

        private delegate bool emptyCheckerType(String str, TextBox ctrl, String prompt);
        private delegate bool addressCheckerType(ref String fullAddr, ref String addrOut, ref int portOut);
        private void btnConnect_Click(object sender, EventArgs e)
        {
            if (btnConnect.Text == "Cancel")
            {
                txtServ.Enabled = true;
                txtUsername.Enabled = true;
                txtPassword.Enabled = true;

                btnConnect.Text = "Close";
                mainWnd.SetStatusBar("User cancelled connection.");
                btnConnect.Focus();
                return;
            }

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
            {
                ServerAddress = servFullAddr;
                ServerPort = 17222; // FC
            }

            errProvider.Clear();

            IPAddress servIpAddress = new IPAddress(0xFFFFFFFF);
            Task<bool> resolveTask = new Task<bool>(() =>
            {
                this.Invoke((MethodInvoker)delegate
                {
                    txtServ.Enabled = false;
                    txtUsername.Enabled = false;
                    txtPassword.Enabled = false;

                    btnConnect.Text = "Cancel";

                    mainWnd.SetStatusBar("Resolving `" + servFullAddr + "`...");
                });

                try
                {
                    IPHostEntry ipHost = Dns.GetHostEntry(ServerAddress);
                    foreach (IPAddress ip in ipHost.AddressList)
                        if (ip.AddressFamily == AddressFamily.InterNetwork)
                            servIpAddress = ip;
                    return true;
                }
                catch (Exception ex)
                {
                    ResetWindow();
                    mainWnd.SetStatusBar("Error: " + ex.Message);
                    return false;
                }
            }, TaskCreationOptions.LongRunning | TaskCreationOptions.PreferFairness);

            resolveTask.ContinueWith( (lastTask) =>
            {
                if (lastTask.Result)
                { 
                    this.Invoke((MethodInvoker)delegate
                    {
                        mainWnd.SetStatusBar("Connecting to `" + servFullAddr + "`...");
                        btnConnect.Text = "Cancel";
                    });

                    ManualResetEvent timeoutObj = new ManualResetEvent(false);
                    TcpClient cliSock = new TcpClient();

                    Task<bool> connectTask = new Task<bool>(() =>
                    {
                        try
                        {
                            cliSock.Connect(servIpAddress, ServerPort);
                            timeoutObj.Set();

                            return true;
                        }
                        catch (Exception ex)
                        {
                            this.Invoke((MethodInvoker)delegate
                            {
                                mainWnd.SetStatusBar("Connection reset: " + ex.Message);
                                ResetWindow();
                            });

                            return false;
                        }
                    });
                    connectTask.Start();

                    if (timeoutObj.WaitOne(5000, false))
                    {
                        if (connectTask.Result)
                        {
                            Task sendAuthTask = new Task(() =>
                            {
                                mainWnd.SetStatusBar("Connected.");
                                mainWnd.ClientSocket = cliSock;

                                mainWnd.SetStatusBar("Sending Username & Password...");
                                String response = mainWnd.SendRequest(".login " + userName + " " + GetMD5(passWord));
                                if (response != String.Empty)
                                {
                                    if (response[0] == '0')
                                    {
                                        mainWnd.SetStatusBar("Connection reset: " + response.Substring(1));
                                        ResetWindow();
                                        mainWnd.ClientSocket.Close();
                                        return;
                                    }

                                    mainWnd.SetStatusBar(response.Substring(1));

                                    this.Invoke((MethodInvoker)delegate
                                    {
                                        mainWnd.SetConnectStatus(true);
                                        mainWnd.AfterConnectedWorks(ServerAddress);
                                        this.Close();
                                    });
                                }
                                else
                                    ResetWindow();
                            });
                            sendAuthTask.Start();
                        }
                    }
                    else
                    {
                        cliSock.Close();

                        try
                        {
                            this.Invoke((MethodInvoker)delegate
                            {
                                mainWnd.SetStatusBar("Connection reset: Timeout.");
                                ResetWindow();
                            });
                        }
                        catch (Exception)
                        {
                        	
                        }
                    }
                }  
            });

            resolveTask.Start();
        }
    }
}
