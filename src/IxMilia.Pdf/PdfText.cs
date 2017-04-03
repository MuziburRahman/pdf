// Copyright (c) IxMilia.  All Rights Reserved.  Licensed under the Apache License, Version 2.0.  See License.txt in the project root for license information.

namespace IxMilia.Pdf
{
    public class PdfText : PdfStreamItem
    {
        public string Value { get; set; }
        public PdfFont Font { get; set; }
        public double FontSize { get; set; }
        public PdfPoint Location { get; set; }
        public PdfColor Color { get; set; }

        public double CharacterWidth { get; set; }

        public PdfText(string value, PdfFont font, double fontSize, PdfPoint location)
        {
            Value = value;
            Font = font;
            FontSize = fontSize;
            Location = location;
        }

        internal override void Write(PdfStreamWriter writer)
        {
            writer.SetState(color: Color);
            writer.WriteLine("BT");
            writer.WriteLine($"    /F{Font.FontId} {FontSize} Tf");
            writer.WriteLine($"    {Location} Td");
            if (CharacterWidth != 0.0)
            {
                writer.WriteLine($"    {CharacterWidth:f2} Tc");
            }

            writer.WriteLine($"    ({Value}) Tj");
            writer.WriteLine("ET");
        }
    }
}
