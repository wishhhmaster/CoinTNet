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
            this.btnRefresh = new System.Windows.Forms.Button();
            this.colType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colAmount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colPrice = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTotalPrice = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colIfExecuted = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCancel = new System.Windows.Forms.DataGridViewLinkColumn();
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
            this.dgvOpenOrders.Size = new System.Drawing.Size(496, 186);
            this.dgvOpenOrders.TabIndex = 0;
            this.dgvOpenOrders.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvOpenOrders_CellClick);
            // 
            // btnRefresh
            // 
            this.btnRefresh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnRefresh.Image = global::CoinTNet.Properties.Resources.arrow_refresh;
            this.btnRefresh.Location = new System.Drawing.Point(509, 7);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(31, 23);
            this.btnRefresh.TabIndex = 1;
            this.btnRefresh.UseVisualStyleBackColor = true;
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
            // OpenOrdersControl
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.Controls.Add(this.btnRefresh);
            this.Controls.Add(this.dgvOpenOrders);
            this.Name = "OpenOrdersControl";
            this.Size = new System.Drawing.Size(545, 203);
            ((System.ComponentModel.ISupportInitialize)(this.dgvOpenOrders)).EndInit();
            this.ResumeLayout(false);

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
    }
}
