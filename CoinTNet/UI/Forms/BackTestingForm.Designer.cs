namespace CoinTNet.UI.Forms
{
    partial class BackTestingForm
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
            this.cbbStrategy = new System.Windows.Forms.ComboBox();
            this.pnlPlaceholder = new System.Windows.Forms.Panel();
            this.lblMinAmountItem2ToKeep = new System.Windows.Forms.Label();
            this.lblMinAmountItem1ToKeep = new System.Windows.Forms.Label();
            this.numMinAmountMoney = new System.Windows.Forms.NumericUpDown();
            this.numMinAmountBTC = new System.Windows.Forms.NumericUpDown();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.numFee = new System.Windows.Forms.NumericUpDown();
            this.label8 = new System.Windows.Forms.Label();
            this.numBalBTCTest = new System.Windows.Forms.NumericUpDown();
            this.lblInitialItem1Balance = new System.Windows.Forms.Label();
            this.numBalUSDTest = new System.Windows.Forms.NumericUpDown();
            this.lblInitialItem2Balance = new System.Windows.Forms.Label();
            this.numMaxNbBTCPerSellOrder = new System.Windows.Forms.NumericUpDown();
            this.numMaxAmountMoneyPerBuyOrder = new System.Windows.Forms.NumericUpDown();
            this.lblMaxAmountItem2PerBuy = new System.Windows.Forms.Label();
            this.lblMaxAmountItem1PerSell = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.lblResult = new System.Windows.Forms.Label();
            this.chartSelector = new CoinTNet.UI.Controls.DataSelector();
            this.btnIndicators = new CoinTNet.UI.Controls.Common.MyButton();
            this.chartCtrl = new CoinTNet.UI.Controls.MyChartControl();
            this.btnApply = new CoinTNet.UI.Controls.Common.MyButton();
            ((System.ComponentModel.ISupportInitialize)(this.numMinAmountMoney)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numMinAmountBTC)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numFee)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numBalBTCTest)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numBalUSDTest)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numMaxNbBTCPerSellOrder)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numMaxAmountMoneyPerBuyOrder)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(46, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Strategy";
            // 
            // cbbStrategy
            // 
            this.cbbStrategy.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbbStrategy.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbbStrategy.FormattingEnabled = true;
            this.cbbStrategy.Location = new System.Drawing.Point(171, 22);
            this.cbbStrategy.Name = "cbbStrategy";
            this.cbbStrategy.Size = new System.Drawing.Size(121, 21);
            this.cbbStrategy.TabIndex = 0;
            this.cbbStrategy.SelectedIndexChanged += new System.EventHandler(this.cbbStrategy_SelectedIndexChanged);
            // 
            // pnlPlaceholder
            // 
            this.pnlPlaceholder.Location = new System.Drawing.Point(6, 20);
            this.pnlPlaceholder.Name = "pnlPlaceholder";
            this.pnlPlaceholder.Size = new System.Drawing.Size(330, 138);
            this.pnlPlaceholder.TabIndex = 0;
            // 
            // lblMinAmountItem2ToKeep
            // 
            this.lblMinAmountItem2ToKeep.AutoSize = true;
            this.lblMinAmountItem2ToKeep.Location = new System.Drawing.Point(16, 61);
            this.lblMinAmountItem2ToKeep.Name = "lblMinAmountItem2ToKeep";
            this.lblMinAmountItem2ToKeep.Size = new System.Drawing.Size(137, 13);
            this.lblMinAmountItem2ToKeep.TabIndex = 5;
            this.lblMinAmountItem2ToKeep.Text = "Min Amount money to Keep";
            // 
            // lblMinAmountItem1ToKeep
            // 
            this.lblMinAmountItem1ToKeep.AutoSize = true;
            this.lblMinAmountItem1ToKeep.Location = new System.Drawing.Point(16, 98);
            this.lblMinAmountItem1ToKeep.Name = "lblMinAmountItem1ToKeep";
            this.lblMinAmountItem1ToKeep.Size = new System.Drawing.Size(127, 13);
            this.lblMinAmountItem1ToKeep.TabIndex = 6;
            this.lblMinAmountItem1ToKeep.Text = "Min Amount BTC to Keep";
            // 
            // numMinAmountMoney
            // 
            this.numMinAmountMoney.DecimalPlaces = 2;
            this.numMinAmountMoney.Location = new System.Drawing.Point(171, 59);
            this.numMinAmountMoney.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.numMinAmountMoney.Name = "numMinAmountMoney";
            this.numMinAmountMoney.Size = new System.Drawing.Size(120, 20);
            this.numMinAmountMoney.TabIndex = 1;
            // 
            // numMinAmountBTC
            // 
            this.numMinAmountBTC.DecimalPlaces = 2;
            this.numMinAmountBTC.Location = new System.Drawing.Point(171, 96);
            this.numMinAmountBTC.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.numMinAmountBTC.Name = "numMinAmountBTC";
            this.numMinAmountBTC.Size = new System.Drawing.Size(120, 20);
            this.numMinAmountBTC.TabIndex = 2;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.numFee);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.numBalBTCTest);
            this.groupBox1.Controls.Add(this.lblInitialItem1Balance);
            this.groupBox1.Controls.Add(this.numBalUSDTest);
            this.groupBox1.Controls.Add(this.lblInitialItem2Balance);
            this.groupBox1.Controls.Add(this.numMaxNbBTCPerSellOrder);
            this.groupBox1.Controls.Add(this.numMaxAmountMoneyPerBuyOrder);
            this.groupBox1.Controls.Add(this.lblMaxAmountItem2PerBuy);
            this.groupBox1.Controls.Add(this.lblMaxAmountItem1PerSell);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.numMinAmountBTC);
            this.groupBox1.Controls.Add(this.cbbStrategy);
            this.groupBox1.Controls.Add(this.numMinAmountMoney);
            this.groupBox1.Controls.Add(this.lblMinAmountItem2ToKeep);
            this.groupBox1.Controls.Add(this.lblMinAmountItem1ToKeep);
            this.groupBox1.Location = new System.Drawing.Point(24, 383);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(610, 164);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Base settings";
            // 
            // numFee
            // 
            this.numFee.DecimalPlaces = 2;
            this.numFee.Location = new System.Drawing.Point(474, 129);
            this.numFee.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.numFee.Name = "numFee";
            this.numFee.Size = new System.Drawing.Size(120, 20);
            this.numFee.TabIndex = 7;
            this.numFee.Value = new decimal(new int[] {
            3,
            0,
            0,
            65536});
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(319, 129);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(25, 13);
            this.label8.TabIndex = 17;
            this.label8.Text = "Fee";
            // 
            // numBalBTCTest
            // 
            this.numBalBTCTest.DecimalPlaces = 2;
            this.numBalBTCTest.Location = new System.Drawing.Point(474, 100);
            this.numBalBTCTest.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.numBalBTCTest.Name = "numBalBTCTest";
            this.numBalBTCTest.Size = new System.Drawing.Size(120, 20);
            this.numBalBTCTest.TabIndex = 6;
            this.numBalBTCTest.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            // 
            // lblInitialItem1Balance
            // 
            this.lblInitialItem1Balance.AutoSize = true;
            this.lblInitialItem1Balance.Location = new System.Drawing.Point(319, 100);
            this.lblInitialItem1Balance.Name = "lblInitialItem1Balance";
            this.lblInitialItem1Balance.Size = new System.Drawing.Size(150, 13);
            this.lblInitialItem1Balance.TabIndex = 15;
            this.lblInitialItem1Balance.Text = "Initial BTC balance (Back test)";
            // 
            // numBalUSDTest
            // 
            this.numBalUSDTest.DecimalPlaces = 2;
            this.numBalUSDTest.Location = new System.Drawing.Point(474, 68);
            this.numBalUSDTest.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.numBalUSDTest.Name = "numBalUSDTest";
            this.numBalUSDTest.Size = new System.Drawing.Size(120, 20);
            this.numBalUSDTest.TabIndex = 5;
            this.numBalUSDTest.Value = new decimal(new int[] {
            4000,
            0,
            0,
            0});
            // 
            // lblInitialItem2Balance
            // 
            this.lblInitialItem2Balance.AutoSize = true;
            this.lblInitialItem2Balance.Location = new System.Drawing.Point(319, 68);
            this.lblInitialItem2Balance.Name = "lblInitialItem2Balance";
            this.lblInitialItem2Balance.Size = new System.Drawing.Size(152, 13);
            this.lblInitialItem2Balance.TabIndex = 13;
            this.lblInitialItem2Balance.Text = "Initial USD balance (Back test)";
            // 
            // numMaxNbBTCPerSellOrder
            // 
            this.numMaxNbBTCPerSellOrder.DecimalPlaces = 2;
            this.numMaxNbBTCPerSellOrder.Location = new System.Drawing.Point(474, 37);
            this.numMaxNbBTCPerSellOrder.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.numMaxNbBTCPerSellOrder.Name = "numMaxNbBTCPerSellOrder";
            this.numMaxNbBTCPerSellOrder.Size = new System.Drawing.Size(120, 20);
            this.numMaxNbBTCPerSellOrder.TabIndex = 4;
            this.numMaxNbBTCPerSellOrder.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // numMaxAmountMoneyPerBuyOrder
            // 
            this.numMaxAmountMoneyPerBuyOrder.DecimalPlaces = 2;
            this.numMaxAmountMoneyPerBuyOrder.Location = new System.Drawing.Point(171, 131);
            this.numMaxAmountMoneyPerBuyOrder.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numMaxAmountMoneyPerBuyOrder.Name = "numMaxAmountMoneyPerBuyOrder";
            this.numMaxAmountMoneyPerBuyOrder.Size = new System.Drawing.Size(120, 20);
            this.numMaxAmountMoneyPerBuyOrder.TabIndex = 3;
            this.numMaxAmountMoneyPerBuyOrder.Value = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            // 
            // lblMaxAmountItem2PerBuy
            // 
            this.lblMaxAmountItem2PerBuy.AutoSize = true;
            this.lblMaxAmountItem2PerBuy.Location = new System.Drawing.Point(16, 133);
            this.lblMaxAmountItem2PerBuy.Name = "lblMaxAmountItem2PerBuy";
            this.lblMaxAmountItem2PerBuy.Size = new System.Drawing.Size(152, 13);
            this.lblMaxAmountItem2PerBuy.TabIndex = 9;
            this.lblMaxAmountItem2PerBuy.Text = "Max Amount money/ buy order";
            // 
            // lblMaxAmountItem1PerSell
            // 
            this.lblMaxAmountItem1PerSell.AutoSize = true;
            this.lblMaxAmountItem1PerSell.Location = new System.Drawing.Point(319, 39);
            this.lblMaxAmountItem1PerSell.Name = "lblMaxAmountItem1PerSell";
            this.lblMaxAmountItem1PerSell.Size = new System.Drawing.Size(118, 13);
            this.lblMaxAmountItem1PerSell.TabIndex = 10;
            this.lblMaxAmountItem1PerSell.Text = "Max Nb BTC/ sell order";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.pnlPlaceholder);
            this.groupBox2.Location = new System.Drawing.Point(666, 383);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(342, 164);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Strategy specific settings";
            // 
            // lblResult
            // 
            this.lblResult.AutoSize = true;
            this.lblResult.Location = new System.Drawing.Point(26, 561);
            this.lblResult.Name = "lblResult";
            this.lblResult.Size = new System.Drawing.Size(67, 13);
            this.lblResult.TabIndex = 14;
            this.lblResult.Text = "[Test Result]";
            // 
            // chartSelector
            // 
            this.chartSelector.BackColor = System.Drawing.Color.White;
            this.chartSelector.Location = new System.Drawing.Point(40, 12);
            this.chartSelector.Name = "chartSelector";
            this.chartSelector.SelectorType = CoinTNet.UI.Common.SelectorType.BackTest;
            this.chartSelector.Size = new System.Drawing.Size(578, 67);
            this.chartSelector.TabIndex = 0;
            this.chartSelector.UseLiveData = false;
            // 
            // btnIndicators
            // 
            this.btnIndicators.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnIndicators.Image = global::CoinTNet.Properties.Resources.chart_curve;
            this.btnIndicators.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnIndicators.Location = new System.Drawing.Point(810, 37);
            this.btnIndicators.Name = "btnIndicators";
            this.btnIndicators.Size = new System.Drawing.Size(85, 23);
            this.btnIndicators.TabIndex = 1;
            this.btnIndicators.Text = "Indicators";
            this.btnIndicators.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnIndicators.UseVisualStyleBackColor = true;
            this.btnIndicators.Click += new System.EventHandler(this.btnIndicators_Click);
            // 
            // chartCtrl
            // 
            this.chartCtrl.Location = new System.Drawing.Point(29, 69);
            this.chartCtrl.Name = "chartCtrl";
            this.chartCtrl.Size = new System.Drawing.Size(953, 305);
            this.chartCtrl.TabIndex = 11;
            // 
            // btnApply
            // 
            this.btnApply.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnApply.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnApply.Location = new System.Drawing.Point(12, 587);
            this.btnApply.Name = "btnApply";
            this.btnApply.Size = new System.Drawing.Size(75, 23);
            this.btnApply.TabIndex = 4;
            this.btnApply.Text = "Run strategy";
            this.btnApply.UseVisualStyleBackColor = true;
            this.btnApply.Click += new System.EventHandler(this.btnApply_Click);
            // 
            // BackTestingForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1112, 622);
            this.Controls.Add(this.chartSelector);
            this.Controls.Add(this.lblResult);
            this.Controls.Add(this.btnIndicators);
            this.Controls.Add(this.chartCtrl);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnApply);
            this.Name = "BackTestingForm";
            this.Text = "Backtesting settings";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.BackTestingForm_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.numMinAmountMoney)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numMinAmountBTC)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numFee)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numBalBTCTest)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numBalUSDTest)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numMaxNbBTCPerSellOrder)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numMaxAmountMoneyPerBuyOrder)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbbStrategy;
        private System.Windows.Forms.Panel pnlPlaceholder;
        private CoinTNet.UI.Controls.Common.MyButton btnApply;
        private System.Windows.Forms.Label lblMinAmountItem2ToKeep;
        private System.Windows.Forms.Label lblMinAmountItem1ToKeep;
        private System.Windows.Forms.NumericUpDown numMinAmountMoney;
        private System.Windows.Forms.NumericUpDown numMinAmountBTC;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.NumericUpDown numMaxNbBTCPerSellOrder;
        private System.Windows.Forms.NumericUpDown numMaxAmountMoneyPerBuyOrder;
        private System.Windows.Forms.Label lblMaxAmountItem2PerBuy;
        private System.Windows.Forms.Label lblMaxAmountItem1PerSell;
        private System.Windows.Forms.Label lblInitialItem2Balance;
        private System.Windows.Forms.NumericUpDown numBalBTCTest;
        private System.Windows.Forms.Label lblInitialItem1Balance;
        private System.Windows.Forms.NumericUpDown numBalUSDTest;
        private System.Windows.Forms.NumericUpDown numFee;
        private System.Windows.Forms.Label label8;
        private Controls.MyChartControl chartCtrl;
        private Controls.Common.MyButton btnIndicators;
        private System.Windows.Forms.Label lblResult;
        private Controls.DataSelector chartSelector;
    }
}