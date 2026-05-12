using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace ClassProject._ui.comps
{
    /// <summary>
    /// Panel với GDI+ Smooth Rendering, Drop Shadow mềm, Rounded Corners
    /// Thay thế Tailwind CSS với custom WinForms controls
    /// </summary>
    public class SmoothPanel : Panel
    {
        #region Properties

        private float _borderRadius = 12f;
        [Category("Modern UI")]
        [DefaultValue(12f)]
        public float BorderRadius
        {
            get => _borderRadius;
            set
            {
                _borderRadius = Math.Max(0, value);
                Invalidate();
            }
        }

        private Color _shadowColor = Color.FromArgb(60, 0, 0, 0);
        [Category("Modern UI")]
        public Color ShadowColor
        {
            get => _shadowColor;
            set
            {
                _shadowColor = value;
                Invalidate();
            }
        }

        private int _shadowBlur = 20;
        [Category("Modern UI")]
        [DefaultValue(20)]
        public int ShadowBlur
        {
            get => _shadowBlur;
            set
            {
                _shadowBlur = Math.Max(0, value);
                Invalidate();
            }
        }

        private int _shadowOffset = 8;
        [Category("Modern UI")]
        [DefaultValue(8)]
        public int ShadowOffset
        {
            get => _shadowOffset;
            set
            {
                _shadowOffset = Math.Max(0, value);
                Invalidate();
            }
        }

        private bool _useSmoothRendering = true;
        [Category("Modern UI")]
        [DefaultValue(true)]
        public bool UseSmoothRendering
        {
            get => _useSmoothRendering;
            set
            {
                _useSmoothRendering = value;
                Invalidate();
            }
        }

        private Color _gradientStart = Color.White;
        [Category("Modern UI")]
        public Color GradientStart
        {
            get => _gradientStart;
            set
            {
                _gradientStart = value;
                Invalidate();
            }
        }

        private Color _gradientEnd = Color.WhiteSmoke;
        [Category("Modern UI")]
        public Color GradientEnd
        {
            get => _gradientEnd;
            set
            {
                _gradientEnd = value;
                Invalidate();
            }
        }

        private bool _useGradient = false;
        [Category("Modern UI")]
        [DefaultValue(false)]
        public bool UseGradient
        {
            get => _useGradient;
            set
            {
                _useGradient = value;
                Invalidate();
            }
        }

        private bool _showBorder = false;
        [Category("Modern UI")]
        [DefaultValue(false)]
        public bool ShowBorder
        {
            get => _showBorder;
            set
            {
                _showBorder = value;
                Invalidate();
            }
        }

        private Color _borderColor = Color.FromArgb(230, 230, 230);
        [Category("Modern UI")]
        public Color BorderColor
        {
            get => _borderColor;
            set
            {
                _borderColor = value;
                Invalidate();
            }
        }

        private float _borderWidth = 1f;
        [Category("Modern UI")]
        [DefaultValue(1f)]
        public float BorderWidth
        {
            get => _borderWidth;
            set
            {
                _borderWidth = Math.Max(0.5f, value);
                Invalidate();
            }
        }

        #endregion

        public SmoothPanel()
        {
            // Disable double buffering để tự vẽ
            SetStyle(ControlStyles.OptimizedDoubleBuffer |
                     ControlStyles.AllPaintingInWmPaint |
                     ControlStyles.UserPaint, true);

            BackColor = Color.White;
            Size = new Size(200, 100);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            if (!UseSmoothRendering)
            {
                base.OnPaint(e);
                return;
            }

            var g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
            g.PixelOffsetMode = PixelOffsetMode.HighQuality;

            var rect = new RectangleF(0, 0, Width - 1, Height - 1);

            // Vẽ shadow (bên dưới)
            DrawSoftShadow(g, rect);

            // Vẽ nền với gradient hoặc solid
            using (var path = CreateRoundedRectPath(rect, BorderRadius))
            {
                if (UseGradient)
                {
                    using var brush = new LinearGradientBrush(
                        ClientRectangle, GradientStart, GradientEnd, 90f);
                    g.FillPath(brush, path);
                }
                else
                {
                    using var brush = new SolidBrush(BackColor);
                    g.FillPath(brush, path);
                }

                // Vẽ border nếu cần
                if (ShowBorder)
                {
                    using var pen = new Pen(BorderColor, BorderWidth);
                    g.DrawPath(pen, path);
                }
            }
        }

        private void DrawSoftShadow(Graphics g, RectangleF rect)
        {
            // Tạo shadow mềm bằng cách vẽ nhiều layers
            int shadowLayers = 8;
            float offsetStep = (float)_shadowOffset / shadowLayers;
            float blurStep = (float)_shadowBlur / shadowLayers;
            float alphaStep = (float)_shadowColor.A / shadowLayers;

            for (int i = shadowLayers; i > 0; i--)
            {
                float offset = offsetStep * i;
                float blur = blurStep * (shadowLayers - i);
                float alpha = alphaStep * (shadowLayers - i);

                var shadowRect = new RectangleF(
                    rect.X + offset,
                    rect.Y + offset,
                    rect.Width,
                    rect.Height);

                using var shadowPath = CreateRoundedRectPath(shadowRect, BorderRadius + blur);
                using var shadowBrush = new SolidBrush(
                    Color.FromArgb((int)alpha, _shadowColor));

                g.FillPath(shadowBrush, shadowPath);
            }
        }

        /// <summary>
        /// Tạo Rounded Rectangle Path với Region support
        /// </summary>
        public static GraphicsPath CreateRoundedRectPath(RectangleF rect, float radius)
        {
            var path = new GraphicsPath();
            float diameter = radius * 2;

            // Four corners
            path.AddArc(rect.X, rect.Y, diameter, diameter, 180, 90); // Top-left
            path.AddArc(rect.Right - diameter, rect.Y, diameter, diameter, 270, 90); // Top-right
            path.AddArc(rect.Right - diameter, rect.Bottom - diameter, diameter, diameter, 0, 90); // Bottom-right
            path.AddArc(rect.X, rect.Bottom - diameter, diameter, diameter, 90, 90); // Bottom-left
            path.CloseFigure();

            return path;
        }

        protected override void OnResize(EventArgs eventargs)
        {
            base.OnResize(eventargs);
            // Set Region để clip content trong rounded corners
            using var path = CreateRoundedRectPath(new RectangleF(0, 0, Width, Height), BorderRadius);
            Region = new Region(path);
        }
    }
}