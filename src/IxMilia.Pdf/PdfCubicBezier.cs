using System.Numerics;
using IxMilia.Pdf.Extensions;

namespace IxMilia.Pdf
{
    public class PdfCubicBezier : PdfPathCommand
    {
        public Vector2 P1 { get; set; }
        public Vector2 P2 { get; set; }
        public Vector2 P3 { get; set; }

        public PdfCubicBezier(Vector2 p1, Vector2 p2, Vector2 p3)
        {
            P1 = p1;
            P2 = p2;
            P3 = p3;
        }

        internal override void Write(PdfStreamWriter writer)
        {
            writer.WriteLine($"{P1.ToPdfString()} {P2.ToPdfString()} {P3.ToPdfString()} c");
        }
    }
}
