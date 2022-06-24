// See https://aka.ms/new-console-template for more information

using RayTracer.Data;
using RayTracer.Data.Extensions;
using Color = RayTracer.Data.Vector3;
using Color2 = RayTracer.Data.Vector3;
if (args.Length == 0)
{
    throw new ArgumentNullException("args[0]", "Expected to receive output filepath as argument");
}

var outputPath = args[0];

var image = new Image(255, 255);

Console.WriteLine("Rendering image");
for (var y = 0; y < image.Height; y++)
{
    Console.WriteLine($"Scanlines remaining: {image.Height-y}");
    for (var x = 0; x < image.Width; x++)
    {
        var color = new Color(0, (double)x / image.Width, (double)y / image.Height);
        image.SetPixel(x, y, color);
    }
}

Console.WriteLine("Done.");
Console.WriteLine("Writing image to file.");
image.ToPpm(outputPath);
Console.WriteLine("Done.");