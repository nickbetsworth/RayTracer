using System.Diagnostics;
using CommandLine;
using RayTracer;
using RayTracer.Configuration;
using RayTracer.Configuration.PrebuiltScenes.Scenes;
using RayTracer.Data;
using RayTracer.Extensions;
using RayTracer.Utility;
using Color = RayTracer.Data.Vector3;
using Point3 = RayTracer.Data.Vector3;
using Tracer = RayTracer.Tracer;

var parsedOptions = Parser.Default.ParseArguments<Options>(args);
if (parsedOptions.Tag == ParserResultType.NotParsed)
{
    return;
}

var tracerConfiguration = new TracerConfiguration
{
    SamplesPerPixel = parsedOptions.Value.SamplesPerPixel,
    MaxSampleDelta = parsedOptions.Value.MaxSampleDelta,
    MaxRayReflections = parsedOptions.Value.MaxRayReflections
};

var scene = new PrebuiltSceneA();
var camera = scene.GetSuggestedCamera();
var tracer = new Tracer(tracerConfiguration, camera, scene);

var image = new Image(parsedOptions.Value.Width, parsedOptions.Value.Height);

var timer = Stopwatch.StartNew();
var reporter = new ProgressReporter(Console.Out, 0, image.Height-1, 10);
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
Console.WriteLine($"Render time taken: {timer.Elapsed}ms");
image.ToPpm(parsedOptions.Value.OutputFilepath);
