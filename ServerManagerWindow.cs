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
            if (btnConnect.Text == "Connect")
            {
                IDockContent conn = mainWnd.FindWindow("Connect");
                if (conn == null)
                {
                    ConnectWindow connWnd = new ConnectWindow(mainWnd);
                    connWnd.Show(mainWnd.dockPanel, DockState.DockRight);
                }
                else
                {
                    conn.DockHandler.Activate();
                    conn.DockHandler.Form.Focus();
                }
            }
            else
            {
                // disconnect
                List<Form> formList = new List<Form>();
                foreach (IDockContent content in mainWnd.dockPanel.Documents)
                    if (content.DockHandler.TabText != "Terminal")
                        formList.Add(content.DockHandler.Form);

                foreach (Form f in formList)
                    f.Close();

                mainWnd.ClientSocket.Close();
                
                mainWnd.SetStatusBar("Disconnected.");
                treeServer.TopNode.Nodes.Clear();
                mainWnd.SetConnectStatus(false);
            }
        }

        internal void btnRefresh_Click(object sender, EventArgs e)
        {
            treeServer.TopNode.Nodes.Clear();
            treeServer.TopNode.Nodes.Add("Loading...");

            Task refreshTask = new Task(() => {
                String response = mainWnd.SendRequest("list bucket");
                if (response != String.Empty)
                {
                    this.Invoke((MethodInvoker)delegate {
                        treeServer.TopNode.Nodes.Clear();
                    });

                    if (response.Length == 1) // only '1', no buckets
                        return;

                    response = response.Substring(1, response.Length - 2);
                    String[] buckets = response.Split(new Char[] { '\0' });

                    foreach (String item in buckets)
                        this.Invoke((MethodInvoker)delegate
                        {
                            TreeNode node = treeServer.TopNode.Nodes.Add(item);
                            node.SelectedImageKey = "Bucket.ico";
                            node.ImageKey = "Bucket.ico";
                            node.Nodes.Add("Loading...");
                        });

                    this.Invoke((MethodInvoker)delegate
                    {
                        treeServer.TopNode.Expand();
                    });
                }
            });
            refreshTask.Start();
        }

        private void treeServer_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            
        }

        private void treeServer_AfterSelect(object sender, TreeViewEventArgs e)
        {
            switch (e.Node.Level)
            { 
            case 0:
                btnDelete.Enabled = false;
                btnEdit.Enabled = false;
                break;

            case 1:
                btnEdit.Enabled = false;
                btnDelete.Enabled = true;
                break;

            case 2:
                btnDelete.Enabled = true;
                btnEdit.Enabled = true;
                break;
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            TreeNode cur = treeServer.SelectedNode;
            if (cur == null)
                return;

            if (cur.Level == 2) // key-value
            {
                String bucket = cur.Parent.Text;
                TreeNode bucketNode = cur.Parent;
                String key = cur.Text;

                mainWnd.ExecuteCommand("get '" + key + "'", 
                    (response) => {
                        this.Invoke((MethodInvoker)delegate
                        {
                            AddEditWindow addWnd = new AddEditWindow(mainWnd, bucket, cur.Text, response, false, bucketNode);
                            addWnd.Show(mainWnd.dockPanel, DockState.Document);
                        });
                    },
                    (sendError, response) =>
                    {
                        mainWnd.SetStatusBar("Error occured when get value of key `" + key + "` in bucket `" + bucket + "`");
                        if (response != String.Empty)
                            mainWnd.DoLog("Detail information: " + response + Environment.NewLine);

                        this.Invoke((MethodInvoker)delegate
                        {
                            cur.ImageKey = "Error.ico";
                            cur.SelectedImageKey = "Error.ico";
                        });
                    });
            }
        }

        private TreeNode newBucketNode = null;
        private void btnAdd_Click(object sender, EventArgs e)
        {
            TreeNode cur = treeServer.SelectedNode;
            if (cur == null)
                return;

            if (cur.Level != 2) // server or bucket, then add bucket
            {
                if (cur.Level == 1 && cur.IsExpanded) // speical check for bucket
                    goto insertKeyValue;

                newBucketNode = treeServer.TopNode.Nodes.Add("");
                newBucketNode.SelectedImageKey = "Bucket.ico";
                newBucketNode.ImageKey = "Bucket.ico";
                treeServer.TopNode.Expand();
                newBucketNode.BeginEdit();
                return;
            }

        insertKeyValue:
            String bucket = cur.Level == 1 ? cur.Text : cur.Parent.Text;
            TreeNode bucketNode = cur.Level == 1 ? cur : cur.Parent;
            AddEditWindow addWnd = new AddEditWindow(mainWnd, bucket, "", "", true, bucketNode);
            addWnd.Show(mainWnd.dockPanel, DockState.Document);
        }

        private void ServerManagerWindow_Load(object sender, EventArgs e)
        {
            treeServer.SelectedNode = treeServer.TopNode;
        }

        private void treeServer_AfterLabelEdit(object sender, NodeLabelEditEventArgs e)
        {
            newBucketNode = null;
            if (e == null) return;
            if (e.Label == null)
            {
                e.Node.Remove();
                return;
            }
            if (e.Label.Trim() == String.Empty) // still empty, don't add
            {
                e.Node.Remove();
                return;
            }

            String name = e.Label.Trim();
            if (name.IndexOf(" ") != -1) // no spaces
            {
                MessageBox.Show("No spaces in bucket name.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                e.Node.Remove();
                return;
            }

            mainWnd.ExecuteCommand("create bucket " + name,
                (_unused) =>
                {
                    mainWnd.SetStatusBar("Bucket `" + e.Node.Text + "` created.");
                    this.Invoke((MethodInvoker)delegate
                    {
                        e.Node.Nodes.Add("Loading...");
                    });
                }, 
                (sendError, response) =>
                {
                    mainWnd.SetStatusBar("Error occured when creating bucket `" + name + "`");
                    if (response != String.Empty)
                        mainWnd.DoLog("Detail information: " + response + Environment.NewLine);

                    this.Invoke((MethodInvoker)delegate
                    {
                        // delete this node
                        e.Node.Remove();
                    });
                });
        }

        private void treeServer_BeforeLabelEdit(object sender, NodeLabelEditEventArgs e)
        {
            if (e.Node != newBucketNode)
                e.CancelEdit = true;
        }

        private void treeServer_BeforeExpand(object sender, TreeViewCancelEventArgs e)
        {
            if (e.Node.Level == 0) // server
            {
                if (mainWnd.IsConnected())
                    btnRefresh_Click(sender, e);
            }
            else if (e.Node.Level == 1) // bucket
            {
                e.Node.Nodes.Clear();
                e.Node.Nodes.Add("Loading...");

                ErrorCallback ec = (sendError, response) =>
                {
                    mainWnd.SetStatusBar("Error occured when expanding bucket `" + e.Node.Text + "`");
                    if (response != String.Empty)
                        mainWnd.DoLog("Detail information: " + response + Environment.NewLine);

                    this.Invoke((MethodInvoker)delegate
                    {
                        e.Node.Nodes.Clear();
                        e.Node.ImageKey = "Error.ico";
                        e.Node.SelectedImageKey = "Error.ico";
                    });
                };

                mainWnd.ExecuteCommand("select bucket " + e.Node.Text,
                    (_unused) =>
                    {
                        mainWnd.ExecuteCommand("enumerate", (response) =>
                        {
                            this.Invoke((MethodInvoker)delegate
                            {
                                e.Node.Nodes.Clear();
                            });

                            if (response.Length == 0)
                                return;

                            String[] KVPairs = response.Substring(0, response.Length - 1).Split(new Char[] { '\x1' });
                            foreach (String item in KVPairs)
                            {
                                try
                                {
                                    String[] kvp = item.Split(new Char[] { '\0' });

                                    this.Invoke((MethodInvoker)delegate
                                    {
                                        e.Node.Nodes.Add(kvp[0]);
                                    });
                                }
                                catch (Exception)
                                {

                                }
                            }
                        }, ec);
                    }, ec);
            }
        }

        private void treeServer_DoubleClick(object sender, EventArgs e)
        {
            TreeNode cur = treeServer.SelectedNode;
            if (cur != null) // only deal with bucket
            {
                if (cur.ImageKey == "Error.ico") // error node, no loading
                    return;

                if (cur.Level == 0) // server
                { 
                    if (mainWnd.IsConnected())
                        btnRefresh_Click(sender, e);
                }
                else if (cur.Level == 1) // bucket
                {
                    if (cur.Nodes.Count == 0) // try loading
                    {
                        cur.Nodes.Add("Loading...");
                        cur.Collapse(); cur.Expand();
                    }
                }
                else if (cur.Level == 2) // key-value pair
                {
                    btnEdit_Click(sender, e);
                }
            }    
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            TreeNode cur = treeServer.SelectedNode;
            if (cur != null && cur.Level != 0)
            {
                if (cur.ImageKey == "Error.ico") // error node, delete directly
                {
                    cur.Remove();
                    return;
                }

                if (DialogResult.No == MessageBox.Show("Confirm deleting? Once deleted, cannot be resotred.", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning))
                    return;

                if (cur.Level == 1) // bucket
                {
                    mainWnd.ExecuteCommand("remove bucket " + cur.Text,
                        (_unused) =>
                        {
                            mainWnd.SetStatusBar("Bucket `" + cur.Text + "` deleted.");
                            this.Invoke((MethodInvoker)delegate
                            {
                                // delete this node
                                cur.Remove();
                            });
                        },
                        (sendError, response) =>
                        {
                            mainWnd.SetStatusBar("Error occured when deleting bucket `" + cur.Text + "`");
                            if (response != String.Empty)
                                mainWnd.DoLog("Detail information: " + response + Environment.NewLine);

                            this.Invoke((MethodInvoker)delegate
                            {
                                // delete this node
                                cur.Nodes.Clear();
                                cur.SelectedImageKey = "Error.ico";
                                cur.ImageKey = "Error.ico";
                            });
                        });
                }
                else if (cur.Level == 2) // key-value pair
                {
                    ErrorCallback ec = (sendError, response) =>
                    {
                        mainWnd.SetStatusBar("Error occured when deleting key-value pair `" + cur.Text + "` in bucket `" + cur.Parent.Text + "`");
                        if (response != String.Empty)
                            mainWnd.DoLog("Detail information: " + response + Environment.NewLine);

                        this.Invoke((MethodInvoker)delegate
                        {
                            cur.SelectedImageKey = "Error.ico";
                            cur.ImageKey = "Error.ico"; // change to error icon
                        });
                    };

                    mainWnd.ExecuteCommand("select bucket " + cur.Parent.Text,
                        (_unused) =>
                        {
                            mainWnd.ExecuteCommand("delete '" + cur.Text + "'", (response) =>
                            {
                                mainWnd.SetStatusBar("Key `" + cur.Text + "` deleted.");
                                this.Invoke((MethodInvoker)delegate
                                {
                                    cur.Remove();
                                });
                            }, ec);
                        }, ec);
                }
            }
        }
    }
}