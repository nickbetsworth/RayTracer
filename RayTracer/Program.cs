using System.Diagnostics;
using RayTracer.Collision;
using RayTracer.Configuration;
using RayTracer.Data;
using RayTracer.Extensions;
using RayTracer.Materials;
using RayTracer.Utility;
using Color = RayTracer.Data.Vector3;
using Point3 = RayTracer.Data.Vector3;
using Tracer = RayTracer.Tracer;

if (args.Length == 0)
{
    throw new ArgumentNullException("args[0]", "Expected to receive output filepath as argument");
}

var outputPath = args[0];

var tracerConfiguration = new TracerConfiguration
{
    SamplesPerPixel = 100,
    MaxSampleDelta = 0.005,
    MaxRayReflections = 50
};

var camera = new Camera
{
    AspectRatio = 16.0 / 9.0,
    FocalLength = 1.0,
    Origin = new Color(0, 0, 0)
};

// Configure the scene
var materialBlueDiffuse = new LambertianMaterial(new Color(0.0, 0.25, 0.9));
var materialMetal = new MetalMaterial(new Color(0.5, 0.5, 0.5));
var materialMetalFuzz = new MetalMaterial(new Color(0.5, 0.5, 0.5), 0.75);
var materialGlass = new DielectricMaterial(1.5);
var materialGround = new LambertianMaterial(new Color(0.1, 0.1, 0.1));
var materialDielectricDiamond = new DielectricMaterial(2.4);

var scene = new Scene();
scene.Add(new Sphere(new Point3(1.0, 0.0, -1.0), 0.5, materialMetal));
scene.Add(new Sphere(new Point3(-1.0, 0.0, -1.0), -0.4, materialGlass));
scene.Add(new Sphere(new Point3(-1.0, 0.0, -1.0), 0.5, materialGlass));
scene.Add(new Sphere(new Point3(0.0, 0.0, -1.0), 0.5, materialBlueDiffuse));
scene.Add(new Sphere(new Point3(0.0, -100.5, -1.0), 100, materialGround));

var tracer = new Tracer(tracerConfiguration, camera, scene);

const int width = 400;
var image = new Image(width, (int)(width / camera.AspectRatio));

var timer = Stopwatch.StartNew();
var reporter = new ProgressReporter(0, image.Height-1, 10);
Console.WriteLine("Rendering image");
for (var y = 0; y < image.Height; y++)
{
    reporter.Update(y);
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