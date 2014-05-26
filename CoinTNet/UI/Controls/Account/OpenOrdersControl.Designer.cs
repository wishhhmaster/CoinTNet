namespace CoinTNet.UI.Controls
{
    partial class OpenOrdersControl
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
            this.dgvOpenOrders = new System.Windows.Forms.DataGridView();
            this.colType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colAmount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colPrice = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTotalPrice = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colIfExecuted = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCancel = new System.Windows.Forms.DataGridViewLinkColumn();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lblTotalBuyOrders = new System.Windows.Forms.Label();
            this.lblTotalSellOrders = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvOpenOrders)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvOpenOrders
            // 
            this.dgvOpenOrders.AllowUserToAddRows = false;
            this.dgvOpenOrders.AllowUserToDeleteRows = false;
            this.dgvOpenOrders.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvOpenOrders.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvOpenOrders.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colType,
            this.colDate,
            this.colAmount,
            this.colPrice,
            this.colTotalPrice,
            this.colIfExecuted,
            this.colCancel});
            this.dgvOpenOrders.Location = new System.Drawing.Point(4, 11);
            this.dgvOpenOrders.Name = "dgvOpenOrders";
            this.dgvOpenOrders.ReadOnly = true;
            this.dgvOpenOrders.RowHeadersVisible = false;
            this.dgvOpenOrders.Size = new System.Drawing.Size(496, 181);
            this.dgvOpenOrders.TabIndex = 0;
            this.dgvOpenOrders.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvOpenOrders_CellClick);
            // 
            // colType
            // 
            this.colType.HeaderText = "Type";
            this.colType.Name = "colType";
            this.colType.ReadOnly = true;
            this.colType.Width = 50;
            // 
            // colDate
            // 
            this.colDate.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colDate.HeaderText = "Date";
            this.colDate.Name = "colDate";
            this.colDate.ReadOnly = true;
            // 
            // colAmount
            // 
            this.colAmount.HeaderText = "BTC Amount";
            this.colAmount.Name = "colAmount";
            this.colAmount.ReadOnly = true;
            this.colAmount.Width = 70;
            // 
            // colPrice
            // 
            this.colPrice.HeaderText = "BTC Price";
            this.colPrice.Name = "colPrice";
            this.colPrice.ReadOnly = true;
            this.colPrice.Width = 70;
            // 
            // colTotalPrice
            // 
            this.colTotalPrice.HeaderText = "Total Price";
            this.colTotalPrice.Name = "colTotalPrice";
            this.colTotalPrice.ReadOnly = true;
            this.colTotalPrice.Width = 70;
            // 
            // colIfExecuted
            // 
            this.colIfExecuted.HeaderText = "B/S Price If Executed";
            this.colIfExecuted.Name = "colIfExecuted";
            this.colIfExecuted.ReadOnly = true;
            this.colIfExecuted.Width = 70;
            // 
            // colCancel
            // 
            this.colCancel.HeaderText = "Cancel";
            this.colCancel.Name = "colCancel";
            this.colCancel.ReadOnly = true;
            this.colCancel.Width = 50;
            // 
            // btnRefresh
            // 
            this.btnRefresh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnRefresh.Image = global::CoinTNet.Properties.Resources.arrow_refresh;
            this.btnRefresh.Location = new System.Drawing.Point(509, 28);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(31, 23);
            this.btnRefresh.TabIndex = 1;
            this.btnRefresh.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 200);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(86, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Total Buy Orders";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(266, 200);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(85, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Total Sell Orders";
            // 
            // lblTotalBuyOrders
            // 
            this.lblTotalBuyOrders.AutoSize = true;
            this.lblTotalBuyOrders.Location = new System.Drawing.Point(111, 200);
            this.lblTotalBuyOrders.Name = "lblTotalBuyOrders";
            this.lblTotalBuyOrders.Size = new System.Drawing.Size(13, 13);
            this.lblTotalBuyOrders.TabIndex = 4;
            this.lblTotalBuyOrders.Text = "0";
            // 
            // lblTotalSellOrders
            // 
            this.lblTotalSellOrders.AutoSize = true;
            this.lblTotalSellOrders.Location = new System.Drawing.Point(357, 200);
            this.lblTotalSellOrders.Name = "lblTotalSellOrders";
            this.lblTotalSellOrders.Size = new System.Drawing.Size(13, 13);
            this.lblTotalSellOrders.TabIndex = 5;
            this.lblTotalSellOrders.Text = "0";
            // 
            // OpenOrdersControl
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.Controls.Add(this.lblTotalSellOrders);
            this.Controls.Add(this.lblTotalBuyOrders);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnRefresh);
            this.Controls.Add(this.dgvOpenOrders);
            this.Name = "OpenOrdersControl";
            this.Size = new System.Drawing.Size(545, 224);
            ((System.ComponentModel.ISupportInitialize)(this.dgvOpenOrders)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvOpenOrders;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.DataGridViewTextBoxColumn colType;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn colAmount;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPrice;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTotalPrice;
        private System.Windows.Forms.DataGridViewTextBoxColumn colIfExecuted;
        private System.Windows.Forms.DataGridViewLinkColumn colCancel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblTotalBuyOrders;
        private System.Windows.Forms.Label lblTotalSellOrders;
    }
}
