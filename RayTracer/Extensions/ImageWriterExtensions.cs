using System.Diagnostics.CodeAnalysis;
using RayTracer.Data;
using Color = RayTracer.Data.Vector3;
namespace RayTracer.Extensions;

public static class ImageWriterExtensions
{
    public static void ToPpm(this Image image, string filepath)
    {
        using StreamWriter file = new(filepath);
        file.WriteLine("P3");                               // Colours are in ASCII
        file.WriteLine($"{image.Width} {image.Height}");    // Image dimensions
        file.WriteLine("255");                              // Max color
        
        for (var y = image.Height-1; y >= 0; y--)
        {
            for (var x = 0; x < image.Width; x++)
            {
                var pixel = image.GetPixel(x, y).ToRgb();
                file.WriteLine($"{pixel.R} {pixel.G} {pixel.B}");
            }
        }
    }

    public record ColorRgb(byte R, byte G, byte B);
    public static ColorRgb ToRgb([NotNull] this Color pixel)
    {
        return new ColorRgb(
            (byte)(pixel.X * 255.99),
            (byte)(pixel.Y * 255.99),
            (byte)(pixel.Z * 255.99));
    }
}