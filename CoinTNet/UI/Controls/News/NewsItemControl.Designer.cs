namespace CoinTNet.UI.Controls.News
{
    partial class NewsItemControl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tlpNewsItem = new System.Windows.Forms.TableLayoutPanel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblFrom = new System.Windows.Forms.Label();
            this.tlpNewsItem.SuspendLayout();
            this.SuspendLayout();
            // 
            // tlpNewsItem
            // 
            this.tlpNewsItem.AutoSize = true;
            this.tlpNewsItem.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tlpNewsItem.ColumnCount = 1;
            this.tlpNewsItem.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpNewsItem.Controls.Add(this.lblTitle, 0, 1);
            this.tlpNewsItem.Controls.Add(this.lblFrom, 0, 0);
            this.tlpNewsItem.Location = new System.Drawing.Point(0, 0);
            this.tlpNewsItem.Margin = new System.Windows.Forms.Padding(0);
            this.tlpNewsItem.Name = "tlpNewsItem";
            this.tlpNewsItem.RowCount = 2;
            this.tlpNewsItem.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpNewsItem.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpNewsItem.Size = new System.Drawing.Size(72, 39);
            this.tlpNewsItem.TabIndex = 0;
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblTitle.ForeColor = System.Drawing.Color.SteelBlue;
            this.lblTitle.Location = new System.Drawing.Point(3, 13);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(66, 26);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "News Line 1\r\nNews Line 2";
            this.lblTitle.Click += new System.EventHandler(this.lblTitle_Click);
            // 
            // lblFrom
            // 
            this.lblFrom.AutoSize = true;
            this.lblFrom.Location = new System.Drawing.Point(3, 0);
            this.lblFrom.Name = "lblFrom";
            this.lblFrom.Size = new System.Drawing.Size(35, 13);
            this.lblFrom.TabIndex = 1;
            this.lblFrom.Text = "label1";
            // 
            // NewsItemControl
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.Controls.Add(this.tlpNewsItem);
            this.MaximumSize = new System.Drawing.Size(150, 0);
            this.Name = "NewsItemControl";
            this.Size = new System.Drawing.Size(72, 39);
            this.tlpNewsItem.ResumeLayout(false);
            this.tlpNewsItem.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tlpNewsItem;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblFrom;
    }
}
