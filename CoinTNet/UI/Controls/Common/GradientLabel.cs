using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace CoinTNet.UI.Controls
{
    /// <summary>
    /// Normal label with a gradient for the background
    /// </summary>
    class GradientLabel : Label
    {
        /// <summary>
        /// Initialises a new instance of the GradientLabel class
        /// </summary>
        public GradientLabel()
        {
            Color1 = Color.DarkOrange;
            Color2 = Color.Orange;
            Angle = 90;
        }

        #region Public Properties

        /// <summary>
        /// Gets ot sets the first colour of the gradient
        /// </summary>
        public Color Color1 { get; set; }
        /// <summary>
        /// Gets ot sets the second colour of the gradient
        /// </summary>
        public Color Color2 { get; set; }
        /// <summary>
        /// Gets or sets the gradient's angle
        /// </summary>
        public int Angle { get; set; }

        #endregion

        /// <summary>
        /// Draws a gradient in the background
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPaintBackground(PaintEventArgs e)
        {
            base.OnPaintBackground(e);
            if (this.Width > 0 && this.Height > 0)
            {
                Graphics g = e.Graphics;
                using (Brush linearGradientBrush = new LinearGradientBrush(
                   new Rectangle(0, 0, this.Width, this.Height), Color1, Color2, Angle))
                {
                    g.FillRectangle(linearGradientBrush, new Rectangle(0, 0, this.Width, this.Height));
                }
            }
        }
    }
}
