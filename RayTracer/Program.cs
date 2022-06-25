using System.Diagnostics;
using RayTracer;
using RayTracer.Collision;
using RayTracer.Data;
using RayTracer.Data.Extensions;
using RayTracer.SceneConfiguration;
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

// Configure the scene
var scene = new Scene();
scene.Add(new Sphere(new Vector3(0, 0, -1), 0.5)); // Subject
scene.Add(new Sphere(new Vector3(0, -100.5, -1), 100)); // Ground
// scene.Add(new Sphere(new Vector3(0.1, 0, -0.5), 0.1));
var tracer = new Tracer(scene);

const int width = 400;
var image = new Image(width, (int)(width / camera.AspectRatio));

var timer = Stopwatch.StartNew();
Console.WriteLine("Rendering image");
for (var y = 0; y < image.Height; y++)
{
    // Console.WriteLine($"Scanlines remaining: {image.Height-y}");
    for (var x = 0; x < image.Width; x++)
    {
        var u = (double)x / (image.Width-1);
        var v = (double)y / (image.Height-1);

        var ray = new Ray(camera.Origin,
            camera.LowerLeftCorner + camera.Horizontal * u + camera.Vertical * v - camera.Origin);
        
        image.SetPixel(x, y, tracer.Trace(ray));
    }
}

Console.WriteLine("Done.");
Console.WriteLine($"Time taken: {timer.Elapsed.Milliseconds}ms");
Console.WriteLine("Writing image to file.");
image.ToPpm(outputPath);
Console.WriteLine("Done.");