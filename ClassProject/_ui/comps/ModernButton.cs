using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace ClassProject._ui.comps
{
    /// <summary>
    /// Button hiện đại với Tailwind-like animations:
    /// - Hover color transition với shimmer effect
    /// - Smooth scale animation
    /// - Bezier curve menu transitions
    /// </summary>
    public class ModernButton : Button
    {
        #region Fields

        private AnimationTimer? _hoverTimer;
        private AnimationTimer? _clickTimer;
        private float _animationProgress = 0f;
        private bool _isHovered = false;
        private bool _isPressed = false;

        // Base colors
        private Color _baseColor = Color.FromArgb(59, 130, 246); // Blue-500
        private Color _hoverColor = Color.FromArgb(96, 165, 250); // Blue-400
        private Color _pressedColor = Color.FromArgb(30, 64, 175); // Blue-800
        private Color _textColor = Color.White;

        // Gradient colors
        private Color _gradientStart = Color.FromArgb(59, 130, 246);
        private Color _gradientEnd = Color.FromArgb(147, 51, 234); // Purple-600

        private bool _useGradient = false;
        private float _borderRadius = 8f;
        private bool _useShadow = true;
        private int _shadowBlur = 15;
        private int _shadowOffset = 5;

        // Hover animation settings
        private int _hoverDurationMs = 200;
        private int _clickDurationMs = 100;

        #endregion

        #region Properties

        [Category("Modern Button")]
        public Color BaseColor
        {
            get => _baseColor;
            set { _baseColor = value; Invalidate(); }
        }

        [Category("Modern Button")]
        public Color HoverColor
        {
            get => _hoverColor;
            set { _hoverColor = value; Invalidate(); }
        }

        [Category("Modern Button")]
        public Color PressedColor
        {
            get => _pressedColor;
            set { _pressedColor = value; Invalidate(); }
        }

        [Category("Modern Button")]
        public Color TextColor
        {
            get => _textColor;
            set { _textColor = value; ForeColor = value; }
        }

        [Category("Modern Button")]
        public bool UseGradient
        {
            get => _useGradient;
            set { _useGradient = value; Invalidate(); }
        }

        [Category("Modern Button")]
        public Color GradientStart
        {
            get => _gradientStart;
            set { _gradientStart = value; Invalidate(); }
        }

        [Category("Modern Button")]
        public Color GradientEnd
        {
            get => _gradientEnd;
            set { _gradientEnd = value; Invalidate(); }
        }

        [Category("Modern Button")]
        [DefaultValue(8f)]
        public float BorderRadius
        {
            get => _borderRadius;
            set { _borderRadius = Math.Max(0, value); Invalidate(); }
        }

        [Category("Modern Button")]
        [DefaultValue(true)]
        public bool UseShadow
        {
            get => _useShadow;
            set { _useShadow = value; Invalidate(); }
        }

        [Category("Modern Button")]
        [DefaultValue(15)]
        public int ShadowBlur
        {
            get => _shadowBlur;
            set { _shadowBlur = Math.Max(0, value); Invalidate(); }
        }

        [Category("Modern Button")]
        [DefaultValue(5)]
        public int ShadowOffset
        {
            get => _shadowOffset;
            set { _shadowOffset = Math.Max(0, value); Invalidate(); }
        }

        [Category("Modern Animation")]
        [DefaultValue(200)]
        public int HoverDurationMs
        {
            get => _hoverDurationMs;
            set => _hoverDurationMs = Math.Max(50, value);
        }

        [Category("Modern Animation")]
        [DefaultValue(100)]
        public int ClickDurationMs
        {
            get => _clickDurationMs;
            set => _clickDurationMs = Math.Max(50, value);
        }

        #endregion

        public ModernButton()
        {
            SetStyle(ControlStyles.OptimizedDoubleBuffer |
                     ControlStyles.AllPaintingInWmPaint |
                     ControlStyles.UserPaint, true);

            FlatStyle = FlatStyle.Flat;
            FlatAppearance.BorderSize = 0;
            Cursor = Cursors.Hand;
            Font = new Font("Segoe UI", 10F, FontStyle.Regular);
            ForeColor = _textColor;
            TextAlign = ContentAlignment.MiddleCenter;
            Size = new Size(120, 40);

            // Smooth hover events
            MouseEnter += (s, e) => StartHoverAnimation(true);
            MouseLeave += (s, e) => StartHoverAnimation(false);
            MouseDown += (s, e) => StartClickAnimation(true);
            MouseUp += (s, e) => StartClickAnimation(false);
        }

        private void StartHoverAnimation(bool entering)
        {
            _hoverTimer?.Dispose();
            _isHovered = entering;

            _hoverTimer = new AnimationTimer(
                _hoverDurationMs,
                entering ? ModernAnimator.EasingType.EaseOutCubic : ModernAnimator.EasingType.EaseInQuad,
                t => {
                    _animationProgress = entering ? t : 1 - t;
                    Invalidate();
                }
            );
            _hoverTimer.Start();
        }

        private void StartClickAnimation(bool pressed)
        {
            _clickTimer?.Dispose();
            _isPressed = pressed;

            _clickTimer = new AnimationTimer(
                _clickDurationMs,
                ModernAnimator.EasingType.EaseOutQuad,
                t => {
                    if (pressed) _animationProgress = 1 - t * 0.3f;
                    else _animationProgress = 1;
                    Invalidate();
                }
            );
            _clickTimer.Start();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            var g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
            g.PixelOffsetMode = PixelOffsetMode.HighQuality;

            var rect = new RectangleF(1, 1, Width - 2, Height - 2);

            // Calculate current color based on animation
            Color currentColor = _isHovered
                ? ModernAnimator.InterpolateColor(_baseColor, _hoverColor, _animationProgress)
                : _baseColor;

            // Apply shimmer effect on hover
            if (_isHovered && _animationProgress > 0)
            {
                currentColor = ModernAnimator.ShimmerColor(currentColor, _animationProgress);
            }

            // Draw shadow
            if (_useShadow && !_isPressed)
            {
                DrawShadow(g, rect, currentColor);
            }

            // Draw button background
            using (var path = SmoothPanel.CreateRoundedRectPath(rect, BorderRadius))
            {
                if (UseGradient)
                {
                    var gradRect = new Rectangle(0, 0, Width, Height);
                    using var brush = new LinearGradientBrush(
                        gradRect, _gradientStart, _gradientEnd, 45f);
                    g.FillPath(brush, path);
                }
                else
                {
                    using var brush = new SolidBrush(currentColor);
                    g.FillPath(brush, path);
                }
            }

            // Draw text
            using var sf = new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center };
            using var textBrush = new SolidBrush(ForeColor);
            g.DrawString(Text, Font, textBrush, ClientRectangle, sf);
        }

        private void DrawShadow(Graphics g, RectangleF rect, Color baseColor)
        {
            int layers = 6;
            float offsetStep = (float)_shadowOffset / layers;
            float blurStep = (float)_shadowBlur / layers;

            for (int i = layers; i > 0; i--)
            {
                float offset = offsetStep * i;
                float blur = blurStep * (layers - i) * 0.5f;
                float alpha = 0.15f * (layers - i) / layers;

                var shadowRect = new RectangleF(
                    rect.X + offset,
                    rect.Y + offset,
                    rect.Width,
                    rect.Height);

                using var shadowPath = SmoothPanel.CreateRoundedRectPath(shadowRect, BorderRadius + blur);
                using var shadowBrush = new SolidBrush(
                    Color.FromArgb((int)(alpha * 255), 0, 0, 0));
                g.FillPath(shadowBrush, shadowPath);
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
            _hoverTimer?.Dispose();
            _clickTimer?.Dispose();
            base.Dispose(disposing);
        }
    }
}