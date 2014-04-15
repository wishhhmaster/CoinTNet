namespace CoinTNet.UI.Controls
{
    partial class OrderControl
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
            this.tlpOrder = new System.Windows.Forms.TableLayoutPanel();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.lblSellFee = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.btnSell = new System.Windows.Forms.Button();
            this.lblSellTotal = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.lblCurrency2 = new System.Windows.Forms.Label();
            this.txtSellPrice = new System.Windows.Forms.TextBox();
            this.txtSellAmount = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.lblItemBalance = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.lblSell = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.lblBuyFee = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.btnBuy = new System.Windows.Forms.Button();
            this.lblBuyTotal = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.lblCurrency1 = new System.Windows.Forms.Label();
            this.txtBuyPrice = new System.Windows.Forms.TextBox();
            this.txtBuyAmount = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lblBuy = new System.Windows.Forms.Label();
            this.lblCurrencyBalance = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tlpOrder.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tlpOrder
            // 
            this.tlpOrder.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tlpOrder.ColumnCount = 2;
            this.tlpOrder.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpOrder.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpOrder.Controls.Add(this.groupBox3, 1, 0);
            this.tlpOrder.Controls.Add(this.groupBox2, 0, 0);
            this.tlpOrder.Location = new System.Drawing.Point(0, 0);
            this.tlpOrder.Name = "tlpOrder";
            this.tlpOrder.RowCount = 1;
            this.tlpOrder.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpOrder.Size = new System.Drawing.Size(311, 250);
            this.tlpOrder.TabIndex = 10;
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.Controls.Add(this.lblSellFee);
            this.groupBox3.Controls.Add(this.label8);
            this.groupBox3.Controls.Add(this.btnRefresh);
            this.groupBox3.Controls.Add(this.btnSell);
            this.groupBox3.Controls.Add(this.lblSellTotal);
            this.groupBox3.Controls.Add(this.label12);
            this.groupBox3.Controls.Add(this.lblCurrency2);
            this.groupBox3.Controls.Add(this.txtSellPrice);
            this.groupBox3.Controls.Add(this.txtSellAmount);
            this.groupBox3.Controls.Add(this.label6);
            this.groupBox3.Controls.Add(this.lblItemBalance);
            this.groupBox3.Controls.Add(this.label5);
            this.groupBox3.Controls.Add(this.label4);
            this.groupBox3.Controls.Add(this.lblSell);
            this.groupBox3.Controls.Add(this.label2);
            this.groupBox3.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox3.Location = new System.Drawing.Point(158, 3);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(150, 244);
            this.groupBox3.TabIndex = 8;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Sell";
            // 
            // lblSellFee
            // 
            this.lblSellFee.AutoSize = true;
            this.lblSellFee.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSellFee.Location = new System.Drawing.Point(46, 190);
            this.lblSellFee.Name = "lblSellFee";
            this.lblSellFee.Size = new System.Drawing.Size(38, 13);
            this.lblSellFee.TabIndex = 22;
            this.lblSellFee.Text = "0 USD";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(5, 190);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(28, 13);
            this.label8.TabIndex = 21;
            this.label8.Text = "Fee:";
            // 
            // btnRefresh
            // 
            this.btnRefresh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRefresh.Image = global::CoinTNet.Properties.Resources.arrow_refresh;
            this.btnRefresh.Location = new System.Drawing.Point(112, 17);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(32, 23);
            this.btnRefresh.TabIndex = 14;
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // btnSell
            // 
            this.btnSell.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSell.BackColor = System.Drawing.Color.LightGreen;
            this.btnSell.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnSell.Location = new System.Drawing.Point(90, 156);
            this.btnSell.Name = "btnSell";
            this.btnSell.Size = new System.Drawing.Size(44, 23);
            this.btnSell.TabIndex = 20;
            this.btnSell.Text = "Sell";
            this.btnSell.UseVisualStyleBackColor = false;
            this.btnSell.Click += new System.EventHandler(this.btnSell_Click);
            // 
            // lblSellTotal
            // 
            this.lblSellTotal.AutoSize = true;
            this.lblSellTotal.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSellTotal.Location = new System.Drawing.Point(46, 217);
            this.lblSellTotal.Name = "lblSellTotal";
            this.lblSellTotal.Size = new System.Drawing.Size(38, 13);
            this.lblSellTotal.TabIndex = 19;
            this.lblSellTotal.Text = "0 USD";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(5, 217);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(35, 13);
            this.label12.TabIndex = 17;
            this.label12.Text = "Total:";
            // 
            // lblCurrency2
            // 
            this.lblCurrency2.AutoSize = true;
            this.lblCurrency2.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCurrency2.Location = new System.Drawing.Point(64, 135);
            this.lblCurrency2.Name = "lblCurrency2";
            this.lblCurrency2.Size = new System.Drawing.Size(29, 13);
            this.lblCurrency2.TabIndex = 10;
            this.lblCurrency2.Text = "USD";
            // 
            // txtSellPrice
            // 
            this.txtSellPrice.Location = new System.Drawing.Point(9, 156);
            this.txtSellPrice.Name = "txtSellPrice";
            this.txtSellPrice.Size = new System.Drawing.Size(75, 22);
            this.txtSellPrice.TabIndex = 9;
            this.txtSellPrice.TextChanged += new System.EventHandler(this.UpdateSellTotal);
            // 
            // txtSellAmount
            // 
            this.txtSellAmount.Location = new System.Drawing.Point(9, 109);
            this.txtSellAmount.Name = "txtSellAmount";
            this.txtSellAmount.Size = new System.Drawing.Size(75, 22);
            this.txtSellAmount.TabIndex = 8;
            this.txtSellAmount.TextChanged += new System.EventHandler(this.UpdateSellTotal);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(5, 135);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(53, 13);
            this.label6.TabIndex = 7;
            this.label6.Text = "Sell price";
            // 
            // lblItemBalance
            // 
            this.lblItemBalance.AutoSize = true;
            this.lblItemBalance.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblItemBalance.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblItemBalance.ForeColor = System.Drawing.Color.RoyalBlue;
            this.lblItemBalance.Location = new System.Drawing.Point(7, 39);
            this.lblItemBalance.Name = "lblItemBalance";
            this.lblItemBalance.Size = new System.Drawing.Size(35, 13);
            this.lblItemBalance.TabIndex = 4;
            this.lblItemBalance.Text = "? BTC";
            this.lblItemBalance.Click += new System.EventHandler(this.OnFillItemAmount);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 93);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(73, 13);
            this.label5.TabIndex = 6;
            this.label5.Text = "Amount BTC:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(7, 22);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(68, 13);
            this.label4.TabIndex = 4;
            this.label4.Text = "Av. Balance:";
            // 
            // lblSell
            // 
            this.lblSell.AutoSize = true;
            this.lblSell.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblSell.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSell.Location = new System.Drawing.Point(9, 73);
            this.lblSell.Name = "lblSell";
            this.lblSell.Size = new System.Drawing.Size(65, 13);
            this.lblSell.TabIndex = 3;
            this.lblSell.Text = "000.00 USD";
            this.lblSell.TextAlign = System.Drawing.ContentAlignment.TopRight;
            this.lblSell.Click += new System.EventHandler(this.OnFillPriceCurrency);
            // 
            // label2
            // 
            this.label2.AutoEllipsis = true;
            this.label2.Location = new System.Drawing.Point(7, 56);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(107, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Highest Bid Price";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.lblBuyFee);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.btnBuy);
            this.groupBox2.Controls.Add(this.lblBuyTotal);
            this.groupBox2.Controls.Add(this.label11);
            this.groupBox2.Controls.Add(this.lblCurrency1);
            this.groupBox2.Controls.Add(this.txtBuyPrice);
            this.groupBox2.Controls.Add(this.txtBuyAmount);
            this.groupBox2.Controls.Add(this.label9);
            this.groupBox2.Controls.Add(this.label10);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.lblBuy);
            this.groupBox2.Controls.Add(this.lblCurrencyBalance);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(3, 3);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(149, 244);
            this.groupBox2.TabIndex = 7;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Buy";
            // 
            // lblBuyFee
            // 
            this.lblBuyFee.AutoSize = true;
            this.lblBuyFee.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBuyFee.Location = new System.Drawing.Point(49, 190);
            this.lblBuyFee.Name = "lblBuyFee";
            this.lblBuyFee.Size = new System.Drawing.Size(38, 13);
            this.lblBuyFee.TabIndex = 23;
            this.lblBuyFee.Text = "0 USD";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(8, 190);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(28, 13);
            this.label7.TabIndex = 20;
            this.label7.Text = "Fee:";
            // 
            // btnBuy
            // 
            this.btnBuy.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBuy.BackColor = System.Drawing.Color.LightGreen;
            this.btnBuy.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnBuy.Location = new System.Drawing.Point(99, 156);
            this.btnBuy.Name = "btnBuy";
            this.btnBuy.Size = new System.Drawing.Size(44, 23);
            this.btnBuy.TabIndex = 19;
            this.btnBuy.Text = "Buy";
            this.btnBuy.UseVisualStyleBackColor = false;
            this.btnBuy.Click += new System.EventHandler(this.btnBuy_Click);
            // 
            // lblBuyTotal
            // 
            this.lblBuyTotal.AutoSize = true;
            this.lblBuyTotal.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBuyTotal.Location = new System.Drawing.Point(49, 217);
            this.lblBuyTotal.Name = "lblBuyTotal";
            this.lblBuyTotal.Size = new System.Drawing.Size(38, 13);
            this.lblBuyTotal.TabIndex = 18;
            this.lblBuyTotal.Text = "0 USD";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(8, 217);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(35, 13);
            this.label11.TabIndex = 16;
            this.label11.Text = "Total:";
            // 
            // lblCurrency1
            // 
            this.lblCurrency1.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lblCurrency1.AutoSize = true;
            this.lblCurrency1.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCurrency1.Location = new System.Drawing.Point(75, 135);
            this.lblCurrency1.Name = "lblCurrency1";
            this.lblCurrency1.Size = new System.Drawing.Size(29, 13);
            this.lblCurrency1.TabIndex = 15;
            this.lblCurrency1.Text = "USD";
            // 
            // txtBuyPrice
            // 
            this.txtBuyPrice.Location = new System.Drawing.Point(11, 157);
            this.txtBuyPrice.Name = "txtBuyPrice";
            this.txtBuyPrice.Size = new System.Drawing.Size(75, 22);
            this.txtBuyPrice.TabIndex = 14;
            this.txtBuyPrice.TextChanged += new System.EventHandler(this.UpdateBuyTotal);
            // 
            // txtBuyAmount
            // 
            this.txtBuyAmount.Location = new System.Drawing.Point(11, 109);
            this.txtBuyAmount.Name = "txtBuyAmount";
            this.txtBuyAmount.Size = new System.Drawing.Size(75, 22);
            this.txtBuyAmount.TabIndex = 13;
            this.txtBuyAmount.TextChanged += new System.EventHandler(this.UpdateBuyTotal);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(9, 135);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(54, 13);
            this.label9.TabIndex = 12;
            this.label9.Text = "Buy price";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(9, 93);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(48, 13);
            this.label10.TabIndex = 11;
            this.label10.Text = "Amount";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(9, 22);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(68, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "Av. Balance:";
            // 
            // lblBuy
            // 
            this.lblBuy.AutoSize = true;
            this.lblBuy.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblBuy.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBuy.Location = new System.Drawing.Point(9, 73);
            this.lblBuy.Name = "lblBuy";
            this.lblBuy.Size = new System.Drawing.Size(65, 13);
            this.lblBuy.TabIndex = 2;
            this.lblBuy.Text = "000.00 USD";
            this.lblBuy.TextAlign = System.Drawing.ContentAlignment.TopRight;
            this.lblBuy.Click += new System.EventHandler(this.OnFillPriceItem);
            // 
            // lblCurrencyBalance
            // 
            this.lblCurrencyBalance.AutoSize = true;
            this.lblCurrencyBalance.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblCurrencyBalance.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCurrencyBalance.ForeColor = System.Drawing.Color.RoyalBlue;
            this.lblCurrencyBalance.Location = new System.Drawing.Point(9, 39);
            this.lblCurrencyBalance.Name = "lblCurrencyBalance";
            this.lblCurrencyBalance.Size = new System.Drawing.Size(37, 13);
            this.lblCurrencyBalance.TabIndex = 5;
            this.lblCurrencyBalance.Text = "? USD";
            this.lblCurrencyBalance.Click += new System.EventHandler(this.OnFillCurrencyAmount);
            // 
            // label1
            // 
            this.label1.AutoEllipsis = true;
            this.label1.Location = new System.Drawing.Point(8, 52);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(107, 21);
            this.label1.TabIndex = 0;
            this.label1.Text = "Lowest ask Price";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // OrderControl
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.Controls.Add(this.tlpOrder);
            this.Name = "OrderControl";
            this.Size = new System.Drawing.Size(315, 254);
            this.tlpOrder.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tlpOrder;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label lblSellTotal;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label lblCurrency2;
        private System.Windows.Forms.TextBox txtSellPrice;
        private System.Windows.Forms.TextBox txtSellAmount;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lblCurrencyBalance;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lblSell;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label lblBuyTotal;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label lblCurrency1;
        private System.Windows.Forms.TextBox txtBuyPrice;
        private System.Windows.Forms.TextBox txtBuyAmount;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label lblItemBalance;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblBuy;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Button btnBuy;
        private System.Windows.Forms.Button btnSell;
        private System.Windows.Forms.Label lblSellFee;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label lblBuyFee;
        private System.Windows.Forms.Label label7;
    }
}
