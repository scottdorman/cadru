namespace Cadru.Color
{
    public struct HSV
    {
        public static readonly HSV Black = new HSV(1, 1, 0);
        public static readonly HSV White = new HSV(0, 0, 1);

        public HSV(double hue, double saturation, double value)
        {
            this.Hue = hue.Clamp(360, 0);
            this.Saturation = saturation.Clamp();
            this.Value = value.Clamp();
        }

        public double Hue { get; }
        public double Saturation { get; }
        public double Value { get; }
    }
}