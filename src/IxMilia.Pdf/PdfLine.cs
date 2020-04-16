using System.Collections.Generic;
using System.Numerics;

namespace IxMilia.Pdf
{
    public class PdfLine : IPdfPathItem
    {
        public Vector2 P1 { get; set; }
        public Vector2 P2 { get; set; }
        public PdfStreamState State { get; set; }

        public PdfLine(Vector2 p1, Vector2 p2, PdfStreamState state = default)
        {
            P1 = p1;
            P2 = p2;
            State = state;
        }

        public IEnumerable<PdfPathCommand> GetCommands()
        {
            yield return new PdfSetState(State);
            yield return new PdfPathMoveTo(P1);
            yield return new PdfPathLineTo(P2);
        }
    }
}
