namespace EaslyDraw
{
    using EaslyController.Controller;
    using EaslyController.Layout;
    using System.Windows.Media;

    /// <summary>
    /// An implementation of IxxxDrawContext for WPF.
    /// </summary>
    public partial class DrawContext : MeasureContext, ILayoutDrawContext
    {
        /// <summary>
        /// Updates the drawing context.
        /// </summary>
        /// <param name="wpfDrawingContext">The new instance of <see cref="DrawingContext"/>.</param>
        public virtual void SetWpfDrawingContext(DrawingContext wpfDrawingContext)
        {
            WpfDrawingContext = wpfDrawingContext;
        }

        /// <summary>
        /// Recalculate internal constants.
        /// To call after a property was changed.
        /// </summary>
        public override void Update()
        {
            base.Update();

            FormattedText ft;

            ft = CreateFormattedText(" ", EmSize, GetBrush(BrushSettings.Default));

            LeftBracketGeometry = ScaleGlyphGeometryHeight("[", true, 0.3, 0.3);
            RightBracketGeometry = ScaleGlyphGeometryHeight("]", true, 0.3, 0.3);
            LeftCurlyBracketGeometry = ScaleGlyphGeometryHeight("{", true, 0.25, 0.3);
            RightCurlyBracketGeometry = ScaleGlyphGeometryHeight("}", true, 0.25, 0.3);
            LeftParenthesisGeometry = ScaleGlyphGeometryHeight("(", true, 0, 0);
            RightParenthesisGeometry = ScaleGlyphGeometryHeight(")", true, 0, 0);
            HorizontalLineGeometry = ScaleGlyphGeometryWidth("-", true, 0, 0);
            CommentPadding = new Padding(new Measure() { Draw = WhitespaceWidth / 2 }, new Measure() { Draw = LineHeight.Draw / 4 }, new Measure() { Draw = WhitespaceWidth / 2 }, new Measure() { Draw = LineHeight.Draw / 4 });

            if (CommentIcon != null)
            {
                double PagePaddingX = CommentIcon.Width / 2;
                double PagePaddingY = CommentIcon.Height / 2;
                PagePadding = new Padding(new Measure() { Draw = PagePaddingX }, new Measure() { Draw = PagePaddingY }, new Measure() { Draw = InsertionCaretWidth }, Measure.Zero);
            }
        }
    }
}
