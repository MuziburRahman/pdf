using System;
using System.Collections.Generic;
using System.Linq;
using IxMilia.Pdf.Encoders;
using IxMilia.Pdf.Extensions;

namespace IxMilia.Pdf
{
    public class PdfPage : PdfObject
    {
        /// <summary>
        /// in inch
        /// </summary>
        public const float LetterWidth = 8.5f;

        /// <summary>
        /// in inch
        /// </summary>
        public const float LetterHeight = 11f;

        internal PdfStream Stream { get; }

        /// <summary>
        /// in points
        /// </summary>
        public float Width { get; set; }


        /// <summary>
        /// in points
        /// </summary>
        public float Height { get; set; }
        public IList<PdfStreamItem> Items => Stream.Items;

        /// <param name="width">in point</param>
        /// <param name="height">in point</param>
        /// <param name="encoders"></param>
        public PdfPage(float width, float height, params IPdfEncoder[] encoders)
        {
            Width = width;
            Height = height;
            Stream = new PdfStream(encoders);
        }

        public static PdfPage NewLetter()
        {
            return new PdfPage(LetterWidth.InchToPoint(), LetterHeight.InchToPoint());
        }

        public static PdfPage NewLetterLandscape()
        {
            return new PdfPage(LetterHeight.InchToPoint(), LetterWidth.InchToPoint());
        }

        public static PdfPage NewASeries(int n, bool isPortrait = true)
        {
            float longSide = (int)(1000.0 / Math.Pow(2.0, (2.0 * n - 1.0) / 4.0) + 0.2);
            float shortSide = (int)(longSide / Math.Sqrt(2.0));
            switch (n)
            {
                case 0:
                case 3:
                case 6:
                    // manually correct rounding errors
                    shortSide++;
                    break;
            }

            var width = isPortrait ? shortSide : longSide;
            var height = isPortrait ? longSide : shortSide;
            return new PdfPage(width.MmToPoint(), height.MmToPoint());
        }

        public override IEnumerable<PdfObject> GetChildren()
        {
            yield return Stream;
            foreach (var text in Items.OfType<PdfText>())
            {
                yield return text.Font;
            }
        }

        protected override byte[] GetContent()
        {
            var resources = new List<string>();
            var seenFonts = new HashSet<PdfFont>();
            foreach (var font in GetAllFonts())
            {
                if (seenFonts.Add(font))
                {
                    resources.Add($"/Font <</F{font.FontId} {font.Id.AsObjectReference()}>>");
                }
            }

            return $"<</Type /Page /Parent {Parent.Id.AsObjectReference()} /Contents {Stream.Id.AsObjectReference()} /MediaBox [0 0 {Width.AsFixed()} {Height.AsFixed()}] /Resources <<{string.Join(" ", resources)}>>>>".GetNewLineBytes();
        }

        private IEnumerable<PdfFont> GetAllFonts()
        {
            return Items.OfType<PdfText>().Select(t => t.Font);
        }
    }
}
