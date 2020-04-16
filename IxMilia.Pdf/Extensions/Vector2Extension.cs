using System.Numerics;
using static System.Math;

namespace IxMilia.Pdf.Extensions
{
    public static class Vector2Extension
    {
        public static string ToPdfString(this Vector2 vector)
        {
            return string.Format("{0:0.##}", vector.X) + " " + string.Format("{0:0.##}", vector.Y);
        }

        public static Vector2 RotateCWAboutOriginR(this Vector2 self, float theta)
        {
            if (theta == 0)
            {
                return self;
            }

            float sin = (float)Sin(theta);
            float cos = (float)Cos(theta);
            return new Vector2(self.X * cos - self.Y * sin, self.X * sin + self.Y * cos);
        }

    }
}
