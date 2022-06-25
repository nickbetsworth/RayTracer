using System.Diagnostics.CodeAnalysis;
using System.Security.Claims;
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
    public static ColorRgb ToRgb(this Color pixel)
    {
        return new ColorRgb(
            (byte)(ClampValue(pixel.X) * 255.99),
            (byte)(ClampValue(pixel.Y) * 255.99),
            (byte)(ClampValue(pixel.Z) * 255.99));
    }

    private static double ClampValue(double value)
    {
        return value switch
        {
            > 1.0 => 1.0,
            < 0.0 => 0.0,
            _ => value
        };
    }
}