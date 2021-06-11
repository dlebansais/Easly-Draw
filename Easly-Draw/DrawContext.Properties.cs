namespace EaslyDraw
{
    using EaslyController.Controller;
    using EaslyController.Layout;
    using System.Windows.Media;
    using System.Windows.Media.Imaging;

    /// <summary>
    /// An implementation of IxxxDrawContext for WPF.
    /// </summary>
    public partial class DrawContext : MeasureContext, ILayoutDrawContext
    {
        /// <summary>
        /// The WPF context used to draw.
        /// </summary>
        public DrawingContext WpfDrawingContext { get; private set; }

        /// <summary>
        /// The icon to use to signal a comment.
        /// </summary>
        public BitmapSource CommentIcon { get; set; }

        /// <summary>
        /// The padding margin applied to comment text.
        /// </summary>
        public Padding CommentPadding { get; private set; }

        /// <summary>
        /// True if focused elements should be displayed as such.
        /// </summary>
        public bool DisplayFocus { get; }
    }
}
