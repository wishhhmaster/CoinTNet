namespace CoinTNet.UI.Controls
{
    partial class OrderBookControl
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea3 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Series series5 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series6 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.numPriceRange = new System.Windows.Forms.NumericUpDown();
            this.chartOrderBook = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.tmrRefresh = new System.Windows.Forms.Timer(this.components);
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.numPriceRange)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartOrderBook)).BeginInit();
            this.SuspendLayout();
            // 
            // numPriceRange
            // 
            this.numPriceRange.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.numPriceRange.Location = new System.Drawing.Point(200, 6);
            this.numPriceRange.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numPriceRange.Name = "numPriceRange";
            this.numPriceRange.Size = new System.Drawing.Size(39, 20);
            this.numPriceRange.TabIndex = 19;
            this.numPriceRange.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            // 
            // chartOrderBook
            // 
            this.chartOrderBook.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            chartArea3.Name = "ChartArea1";
            this.chartOrderBook.ChartAreas.Add(chartArea3);
            this.chartOrderBook.Location = new System.Drawing.Point(3, 26);
            this.chartOrderBook.Name = "chartOrderBook";
            series5.ChartArea = "ChartArea1";
            series5.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.SplineArea;
            series5.Name = "ask";
            series6.ChartArea = "ChartArea1";
            series6.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Area;
            series6.Name = "bid";
            this.chartOrderBook.Series.Add(series5);
            this.chartOrderBook.Series.Add(series6);
            this.chartOrderBook.Size = new System.Drawing.Size(293, 150);
            this.chartOrderBook.TabIndex = 18;
            this.chartOrderBook.Text = "chart1";
            // 
            // btnRefresh
            // 
            this.btnRefresh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRefresh.Image = global::CoinTNet.Properties.Resources.arrow_refresh;
            this.btnRefresh.Location = new System.Drawing.Point(257, 3);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(28, 23);
            this.btnRefresh.TabIndex = 21;
            this.btnRefresh.UseVisualStyleBackColor = true;
            // 
            // tmrRefresh
            // 
            this.tmrRefresh.Interval = 5000;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(137, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 13);
            this.label1.TabIndex = 22;
            this.label1.Text = "Range (%)";
            // 
            // OrderBookControl
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnRefresh);
            this.Controls.Add(this.numPriceRange);
            this.Controls.Add(this.chartOrderBook);
            this.Name = "OrderBookControl";
            this.Size = new System.Drawing.Size(299, 179);
            ((System.ComponentModel.ISupportInitialize)(this.numPriceRange)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartOrderBook)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.NumericUpDown numPriceRange;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartOrderBook;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Timer tmrRefresh;
        private System.Windows.Forms.Label label1;
    }
}
