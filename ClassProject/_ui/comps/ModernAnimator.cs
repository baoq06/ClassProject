using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace ClassProject._ui.comps
{
    /// <summary>
    /// Animation utilities với Bezier curves - Thay thế CSS transitions
    /// </summary>
    public static class ModernAnimator
    {
        // Timing functions như CSS
        public enum EasingType
        {
            Linear,
            EaseInQuad,
            EaseOutQuad,
            EaseInOutQuad,
            EaseInCubic,
            EaseOutCubic,
            EaseInOutCubic,
            EaseOutElastic,
            EaseOutBack,
            EaseOutBounce
        }

        /// <summary>
        /// Tính Bezier point tại thời điểm t
        /// </summary>
        public static PointF BezierPoint(PointF p0, PointF p1, PointF p2, PointF p3, float t)
        {
            float u = 1 - t;
            float tt = t * t;
            float uu = u * u;
            float uuu = uu * u;
            float ttt = tt * t;

            return new PointF(
                uuu * p0.X + 3 * uu * t * p1.X + 3 * u * tt * p2.X + ttt * p3.X,
                uuu * p0.Y + 3 * uu * t * p1.Y + 3 * u * tt * p2.Y + ttt * p3.Y
            );
        }

        /// <summary>
        /// Tính giá trị easing theo thời gian
        /// </summary>
        public static float Ease(float t, EasingType type)
        {
            return type switch
            {
                EasingType.Linear => t,
                EasingType.EaseInQuad => t * t,
                EasingType.EaseOutQuad => t * (2 - t),
                EasingType.EaseInOutQuad => t < 0.5f ? 2 * t * t : -1 + (4 - 2 * t) * t,
                EasingType.EaseInCubic => t * t * t,
                EasingType.EaseOutCubic => (--t) * t * t + 1,
                EasingType.EaseInOutCubic => t < 0.5f ? 4 * t * t * t : (t - 1) * (2 * t - 2) * (2 * t - 2) + 1,
                EasingType.EaseOutElastic => t == 0 ? 0 : t == 1 ? 1 :
                    MathF.Pow(2, -10 * t) * MathF.Sin((t * 10 - 0.75f) * (2 * MathF.PI / 3)) + 1,
                EasingType.EaseOutBack => 1 + 2.70158f * MathF.Pow(t - 1, 3) + 1.70158f * MathF.Pow(t - 1, 2),
                EasingType.EaseOutBounce => BounceOut(t),
                _ => t
            };
        }

        private static float BounceOut(float t)
        {
            const float n1 = 7.5625f;
            const float d1 = 2.75f;
            if (t < 1 / d1) return n1 * t * t;
            if (t < 2 / d1) return n1 * (t -= 1.5f / d1) * t + 0.75f;
            if (t < 2.5f / d1) return n1 * (t -= 2.25f / d1) * t + 0.9375f;
            return n1 * (t -= 2.625f / d1) * t + 0.984375f;
        }

        /// <summary>
        /// Interpolate màu như CSS gradient transition
        /// </summary>
        public static Color InterpolateColor(Color from, Color to, float t)
        {
            t = Math.Clamp(t, 0, 1);
            return Color.FromArgb(
                (int)(from.A + (to.A - from.A) * t),
                (int)(from.R + (to.R - from.R) * t),
                (int)(from.G + (to.G - from.G) * t),
                (int)(from.B + (to.B - from.B) * t)
            );
        }

        /// <summary>
        /// Trộn màu với shimmer/ánh kim effect
        /// </summary>
        public static Color ShimmerColor(Color baseColor, float progress)
        {
            // Thêm ánh kim vàng/trắng
            int r = Math.Min(255, (int)(baseColor.R + progress * 60));
            int g = Math.Min(255, (int)(baseColor.G + progress * 50));
            int b = Math.Min(255, (int)(baseColor.B + progress * 40));
            return Color.FromArgb(baseColor.A, r, g, b);
        }
    }

    /// <summary>
    /// Animation timer đơn giản, không cần external library
    /// </summary>
    public class AnimationTimer : IDisposable
    {
        private readonly System.Windows.Forms.Timer _timer;
        private readonly int _durationMs;
        private readonly Action<float> _onUpdate;
        private readonly Action? _onComplete;
        private DateTime _startTime;
        private bool _isRunning;
        private readonly ModernAnimator.EasingType _easing;

        public AnimationTimer(int durationMs, ModernAnimator.EasingType easing, Action<float> onUpdate, Action? onComplete = null)
        {
            _durationMs = durationMs;
            _easing = easing;
            _onUpdate = onUpdate;
            _onComplete = onComplete;
            _timer = new System.Windows.Forms.Timer { Interval = 16 }; // ~60fps
            _timer.Tick += Timer_Tick;
        }

        private void Timer_Tick(object? sender, EventArgs e)
        {
            var elapsed = (DateTime.Now - _startTime).TotalMilliseconds;
            float t = (float)(elapsed / _durationMs);

            if (t >= 1)
            {
                t = 1;
                _timer.Stop();
                _isRunning = false;
                _onComplete?.Invoke();
            }
            else
            {
                t = ModernAnimator.Ease(t, _easing);
            }

            _onUpdate(t);
        }

        public void Start()
        {
            _startTime = DateTime.Now;
            _isRunning = true;
            _timer.Start();
        }

        public void Stop()
        {
            _timer.Stop();
            _isRunning = false;
        }

        public bool IsRunning => _isRunning;

        public void Dispose()
        {
            _timer.Stop();
            _timer.Dispose();
        }
    }
}
