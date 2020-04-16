using System.Numerics;

namespace IxMilia.Pdf
{
    public class PdfCircle : PdfEllipse
    {
        public float Radius { get; set; }

        public override float RadiusX { get => Radius; set => Radius = value; }
        public override float RadiusY { get => Radius; set => Radius = value; }

        public PdfCircle(Vector2 center, float radius, PdfStreamState state = default(PdfStreamState))
            : base(center, radius, radius, state: state)
        {
            Radius = radius;
        }
    }
}