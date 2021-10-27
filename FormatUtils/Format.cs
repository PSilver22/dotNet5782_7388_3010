using System;

namespace FormatUtils
{
    public class Coordinates
    {
        public enum Axis { longitude, latitude }

        public static string Sexagesimal(double degs, Axis axis)
        {
            var direction = axis switch
            {
                Axis.longitude => degs < 0 ? "W" : "E",
                Axis.latitude => degs < 0 ? "S" : "N",
                _ => throw new NotImplementedException(),
            };

            degs = Math.Abs(degs);

            int degsInt = (int)degs;
            int mins = (int)(60 * (degs - degsInt));
            double secs = 3600 * (degs - degsInt) - 60 * (mins);

            return $"{degsInt}° {mins}' {secs:F3}\" {direction}";
        }
    }
}
