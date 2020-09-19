using System.Collections.Generic;
using IxMilia.Pdf.Extensions;

namespace IxMilia.Pdf
{
    internal class PdfStreamWriter
    {
        private List<byte> _bytes = new List<byte>();
        private PdfStreamState _lastState = PdfStreamState.DefaultState;

        public PdfStreamWriter()
        {
            // set initial state
            WriteStrokeWidth(_lastState.StrokeWidth);
            WriteStrokeColor(_lastState.StrokeColor);
            if (_lastState.StrokeDashArray != null && _lastState.StrokeDashArray.Length > 0)
                WriteStrokeDash(_lastState.StrokeDashArray);
        }

        public void Write(byte b)
        {
            _bytes.Add(b);
        }

        public void Write(string value)
        {
            _bytes.AddRange(value.GetBytes());
        }

        public void WriteLine(string value)
        {
            Write(value);
            Write("\r\n");
        }

        public void SetState(PdfStreamState state)
        {
            if (state is null)
                state = PdfStreamState.DefaultState;

            if (state != _lastState)
            {
                WriteLine("S");
            }
            else
            {
                return;
            }

            if (state.StrokeWidth != _lastState.StrokeWidth)
            {
                WriteStrokeWidth(state.StrokeWidth);
            }

            if (state.StrokeColor != _lastState.StrokeColor)
            {
                WriteStrokeColor(state.StrokeColor);
            }
            if (!state.StrokeDashArrayEqual(_lastState))
            {
                WriteStrokeDash(state.StrokeDashArray);
            }
            //if (state.NonStrokeColor != _lastState.NonStrokeColor)
            //{
            //    WriteNonStrokeColor(state.NonStrokeColor);
            //}

            _lastState = state;
        }

        private void WriteStrokeWidth(PdfMeasurement strokeWidth)
        {
            WriteLine($"{strokeWidth.AsPoints().AsInvariant()} w");
        }

        private void WriteStrokeColor(PdfColor color)
        {
            WriteLine($"{color} RG");
        }
        
        private void WriteStrokeDash(int[] dash)
        {
            string s = string.Join(" ", dash);
            WriteLine("[" + s + "] 0 d");
        }

        private void WriteNonStrokeColor(PdfColor color)
        {
            WriteLine($"{color} rg");
        }

        public byte[] GetBytes()
        {
            return _bytes.ToArray();
        }

        public int Length => _bytes.Count;

        internal void Stroke()
        {
            WriteLine("S");
        }
    }
}
