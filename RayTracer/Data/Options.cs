using CommandLine;

namespace RayTracer.Data;

public class Options
{
    [Option('o', "output-file", Required = true, HelpText = "The path of the output file")]
    public string OutputFilepath { get; set; } = null!;
    
    [Option('w', "width", Required = false, Default = 400, HelpText = "Width of the output image")]
    public int Width { get; set; }
    
    [Option('h', "height", Required = false, Default = 225, HelpText = "Height of the output image")]
    public int Height { get; set; }
    
    [Option('s', "samples", Required = false, Default = 200, HelpText = "The number of samples per pixel")]
    public int SamplesPerPixel { get; set; }

    [Option('p', "max-delta", Required = false, Default = 0.0005, HelpText = "Maximum jitter of a unit-length ray to help with anti-aliasing")]
    public double MaxSampleDelta { get; set; }
    
    [Option('d', "max-depth", Required = false, Default = 100, HelpText = "The maximum depth of recursion from a ray")]
    public int MaxRayReflections { get; set; }
}