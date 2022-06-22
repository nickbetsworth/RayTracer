using System.Diagnostics.CodeAnalysis;

namespace RayTracer;

public static class ImageWriterExtensions
{
    public static void ToPpm([NotNull] this Image image, string filepath)
    {
        using StreamWriter file = new(filepath);
        file.WriteLine("P3");                               // Colours are in ASCII
        file.WriteLine($"{image.Width} {image.Height}");    // Image dimensions
        file.WriteLine("255");                              // Max color
        
        for (var y = 0; y < image.Height; y++)
        {
            for (var x = 0; x < image.Width; x++)
            {
                var data = image.GetPixel(x, y);
                file.WriteLine($"{data.R} {data.G} {data.B}");
            }
        }
    }
}