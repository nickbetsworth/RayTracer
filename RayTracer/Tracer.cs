using RayTracer.Collision;
using RayTracer.Configuration;
using RayTracer.Data;

namespace RayTracer;

using Color = Vector3;
public class Tracer
{
    private readonly TracerConfiguration _configuration;
    private readonly Camera _camera;
    private readonly Scene _scene;
    private readonly Random _random;

    public Tracer(TracerConfiguration configuration, Camera camera, Scene scene)
    {
        _configuration = configuration;
        _camera = camera;
        _scene = scene;
        _random = new Random();
    }

    public Color Trace(double u, double v)
    {
        var color = new Color();
        for (var i = 0; i < _configuration.SamplesPerPixel; i++)
        {
            var uJittered = u + _random.NextDouble() * _configuration.MaxSampleDelta;
            var vJittered = v + _random.NextDouble() * _configuration.MaxSampleDelta;
            
            var ray = new Ray(
                _camera.Origin,
                _camera.LowerLeftCorner +
                _camera.Horizontal * uJittered +
                _camera.Vertical * vJittered -
                _camera.Origin);
            
            color += Trace(ray, _configuration.MaxRayReflections);
        }

        return color / _configuration.SamplesPerPixel;
    }

    private Color Trace(Ray ray, int depth)
    {
        if (depth <= 0)
        {
            return new Color();
        }
        
        IntersectionResult? closestIntersection = null;
        foreach (var item in _scene.Objects)
        {
            // Start with tMin = eps to prevent shadow acne where reflected rays intersect object they are reflected off
            var intersection = item.Intersect(ray, 0.00001, closestIntersection?.T ?? double.MaxValue); 
            if (intersection is not null)
            {
                closestIntersection = intersection;
            }
        }

        if (closestIntersection is null)
        {
            return BackgroundColor(ray);
        }

        var scatterResult = closestIntersection.Object.Material.Scatter(ray, closestIntersection);
        if (scatterResult is null)
        {
            return new Color();
        }

        return scatterResult.Attenuation * Trace(scatterResult.Ray, depth - 1);
    }

    private static Color BackgroundColor(Ray ray)
    {
        var normalizedDirection = Vector3.Normalize(ray.Direction);
        var t = 0.5 * (normalizedDirection.Y + 1.0);
        return new Color(1, 1, 1) * (1 - t) + new Color(0.5, 0.7, 1.0) * t;
    }
}