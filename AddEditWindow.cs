using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DBExplorer
{
    public partial class AddEditWindow : WeifenLuo.WinFormsUI.Docking.DockContent
    {
        private bool contentChanged = false;
        private bool IsAdd = true;
        private String bucketName, origKeyName, origValue;
        private TreeNode bucketNode;

        private MainWindow mainWnd;
        public AddEditWindow(MainWindow _mainWnd, String _bucketName, String _origKeyName, String _origValue, bool _IsAdd, TreeNode _bucketNode)
        {
            mainWnd = _mainWnd;
            bucketName = _bucketName;
            origKeyName = _origKeyName;
            IsAdd = _IsAdd;
            bucketNode = _bucketNode;
            origValue = _origValue;
            
            InitializeComponent();
        }

        private delegate bool emptyCheckerType(String str, TextBox ctrl, String prompt);
        private void btnSubmit_Click(object sender, EventArgs e)
        {
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

            String key = txtKey.Text.Trim();
            String value = txtValue.Text.Trim();

            if (!emptyChecker(key, txtKey, "Key can't be null")) return;
            if (txtKey.Text.IndexOf('"') != -1 || txtKey.Text.IndexOf('\'') != -1)
            {
                errProvider.SetError(txtKey, "Key can't contain `\"` and `'`");
                txtKey.Focus(); txtKey.SelectAll();
            }

            ErrorCallback ec = (sendError, response) =>
            {
                mainWnd.SetStatusBar("Error occured when put `" + key  + "` into bucket `" + bucketName + "`");
                if (response != String.Empty)
                    mainWnd.DoLog("Detail information: " + response + Environment.NewLine);

                this.Invoke((MethodInvoker)delegate
                {
                    btnSubmit.Text = "Put";
                    btnSubmit.Enabled = true;
                    txtKey.Enabled = true;
                    txtValue.Enabled = true;
                    txtValue.Focus();
                });
            };

            btnSubmit.Text = "Putting...";
            txtKey.Enabled = false;
            txtValue.Enabled = false;
            btnSubmit.Enabled = false;

            mainWnd.ExecuteCommand("select bucket " + bucketName,
                (_unused) =>
                {
                    if (IsAdd)
                    {
                        String response = mainWnd.SendRequest("get '" + key + "'");
                        if (response == String.Empty)
                        {
                            this.Invoke((MethodInvoker)delegate
                            {
                                btnSubmit.Text = "Put";
                                btnSubmit.Enabled = true;
                                txtKey.Enabled = true;
                                txtValue.Enabled = true;
                                txtValue.Focus();
                            });
                            return;
                        }
                        else
                        {
                            if (response[0] == '1')
                            {
                                this.Invoke((MethodInvoker)delegate
                                {
                                    MessageBox.Show(this, "Key `" + key + "` has already existed in bucket `" + bucketName + "`", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    btnSubmit.Text = "Put";
                                    btnSubmit.Enabled = true;
                                    txtKey.Enabled = true;
                                    txtValue.Enabled = true;
                                    txtKey.Focus(); txtKey.SelectAll();
                                });
                                return;
                            }
                        }
                    }

                    String cmd = "put '" + key + "' '" + value + "'";
                    mainWnd.ExecuteCommand(cmd, (response) =>
                    {
                        mainWnd.SetStatusBar("Key `" + key + "` has been put into bucket `" + bucketName + "`");
                        origKeyName = key; contentChanged = false;

                        this.Invoke((MethodInvoker)delegate
                        {
                            UpdateTitle();

                            if (bucketNode.Nodes.Count == 0) // try loading for this new add node
                                bucketNode.Nodes.Add("Loading...");
                            bucketNode.Collapse(); bucketNode.Expand();

                            btnSubmit.Text = "Put";
                            btnSubmit.Enabled = true;
                            txtKey.Enabled = true;
                            txtValue.Enabled = true;
                            txtValue.Focus();
                        });
                    }, ec);
                }, ec);
        }

        private void UpdateTitle()
        {
            String title = String.Empty;
            if (contentChanged) title += "*";
            title += "[" + txtBucket.Text + "] " + txtKey.Text;
            this.Text = title;
        }

        private void txtKey_TextChanged(object sender, EventArgs e)
        {
            if (txtKey.Text != origKeyName)
            {
                contentChanged = true;
                UpdateTitle();
            }
        }

        private void txtValue_TextChanged(object sender, EventArgs e)
        {
            if (contentChanged) return;

            contentChanged = true;
            UpdateTitle();
        }

        private void AddEditWindow_Load(object sender, EventArgs e)
        {
            this.Text = "[" + bucketName + "] " + origKeyName;
            txtBucket.Text = bucketName;
            txtKey.Text = origKeyName;
            txtValue.Text = origValue;
            if (!IsAdd) txtKey.ReadOnly = true;
        }
    }
}
