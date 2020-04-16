
using System.Numerics;

namespace IxMilia.Pdf
{
    public class PdfArc : PdfEllipse
    {
        public PdfArc(Vector2 center, float radius, float startAngle, float endAngle, PdfStreamState state = default)
            : base(center, radius, radius, startAngle: startAngle, endAngle: endAngle, state: state)
        {
        }
    }
}
