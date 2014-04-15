namespace CoinTNet.UI.Controls.StrategyParameters
{
    partial class SimpleStrategyControl
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.numBuyThreshold = new System.Windows.Forms.NumericUpDown();
            this.numSellThreshold = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.numBuyThreshold)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numSellThreshold)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(71, 13);
            this.label1.TabIndex = 14;
            this.label1.Text = "Buy threshold";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 39);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(70, 13);
            this.label2.TabIndex = 15;
            this.label2.Text = "Sell threshold";
            // 
            // numBuyThreshold
            // 
            this.numBuyThreshold.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.numBuyThreshold.DecimalPlaces = 3;
            this.numBuyThreshold.Location = new System.Drawing.Point(102, 11);
            this.numBuyThreshold.Name = "numBuyThreshold";
            this.numBuyThreshold.Size = new System.Drawing.Size(54, 20);
            this.numBuyThreshold.TabIndex = 0;
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
            this.numSellThreshold.Location = new System.Drawing.Point(102, 37);
            this.numSellThreshold.Name = "numSellThreshold";
            this.numSellThreshold.Size = new System.Drawing.Size(54, 20);
            this.numSellThreshold.TabIndex = 1;
            this.numSellThreshold.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numSellThreshold.Value = new decimal(new int[] {
            25,
            0,
            0,
            196608});
            // 
            // UpDownStrategyControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.numSellThreshold);
            this.Controls.Add(this.numBuyThreshold);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "UpDownStrategyControl";
            this.Size = new System.Drawing.Size(172, 126);
            ((System.ComponentModel.ISupportInitialize)(this.numBuyThreshold)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numSellThreshold)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown numBuyThreshold;
        private System.Windows.Forms.NumericUpDown numSellThreshold;
    }
}
