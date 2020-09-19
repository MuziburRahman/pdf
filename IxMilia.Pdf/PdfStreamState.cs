using System.Collections.Generic;
using System.Linq;

namespace IxMilia.Pdf
{
    public class PdfStreamState
    {
        public static readonly PdfStreamState DefaultState = new PdfStreamState();

        //public PdfColor NonStrokeColor { get; }
        public PdfColor StrokeColor { get; }
        public PdfMeasurement StrokeWidth { get; }

        public int[] StrokeDashArray { get; }

        public PdfStreamState(/*PdfColor nonStrokeColor = default, */PdfColor strokeColor = default, PdfMeasurement strokeWidth = default, params int[] dash_array)
        {
            //NonStrokeColor = nonStrokeColor;
            StrokeColor = strokeColor;
            StrokeWidth = strokeWidth;
            StrokeDashArray = dash_array;
        }

        internal bool StrokeDashArrayEqual(PdfStreamState s)
        {
            if (StrokeDashArray != null && s.StrokeDashArray != null)
                return StrokeDashArray.SequenceEqual(s.StrokeDashArray);
            else if (StrokeDashArray is null && s.StrokeDashArray is null)
                return true;
            else
                return false;
        }

        public static bool operator == (PdfStreamState s1, PdfStreamState s2)
        {
            if (s1 is null && s2 is null)
                return true;
            if (s1 is null || s2 is null)
                return false;
            return s1.Equals(s2);
        }

        public static bool operator != (PdfStreamState s1, PdfStreamState s2)
        {
            return !(s1 == s2);
        }

        public override int GetHashCode()
        {
            int hashCode = -1256928811;
            hashCode = hashCode * -1521134295 + StrokeColor.GetHashCode();
            hashCode = hashCode * -1521134295 + StrokeWidth.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<int[]>.Default.GetHashCode(StrokeDashArray);
            return hashCode;
        }

        public override bool Equals(object obj)
        {
            if (obj is null)
                return false;
            if (!(obj is PdfStreamState state))
                return false;

            return StrokeColor == state.StrokeColor &&
                   StrokeWidth == state.StrokeWidth &&
                   StrokeDashArrayEqual(state);
        }
    }
}