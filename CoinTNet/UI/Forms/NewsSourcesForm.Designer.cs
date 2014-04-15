namespace CoinTNet.UI.Forms
{
    partial class NewsSourcesForm
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
            this.dgvNews = new System.Windows.Forms.DataGridView();
            this.cbbSourceType = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnClose = new CoinTNet.UI.Controls.Common.MyButton();
            this.colRemove = new System.Windows.Forms.DataGridViewImageColumn();
            this.colName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colURL = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colExpiry = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.colMaxNbItems = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgvNews)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvNews
            // 
            this.dgvNews.AllowUserToAddRows = false;
            this.dgvNews.AllowUserToDeleteRows = false;
            this.dgvNews.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvNews.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colRemove,
            this.colName,
            this.colURL,
            this.colExpiry,
            this.colMaxNbItems});
            this.dgvNews.Location = new System.Drawing.Point(28, 62);
            this.dgvNews.Name = "dgvNews";
            this.dgvNews.RowHeadersVisible = false;
            this.dgvNews.Size = new System.Drawing.Size(544, 150);
            this.dgvNews.TabIndex = 0;
            this.dgvNews.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvNews_CellClick);
            // 
            // cbbSourceType
            // 
            this.cbbSourceType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbbSourceType.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbbSourceType.FormattingEnabled = true;
            this.cbbSourceType.Location = new System.Drawing.Point(84, 23);
            this.cbbSourceType.Name = "cbbSourceType";
            this.cbbSourceType.Size = new System.Drawing.Size(121, 21);
            this.cbbSourceType.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(25, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(31, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Type";
            // 
            // btnAdd
            // 
            this.btnAdd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAdd.Image = global::CoinTNet.Properties.Resources.add;
            this.btnAdd.Location = new System.Drawing.Point(578, 62);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(30, 23);
            this.btnAdd.TabIndex = 3;
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnClose
            // 
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Location = new System.Drawing.Point(28, 242);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 4;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            // 
            // colRemove
            // 
            this.colRemove.Frozen = true;
            this.colRemove.HeaderText = "";
            this.colRemove.Image = global::CoinTNet.Properties.Resources.cross;
            this.colRemove.Name = "colRemove";
            this.colRemove.ReadOnly = true;
            this.colRemove.Width = 20;
            // 
            // colName
            // 
            this.colName.DataPropertyName = "Name";
            this.colName.Frozen = true;
            this.colName.HeaderText = "Name";
            this.colName.Name = "colName";
            this.colName.Width = 150;
            // 
            // colURL
            // 
            this.colURL.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colURL.DataPropertyName = "Url";
            this.colURL.FillWeight = 50F;
            this.colURL.HeaderText = "URL";
            this.colURL.Name = "colURL";
            // 
            // colExpiry
            // 
            this.colExpiry.DataPropertyName = "ExpiryInHours";
            this.colExpiry.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.colExpiry.HeaderText = "Expiry";
            this.colExpiry.Name = "colExpiry";
            // 
            // colMaxNbItems
            // 
            this.colMaxNbItems.DataPropertyName = "NbItems";
            this.colMaxNbItems.HeaderText = "Max Nb Items";
            this.colMaxNbItems.Name = "colMaxNbItems";
            // 
            // NewsSourcesForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(616, 277);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cbbSourceType);
            this.Controls.Add(this.dgvNews);
            this.Name = "NewsSourcesForm";
            this.Text = "News Sources";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.NewsSourcesForm_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.dgvNews)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvNews;
        private System.Windows.Forms.ComboBox cbbSourceType;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnAdd;
        private Controls.Common.MyButton btnClose;
        private System.Windows.Forms.DataGridViewImageColumn colRemove;
        private System.Windows.Forms.DataGridViewTextBoxColumn colName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colURL;
        private System.Windows.Forms.DataGridViewComboBoxColumn colExpiry;
        private System.Windows.Forms.DataGridViewTextBoxColumn colMaxNbItems;
    }
}