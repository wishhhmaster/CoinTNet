namespace CoinTNet.UI.Controls
{
    partial class TickerControl
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
            this.tlpTicker = new System.Windows.Forms.TableLayoutPanel();
            this.lblLast = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lblLow = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.lblLastUpdate = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lblHigh = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tmrTicker = new System.Windows.Forms.Timer(this.components);
            this.tlpTicker.SuspendLayout();
            this.SuspendLayout();
            // 
            // tlpTicker
            // 
            this.tlpTicker.ColumnCount = 5;
            this.tlpTicker.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlpTicker.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 79F));
            this.tlpTicker.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlpTicker.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 75F));
            this.tlpTicker.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 146F));
            this.tlpTicker.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlpTicker.Controls.Add(this.lblLast, 1, 0);
            this.tlpTicker.Controls.Add(this.label1, 0, 0);
            this.tlpTicker.Controls.Add(this.lblLow, 3, 0);
            this.tlpTicker.Controls.Add(this.label4, 2, 0);
            this.tlpTicker.Controls.Add(this.lblLastUpdate, 4, 1);
            this.tlpTicker.Controls.Add(this.label3, 4, 0);
            this.tlpTicker.Controls.Add(this.lblHigh, 3, 1);
            this.tlpTicker.Controls.Add(this.label2, 2, 1);
            this.tlpTicker.Location = new System.Drawing.Point(0, 0);
            this.tlpTicker.Name = "tlpTicker";
            this.tlpTicker.RowCount = 2;
            this.tlpTicker.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpTicker.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpTicker.Size = new System.Drawing.Size(344, 48);
            this.tlpTicker.TabIndex = 24;
            // 
            // lblLast
            // 
            this.lblLast.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblLast.AutoSize = true;
            this.lblLast.Location = new System.Drawing.Point(36, 0);
            this.lblLast.Name = "lblLast";
            this.tlpTicker.SetRowSpan(this.lblLast, 2);
            this.lblLast.Size = new System.Drawing.Size(73, 48);
            this.lblLast.TabIndex = 1;
            this.lblLast.Text = "[Last]";
            this.lblLast.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.tlpTicker.SetRowSpan(this.label1, 2);
            this.label1.Size = new System.Drawing.Size(27, 48);
            this.label1.TabIndex = 0;
            this.label1.Text = "Last";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblLow
            // 
            this.lblLow.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblLow.AutoSize = true;
            this.lblLow.Location = new System.Drawing.Point(150, 0);
            this.lblLow.Name = "lblLow";
            this.lblLow.Size = new System.Drawing.Size(69, 24);
            this.lblLow.TabIndex = 3;
            this.lblLow.Text = "[Low]";
            this.lblLow.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(115, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(29, 24);
            this.label4.TabIndex = 14;
            this.label4.Text = "Low";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblLastUpdate
            // 
            this.lblLastUpdate.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblLastUpdate.Location = new System.Drawing.Point(225, 24);
            this.lblLastUpdate.Name = "lblLastUpdate";
            this.lblLastUpdate.Size = new System.Drawing.Size(140, 24);
            this.lblLastUpdate.TabIndex = 12;
            this.lblLastUpdate.Text = "[LastUpdate]";
            this.lblLastUpdate.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(225, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(140, 24);
            this.label3.TabIndex = 13;
            this.label3.Text = "Last update";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblHigh
            // 
            this.lblHigh.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.lblHigh.AutoSize = true;
            this.lblHigh.Location = new System.Drawing.Point(150, 24);
            this.lblHigh.Name = "lblHigh";
            this.lblHigh.Size = new System.Drawing.Size(35, 24);
            this.lblHigh.TabIndex = 2;
            this.lblHigh.Text = "[High]";
            this.lblHigh.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(115, 24);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 24);
            this.label2.TabIndex = 24;
            this.label2.Text = "High";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tmrTicker
            // 
            this.tmrTicker.Interval = 1000;
            this.tmrTicker.Tick += new System.EventHandler(this.tmrTicker_Tick);
            // 
            // TickerControl
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.Controls.Add(this.tlpTicker);
            this.Name = "TickerControl";
            this.Size = new System.Drawing.Size(347, 49);
            this.tlpTicker.ResumeLayout(false);
            this.tlpTicker.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tlpTicker;
        private System.Windows.Forms.Label lblLast;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblLow;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lblLastUpdate;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblHigh;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Timer tmrTicker;
    }
}
