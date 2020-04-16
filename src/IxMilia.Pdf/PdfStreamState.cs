namespace IxMilia.Pdf
{
    public struct PdfStreamState
    {
        public PdfColor NonStrokeColor { get; set; }
        public PdfColor StrokeColor { get; set; }
        public PdfMeasurement StrokeWidth { get; set; }

        public PdfStreamState(PdfColor nonStrokeColor = default, PdfColor strokeColor = default, PdfMeasurement strokeWidth = default)
        {
            NonStrokeColor = nonStrokeColor;
            StrokeColor = strokeColor;
            StrokeWidth = strokeWidth;
        }
    }
}