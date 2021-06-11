namespace EaslyDraw
{
    using EaslyController.Layout;
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.IO;
    using System.Reflection;
    using System.Runtime.InteropServices;
    using System.Windows.Interop;
    using System.Windows.Media;
    using System.Windows.Media.Animation;
    using System.Windows.Media.Imaging;

    /// <summary>
    /// An implementation of IxxxDrawContext for WPF.
    /// </summary>
    public partial class DrawContext : MeasureContext, ILayoutDrawContext
    {
        /// <summary>
        /// Creates and initializes a new context.
        /// </summary>
        /// <param name="typeface">The font to use for text.</param>
        /// <param name="fontSize">The font size to use for text.</param>
        /// <param name="culture">The culture to use for text.</param>
        /// <param name="flowDirection">The flow direction to use for text.</param>
        /// <param name="brushTable">Brushes for each element to display.</param>
        /// <param name="penTable">Pens for each element to display.</param>
        /// <param name="hasCommentIcon">True if the comment icon must be displayed.</param>
        /// <param name="displayFocus">True if focused elements should be displayed as such.</param>
        public static DrawContext CreateDrawContext(Typeface typeface, double fontSize, CultureInfo culture, System.Windows.FlowDirection flowDirection, IReadOnlyDictionary<BrushSettings, Brush> brushTable, IReadOnlyDictionary<PenSettings, Pen> penTable, bool hasCommentIcon, bool displayFocus)
        {
            DrawContext Result = new DrawContext(typeface, fontSize, culture, flowDirection, brushTable, penTable, hasCommentIcon, displayFocus);
            Result.Update();
            return Result;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DrawContext"/> class.
        /// </summary>
        /// <param name="typeface">The font to use for text.</param>
        /// <param name="fontSize">The font size to use for text.</param>
        /// <param name="culture">The culture to use for text.</param>
        /// <param name="flowDirection">The flow direction to use for text.</param>
        /// <param name="brushTable">Brushes for each element to display.</param>
        /// <param name="penTable">Pens for each element to display.</param>
        /// <param name="hasCommentIcon">True if the comment icon must be displayed.</param>
        /// <param name="displayFocus">True if focused elements should be displayed as such.</param>
        protected DrawContext(Typeface typeface, double fontSize, CultureInfo culture, System.Windows.FlowDirection flowDirection, IReadOnlyDictionary<BrushSettings, Brush> brushTable, IReadOnlyDictionary<PenSettings, Pen> penTable, bool hasCommentIcon, bool displayFocus)
            : base(typeface, fontSize, culture, flowDirection, brushTable, penTable)
        {
            IsLastFocusedFullCell = false;
            DisplayFocus = displayFocus;

            FlashAnimation = new DoubleAnimation(0, new System.Windows.Duration(TimeSpan.FromSeconds(1)));
            FlashAnimation.RepeatBehavior = RepeatBehavior.Forever;
            FlashAnimation.EasingFunction = new FlashEasingFunction();
            FlashClock = FlashAnimation.CreateClock();

            if (hasCommentIcon)
                CommentIcon = LoadPngResource("Comment");
        }

        private BitmapSource LoadPngResource(string resourceName)
        {
            Assembly CurrentAssembly = Assembly.GetExecutingAssembly();
            string ResourcePath = $"EaslyDraw.Resources.{resourceName}.png";

            using (Stream ResourceStream = CurrentAssembly.GetManifestResourceStream(ResourcePath))
            {
                System.Drawing.Bitmap bmp = new System.Drawing.Bitmap(ResourceStream);
                return BitmapToBitmapSource(bmp);
            }
        }

        [DllImport("gdi32")]
        private static extern int DeleteObject(IntPtr o);

        private BitmapSource BitmapToBitmapSource(System.Drawing.Bitmap bmp)
        {
            IntPtr ip = bmp.GetHbitmap();
            try
            {
                return Imaging.CreateBitmapSourceFromHBitmap(ip, IntPtr.Zero, System.Windows.Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
            }
            catch
            {
                throw;
            }
            finally
            {
                DeleteObject(ip);
            }
        }
    }
}
