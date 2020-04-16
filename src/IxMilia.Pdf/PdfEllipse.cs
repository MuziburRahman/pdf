
using System;
using System.Collections.Generic;
using System.Numerics;
using IxMilia.Pdf.Extensions;

namespace IxMilia.Pdf
{
    public class PdfEllipse : IPdfPathItem
    {
        public Vector2 Center { get; set; }
        public virtual float RadiusX { get; set; }
        public virtual float RadiusY { get; set; }
        public float RotationAngle { get; set; }
        public float StartAngle { get; set; }
        public float EndAngle { get; set; }
        public PdfStreamState State { get; set; }

        private double EndAngleNormalized => EndAngle > StartAngle ? EndAngle : EndAngle + Math.PI * 2.0;

        public PdfEllipse(Vector2 center, float radiusX, float radiusY, float rotationAngle = 0, float startAngle = 0, float endAngle = MathPlus.PI * 2, PdfStreamState state = default(PdfStreamState))
        {
            Center = center;
            RadiusX = radiusX;
            RadiusY = radiusY;
            RotationAngle = rotationAngle;
            StartAngle = startAngle;
            EndAngle = endAngle;
            State = state;

            while (StartAngle < 0.0)
            {
                StartAngle += MathPlus.PI * 2;
            }

            while (EndAngle < 0.0)
            {
                EndAngle += MathPlus.PI * 2;
            }
        }

        public IEnumerable<PdfPathCommand> GetCommands()
        {
            yield return new PdfSetState(State);
            foreach (var command in GetArcCommands(1))
            {
                yield return command;
            }

            foreach (var command in GetArcCommands(2))
            {
                yield return command;
            }

            foreach (var command in GetArcCommands(3))
            {
                yield return command;
            }

            foreach (var command in GetArcCommands(4))
            {
                yield return command;
            }
        }

        private IEnumerable<PdfPathCommand> GetArcCommands(int quadrant)
        {
            var qStart = Math.PI / 2.0 * (quadrant - 1);
            var qEnd = qStart + Math.PI / 2.0;

            if (StartAngle <= qStart && EndAngleNormalized >= qEnd)
            {
                // included angle spans quadrant; draw entire thing
                foreach (var command in GetCommands(qEnd - qStart, qStart))
                {
                    yield return command;
                }
            }
            else
            {
                var includesStart = StartAngle >= qStart && StartAngle <= qEnd;
                var includesEnd = EndAngle >= qStart && EndAngle <= qEnd;

                if (includesStart && includesEnd)
                {
                    if (EndAngle > StartAngle)
                    {
                        // acute angle
                        foreach (var command in GetCommands(EndAngle - StartAngle, StartAngle))
                        {
                            yield return command;
                        }
                    }
                    else
                    {
                        // two small angles
                        foreach (var command in GetCommands(qEnd - StartAngle, StartAngle))
                        {
                            yield return command;
                        }

                        foreach (var command in GetCommands(EndAngle - qStart, qStart))
                        {
                            yield return command;
                        }
                    }
                }
                else if (includesStart)
                {
                    // only includes start angle
                    foreach (var command in GetCommands(qEnd - StartAngle, StartAngle))
                    {
                        yield return command;
                    }
                }
                else if (includesEnd)
                {
                    // only includes end angle
                    foreach (var command in GetCommands(EndAngle - qStart, qStart))
                    {
                        yield return command;
                    }
                }
            }
        }

        private IEnumerable<PdfPathCommand> GetCommands(double theta, double startAngle)
        {
            if (theta <= 0.0)
            {
                yield break;
            }

            // from http://www.tinaja.com/glib/bezcirc2.pdf
            var x0 = (float)Math.Cos(theta * 0.5);
            var y0 = -(float)Math.Sin(theta * 0.5);

            var x3 = x0;
            var y3 = -y0;

            var x1 = (4 - x0) / 3;
            var y1 = (1 - x0) * (3 - x0) / (3 * y0);

            var x2 = x1;
            var y2 = -y1;

            var p0 = new Vector2(x0, y0);
            var p1 = new Vector2(x1, y1);
            var p2 = new Vector2(x2, y2);
            var p3 = new Vector2(x3, y3);

            // now rotate points by (theta / 2) + startAngle
            var rotTheta = (float)theta * 0.5f + (float)startAngle;
            p0 = p0.RotateCWAboutOriginR(rotTheta);
            p1 = p1.RotateCWAboutOriginR(rotTheta);
            p2 = p2.RotateCWAboutOriginR(rotTheta);
            p3 = p3.RotateCWAboutOriginR(rotTheta);

            // multiply by the radius
            p0 = new Vector2(p0.X * RadiusX, p0.Y * RadiusY);
            p1 = new Vector2(p1.X * RadiusX, p1.Y * RadiusY);
            p2 = new Vector2(p2.X * RadiusX, p2.Y * RadiusY);
            p3 = new Vector2(p3.X * RadiusX, p3.Y * RadiusY);

            // do final rotation
            p0 = p0.RotateCWAboutOriginR(RotationAngle);
            p1 = p1.RotateCWAboutOriginR(RotationAngle);
            p2 = p2.RotateCWAboutOriginR(RotationAngle);
            p3 = p3.RotateCWAboutOriginR(RotationAngle);

            // offset for the center
            p0 += Center;
            p1 += Center;
            p2 += Center;
            p3 += Center;

            yield return new PdfPathMoveTo(p0);
            yield return new PdfCubicBezier(p1, p2, p3);
        }
    }
}
