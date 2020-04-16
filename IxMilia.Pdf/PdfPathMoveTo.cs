using System.Numerics;
using IxMilia.Pdf.Extensions;

namespace IxMilia.Pdf
{
    public class PdfPathMoveTo : PdfPathCommand
    {
        public Vector2 Point { get; set; }

        public PdfPathMoveTo(Vector2 point)
        {
            Point = point;
        }

        internal override void Write(PdfStreamWriter writer)
        {
            writer.WriteLine($"{Point.ToPdfString()} m");
        }
    }
}
