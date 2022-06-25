using System.Diagnostics;
using RayTracer.Collision;
using RayTracer.Configuration;
using RayTracer.Data;
using RayTracer.Extensions;
using Color = RayTracer.Data.Vector3;
using Tracer = RayTracer.Tracer;

if (args.Length == 0)
{
    throw new ArgumentNullException("args[0]", "Expected to receive output filepath as argument");
}

var outputPath = args[0];

var camera = new Camera
{
    AspectRatio = 16.0 / 9.0,
    FocalLength = 1.0,
    Origin = new Color(0, 0, 0)
};

// Configure the scene
var scene = new Scene();
scene.Add(new Sphere(new Color(0, 0, -1), 0.5)); // Subject
scene.Add(new Sphere(new Color(0, -100.5, -1), 100)); // Ground
// scene.Add(new Sphere(new Vector3(0.1, 0, -0.5), 0.1));
var tracerConfiguration = new TracerConfiguration
{
    SamplesPerPixel = 100,
    MaxSampleDelta = 0.005,
    MaxRayReflections = 10
};
var tracer = new Tracer(tracerConfiguration, camera, scene);

const int width = 400;
var image = new Image(width, (int)(width / camera.AspectRatio));

var timer = Stopwatch.StartNew();
Console.WriteLine("Rendering image");
for (var y = 0; y < image.Height; y++)
{
    Console.WriteLine($"Scanlines remaining: {image.Height-y}");
    for (var x = 0; x < image.Width; x++)
    {
        var u = (double)x / (image.Width-1);
        var v = (double)y / (image.Height-1);

        image.SetPixel(x, y, tracer.Trace(u, v));
    }
}

Console.WriteLine("Done.");
Console.WriteLine($"Time taken: {timer.Elapsed}ms");
Console.WriteLine("Writing image to file.");
image.ToPpm(outputPath);
Console.WriteLine("Done.");