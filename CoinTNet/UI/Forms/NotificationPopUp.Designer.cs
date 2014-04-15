namespace CoinTNet.UI.Forms
{
    partial class NotificationPopUp<T>
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
            this.components = new System.ComponentModel.Container();
            this.pcbIcon = new System.Windows.Forms.PictureBox();
            this.pnlContainer = new System.Windows.Forms.Panel();
            this.lblApplicationName = new System.Windows.Forms.Label();
            this.gradientLabel1 = new CoinTNet.UI.Controls.GradientLabel();
            this.lblText = new System.Windows.Forms.Label();
            this.tmShow = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.pcbIcon)).BeginInit();
            this.pnlContainer.SuspendLayout();
            this.SuspendLayout();
            // 
            // pcbIcon
            // 
            this.pcbIcon.Image = global::CoinTNet.Properties.Resources.information;
            this.pcbIcon.Location = new System.Drawing.Point(11, 36);
            this.pcbIcon.Name = "pcbIcon";
            this.pcbIcon.Size = new System.Drawing.Size(16, 16);
            this.pcbIcon.TabIndex = 3;
            this.pcbIcon.TabStop = false;
            this.pcbIcon.Click += new System.EventHandler(this.OnUserClick);
            // 
            // pnlContainer
            // 
            this.pnlContainer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlContainer.AutoSize = true;
            this.pnlContainer.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.pnlContainer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlContainer.Controls.Add(this.lblApplicationName);
            this.pnlContainer.Controls.Add(this.gradientLabel1);
            this.pnlContainer.Controls.Add(this.lblText);
            this.pnlContainer.Controls.Add(this.pcbIcon);
            this.pnlContainer.Location = new System.Drawing.Point(0, 0);
            this.pnlContainer.MinimumSize = new System.Drawing.Size(350, 95);
            this.pnlContainer.Name = "pnlContainer";
            this.pnlContainer.Padding = new System.Windows.Forms.Padding(0, 0, 0, 10);
            this.pnlContainer.Size = new System.Drawing.Size(350, 95);
            this.pnlContainer.TabIndex = 4;
            this.pnlContainer.Click += new System.EventHandler(this.OnUserClick);
            // 
            // lblApplicationName
            // 
            this.lblApplicationName.AutoSize = true;
            this.lblApplicationName.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblApplicationName.Location = new System.Drawing.Point(39, 21);
            this.lblApplicationName.Name = "lblApplicationName";
            this.lblApplicationName.Size = new System.Drawing.Size(41, 13);
            this.lblApplicationName.TabIndex = 5;
            this.lblApplicationName.Text = "label1";
            // 
            // gradientLabel1
            // 
            this.gradientLabel1.Angle = 90;
            this.gradientLabel1.Color1 = System.Drawing.Color.Black;
            this.gradientLabel1.Color2 = System.Drawing.Color.Silver;
            this.gradientLabel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.gradientLabel1.Location = new System.Drawing.Point(0, 0);
            this.gradientLabel1.Name = "gradientLabel1";
            this.gradientLabel1.Size = new System.Drawing.Size(348, 10);
            this.gradientLabel1.TabIndex = 4;
            this.gradientLabel1.Click += new System.EventHandler(this.OnUserClick);
            // 
            // lblText
            // 
            this.lblText.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblText.BackColor = System.Drawing.Color.LemonChiffon;
            this.lblText.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.lblText.Location = new System.Drawing.Point(39, 39);
            this.lblText.Name = "lblText";
            this.lblText.Size = new System.Drawing.Size(298, 13);
            this.lblText.TabIndex = 2;
            this.lblText.Text = "label1";
            this.lblText.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblText.Click += new System.EventHandler(this.OnUserClick);
            // 
            // tmShow
            // 
            this.tmShow.Interval = 3000;
            this.tmShow.Tick += new System.EventHandler(this.tmShow_Tick);
            // 
            // NotificationPopUp
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.Color.LemonChiffon;
            this.ClientSize = new System.Drawing.Size(350, 95);
            this.Controls.Add(this.pnlContainer);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MinimumSize = new System.Drawing.Size(350, 95);
            this.Name = "NotificationPopUp";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "NotificationPopUp";
            this.TopMost = true;
            ((System.ComponentModel.ISupportInitialize)(this.pcbIcon)).EndInit();
            this.pnlContainer.ResumeLayout(false);
            this.pnlContainer.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pcbIcon;
        private System.Windows.Forms.Label lblText;
        private System.Windows.Forms.Panel pnlContainer;
        private System.Windows.Forms.Timer tmShow;
        private CoinTNet.UI.Controls.GradientLabel gradientLabel1;
        private System.Windows.Forms.Label lblApplicationName;

    }
}