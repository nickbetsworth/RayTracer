// See https://aka.ms/new-console-template for more information

using RayTracer;
using RayTracer.Data;
using RayTracer.Data.Extensions;
using Color = RayTracer.Data.Vector3;

if (args.Length == 0)
{
    throw new ArgumentNullException("args[0]", "Expected to receive output filepath as argument");
}

var outputPath = args[0];

var camera = new CameraConfiguration
{
    AspectRatio = 16.0 / 9.0,
    FocalLength = 1.0,
    Origin = new Vector3(0, 0, 0)
};

var image = new Image(400, (int)(400 / camera.AspectRatio));

Console.WriteLine("Rendering image");
for (var y = 0; y < image.Height; y++)
{
    Console.WriteLine($"Scanlines remaining: {image.Height-y}");
    for (var x = 0; x < image.Width; x++)
    {
        var u = (double)x / (image.Width-1);
        var v = (double)y / (image.Height-1);

        var ray = new Ray(camera.Origin,
            camera.LowerLeftCorner + camera.Horizontal * u + camera.Vertical * v - camera.Origin);
        image.SetPixel(x, y, ray.ToColor());
    }
}

Console.WriteLine("Done.");
Console.WriteLine("Writing image to file.");
image.ToPpm(outputPath);
Console.WriteLine("Done.");