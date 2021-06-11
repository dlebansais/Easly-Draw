namespace EaslyDraw
{
    using EaslyController.Layout;
    using System.Windows.Media;
    using System.Windows.Media.Animation;

    /// <summary>
    /// An implementation of IxxxDrawContext for WPF.
    /// </summary>
    public partial class DrawContext : MeasureContext, ILayoutDrawContext
    {
        /// <summary></summary>
        protected virtual ScalableGeometry ScaleGlyphGeometryWidth(string text, bool isWidthScaled, double leftPercent, double rightPercent)
        {
            FormattedText GlyphText = CreateFormattedText(text, EmSize, GetBrush(BrushSettings.Symbol));
            GlyphText.Trimming = System.Windows.TextTrimming.None;

            System.Windows.Rect Bounds = new System.Windows.Rect(new System.Windows.Point(0, 0), new System.Windows.Size(GlyphText.Width, GlyphText.Width));
            Geometry GlyphGeometry = GlyphText.BuildGeometry(Bounds.Location);

            return new ScalableGeometry(GlyphGeometry, Bounds, isWidthScaled, leftPercent, rightPercent, false, 0, 0);
        }

        /// <summary></summary>
        protected virtual ScalableGeometry ScaleGlyphGeometryHeight(string text, bool isHeightScaled, double topPercent, double bottomPercent)
        {
            FormattedText GlyphText = CreateFormattedText(text, EmSize, GetBrush(BrushSettings.Symbol));
            GlyphText.Trimming = System.Windows.TextTrimming.None;

            System.Windows.Rect Bounds = new System.Windows.Rect(new System.Windows.Point(0, 0), new System.Windows.Size(GlyphText.Width, GlyphText.Height));
            Geometry GlyphGeometry = GlyphText.BuildGeometry(Bounds.Location);

            return new ScalableGeometry(GlyphGeometry, Bounds, false, 0, 0, isHeightScaled, topPercent, bottomPercent);
        }

        /// <summary></summary>
        protected ScalableGeometry LeftBracketGeometry { get; private set; }
        /// <summary></summary>
        protected ScalableGeometry RightBracketGeometry { get; private set; }
        /// <summary></summary>
        protected ScalableGeometry LeftCurlyBracketGeometry { get; private set; }
        /// <summary></summary>
        protected ScalableGeometry RightCurlyBracketGeometry { get; private set; }
        /// <summary></summary>
        protected ScalableGeometry LeftParenthesisGeometry { get; private set; }
        /// <summary></summary>
        protected ScalableGeometry RightParenthesisGeometry { get; private set; }
        /// <summary></summary>
        protected ScalableGeometry HorizontalLineGeometry { get; private set; }
        /// <summary></summary>
        protected DoubleAnimation FlashAnimation { get; private set; }
        /// <summary></summary>
        protected AnimationClock FlashClock { get; private set; }
        /// <summary></summary>
        protected bool IsLastFocusedFullCell { get; private set; }
    }
}
