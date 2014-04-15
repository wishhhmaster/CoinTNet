namespace CoinTNet.UI.Controls
{
    partial class NewsListControl
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
            this.components = new System.ComponentModel.Container();
            this.tlpNewsItems = new System.Windows.Forms.TableLayoutPanel();
            this.tmrRefresh = new System.Windows.Forms.Timer(this.components);
            this.btnManageSources = new CoinTNet.UI.Controls.Common.MyButton();
            this.btnMarkAllRead = new CoinTNet.UI.Controls.Common.MyButton();
            this.btnRefresh = new CoinTNet.UI.Controls.Common.MyButton();
            this.SuspendLayout();
            // 
            // tlpNewsItems
            // 
            this.tlpNewsItems.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tlpNewsItems.AutoScroll = true;
            this.tlpNewsItems.ColumnCount = 1;
            this.tlpNewsItems.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpNewsItems.Location = new System.Drawing.Point(3, 32);
            this.tlpNewsItems.Name = "tlpNewsItems";
            this.tlpNewsItems.RowCount = 1;
            this.tlpNewsItems.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpNewsItems.Size = new System.Drawing.Size(312, 434);
            this.tlpNewsItems.TabIndex = 1;
            this.tlpNewsItems.SizeChanged += new System.EventHandler(this.tlpNewsItems_SizeChanged);
            // 
            // tmrRefresh
            // 
            this.tmrRefresh.Interval = 60000;
            // 
            // btnManageSources
            // 
            this.btnManageSources.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnManageSources.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnManageSources.Location = new System.Drawing.Point(86, 3);
            this.btnManageSources.Name = "btnManageSources";
            this.btnManageSources.Size = new System.Drawing.Size(107, 23);
            this.btnManageSources.TabIndex = 33;
            this.btnManageSources.Text = "Manage sources...";
            this.btnManageSources.UseVisualStyleBackColor = true;
            this.btnManageSources.Click += new System.EventHandler(this.btnNews_Click);
            // 
            // btnMarkAllRead
            // 
            this.btnMarkAllRead.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnMarkAllRead.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnMarkAllRead.Image = global::CoinTNet.Properties.Resources.email_open;
            this.btnMarkAllRead.Location = new System.Drawing.Point(233, 3);
            this.btnMarkAllRead.Name = "btnMarkAllRead";
            this.btnMarkAllRead.Size = new System.Drawing.Size(32, 23);
            this.btnMarkAllRead.TabIndex = 2;
            this.btnMarkAllRead.UseVisualStyleBackColor = true;
            this.btnMarkAllRead.Click += new System.EventHandler(this.btnMarkAllRead_Click);
            // 
            // btnRefresh
            // 
            this.btnRefresh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnRefresh.Image = global::CoinTNet.Properties.Resources.arrow_refresh;
            this.btnRefresh.Location = new System.Drawing.Point(271, 3);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(32, 23);
            this.btnRefresh.TabIndex = 0;
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // NewsListControl
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.Controls.Add(this.btnManageSources);
            this.Controls.Add(this.btnMarkAllRead);
            this.Controls.Add(this.tlpNewsItems);
            this.Controls.Add(this.btnRefresh);
            this.Name = "NewsListControl";
            this.Size = new System.Drawing.Size(318, 469);
            this.ResumeLayout(false);

        }

        #endregion

        private CoinTNet.UI.Controls.Common.MyButton btnRefresh;
        private System.Windows.Forms.TableLayoutPanel tlpNewsItems;
        private System.Windows.Forms.Timer tmrRefresh;
        private CoinTNet.UI.Controls.Common.MyButton btnMarkAllRead;
        private CoinTNet.UI.Controls.Common.MyButton btnManageSources;
    }
}
