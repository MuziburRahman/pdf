using System.Numerics;
using IxMilia.Pdf.Extensions;

namespace IxMilia.Pdf
{
    public class PdfPathLineTo : PdfPathCommand
    {
        public Vector2 Point { get; set; }

        public PdfPathLineTo(Vector2 point)
        {
            Point = point;
        }

        internal override void Write(PdfStreamWriter writer)
        {
            writer.WriteLine($"{Point.ToPdfString()} l");
        }
    }
}