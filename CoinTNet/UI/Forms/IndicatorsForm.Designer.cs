namespace CoinTNet.UI.Forms
{
    partial class IndicatorsForm
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
            this.gbMSChart = new System.Windows.Forms.GroupBox();
            this.tlpMSChartsOptions = new System.Windows.Forms.TableLayoutPanel();
            this.label16 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.numSlowMAPeriod = new System.Windows.Forms.NumericUpDown();
            this.numFastMAPeriod = new System.Windows.Forms.NumericUpDown();
            this.cbbMSSlowAvgType = new System.Windows.Forms.ComboBox();
            this.cbbMSFastAvgType = new System.Windows.Forms.ComboBox();
            this.gbMSChart.SuspendLayout();
            this.tlpMSChartsOptions.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numSlowMAPeriod)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numFastMAPeriod)).BeginInit();
            this.SuspendLayout();
            // 
            // gbMSChart
            // 
            this.gbMSChart.Controls.Add(this.tlpMSChartsOptions);
            this.gbMSChart.Location = new System.Drawing.Point(12, 21);
            this.gbMSChart.Name = "gbMSChart";
            this.gbMSChart.Size = new System.Drawing.Size(229, 80);
            this.gbMSChart.TabIndex = 0;
            this.gbMSChart.TabStop = false;
            this.gbMSChart.Text = "MA Indicators";
            // 
            // tlpMSChartsOptions
            // 
            this.tlpMSChartsOptions.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tlpMSChartsOptions.ColumnCount = 3;
            this.tlpMSChartsOptions.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpMSChartsOptions.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlpMSChartsOptions.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlpMSChartsOptions.Controls.Add(this.label16, 1, 1);
            this.tlpMSChartsOptions.Controls.Add(this.label15, 1, 0);
            this.tlpMSChartsOptions.Controls.Add(this.numSlowMAPeriod, 2, 1);
            this.tlpMSChartsOptions.Controls.Add(this.numFastMAPeriod, 2, 0);
            this.tlpMSChartsOptions.Controls.Add(this.cbbMSSlowAvgType, 0, 1);
            this.tlpMSChartsOptions.Controls.Add(this.cbbMSFastAvgType, 0, 0);
            this.tlpMSChartsOptions.Location = new System.Drawing.Point(7, 22);
            this.tlpMSChartsOptions.Name = "tlpMSChartsOptions";
            this.tlpMSChartsOptions.RowCount = 2;
            this.tlpMSChartsOptions.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpMSChartsOptions.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpMSChartsOptions.Size = new System.Drawing.Size(217, 52);
            this.tlpMSChartsOptions.TabIndex = 10;
            // 
            // label16
            // 
            this.label16.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(111, 26);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(40, 26);
            this.label16.TabIndex = 9;
            this.label16.Text = "Period:";
            this.label16.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label15
            // 
            this.label15.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(111, 0);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(40, 26);
            this.label15.TabIndex = 5;
            this.label15.Text = "Period:";
            this.label15.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // numSlowMAPeriod
            // 
            this.numSlowMAPeriod.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.numSlowMAPeriod.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.numSlowMAPeriod.Location = new System.Drawing.Point(157, 29);
            this.numSlowMAPeriod.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numSlowMAPeriod.Name = "numSlowMAPeriod";
            this.numSlowMAPeriod.Size = new System.Drawing.Size(57, 20);
            this.numSlowMAPeriod.TabIndex = 3;
            this.numSlowMAPeriod.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numSlowMAPeriod.Value = new decimal(new int[] {
            21,
            0,
            0,
            0});
            this.numSlowMAPeriod.ValueChanged += new System.EventHandler(this.OnMAPeriodChanged);
            // 
            // numFastMAPeriod
            // 
            this.numFastMAPeriod.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.numFastMAPeriod.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.numFastMAPeriod.Location = new System.Drawing.Point(157, 3);
            this.numFastMAPeriod.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numFastMAPeriod.Name = "numFastMAPeriod";
            this.numFastMAPeriod.Size = new System.Drawing.Size(57, 20);
            this.numFastMAPeriod.TabIndex = 1;
            this.numFastMAPeriod.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numFastMAPeriod.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numFastMAPeriod.ValueChanged += new System.EventHandler(this.OnMAPeriodChanged);
            // 
            // cbbMSSlowAvgType
            // 
            this.cbbMSSlowAvgType.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cbbMSSlowAvgType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbbMSSlowAvgType.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.cbbMSSlowAvgType.FormattingEnabled = true;
            this.cbbMSSlowAvgType.Items.AddRange(new object[] {
            "Simple",
            "Exponential",
            "Triangular",
            "Weighted"});
            this.cbbMSSlowAvgType.Location = new System.Drawing.Point(3, 29);
            this.cbbMSSlowAvgType.Name = "cbbMSSlowAvgType";
            this.cbbMSSlowAvgType.Size = new System.Drawing.Size(102, 21);
            this.cbbMSSlowAvgType.TabIndex = 2;
            this.cbbMSSlowAvgType.SelectedIndexChanged += new System.EventHandler(this.OnMATypeChanged);
            // 
            // cbbMSFastAvgType
            // 
            this.cbbMSFastAvgType.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cbbMSFastAvgType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbbMSFastAvgType.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.cbbMSFastAvgType.FormattingEnabled = true;
            this.cbbMSFastAvgType.Items.AddRange(new object[] {
            "Simple",
            "Exponential",
            "Triangular",
            "Weighted"});
            this.cbbMSFastAvgType.Location = new System.Drawing.Point(3, 3);
            this.cbbMSFastAvgType.Name = "cbbMSFastAvgType";
            this.cbbMSFastAvgType.Size = new System.Drawing.Size(102, 21);
            this.cbbMSFastAvgType.TabIndex = 0;
            this.cbbMSFastAvgType.SelectedIndexChanged += new System.EventHandler(this.OnMATypeChanged);
            // 
            // IndicatorsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(256, 139);
            this.Controls.Add(this.gbMSChart);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "IndicatorsForm";
            this.Text = "Indicators";
            this.TopMost = true;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.IndicatorsForm_FormClosing);
            this.gbMSChart.ResumeLayout(false);
            this.tlpMSChartsOptions.ResumeLayout(false);
            this.tlpMSChartsOptions.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numSlowMAPeriod)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numFastMAPeriod)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gbMSChart;
        private System.Windows.Forms.TableLayoutPanel tlpMSChartsOptions;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.NumericUpDown numSlowMAPeriod;
        private System.Windows.Forms.NumericUpDown numFastMAPeriod;
        private System.Windows.Forms.ComboBox cbbMSSlowAvgType;
        private System.Windows.Forms.ComboBox cbbMSFastAvgType;
    }
}