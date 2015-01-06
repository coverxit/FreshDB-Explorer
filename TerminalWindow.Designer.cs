namespace DBExplorer
{
    partial class TerminalWindow
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
            this.richTxtTerm = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // richTxtTerm
            // 
            this.richTxtTerm.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.richTxtTerm.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richTxtTerm.Font = new System.Drawing.Font("Courier New", 9.818182F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.richTxtTerm.Location = new System.Drawing.Point(0, 0);
            this.richTxtTerm.Name = "richTxtTerm";
            this.richTxtTerm.Size = new System.Drawing.Size(282, 256);
            this.richTxtTerm.TabIndex = 0;
            this.richTxtTerm.Text = "";
            // 
            // TerminalWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(282, 256);
            this.Controls.Add(this.richTxtTerm);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.HideOnClose = true;
            this.Name = "TerminalWindow";
            this.RightToLeftLayout = true;
            this.Text = "Terminal";
            this.Load += new System.EventHandler(this.TerminalWindow_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RichTextBox richTxtTerm;
    }
}