namespace DBExplorer
{
    partial class MainWindow
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            WeifenLuo.WinFormsUI.Docking.DockPanelSkin dockPanelSkin1 = new WeifenLuo.WinFormsUI.Docking.DockPanelSkin();
            WeifenLuo.WinFormsUI.Docking.AutoHideStripSkin autoHideStripSkin1 = new WeifenLuo.WinFormsUI.Docking.AutoHideStripSkin();
            WeifenLuo.WinFormsUI.Docking.DockPanelGradient dockPanelGradient1 = new WeifenLuo.WinFormsUI.Docking.DockPanelGradient();
            WeifenLuo.WinFormsUI.Docking.TabGradient tabGradient1 = new WeifenLuo.WinFormsUI.Docking.TabGradient();
            WeifenLuo.WinFormsUI.Docking.DockPaneStripSkin dockPaneStripSkin1 = new WeifenLuo.WinFormsUI.Docking.DockPaneStripSkin();
            WeifenLuo.WinFormsUI.Docking.DockPaneStripGradient dockPaneStripGradient1 = new WeifenLuo.WinFormsUI.Docking.DockPaneStripGradient();
            WeifenLuo.WinFormsUI.Docking.TabGradient tabGradient2 = new WeifenLuo.WinFormsUI.Docking.TabGradient();
            WeifenLuo.WinFormsUI.Docking.DockPanelGradient dockPanelGradient2 = new WeifenLuo.WinFormsUI.Docking.DockPanelGradient();
            WeifenLuo.WinFormsUI.Docking.TabGradient tabGradient3 = new WeifenLuo.WinFormsUI.Docking.TabGradient();
            WeifenLuo.WinFormsUI.Docking.DockPaneStripToolWindowGradient dockPaneStripToolWindowGradient1 = new WeifenLuo.WinFormsUI.Docking.DockPaneStripToolWindowGradient();
            WeifenLuo.WinFormsUI.Docking.TabGradient tabGradient4 = new WeifenLuo.WinFormsUI.Docking.TabGradient();
            WeifenLuo.WinFormsUI.Docking.TabGradient tabGradient5 = new WeifenLuo.WinFormsUI.Docking.TabGradient();
            WeifenLuo.WinFormsUI.Docking.DockPanelGradient dockPanelGradient3 = new WeifenLuo.WinFormsUI.Docking.DockPanelGradient();
            WeifenLuo.WinFormsUI.Docking.TabGradient tabGradient6 = new WeifenLuo.WinFormsUI.Docking.TabGradient();
            WeifenLuo.WinFormsUI.Docking.TabGradient tabGradient7 = new WeifenLuo.WinFormsUI.Docking.TabGradient();
            this.windowTheme = new WeifenLuo.WinFormsUI.Docking.VS2012LightTheme();
            this.statusBar = new System.Windows.Forms.StatusStrip();
            this.lblInfo = new System.Windows.Forms.ToolStripStatusLabel();
            this.menuMain = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.closeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.exitXToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.windowWToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.vS2012ToolStripExtender = new DBExplorer.VS2012ToolStripExtender(this.components);
            this.toolBar = new System.Windows.Forms.ToolStrip();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.dockPanel = new WeifenLuo.WinFormsUI.Docking.DockPanel();
            this.btnNewInstance = new System.Windows.Forms.ToolStripButton();
            this.btnServManWnd = new System.Windows.Forms.ToolStripButton();
            this.btnLogWnd = new System.Windows.Forms.ToolStripButton();
            this.btnTermWnd = new System.Windows.Forms.ToolStripButton();
            this.newInstanceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.serverManagerWindowToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.logWindowToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.terminalWindowToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusBar.SuspendLayout();
            this.menuMain.SuspendLayout();
            this.toolBar.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusBar
            // 
            this.statusBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblInfo});
            this.statusBar.Location = new System.Drawing.Point(0, 674);
            this.statusBar.Name = "statusBar";
            this.statusBar.Size = new System.Drawing.Size(1072, 24);
            this.statusBar.TabIndex = 6;
            this.statusBar.Text = "statusStrip1";
            // 
            // lblInfo
            // 
            this.lblInfo.Name = "lblInfo";
            this.lblInfo.Size = new System.Drawing.Size(46, 19);
            this.lblInfo.Text = "Ready";
            // 
            // menuMain
            // 
            this.menuMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.viewToolStripMenuItem,
            this.windowWToolStripMenuItem});
            this.menuMain.Location = new System.Drawing.Point(0, 0);
            this.menuMain.MdiWindowListItem = this.windowWToolStripMenuItem;
            this.menuMain.Name = "menuMain";
            this.menuMain.Size = new System.Drawing.Size(1072, 27);
            this.menuMain.TabIndex = 9;
            this.menuMain.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newInstanceToolStripMenuItem,
            this.closeToolStripMenuItem,
            this.toolStripSeparator1,
            this.exitXToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(41, 23);
            this.fileToolStripMenuItem.Text = "&File";
            // 
            // closeToolStripMenuItem
            // 
            this.closeToolStripMenuItem.Name = "closeToolStripMenuItem";
            this.closeToolStripMenuItem.Size = new System.Drawing.Size(165, 24);
            this.closeToolStripMenuItem.Text = "&Close";
            this.closeToolStripMenuItem.Click += new System.EventHandler(this.closeToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(162, 6);
            // 
            // exitXToolStripMenuItem
            // 
            this.exitXToolStripMenuItem.Image = global::DBExplorer.Properties.Resources.Exit;
            this.exitXToolStripMenuItem.Name = "exitXToolStripMenuItem";
            this.exitXToolStripMenuItem.Size = new System.Drawing.Size(165, 24);
            this.exitXToolStripMenuItem.Text = "E&xit";
            this.exitXToolStripMenuItem.Click += new System.EventHandler(this.exitXToolStripMenuItem_Click);
            // 
            // viewToolStripMenuItem
            // 
            this.viewToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.serverManagerWindowToolStripMenuItem,
            this.logWindowToolStripMenuItem,
            this.terminalWindowToolStripMenuItem});
            this.viewToolStripMenuItem.Name = "viewToolStripMenuItem";
            this.viewToolStripMenuItem.Size = new System.Drawing.Size(50, 23);
            this.viewToolStripMenuItem.Text = "&View";
            // 
            // windowWToolStripMenuItem
            // 
            this.windowWToolStripMenuItem.Name = "windowWToolStripMenuItem";
            this.windowWToolStripMenuItem.Size = new System.Drawing.Size(71, 23);
            this.windowWToolStripMenuItem.Text = "&Window";
            // 
            // vS2012ToolStripExtender
            // 
            this.vS2012ToolStripExtender.DefaultRenderer = null;
            this.vS2012ToolStripExtender.VS2012Renderer = null;
            // 
            // toolBar
            // 
            this.toolBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnNewInstance,
            this.toolStripSeparator2,
            this.btnServManWnd,
            this.btnLogWnd,
            this.btnTermWnd});
            this.toolBar.Location = new System.Drawing.Point(0, 27);
            this.toolBar.Name = "toolBar";
            this.toolBar.Size = new System.Drawing.Size(1072, 26);
            this.toolBar.TabIndex = 17;
            this.toolBar.Text = "toolStrip1";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 26);
            // 
            // dockPanel
            // 
            this.dockPanel.BackColor = System.Drawing.SystemColors.ControlDark;
            this.dockPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dockPanel.Location = new System.Drawing.Point(0, 53);
            this.dockPanel.Name = "dockPanel";
            this.dockPanel.RightToLeftLayout = true;
            this.dockPanel.Size = new System.Drawing.Size(1072, 621);
            dockPanelGradient1.EndColor = System.Drawing.SystemColors.ControlLight;
            dockPanelGradient1.StartColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(204)))));
            autoHideStripSkin1.DockStripGradient = dockPanelGradient1;
            tabGradient1.EndColor = System.Drawing.SystemColors.Control;
            tabGradient1.StartColor = System.Drawing.SystemColors.Control;
            tabGradient1.TextColor = System.Drawing.SystemColors.ControlDarkDark;
            autoHideStripSkin1.TabGradient = tabGradient1;
            autoHideStripSkin1.TextFont = new System.Drawing.Font("Segoe UI", 9.163636F);
            dockPanelSkin1.AutoHideStripSkin = autoHideStripSkin1;
            tabGradient2.EndColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(206)))), ((int)(((byte)(219)))));
            tabGradient2.StartColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(204)))));
            tabGradient2.TextColor = System.Drawing.Color.White;
            dockPaneStripGradient1.ActiveTabGradient = tabGradient2;
            dockPanelGradient2.EndColor = System.Drawing.SystemColors.Control;
            dockPanelGradient2.StartColor = System.Drawing.SystemColors.Control;
            dockPaneStripGradient1.DockStripGradient = dockPanelGradient2;
            tabGradient3.EndColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(151)))), ((int)(((byte)(234)))));
            tabGradient3.StartColor = System.Drawing.SystemColors.Control;
            tabGradient3.TextColor = System.Drawing.Color.Black;
            dockPaneStripGradient1.InactiveTabGradient = tabGradient3;
            dockPaneStripSkin1.DocumentGradient = dockPaneStripGradient1;
            dockPaneStripSkin1.TextFont = new System.Drawing.Font("Segoe UI", 9.163636F);
            tabGradient4.EndColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(170)))), ((int)(((byte)(220)))));
            tabGradient4.LinearGradientMode = System.Drawing.Drawing2D.LinearGradientMode.Vertical;
            tabGradient4.StartColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(204)))));
            tabGradient4.TextColor = System.Drawing.Color.White;
            dockPaneStripToolWindowGradient1.ActiveCaptionGradient = tabGradient4;
            tabGradient5.EndColor = System.Drawing.SystemColors.ControlLightLight;
            tabGradient5.StartColor = System.Drawing.SystemColors.ControlLightLight;
            tabGradient5.TextColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(204)))));
            dockPaneStripToolWindowGradient1.ActiveTabGradient = tabGradient5;
            dockPanelGradient3.EndColor = System.Drawing.SystemColors.Control;
            dockPanelGradient3.StartColor = System.Drawing.SystemColors.Control;
            dockPaneStripToolWindowGradient1.DockStripGradient = dockPanelGradient3;
            tabGradient6.EndColor = System.Drawing.SystemColors.ControlDark;
            tabGradient6.LinearGradientMode = System.Drawing.Drawing2D.LinearGradientMode.Vertical;
            tabGradient6.StartColor = System.Drawing.SystemColors.Control;
            tabGradient6.TextColor = System.Drawing.SystemColors.GrayText;
            dockPaneStripToolWindowGradient1.InactiveCaptionGradient = tabGradient6;
            tabGradient7.EndColor = System.Drawing.SystemColors.Control;
            tabGradient7.StartColor = System.Drawing.SystemColors.Control;
            tabGradient7.TextColor = System.Drawing.SystemColors.GrayText;
            dockPaneStripToolWindowGradient1.InactiveTabGradient = tabGradient7;
            dockPaneStripSkin1.ToolWindowGradient = dockPaneStripToolWindowGradient1;
            dockPanelSkin1.DockPaneStripSkin = dockPaneStripSkin1;
            this.dockPanel.Skin = dockPanelSkin1;
            this.dockPanel.TabIndex = 19;
            this.dockPanel.Theme = this.windowTheme;
            // 
            // btnNewInstance
            // 
            this.btnNewInstance.Image = global::DBExplorer.Properties.Resources.New;
            this.btnNewInstance.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnNewInstance.Name = "btnNewInstance";
            this.btnNewInstance.Size = new System.Drawing.Size(111, 23);
            this.btnNewInstance.Text = "New Instance";
            this.btnNewInstance.Click += new System.EventHandler(this.btnNewInstance_Click);
            // 
            // btnServManWnd
            // 
            this.btnServManWnd.Image = global::DBExplorer.Properties.Resources.ServerManager;
            this.btnServManWnd.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnServManWnd.Name = "btnServManWnd";
            this.btnServManWnd.Size = new System.Drawing.Size(175, 23);
            this.btnServManWnd.Text = "Server Manage Window";
            this.btnServManWnd.Click += new System.EventHandler(this.btnServManWnd_Click);
            // 
            // btnLogWnd
            // 
            this.btnLogWnd.Image = global::DBExplorer.Properties.Resources.Log;
            this.btnLogWnd.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnLogWnd.Name = "btnLogWnd";
            this.btnLogWnd.Size = new System.Drawing.Size(106, 23);
            this.btnLogWnd.Text = "Log Window";
            this.btnLogWnd.Click += new System.EventHandler(this.btnLogWnd_Click);
            // 
            // btnTermWnd
            // 
            this.btnTermWnd.Image = global::DBExplorer.Properties.Resources.Terminal;
            this.btnTermWnd.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnTermWnd.Name = "btnTermWnd";
            this.btnTermWnd.Size = new System.Drawing.Size(135, 23);
            this.btnTermWnd.Text = "Terminal Window";
            this.btnTermWnd.Click += new System.EventHandler(this.btnTermWnd_Click);
            // 
            // newInstanceToolStripMenuItem
            // 
            this.newInstanceToolStripMenuItem.Image = global::DBExplorer.Properties.Resources.New;
            this.newInstanceToolStripMenuItem.Name = "newInstanceToolStripMenuItem";
            this.newInstanceToolStripMenuItem.Size = new System.Drawing.Size(165, 24);
            this.newInstanceToolStripMenuItem.Text = "&New Instance";
            this.newInstanceToolStripMenuItem.Click += new System.EventHandler(this.newInstanceToolStripMenuItem_Click);
            // 
            // serverManagerWindowToolStripMenuItem
            // 
            this.serverManagerWindowToolStripMenuItem.Image = global::DBExplorer.Properties.Resources.ServerManager;
            this.serverManagerWindowToolStripMenuItem.Name = "serverManagerWindowToolStripMenuItem";
            this.serverManagerWindowToolStripMenuItem.Size = new System.Drawing.Size(229, 24);
            this.serverManagerWindowToolStripMenuItem.Text = "&Server Manager Window";
            this.serverManagerWindowToolStripMenuItem.Click += new System.EventHandler(this.serverManagerWindowToolStripMenuItem_Click);
            // 
            // logWindowToolStripMenuItem
            // 
            this.logWindowToolStripMenuItem.Image = global::DBExplorer.Properties.Resources.Log;
            this.logWindowToolStripMenuItem.Name = "logWindowToolStripMenuItem";
            this.logWindowToolStripMenuItem.Size = new System.Drawing.Size(229, 24);
            this.logWindowToolStripMenuItem.Text = "&Log Window";
            this.logWindowToolStripMenuItem.Click += new System.EventHandler(this.logWindowToolStripMenuItem_Click);
            // 
            // terminalWindowToolStripMenuItem
            // 
            this.terminalWindowToolStripMenuItem.Image = global::DBExplorer.Properties.Resources.Terminal;
            this.terminalWindowToolStripMenuItem.Name = "terminalWindowToolStripMenuItem";
            this.terminalWindowToolStripMenuItem.Size = new System.Drawing.Size(229, 24);
            this.terminalWindowToolStripMenuItem.Text = "&Terminal Window";
            this.terminalWindowToolStripMenuItem.Click += new System.EventHandler(this.terminalWindowToolStripMenuItem_Click);
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1072, 698);
            this.Controls.Add(this.dockPanel);
            this.Controls.Add(this.toolBar);
            this.Controls.Add(this.statusBar);
            this.Controls.Add(this.menuMain);
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.menuMain;
            this.Name = "MainWindow";
            this.Text = "FreshDB Explorer";
            this.Load += new System.EventHandler(this.MainWindow_Load);
            this.statusBar.ResumeLayout(false);
            this.statusBar.PerformLayout();
            this.menuMain.ResumeLayout(false);
            this.menuMain.PerformLayout();
            this.toolBar.ResumeLayout(false);
            this.toolBar.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private WeifenLuo.WinFormsUI.Docking.VS2012LightTheme windowTheme;
        private System.Windows.Forms.StatusStrip statusBar;
        private System.Windows.Forms.MenuStrip menuMain;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem viewToolStripMenuItem;
        private System.Windows.Forms.ToolStripStatusLabel lblInfo;
        private System.Windows.Forms.ToolStripMenuItem windowWToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem exitXToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem serverManagerWindowToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem logWindowToolStripMenuItem;
        private VS2012ToolStripExtender vS2012ToolStripExtender;
        private System.Windows.Forms.ToolStripMenuItem terminalWindowToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newInstanceToolStripMenuItem;
        private System.Windows.Forms.ToolStrip toolBar;
        internal WeifenLuo.WinFormsUI.Docking.DockPanel dockPanel;
        private System.Windows.Forms.ToolStripButton btnNewInstance;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton btnServManWnd;
        private System.Windows.Forms.ToolStripButton btnLogWnd;
        private System.Windows.Forms.ToolStripButton btnTermWnd;
        private System.Windows.Forms.ToolStripMenuItem closeToolStripMenuItem;

    }
}