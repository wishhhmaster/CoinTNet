namespace CoinTNet.UI.Controls.StrategyParameters
{
    partial class MACDStrategyControl
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
            this.label16 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.numSlowMAPeriod = new System.Windows.Forms.NumericUpDown();
            this.numFastMAPeriod = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.numBuyThreshold = new System.Windows.Forms.NumericUpDown();
            this.numSellThreshold = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.numSignal = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.numSlowMAPeriod)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numFastMAPeriod)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numBuyThreshold)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numSellThreshold)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numSignal)).BeginInit();
            this.SuspendLayout();
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(12, 37);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(62, 13);
            this.label16.TabIndex = 13;
            this.label16.Text = "Slow period";
            this.label16.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(11, 11);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(59, 13);
            this.label15.TabIndex = 11;
            this.label15.Text = "Fast period";
            this.label15.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // numSlowMAPeriod
            // 
            this.numSlowMAPeriod.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.numSlowMAPeriod.Location = new System.Drawing.Point(111, 35);
            this.numSlowMAPeriod.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numSlowMAPeriod.Name = "numSlowMAPeriod";
            this.numSlowMAPeriod.Size = new System.Drawing.Size(54, 20);
            this.numSlowMAPeriod.TabIndex = 1;
            this.numSlowMAPeriod.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numSlowMAPeriod.Value = new decimal(new int[] {
            26,
            0,
            0,
            0});
            // 
            // numFastMAPeriod
            // 
            this.numFastMAPeriod.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.numFastMAPeriod.Location = new System.Drawing.Point(111, 9);
            this.numFastMAPeriod.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numFastMAPeriod.Name = "numFastMAPeriod";
            this.numFastMAPeriod.Size = new System.Drawing.Size(54, 20);
            this.numFastMAPeriod.TabIndex = 0;
            this.numFastMAPeriod.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numFastMAPeriod.Value = new decimal(new int[] {
            12,
            0,
            0,
            0});
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 87);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(71, 13);
            this.label1.TabIndex = 14;
            this.label1.Text = "Buy threshold";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 113);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(70, 13);
            this.label2.TabIndex = 15;
            this.label2.Text = "Sell threshold";
            // 
            // numBuyThreshold
            // 
            this.numBuyThreshold.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.numBuyThreshold.DecimalPlaces = 3;
            this.numBuyThreshold.Location = new System.Drawing.Point(111, 85);
            this.numBuyThreshold.Name = "numBuyThreshold";
            this.numBuyThreshold.Size = new System.Drawing.Size(54, 20);
            this.numBuyThreshold.TabIndex = 3;
            this.numBuyThreshold.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numBuyThreshold.Value = new decimal(new int[] {
            25,
            0,
            0,
            196608});
            // 
            // numSellThreshold
            // 
            this.numSellThreshold.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.numSellThreshold.DecimalPlaces = 3;
            this.numSellThreshold.Location = new System.Drawing.Point(111, 111);
            this.numSellThreshold.Name = "numSellThreshold";
            this.numSellThreshold.Size = new System.Drawing.Size(54, 20);
            this.numSellThreshold.TabIndex = 4;
            this.numSellThreshold.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numSellThreshold.Value = new decimal(new int[] {
            25,
            0,
            0,
            196608});
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(10, 61);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(36, 13);
            this.label3.TabIndex = 19;
            this.label3.Text = "Signal";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // numSignal
            // 
            this.numSignal.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.numSignal.Location = new System.Drawing.Point(111, 59);
            this.numSignal.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numSignal.Name = "numSignal";
            this.numSignal.Size = new System.Drawing.Size(54, 20);
            this.numSignal.TabIndex = 2;
            this.numSignal.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numSignal.Value = new decimal(new int[] {
            9,
            0,
            0,
            0});
            // 
            // MACDStrategyControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label3);
            this.Controls.Add(this.numSignal);
            this.Controls.Add(this.numSellThreshold);
            this.Controls.Add(this.numBuyThreshold);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label16);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.numSlowMAPeriod);
            this.Controls.Add(this.numFastMAPeriod);
            this.Name = "MACDStrategyControl";
            this.Size = new System.Drawing.Size(172, 138);
            ((System.ComponentModel.ISupportInitialize)(this.numSlowMAPeriod)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numFastMAPeriod)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numBuyThreshold)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numSellThreshold)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numSignal)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.NumericUpDown numSlowMAPeriod;
        private System.Windows.Forms.NumericUpDown numFastMAPeriod;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown numBuyThreshold;
        private System.Windows.Forms.NumericUpDown numSellThreshold;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown numSignal;
    }
}
