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
using System.Runtime.InteropServices;

using FQLParserWrapper;

namespace DBExplorer
{
    public partial class TerminalWindow : WeifenLuo.WinFormsUI.Docking.DockContent
    {
        private String promptSymbol = "freshdb> ";
        private String promptContinue = "    ...> ";
        private FQLParser FQL = new FQLParser();

        private MainWindow mainWnd;
        public TerminalWindow(MainWindow _mainWnd)
        {
            mainWnd = _mainWnd;
            InitializeComponent();
        }

        private int inputStart = 0;
        private void WriteOutput(String output)
        {
            try
            {
                this.Invoke((MethodInvoker)delegate
                {
                    richTxtTerm.SelectedText += output;
                    inputStart = richTxtTerm.SelectionStart;
                });
            }
            catch (Exception)
            {
                
            }
        }

        private void TerminalWindow_Load(object sender, EventArgs e)
        {
            WriteOutput("FreshDB Explorer version " + Assembly.GetExecutingAssembly().GetName().Version + Environment.NewLine);
            WriteOutput("Enter `.help` for instructions" + Environment.NewLine);
            WriteOutput("Enter FQL Statments after connected" + Environment.NewLine + Environment.NewLine);
            WriteOutput(promptSymbol);
        }

        bool canKeyDown = true;
        private void richTxtTerm_KeyDown(object sender, KeyEventArgs e)
        {
            if (!canKeyDown)
            { 
                e.SuppressKeyPress = true;
                return;
            }

            if (richTxtTerm.SelectionStart <= inputStart && e.KeyCode == Keys.Back)
                e.SuppressKeyPress = true;

            if (richTxtTerm.SelectionStart < inputStart && !(e.KeyCode == Keys.Left || e.KeyCode == Keys.Right || e.KeyCode == Keys.Up || e.KeyCode == Keys.Down))
                richTxtTerm.SelectionStart = inputStart;

            if (e.KeyCode == Keys.Return)
            {
                richTxtTerm.SelectionStart = richTxtTerm.Text.Length;

                String input = richTxtTerm.Text.Substring(inputStart, richTxtTerm.SelectionStart - inputStart);
                e.SuppressKeyPress = true;
                WriteOutput(Environment.NewLine);

                if (input.StartsWith("."))
                {
                    if (input == ".help")
                    {
                        WriteOutput(".connect       Connect a remote FreshDB" + Environment.NewLine);
                        WriteOutput(".disconnect    Disconnect from current connected FreshDB" + Environment.NewLine);
                        WriteOutput(".new           Create a new instance of FreshDB Explorer." + Environment.NewLine);
                        WriteOutput(".close         Close current instance of FreshDB Explorer." + Environment.NewLine);
                        WriteOutput(".quit          Exit FreshDB Explorer." + Environment.NewLine);
                        WriteOutput(".help          Show this message" + Environment.NewLine);
                    }
                    else if (input == ".connect")
                    {
                        if (mainWnd.FindWindow("Connect") != null)
                            WriteOutput("Connect window is opened, omitting this operation." + Environment.NewLine);
                        else
                        {
                            if (!mainWnd.IsConnected())
                                mainWnd.ClickConnectBtn(sender, e);
                            else
                                WriteOutput("A connection has been established. Please disconnect first." + Environment.NewLine);
                        }
                    }
                    else if (input == ".disconnect")
                    {
                        if (mainWnd.FindWindow("Connect") != null)
                            WriteOutput("Connect window is opened, omitting this operation." + Environment.NewLine);
                        else
                        {
                            if (!mainWnd.IsConnected())
                                WriteOutput("There is no connection. Please connect first." + Environment.NewLine);
                            else
                                mainWnd.ClickConnectBtn(sender, e);
                        }
                    }
                    else if (input == ".new")
                        mainWnd.newInstanceToolStripMenuItem_Click(sender, e);
                    else if (input == ".close")
                        mainWnd.Close();
                    else if (input == ".quit")
                        mainWnd.exitXToolStripMenuItem_Click(sender, e);
                    else
                        WriteOutput("Unknown command: `" + input + "`. Enter `.help` for help." + Environment.NewLine);

                    WriteOutput(promptSymbol);
                }
                else
                {
                    List<String> param = new List<String>();
                    KeyValuePair<bool, String> ret = FQL.Parse(input, param);

                    if (!FQLParser.IsOK(ret))
                    {
                        WriteOutput(FQLParser.ToString(ret) + Environment.NewLine);
                        WriteOutput(promptSymbol);
                    }
                    else
                    {
                        if (!mainWnd.IsConnected())
                        {
                            WriteOutput("There is no connection. Please connect first." + Environment.NewLine);
                            WriteOutput(promptSymbol);
                            return;
                        }

                        if (!FQL.IsProcBegin())
                        {
                            String cmdToSend = string.Empty;
                            if (param[0] == "proc end")
                            {
                                if (param.Count == 1) // only proc end
                                {
                                    WriteOutput(promptSymbol);
                                    return;
                                }

                                param[0] = "proc begin";
                                foreach (string cmd in param)
                                    cmdToSend += cmd.Trim() + "\0";
                                cmdToSend += "proc end";
                            }
                            else
                            {
                                foreach (string cmd in param)
                                    cmdToSend += " " + cmd;
                                cmdToSend.Trim();
                            }

                            Task sendCmd = new Task(() =>
                            {
                                canKeyDown = false;

                                String response = mainWnd.SendRequest(cmdToSend);
                                if (response != String.Empty)
                                {
                                    if (response[0] == '0')
                                    {
                                        WriteOutput(response.Substring(1) + Environment.NewLine);
                                        canKeyDown = true;
                                        WriteOutput(promptSymbol);
                                        return;
                                    }

                                    response = response.Substring(1);
                                    if (param[0] == "enumerate")
                                    {
                                        String[] KVPairs = response.Split(new Char[] { '\x1' });
                                        foreach (String item in KVPairs)
                                        {
                                            try
                                            {
                                                String[] kvp = item.Split(new Char[] { '\0' });
                                                WriteOutput("Key: " + kvp[0] + Environment.NewLine + "Value: " + kvp[1] + Environment.NewLine + Environment.NewLine);
                                            }
                                            catch (Exception)
                                            {

                                            }
                                        }
                                    }
                                    else if (param[0] == "list bucket")
                                    {
                                        String[] buckets = response.Split(new Char[] { '\0' });

                                        if (buckets.Length > 0)
                                        {
                                            WriteOutput("Bucket list:" + Environment.NewLine);

                                            foreach (String item in buckets)
                                                WriteOutput("  " + item + Environment.NewLine);

                                            WriteOutput("Count: " + buckets.Length + Environment.NewLine);
                                        }
                                        else
                                            WriteOutput("There is no buckets." + Environment.NewLine);
                                    }
                                    else if (param[0] == "get")
                                        WriteOutput("Value: " + response + Environment.NewLine);
                                    else
                                        WriteOutput(response + Environment.NewLine);
                                }
                                else
                                    WriteOutput("An error occured when communicating with server." + Environment.NewLine);

                                canKeyDown = true;
                                WriteOutput(promptSymbol);
                            });
                            sendCmd.Start();
                        }
                        else
                            WriteOutput(promptContinue);
                    }
                }
            }
        }
    }
}
