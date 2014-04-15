namespace CoinTNet.UI.Controls
{
    partial class DataSelector
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
            this.btnRefreshCandles = new System.Windows.Forms.Button();
            this.cbbPeriodInMin = new System.Windows.Forms.ComboBox();
            this.label28 = new System.Windows.Forms.Label();
            this.label25 = new System.Windows.Forms.Label();
            this.label24 = new System.Windows.Forms.Label();
            this.dtpTo = new System.Windows.Forms.DateTimePicker();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.cbbExchange = new System.Windows.Forms.ComboBox();
            this.dtpFrom = new System.Windows.Forms.DateTimePicker();
            this.cbbPairs = new System.Windows.Forms.ComboBox();
            this.btnLoadFromCSV = new CoinTNet.UI.Controls.Common.MyButton();
            this.rdbUseAPI = new System.Windows.Forms.RadioButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.rdbLoadFile = new System.Windows.Forms.RadioButton();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.chkNow = new System.Windows.Forms.CheckBox();
            this.panel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnRefreshCandles
            // 
            this.btnRefreshCandles.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnRefreshCandles.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnRefreshCandles.Image = global::CoinTNet.Properties.Resources.arrow_refresh;
            this.btnRefreshCandles.Location = new System.Drawing.Point(447, 37);
            this.btnRefreshCandles.Name = "btnRefreshCandles";
            this.btnRefreshCandles.Size = new System.Drawing.Size(38, 22);
            this.btnRefreshCandles.TabIndex = 3;
            this.btnRefreshCandles.UseVisualStyleBackColor = true;
            this.btnRefreshCandles.Click += new System.EventHandler(this.btnRefreshCandles_Click);
            // 
            // cbbPeriodInMin
            // 
            this.cbbPeriodInMin.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.cbbPeriodInMin.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbbPeriodInMin.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.cbbPeriodInMin.FormattingEnabled = true;
            this.cbbPeriodInMin.Location = new System.Drawing.Point(499, 5);
            this.cbbPeriodInMin.Name = "cbbPeriodInMin";
            this.cbbPeriodInMin.Size = new System.Drawing.Size(71, 21);
            this.cbbPeriodInMin.TabIndex = 4;
            // 
            // label28
            // 
            this.label28.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label28.AutoSize = true;
            this.label28.Location = new System.Drawing.Point(447, 0);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(38, 32);
            this.label28.TabIndex = 25;
            this.label28.Text = "Period";
            this.label28.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label25
            // 
            this.label25.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label25.AutoSize = true;
            this.label25.Location = new System.Drawing.Point(113, 32);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(40, 33);
            this.label25.TabIndex = 20;
            this.label25.Text = "Pair";
            this.label25.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label24
            // 
            this.label24.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label24.AutoSize = true;
            this.label24.Location = new System.Drawing.Point(113, 0);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(40, 32);
            this.label24.TabIndex = 1;
            this.label24.Text = "Market";
            this.label24.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // dtpTo
            // 
            this.dtpTo.Enabled = false;
            this.dtpTo.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpTo.Location = new System.Drawing.Point(1, 4);
            this.dtpTo.Name = "dtpTo";
            this.dtpTo.Size = new System.Drawing.Size(98, 20);
            this.dtpTo.TabIndex = 0;
            // 
            // label8
            // 
            this.label8.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label8.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(249, 32);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(37, 33);
            this.label8.TabIndex = 19;
            this.label8.Text = "To";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label7
            // 
            this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label7.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(249, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(37, 32);
            this.label7.TabIndex = 17;
            this.label7.Text = "From";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cbbExchange
            // 
            this.cbbExchange.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cbbExchange.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbbExchange.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.cbbExchange.FormattingEnabled = true;
            this.cbbExchange.Location = new System.Drawing.Point(159, 3);
            this.cbbExchange.Name = "cbbExchange";
            this.cbbExchange.Size = new System.Drawing.Size(84, 21);
            this.cbbExchange.TabIndex = 0;
            this.cbbExchange.SelectedIndexChanged += new System.EventHandler(this.cbbExchange_SelectedIndexChanged);
            // 
            // dtpFrom
            // 
            this.dtpFrom.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFrom.Location = new System.Drawing.Point(292, 3);
            this.dtpFrom.Name = "dtpFrom";
            this.dtpFrom.Size = new System.Drawing.Size(96, 20);
            this.dtpFrom.TabIndex = 2;
            // 
            // cbbPairs
            // 
            this.cbbPairs.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cbbPairs.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbbPairs.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.cbbPairs.FormattingEnabled = true;
            this.cbbPairs.Location = new System.Drawing.Point(159, 35);
            this.cbbPairs.Name = "cbbPairs";
            this.cbbPairs.Size = new System.Drawing.Size(84, 21);
            this.cbbPairs.TabIndex = 1;
            this.cbbPairs.SelectedIndexChanged += new System.EventHandler(this.cbbPairs_SelectedIndexChanged);
            // 
            // btnLoadFromCSV
            // 
            this.btnLoadFromCSV.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLoadFromCSV.Location = new System.Drawing.Point(71, 34);
            this.btnLoadFromCSV.Name = "btnLoadFromCSV";
            this.btnLoadFromCSV.Size = new System.Drawing.Size(28, 22);
            this.btnLoadFromCSV.TabIndex = 27;
            this.btnLoadFromCSV.Text = "...";
            this.btnLoadFromCSV.UseVisualStyleBackColor = true;
            this.btnLoadFromCSV.Click += new System.EventHandler(this.btnLoadFromCSV_Click);
            // 
            // rdbUseAPI
            // 
            this.rdbUseAPI.AutoSize = true;
            this.rdbUseAPI.Checked = true;
            this.rdbUseAPI.Location = new System.Drawing.Point(4, 7);
            this.rdbUseAPI.Name = "rdbUseAPI";
            this.rdbUseAPI.Size = new System.Drawing.Size(64, 17);
            this.rdbUseAPI.TabIndex = 0;
            this.rdbUseAPI.TabStop = true;
            this.rdbUseAPI.Text = "Use API";
            this.rdbUseAPI.UseVisualStyleBackColor = true;
            this.rdbUseAPI.CheckedChanged += new System.EventHandler(this.rdbUseAPI_CheckedChanged);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.rdbLoadFile);
            this.panel1.Controls.Add(this.btnLoadFromCSV);
            this.panel1.Controls.Add(this.rdbUseAPI);
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.tableLayoutPanel2.SetRowSpan(this.panel1, 2);
            this.panel1.Size = new System.Drawing.Size(104, 59);
            this.panel1.TabIndex = 0;
            // 
            // rdbLoadFile
            // 
            this.rdbLoadFile.AutoSize = true;
            this.rdbLoadFile.Location = new System.Drawing.Point(3, 39);
            this.rdbLoadFile.Name = "rdbLoadFile";
            this.rdbLoadFile.Size = new System.Drawing.Size(65, 17);
            this.rdbLoadFile.TabIndex = 1;
            this.rdbLoadFile.TabStop = true;
            this.rdbLoadFile.Text = "Load file";
            this.rdbLoadFile.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 7;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 110F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 90F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 43F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel2.Controls.Add(this.panel2, 4, 1);
            this.tableLayoutPanel2.Controls.Add(this.panel1, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.btnRefreshCandles, 5, 1);
            this.tableLayoutPanel2.Controls.Add(this.label28, 5, 0);
            this.tableLayoutPanel2.Controls.Add(this.dtpFrom, 4, 0);
            this.tableLayoutPanel2.Controls.Add(this.label25, 1, 1);
            this.tableLayoutPanel2.Controls.Add(this.cbbExchange, 2, 0);
            this.tableLayoutPanel2.Controls.Add(this.cbbPairs, 2, 1);
            this.tableLayoutPanel2.Controls.Add(this.label24, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.label7, 3, 0);
            this.tableLayoutPanel2.Controls.Add(this.label8, 3, 1);
            this.tableLayoutPanel2.Controls.Add(this.cbbPeriodInMin, 6, 0);
            this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 2;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.76923F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 49.23077F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(582, 65);
            this.tableLayoutPanel2.TabIndex = 0;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.chkNow);
            this.panel2.Controls.Add(this.dtpTo);
            this.panel2.Location = new System.Drawing.Point(289, 32);
            this.panel2.Margin = new System.Windows.Forms.Padding(0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(155, 30);
            this.panel2.TabIndex = 27;
            // 
            // chkNow
            // 
            this.chkNow.AutoSize = true;
            this.chkNow.Checked = true;
            this.chkNow.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkNow.Location = new System.Drawing.Point(105, 7);
            this.chkNow.Name = "chkNow";
            this.chkNow.Size = new System.Drawing.Size(48, 17);
            this.chkNow.TabIndex = 1;
            this.chkNow.Text = "Now";
            this.chkNow.UseVisualStyleBackColor = true;
            this.chkNow.CheckedChanged += new System.EventHandler(this.chkNow_CheckedChanged);
            // 
            // DataSelector
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.tableLayoutPanel2);
            this.Name = "DataSelector";
            this.Size = new System.Drawing.Size(585, 67);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnRefreshCandles;
        private System.Windows.Forms.ComboBox cbbPeriodInMin;
        private System.Windows.Forms.Label label28;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.DateTimePicker dtpTo;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox cbbExchange;
        private System.Windows.Forms.DateTimePicker dtpFrom;
        private System.Windows.Forms.ComboBox cbbPairs;
        private Common.MyButton btnLoadFromCSV;
        private System.Windows.Forms.RadioButton rdbUseAPI;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.RadioButton rdbLoadFile;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.CheckBox chkNow;
    }
}
