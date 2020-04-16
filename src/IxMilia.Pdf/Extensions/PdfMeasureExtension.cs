namespace IxMilia.Pdf.Extensions
{
    public static class PdfMeasureExtension
    {
        private const float PointsPerInch = 72f;
        private const float PointsPerMm = PointsPerInch / 25.4f;

        public static float PointToInch(this in float point) { return point / PointsPerInch; }
        public static float InchToPoint(this in float inch) { return inch * PointsPerInch; }
        public static float MmToPoint(this in float Mm) { return Mm * PointsPerMm; }
        public static float PointToMm(this in float point) { return point / PointsPerMm; }
    }
}
