namespace CoinTNet.UI.Forms
{
    partial class MainForm
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
            this.tmrSlowRefresh = new System.Windows.Forms.Timer(this.components);
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.lblStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.tickerControl1 = new CoinTNet.UI.Controls.TickerControl();
            this.dataSelector = new CoinTNet.UI.Controls.DataSelector();
            this.myChartControl = new CoinTNet.UI.Controls.MyChartControl();
            this.openOrdersControl = new CoinTNet.UI.Controls.OpenOrdersControl();
            this.orderControl = new CoinTNet.UI.Controls.OrderControl();
            this.tcRightPane = new System.Windows.Forms.TabControl();
            this.tpGeneral = new System.Windows.Forms.TabPage();
            this.btnOptions = new CoinTNet.UI.Controls.Common.MyButton();
            this.btnSaveCandles = new System.Windows.Forms.Button();
            this.btnBackTesting = new CoinTNet.UI.Controls.Common.MyButton();
            this.btnIndicators = new CoinTNet.UI.Controls.Common.MyButton();
            this.btnArbitrage = new CoinTNet.UI.Controls.Common.MyButton();
            this.orderBookControl1 = new CoinTNet.UI.Controls.OrderBookControl();
            this.tpNews = new System.Windows.Forms.TabPage();
            this.newsListControl = new CoinTNet.UI.Controls.NewsListControl();
            this.panel1 = new System.Windows.Forms.Panel();
            this.statusStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.tcRightPane.SuspendLayout();
            this.tpGeneral.SuspendLayout();
            this.tpNews.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tmrSlowRefresh
            // 
            this.tmrSlowRefresh.Enabled = true;
            this.tmrSlowRefresh.Interval = 10000;
            this.tmrSlowRefresh.Tick += new System.EventHandler(this.OnSlowTimerTick);
            // 
            // statusStrip
            // 
            this.statusStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Visible;
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblStatus});
            this.statusStrip.Location = new System.Drawing.Point(0, 655);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Padding = new System.Windows.Forms.Padding(1, 0, 16, 0);
            this.statusStrip.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.statusStrip.Size = new System.Drawing.Size(1264, 22);
            this.statusStrip.TabIndex = 5;
            this.statusStrip.Text = "statusStrip1";
            // 
            // lblStatus
            // 
            this.lblStatus.BackColor = System.Drawing.Color.Transparent;
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(48, 17);
            this.lblStatus.Text = "Ready...";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.BackColor = System.Drawing.Color.White;
            this.splitContainer1.Panel1.Controls.Add(this.tickerControl1);
            this.splitContainer1.Panel1.Controls.Add(this.dataSelector);
            this.splitContainer1.Panel1.Controls.Add(this.myChartControl);
            this.splitContainer1.Panel1.Controls.Add(this.openOrdersControl);
            this.splitContainer1.Panel1.Controls.Add(this.orderControl);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.tcRightPane);
            this.splitContainer1.Size = new System.Drawing.Size(1264, 655);
            this.splitContainer1.SplitterDistance = 977;
            this.splitContainer1.SplitterWidth = 5;
            this.splitContainer1.TabIndex = 9;
            // 
            // tickerControl1
            // 
            this.tickerControl1.Location = new System.Drawing.Point(580, 12);
            this.tickerControl1.Name = "tickerControl1";
            this.tickerControl1.Size = new System.Drawing.Size(364, 49);
            this.tickerControl1.TabIndex = 26;
            // 
            // dataSelector
            // 
            this.dataSelector.BackColor = System.Drawing.Color.White;
            this.dataSelector.Location = new System.Drawing.Point(25, 6);
            this.dataSelector.Name = "dataSelector";
            this.dataSelector.SelectorType = CoinTNet.UI.Common.SelectorType.Main;
            this.dataSelector.Size = new System.Drawing.Size(532, 69);
            this.dataSelector.TabIndex = 25;
            this.dataSelector.UseLiveData = true;
            // 
            // myChartControl
            // 
            this.myChartControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.myChartControl.Location = new System.Drawing.Point(12, 327);
            this.myChartControl.Name = "myChartControl";
            this.myChartControl.Size = new System.Drawing.Size(953, 322);
            this.myChartControl.TabIndex = 22;
            // 
            // openOrdersControl
            // 
            this.openOrdersControl.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.openOrdersControl.Location = new System.Drawing.Point(394, 75);
            this.openOrdersControl.Name = "openOrdersControl";
            this.openOrdersControl.Size = new System.Drawing.Size(550, 224);
            this.openOrdersControl.TabIndex = 10;
            // 
            // orderControl
            // 
            this.orderControl.Location = new System.Drawing.Point(16, 72);
            this.orderControl.Name = "orderControl";
            this.orderControl.Size = new System.Drawing.Size(372, 253);
            this.orderControl.TabIndex = 9;
            // 
            // tcRightPane
            // 
            this.tcRightPane.Controls.Add(this.tpGeneral);
            this.tcRightPane.Controls.Add(this.tpNews);
            this.tcRightPane.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tcRightPane.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tcRightPane.Location = new System.Drawing.Point(0, 0);
            this.tcRightPane.Name = "tcRightPane";
            this.tcRightPane.SelectedIndex = 0;
            this.tcRightPane.Size = new System.Drawing.Size(282, 655);
            this.tcRightPane.TabIndex = 9;
            // 
            // tpGeneral
            // 
            this.tpGeneral.Controls.Add(this.panel1);
            this.tpGeneral.Controls.Add(this.orderBookControl1);
            this.tpGeneral.Location = new System.Drawing.Point(4, 24);
            this.tpGeneral.Name = "tpGeneral";
            this.tpGeneral.Padding = new System.Windows.Forms.Padding(3);
            this.tpGeneral.Size = new System.Drawing.Size(274, 627);
            this.tpGeneral.TabIndex = 0;
            this.tpGeneral.Text = "General";
            this.tpGeneral.UseVisualStyleBackColor = true;
            // 
            // btnOptions
            // 
            this.btnOptions.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOptions.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOptions.Image = global::CoinTNet.Properties.Resources.key;
            this.btnOptions.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnOptions.Location = new System.Drawing.Point(32, 107);
            this.btnOptions.Name = "btnOptions";
            this.btnOptions.Size = new System.Drawing.Size(85, 25);
            this.btnOptions.TabIndex = 34;
            this.btnOptions.Text = "Options";
            this.btnOptions.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnOptions.UseVisualStyleBackColor = true;
            this.btnOptions.Click += new System.EventHandler(this.btnOptions_Click);
            // 
            // btnSaveCandles
            // 
            this.btnSaveCandles.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSaveCandles.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnSaveCandles.Image = global::CoinTNet.Properties.Resources.disk;
            this.btnSaveCandles.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSaveCandles.Location = new System.Drawing.Point(32, 24);
            this.btnSaveCandles.Name = "btnSaveCandles";
            this.btnSaveCandles.Size = new System.Drawing.Size(112, 25);
            this.btnSaveCandles.TabIndex = 9;
            this.btnSaveCandles.Text = "Save candles...";
            this.btnSaveCandles.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnSaveCandles.UseVisualStyleBackColor = true;
            this.btnSaveCandles.Click += new System.EventHandler(this.btnSaveCandles_Click);
            // 
            // btnBackTesting
            // 
            this.btnBackTesting.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBackTesting.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBackTesting.Location = new System.Drawing.Point(168, 24);
            this.btnBackTesting.Name = "btnBackTesting";
            this.btnBackTesting.Size = new System.Drawing.Size(93, 25);
            this.btnBackTesting.TabIndex = 33;
            this.btnBackTesting.Text = "Back testing";
            this.btnBackTesting.UseVisualStyleBackColor = true;
            this.btnBackTesting.Click += new System.EventHandler(this.btnBackTesting_Click);
            // 
            // btnIndicators
            // 
            this.btnIndicators.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnIndicators.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnIndicators.Image = global::CoinTNet.Properties.Resources.chart_curve;
            this.btnIndicators.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnIndicators.Location = new System.Drawing.Point(32, 59);
            this.btnIndicators.Name = "btnIndicators";
            this.btnIndicators.Size = new System.Drawing.Size(85, 25);
            this.btnIndicators.TabIndex = 32;
            this.btnIndicators.Text = "Indicators";
            this.btnIndicators.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnIndicators.UseVisualStyleBackColor = true;
            this.btnIndicators.Click += new System.EventHandler(this.btnIndicators_Click);
            // 
            // btnArbitrage
            // 
            this.btnArbitrage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnArbitrage.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnArbitrage.Location = new System.Drawing.Point(168, 60);
            this.btnArbitrage.Name = "btnArbitrage";
            this.btnArbitrage.Size = new System.Drawing.Size(75, 25);
            this.btnArbitrage.TabIndex = 31;
            this.btnArbitrage.Text = "Arbitrage";
            this.btnArbitrage.UseVisualStyleBackColor = true;
            this.btnArbitrage.Click += new System.EventHandler(this.btnArbitrage_Click);
            // 
            // orderBookControl1
            // 
            this.orderBookControl1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.orderBookControl1.Location = new System.Drawing.Point(3, 394);
            this.orderBookControl1.Name = "orderBookControl1";
            this.orderBookControl1.Size = new System.Drawing.Size(268, 230);
            this.orderBookControl1.TabIndex = 29;
            // 
            // tpNews
            // 
            this.tpNews.Controls.Add(this.newsListControl);
            this.tpNews.Location = new System.Drawing.Point(4, 24);
            this.tpNews.Name = "tpNews";
            this.tpNews.Padding = new System.Windows.Forms.Padding(3);
            this.tpNews.Size = new System.Drawing.Size(274, 627);
            this.tpNews.TabIndex = 4;
            this.tpNews.Text = "News";
            this.tpNews.UseVisualStyleBackColor = true;
            // 
            // newsListControl
            // 
            this.newsListControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.newsListControl.Location = new System.Drawing.Point(3, 3);
            this.newsListControl.Name = "newsListControl";
            this.newsListControl.Size = new System.Drawing.Size(268, 621);
            this.newsListControl.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnSaveCandles);
            this.panel1.Controls.Add(this.btnOptions);
            this.panel1.Controls.Add(this.btnArbitrage);
            this.panel1.Controls.Add(this.btnIndicators);
            this.panel1.Controls.Add(this.btnBackTesting);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(268, 186);
            this.panel1.TabIndex = 35;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1264, 677);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.statusStrip);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "MainForm";
            this.Text = "CoinT.Net";
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.tcRightPane.ResumeLayout(false);
            this.tpGeneral.ResumeLayout(false);
            this.tpNews.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Timer tmrSlowRefresh;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel lblStatus;
        private System.Windows.Forms.TabControl tcRightPane;
        private System.Windows.Forms.TabPage tpGeneral;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private UI.Controls.OrderControl orderControl;
        private UI.Controls.OpenOrdersControl openOrdersControl;
        private System.Windows.Forms.TabPage tpNews;
        private UI.Controls.NewsListControl newsListControl;
        private Controls.OrderBookControl orderBookControl1;
        private Controls.MyChartControl myChartControl;
        private System.Windows.Forms.Button btnSaveCandles;
        private Controls.Common.MyButton btnArbitrage;
        private Controls.Common.MyButton btnIndicators;
        private Controls.Common.MyButton btnBackTesting;
        private Controls.DataSelector dataSelector;
        private Controls.TickerControl tickerControl1;
        private Controls.Common.MyButton btnOptions;
        private System.Windows.Forms.Panel panel1;


    }
}

