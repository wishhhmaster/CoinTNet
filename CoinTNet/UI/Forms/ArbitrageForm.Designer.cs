namespace CoinTNet.UI.Forms
{
    partial class ArbitrageForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.txtLog = new System.Windows.Forms.TextBox();
            this.cbbCurrency = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.numAmount = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.numFrequency = new System.Windows.Forms.NumericUpDown();
            this.numProfit = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.chkRealTrading = new System.Windows.Forms.CheckBox();
            this.chkLbAllowedPairs = new System.Windows.Forms.CheckedListBox();
            this.label5 = new System.Windows.Forms.Label();
            this.btnStart = new CoinTNet.UI.Controls.Common.MyButton();
            this.label6 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.numAmount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numFrequency)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numProfit)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(50, 40);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(49, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Currency";
            // 
            // txtLog
            // 
            this.txtLog.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtLog.Location = new System.Drawing.Point(501, 40);
            this.txtLog.Multiline = true;
            this.txtLog.Name = "txtLog";
            this.txtLog.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtLog.Size = new System.Drawing.Size(333, 212);
            this.txtLog.TabIndex = 7;
            // 
            // cbbCurrency
            // 
            this.cbbCurrency.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbbCurrency.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbbCurrency.FormattingEnabled = true;
            this.cbbCurrency.Location = new System.Drawing.Point(159, 37);
            this.cbbCurrency.Name = "cbbCurrency";
            this.cbbCurrency.Size = new System.Drawing.Size(121, 21);
            this.cbbCurrency.TabIndex = 0;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(50, 67);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(43, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Amount";
            // 
            // numAmount
            // 
            this.numAmount.Location = new System.Drawing.Point(159, 67);
            this.numAmount.Name = "numAmount";
            this.numAmount.Size = new System.Drawing.Size(120, 20);
            this.numAmount.TabIndex = 1;
            this.numAmount.Value = new decimal(new int[] {
            30,
            0,
            0,
            0});
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(50, 99);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(83, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Frequency (sec)";
            // 
            // numFrequency
            // 
            this.numFrequency.Location = new System.Drawing.Point(160, 99);
            this.numFrequency.Name = "numFrequency";
            this.numFrequency.Size = new System.Drawing.Size(58, 20);
            this.numFrequency.TabIndex = 2;
            this.numFrequency.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            // 
            // numProfit
            // 
            this.numProfit.DecimalPlaces = 1;
            this.numProfit.Location = new System.Drawing.Point(160, 133);
            this.numProfit.Name = "numProfit";
            this.numProfit.Size = new System.Drawing.Size(58, 20);
            this.numProfit.TabIndex = 3;
            this.numProfit.Value = new decimal(new int[] {
            2,
            0,
            0,
            65536});
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(50, 133);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(48, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "Profit (%)";
            // 
            // chkRealTrading
            // 
            this.chkRealTrading.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkRealTrading.Enabled = false;
            this.chkRealTrading.Location = new System.Drawing.Point(50, 172);
            this.chkRealTrading.Name = "chkRealTrading";
            this.chkRealTrading.Size = new System.Drawing.Size(120, 24);
            this.chkRealTrading.TabIndex = 4;
            this.chkRealTrading.Text = "Real trading";
            this.chkRealTrading.UseVisualStyleBackColor = true;
            // 
            // chkLbAllowedPairs
            // 
            this.chkLbAllowedPairs.CheckOnClick = true;
            this.chkLbAllowedPairs.FormattingEnabled = true;
            this.chkLbAllowedPairs.Location = new System.Drawing.Point(330, 59);
            this.chkLbAllowedPairs.Name = "chkLbAllowedPairs";
            this.chkLbAllowedPairs.Size = new System.Drawing.Size(133, 169);
            this.chkLbAllowedPairs.TabIndex = 5;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(327, 40);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(69, 13);
            this.label5.TabIndex = 12;
            this.label5.Text = "Allowed pairs";
            // 
            // btnStart
            // 
            this.btnStart.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnStart.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnStart.Location = new System.Drawing.Point(840, 45);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(75, 23);
            this.btnStart.TabIndex = 6;
            this.btnStart.Text = "Start";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(50, 9);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(357, 13);
            this.label6.TabIndex = 13;
            this.label6.Text = "Note: this is just a POC, that checks for arbitrage opportunity within BTC-e.";
            // 
            // ArbitrageForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(926, 262);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.chkLbAllowedPairs);
            this.Controls.Add(this.chkRealTrading);
            this.Controls.Add(this.numProfit);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.numFrequency);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.numAmount);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cbbCurrency);
            this.Controls.Add(this.txtLog);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.label1);
            this.Name = "ArbitrageForm";
            this.Text = "BTC-e Arbitrage";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ArbitrageForm_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.numAmount)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numFrequency)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numProfit)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private Controls.Common.MyButton btnStart;
        private System.Windows.Forms.TextBox txtLog;
        private System.Windows.Forms.ComboBox cbbCurrency;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown numAmount;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown numFrequency;
        private System.Windows.Forms.NumericUpDown numProfit;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckBox chkRealTrading;
        private System.Windows.Forms.CheckedListBox chkLbAllowedPairs;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
    }
}