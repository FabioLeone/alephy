namespace goku
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.cMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tSMenuItem3 = new System.Windows.Forms.ToolStripMenuItem();
            this.tSMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.cMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.ContextMenuStrip = this.cMenuStrip1;
            this.notifyIcon1.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon1.Icon")));
            this.notifyIcon1.Text = "R.O.";
            this.notifyIcon1.Visible = true;
            // 
            // cMenuStrip1
            // 
            this.cMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tSMenuItem1,
            this.tSMenuItem3});
            this.cMenuStrip1.Name = "cMenuStrip1";
            this.cMenuStrip1.ShowImageMargin = false;
            this.cMenuStrip1.Size = new System.Drawing.Size(86, 48);
            // 
            // tSMenuItem3
            // 
            this.tSMenuItem3.Name = "tSMenuItem3";
            this.tSMenuItem3.Size = new System.Drawing.Size(85, 22);
            this.tSMenuItem3.Text = "Close";
            this.tSMenuItem3.Click += new System.EventHandler(this.tSMenuItem3_Click);
            // 
            // tSMenuItem1
            // 
            this.tSMenuItem1.Name = "tSMenuItem1";
            this.tSMenuItem1.Size = new System.Drawing.Size(85, 22);
            this.tSMenuItem1.Text = "Config";
            this.tSMenuItem1.Click += new System.EventHandler(this.tSMenuItem1_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(10, 10);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form1";
            this.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.ShowInTaskbar = false;
            this.Text = "R.O.";
            this.WindowState = System.Windows.Forms.FormWindowState.Minimized;
            this.cMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.NotifyIcon notifyIcon1;
        private System.Windows.Forms.ContextMenuStrip cMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem tSMenuItem3;
        private System.Windows.Forms.ToolStripMenuItem tSMenuItem1;

    }
}

