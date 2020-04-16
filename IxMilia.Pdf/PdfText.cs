using IxMilia.Pdf.Extensions;
using System.Numerics;

namespace IxMilia.Pdf
{
    public class PdfText : PdfStreamItem
    {
        public string Value { get; set; }
        public PdfFont Font { get; set; }

        /// <summary>
        /// in point
        /// </summary>
        public float FontSize { get; set; }
        public Vector2 Location { get; set; }

        public PdfStreamState State { get; set; }


        /// <summary>
        /// in point
        /// </summary>
        public float CharacterWidth { get; set; }

        public PdfText(string value, PdfFont font, float fontSize, Vector2 location, PdfStreamState state = default)
        {
            Value = value;
            Font = font;
            FontSize = fontSize;
            Location = location;
            State = state;
        }

        internal override void Write(PdfStreamWriter writer)
        {
            writer.SetState(State);
            writer.WriteLine("BT");
            writer.WriteLine($"    /F{Font.FontId} {FontSize.AsFixed()} Tf");
            writer.WriteLine($"    {Location.ToPdfString()} Td");
            if (CharacterWidth != 0f)
            {
                writer.WriteLine($"    {CharacterWidth.AsFixed()} Tc");
            }

            writer.Write("    [(");
            foreach (var c in Value)
            {
                switch (c)
                {
                    case '(':
                    case ')':
                    case '\\':
                        writer.Write((byte)'\\');
                        break;
                }

                writer.Write((byte)c);
            }

            writer.WriteLine(")] TJ");
            writer.WriteLine("ET");
        }
    }
}