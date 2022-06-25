namespace RayTracer.Configuration;

public class TracerConfiguration
{
    public int SamplesPerPixel { get; set; }
    public double MaxSampleDelta { get; set; }
    public int MaxRayReflections { get; set; }
}