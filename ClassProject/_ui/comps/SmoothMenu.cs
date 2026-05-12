using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace ClassProject._ui.comps
{
    /// <summary>
    /// Menu trượt với Bezier curve animation - như CSS slide transitions
    /// </summary>
    public class SmoothMenu : UserControl
    {
        #region Fields

        private readonly List<MenuItem> _items = new List<MenuItem>();
        private AnimationTimer? _openTimer;
        private AnimationTimer? _closeTimer;
        private float _openProgress = 0f;
        private bool _isOpen = false;
        private bool _isAnimating = false;

        // Settings
        private int _menuWidth = 220;
        private int _itemHeight = 45;
        private int _itemSpacing = 5;
        private int _animationDurationMs = 350;
        private int _staggerDelayMs = 30;

        private Color _backColor = Color.White;
        private Color _itemHoverColor = Color.FromArgb(245, 245, 245);
        private Color _itemTextColor = Color.FromArgb(55, 65, 81);
        private float _borderRadius = 12f;
        private bool _useShadow = true;

        private int? _hoveredIndex = null;

        #endregion

        #region Properties

        [Category("Smooth Menu")]
        public int MenuWidth
        {
            get => _menuWidth;
            set { _menuWidth = Math.Max(100, value); UpdateLayout(); }
        }

        [Category("Smooth Menu")]
        public int ItemHeight
        {
            get => _itemHeight;
            set { _itemHeight = Math.Max(30, value); UpdateLayout(); }
        }

        [Category("Smooth Menu")]
        public int AnimationDurationMs
        {
            get => _animationDurationMs;
            set => _animationDurationMs = Math.Max(100, value);
        }

        [Category("Smooth Menu")]
        public Color ItemHoverColor
        {
            get => _itemHoverColor;
            set => _itemHoverColor = value;
        }

        [Category("Smooth Menu")]
        public Color ItemTextColor
        {
            get => _itemTextColor;
            set => _itemTextColor = value;
        }

        [Category("Smooth Menu")]
        public float BorderRadius
        {
            get => _borderRadius;
            set { _borderRadius = Math.Max(0, value); Invalidate(); }
        }

        [Category("Smooth Menu")]
        public bool UseShadow
        {
            get => _useShadow;
            set { _useShadow = value; Invalidate(); }
        }

        #endregion

        #region Menu Item Class

        public class MenuItem
        {
            public string Text { get; set; } = "";
            public string Icon { get; set; } = "";
            public Action? OnClick { get; set; }
            public bool Enabled { get; set; } = true;

            public MenuItem(string text, Action? onClick = null)
            {
                Text = text;
                OnClick = onClick;
            }
        }

        #endregion

        public SmoothMenu()
        {
            SetStyle(ControlStyles.OptimizedDoubleBuffer |
                     ControlStyles.AllPaintingInWmPaint |
                     ControlStyles.UserPaint, true);

            Width = _menuWidth;
            BackColor = _backColor;
            Visible = false;

            UpdateLayout();
        }

        public void AddItem(string text, Action? onClick = null)
        {
            _items.Add(new MenuItem(text, onClick));
            UpdateLayout();
        }

        public void ClearItems()
        {
            _items.Clear();
            UpdateLayout();
        }

        private void UpdateLayout()
        {
            int totalHeight = _items.Count * _itemHeight + (_items.Count - 1) * _itemSpacing;
            Height = totalHeight;

            if (!_isOpen && !_isAnimating)
            {
                Visible = _items.Count > 0;
            }
        }

        public void Open(Point position)
        {
            Location = position;
            Visible = true;
            _isOpen = true;
            _isAnimating = true;

            _openTimer?.Dispose();
            _openTimer = new AnimationTimer(
                _animationDurationMs,
                ModernAnimator.EasingType.EaseOutBack,
                t => {
                    _openProgress = t;
                    Invalidate();
                },
                () => {
                    _isAnimating = false;
                }
            );
            _openTimer.Start();
        }

        public void Close()
        {
            if (!_isOpen) return;

            _isAnimating = true;
            var closeTimer = new AnimationTimer(
                _animationDurationMs / 2,
                ModernAnimator.EasingType.EaseInQuad,
                t => {
                    _openProgress = 1 - t;
                    Invalidate();
                },
                () => {
                    _isOpen = false;
                    _isAnimating = false;
                    Visible = false;
                }
            );
            closeTimer.Start();
            _closeTimer?.Dispose();
            _closeTimer = closeTimer;
            closeTimer.Start();
        }

        public void Toggle(Point position)
        {
            if (_isOpen)
                Close();
            else
                Open(position);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            var g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;

            var rect = new RectangleF(0, 0, Width - 1, Height - 1);

            // Draw shadow
            if (_useShadow && _openProgress > 0)
            {
                DrawMenuShadow(g, rect);
            }

            // Draw menu background
            using (var path = SmoothPanel.CreateRoundedRectPath(rect, _borderRadius))
            using (var brush = new SolidBrush(_backColor))
            {
                g.FillPath(brush, path);
            }

            // Draw menu items với staggered animation
            for (int i = 0; i < _items.Count; i++)
            {
                float itemProgress = CalculateItemProgress(i);

                if (itemProgress > 0)
                {
                    DrawMenuItem(g, i, itemProgress);
                }
            }
        }

        private float CalculateItemProgress(int index)
        {
            if (!_isAnimating || _openProgress <= 0) return 1f;

            float staggerOffset = index * ((float)_staggerDelayMs / _animationDurationMs);
            float itemStart = Math.Min(1, _openProgress + staggerOffset);
            float itemEnd = Math.Max(0, 1 - staggerOffset);

            return Math.Clamp((itemStart - itemEnd) / (1 - itemEnd), 0, 1);
        }

        private void DrawMenuShadow(Graphics g, RectangleF rect)
        {
            int layers = 8;
            float shadowIntensity = 0.2f * _openProgress;

            for (int i = layers; i > 0; i--)
            {
                float blur = i * 2f;
                float alpha = shadowIntensity * (layers - i) / layers;

                var shadowRect = new RectangleF(
                    rect.X + 4,
                    rect.Y + 4,
                    rect.Width,
                    rect.Height);

                using var shadowPath = SmoothPanel.CreateRoundedRectPath(shadowRect, _borderRadius + blur);
                using var shadowBrush = new SolidBrush(
                    Color.FromArgb((int)(alpha * 255), 0, 0, 0));
                g.FillPath(shadowBrush, shadowPath);
            }
        }

        private void DrawMenuItem(Graphics g, int index, float progress)
        {
            int yOffset = index * (_itemHeight + _itemSpacing);
            var itemRect = new RectangleF(4, yOffset + 2, Width - 8, _itemHeight - 4);

            // Apply Bezier slide animation
            if (progress < 1)
            {
                float startX = Width;
                float endX = 0;
                float currentX = startX + (endX - startX) * ModernAnimator.Ease(progress, ModernAnimator.EasingType.EaseOutCubic);

                // Apply transform
                g.TranslateTransform(currentX, 0);
            }

            var item = _items[index];
            bool isHovered = _hoveredIndex == index && item.Enabled;

            // Draw item background
            if (isHovered)
            {
                using var hoverPath = SmoothPanel.CreateRoundedRectPath(itemRect, 6f);
                using var hoverBrush = new SolidBrush(_itemHoverColor);
                g.FillPath(hoverBrush, hoverPath);
            }

            // Draw item text
            using var font = new Font("Segoe UI", 10F, FontStyle.Regular);
            using var textBrush = new SolidBrush(
                item.Enabled ? _itemTextColor : Color.FromArgb(150, 150, 150));

            var textFormat = new StringFormat
            {
                LineAlignment = StringAlignment.Center,
                Alignment = StringAlignment.Center
            };

            g.DrawString(item.Text, font, textBrush, itemRect, textFormat);

            if (progress < 1)
            {
                g.ResetTransform();
            }
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);

            int newHoverIndex = GetItemIndexAtPoint(e.Location);

            if (newHoverIndex != _hoveredIndex)
            {
                _hoveredIndex = newHoverIndex;
                Invalidate();
            }
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            _hoveredIndex = null;
            Invalidate();
        }

        protected override void OnMouseClick(MouseEventArgs e)
        {
            base.OnMouseClick(e);

            int index = GetItemIndexAtPoint(e.Location);
            if (index >= 0 && index < _items.Count && _items[index].Enabled)
            {
                _items[index].OnClick?.Invoke();
                Close();
            }
        }

        private int GetItemIndexAtPoint(Point point)
        {
            for (int i = 0; i < _items.Count; i++)
            {
                int yOffset = i * (_itemHeight + _itemSpacing);
                var itemRect = new Rectangle(4, yOffset + 2, Width - 8, _itemHeight - 4);

                if (itemRect.Contains(point))
                    return i;
            }
            return -1;
        }

        protected override void Dispose(bool disposing)
        {
            _openTimer?.Dispose();
            _closeTimer?.Dispose();
            base.Dispose(disposing);
        }
    }
}
