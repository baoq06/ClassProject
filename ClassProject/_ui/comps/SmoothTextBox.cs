using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace ClassProject._ui.comps
{
    /// <summary>
    /// TextBox hiện đại với:
    /// - Animated focus border (như Tailwind focus ring)
    /// - Smooth hover state transition
    /// - Shimmer color effect
    /// </summary>
    public class SmoothTextBox : TextBox
    {
        #region Fields

        private AnimationTimer? _animationTimer;
        private float _animationProgress = 0f;
        private bool _isFocused = false;
        private bool _isHovered = false;

        // Colors
        private Color _baseBorderColor = Color.FromArgb(220, 220, 220);
        private Color _focusBorderColor = Color.FromArgb(59, 130, 246); // Blue
        private Color _hoverBorderColor = Color.FromArgb(180, 180, 180);
        private Color _placeholderColor = Color.FromArgb(150, 150, 150);

        private float _borderRadius = 8f;
        private int _borderWidth = 1;
        private int _focusBorderWidth = 2;
        private int _animationDurationMs = 200;
        private string _placeholder = "";

        #endregion

        #region Properties

        [Category("Modern TextBox")]
        public Color BaseBorderColor
        {
            get => _baseBorderColor;
            set { _baseBorderColor = value; Invalidate(); }
        }

        [Category("Modern TextBox")]
        public Color FocusBorderColor
        {
            get => _focusBorderColor;
            set { _focusBorderColor = value; Invalidate(); }
        }

        [Category("Modern TextBox")]
        public Color HoverBorderColor
        {
            get => _hoverBorderColor;
            set { _hoverBorderColor = value; Invalidate(); }
        }

        [Category("Modern TextBox")]
        [DefaultValue(8f)]
        public float BorderRadius
        {
            get => _borderRadius;
            set { _borderRadius = Math.Max(0, value); Invalidate(); }
        }

        [Category("Modern TextBox")]
        [DefaultValue(1)]
        public int BorderWidth
        {
            get => _borderWidth;
            set { _borderWidth = Math.Max(1, value); Invalidate(); }
        }

        [Category("Modern TextBox")]
        [DefaultValue(2)]
        public int FocusBorderWidth
        {
            get => _focusBorderWidth;
            set { _focusBorderWidth = Math.Max(1, value); Invalidate(); }
        }

        [Category("Modern TextBox")]
        public string Placeholder
        {
            get => _placeholder;
            set { _placeholder = value; Invalidate(); }
        }

        #endregion

        public SmoothTextBox()
        {
            SetStyle(ControlStyles.OptimizedDoubleBuffer |
                     ControlStyles.AllPaintingInWmPaint |
                     ControlStyles.UserPaint, true);

            BorderStyle = BorderStyle.None;
            BackColor = Color.White;
            Font = new Font("Segoe UI", 10F);
            Padding = new Padding(12, 10, 12, 10);
            Size = new Size(250, 40);

            GotFocus += (s, e) => StartAnimation(true);
            LostFocus += (s, e) => StartAnimation(false);
            MouseEnter += (s, e) => { _isHovered = true; if (!_isFocused) Invalidate(); };
            MouseLeave += (s, e) => { _isHovered = false; if (!_isFocused) Invalidate(); };
        }

        private void StartAnimation(bool focused)
        {
            _animationTimer?.Dispose();
            _isFocused = focused;

            _animationTimer = new AnimationTimer(
                _animationDurationMs,
                ModernAnimator.EasingType.EaseOutCubic,
                t => {
                    _animationProgress = focused ? t : 1 - t;
                    Invalidate();
                }
            );
            _animationTimer.Start();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            var g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            var rect = new RectangleF(0, 0, Width - 1, Height - 1);

            // Determine border color based on state
            Color currentBorderColor = _baseBorderColor;
            float currentBorderWidth = _borderWidth;

            if (_isFocused || _animationProgress > 0)
            {
                currentBorderColor = ModernAnimator.InterpolateColor(
                    _baseBorderColor, _focusBorderColor, _animationProgress);
                currentBorderWidth = _borderWidth + (_focusBorderWidth - _borderWidth) * _animationProgress;

                // Add shimmer
                currentBorderColor = ModernAnimator.ShimmerColor(currentBorderColor, _animationProgress * 0.5f);
            }
            else if (_isHovered)
            {
                currentBorderColor = _hoverBorderColor;
            }

            // Draw rounded background
            using (var path = SmoothPanel.CreateRoundedRectPath(rect, BorderRadius))
            using (var bgBrush = new SolidBrush(BackColor))
            {
                g.FillPath(bgBrush, path);
            }

            // Draw border
            using (var pen = new Pen(currentBorderColor, currentBorderWidth))
            {
                var borderRect = new RectangleF(
                    currentBorderWidth / 2,
                    currentBorderWidth / 2,
                    Width - currentBorderWidth,
                    Height - currentBorderWidth);

                using var borderPath = SmoothPanel.CreateRoundedRectPath(borderRect, BorderRadius);
                g.DrawPath(pen, borderPath);
            }

            // Draw placeholder text
            if (string.IsNullOrEmpty(Text) && !string.IsNullOrEmpty(_placeholder))
            {
                using var placeholderBrush = new SolidBrush(_placeholderColor);
                var textRect = new RectangleF(Padding.Left, Padding.Top,
                    Width - Padding.Left - Padding.Right,
                    Height - Padding.Top - Padding.Bottom);
                using var font = new Font(Font.FontFamily, Font.Size, FontStyle.Regular);
                var format = new StringFormat
                {
                    LineAlignment = StringAlignment.Center,
                    Alignment = StringAlignment.Near
                };
                g.DrawString(_placeholder, font, placeholderBrush, textRect, format);
            }
        }

        protected override void OnResize(EventArgs eventargs)
        {
            base.OnResize(eventargs);
            using var path = SmoothPanel.CreateRoundedRectPath(
                new RectangleF(0, 0, Width, Height), BorderRadius);
            Region = new Region(path);
        }

        protected override void Dispose(bool disposing)
        {
            _animationTimer?.Dispose();
            base.Dispose(disposing);
        }
    }
}