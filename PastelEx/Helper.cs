using System.Drawing;
using System.Globalization;
using System.Text;

namespace PastelExtended;
internal class Helper
{
    public static int LastIndexOf(in ReadOnlySpan<char> source, in ReadOnlySpan<char> value)
    {
        int position = source.Length - 1;
        int len = value.Length - 1;
        int matches = 0;

        for (int i = position; i >= 0; i--)
        {
            if (source[i] == value[len])
            {
                // Found first char matching
                matches++;
                len--;
            }
            else if (matches > 0)
            {
                // False match, continue
                len = value.Length - 1;
                matches = 0;
            }

            if (value.Length - 1 == matches)
                return i;
        }

        return -1;
    }

    public static Color ParseFromHex(in ReadOnlySpan<char> hexString)
    {
        if (hexString.Length < 3)
            return Color.Empty;

        var plainHex = hexString[0] == '#' ? hexString[1..] : hexString;

        if (plainHex.Length == 3)
        {
            Span<char> biggerSpan = stackalloc char[6];

            biggerSpan[0..2].Fill(plainHex[0]);
            biggerSpan[2..4].Fill(plainHex[1]);
            biggerSpan[4..6].Fill(plainHex[2]);

            if (int.TryParse(biggerSpan, NumberStyles.HexNumber, default, out int color))
                return Color.FromArgb(color);
            else
                return Color.Empty;
        }
        else if (plainHex.Length == 6)
        {
            if (int.TryParse(plainHex, NumberStyles.HexNumber, default, out int color))
                return Color.FromArgb(color);
            else
                return Color.Empty;
        }
        else
            return Color.Empty;
    }

    private const string _endingSequence = "m";
    public static string CreateGradientEffect(in ReadOnlySpan<char> input, ColorPlane plane, in ReadOnlySpan<Color> spectrum)
    {
        var output = new StringBuilder();
        var size = input.Length;

        Span<Color> colorList = new Color[size];

        if (spectrum.Length == 1)
        {
            colorList.Fill(spectrum[0]);
        }
        else
        {
            var stepCount = spectrum.Length - 1;
            var steps = size / stepCount;
            var remainder = size % stepCount;
            var insertionIndex = 0;

            for (var i = 0; i < stepCount; i++)
            {
                var start = spectrum[i];
                var end = spectrum[i + 1];
                var stepSize = steps + (i < remainder ? 1 : 0);
                var gradientStep = GetGradients(start, end, stepSize);

                for (int j = 0; j < stepSize; j++)
                    colorList[insertionIndex + j] = gradientStep[j];

                insertionIndex += gradientStep.Length;
            }
        }

        for (var i = 0; i < size; i++)
        {
            if (input[i] == '\u001b')
            {
                int endIndex = input[i..].IndexOf(_endingSequence) + i;
                if (endIndex != -1)
                {
                    output.Append(input.Slice(i, endIndex - i + 1));
                    i = endIndex;
                    continue;
                }
            }

            if (i == size - 1)
            {
                output.Append(Formatter.ColorRgb(new ReadOnlySpan<char>(input[i]), colorList[i], plane));
            }
            else
            {
                output.Append($"{Formatter.GetRgbColorFormat(colorList[i], plane)}{input[i]}");
            }
        }

        return output.ToString();
    }

    static Color[] GetGradients(Color start, Color end, int steps)
    {
        int rMax = end.R;
        int rMin = start.R;
        int gMax = end.G;
        int gMin = start.G;
        int bMax = end.B;
        int bMin = start.B;

        Span<Color> output = new Color[steps];

        for (int i = 0; i < steps; i++)
            unchecked
            {
                byte rAverage = (byte)(rMin + (rMax - rMin) * i / steps);
                byte gAverage = (byte)(gMin + (int)((gMax - gMin) * i / steps));
                byte bAverage = (byte)(bMin + (bMax - bMin) * i / steps);
                output[i] = Color.FromArgb(rAverage, gAverage, bAverage);
            }

        return output.ToArray();
    }
}
